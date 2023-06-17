using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Logs;
using Coorth.Tasks;

namespace Coorth.Framework; 

public record WorldOptions {

    public string Name { get; set; } = "Worlds-Default";

    private IServiceLocator? services;
    public IServiceLocator Services { get => services ??= new ServiceLocator(); set => services = value; }
    
    private ILogger? logger;
    public ILogger Logger { get => logger ??= new LoggerConsole(); set => logger = value; }
    
    private Dispatcher? dispatcher;
    public Dispatcher Dispatcher { get => dispatcher ??= new Dispatcher(null!); set => dispatcher = value; }

    private TaskSyncContext? schedule;
    public TaskSyncContext SyncContext { get => schedule ?? new TaskSyncContext(Thread.CurrentThread); set => schedule = value; }
    
}

public partial class World : Disposable {
    
    private static int currIndex;
    private static readonly Stack<int> resumeIndex = new();
    private static readonly object locker = new();
    public static int WorldCount { get; private set; }
    private static World?[] worlds = new World?[4];
    public static IReadOnlyList<World?> Worlds => worlds;

    public readonly string Name;

    public readonly int Index;

    public readonly WorldOptions Options;

    public readonly TaskSyncContext SyncContext;

    public readonly Dispatcher Dispatcher;

    public readonly Router<MessageContext> Router;
    
    public readonly IServiceLocator Services;

    public readonly ILogger Logger;

    public readonly WorldActor Actor;
    
    public World(WorldOptions options) {
        Options = options;
        Logger = Options.Logger;
        
        Name = Options.Name;
        lock (locker) {
            Index = resumeIndex.Count > 0 ? resumeIndex.Pop() : currIndex++;
            if (worlds.Length <= Index) {
                Array.Resize(ref worlds, worlds.Length * 2);
            }
            worlds[Index] = this;
            WorldCount++;
        }
        
        Services = options.Services;
        Dispatcher = options.Dispatcher;
        SyncContext = options.SyncContext;
        Router = new Router<MessageContext>();
        Actor = new WorldActor(this);

        SetupArchetypes(out rootArchetype);
        SetupSpaces(out rootSpace, out mainSpace);
        SetupQueries();
        SetupEntities();
        SetupComponents();
        SetupSystems(out RootSystem);
    }

    protected override void OnDispose() {
        if (!SyncContext.IsMain) {
            Logger.Error("Dispose can must be call in world main thread.");
            return;
        }
        ClearSpaces();
        ClearEntities();
        ClearSystems();
        ClearComponents();
        ClearQueries();
        ClearArchetypes();

        lock (locker) {
            WorldCount--;
            worlds[Index] = default;
            resumeIndex.Push(Index);
        }
    }
    
    public static World? FindWorld(int index) {
        return (0 <= index && index < worlds.Length) ? worlds[index] : null;
    }
    
    public static bool HasWorld(int index) {
        return 0 <= index && index < worlds.Length && Worlds[index] != null;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public object GetService(Type serviceType) {
        return Services.Get(serviceType);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public object? FindService(Type serviceType) {
        return Services.Find(serviceType);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetService<T>() where T : class {
        return Services.Get<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T? FindService<T>() where T : class {
        return Services.Find<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispatch<T>(in T e) where T: notnull {
        Dispatcher.Dispatch(e);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask DispatchAsync<T>(in T e) where T: notnull {
        return Dispatcher.DispatchAsync(e);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispatch<T>(in MessageContext context, T message) where T: notnull {
        Router.Dispatch(context, message);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask DispatchAsync<T>(in MessageContext context, T message) where T: notnull {
        return Router.DispatchAsync(context, message);
    }
    
    
}


