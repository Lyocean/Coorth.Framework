using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Coorth.Logs;
using Coorth.Tasks;

namespace Coorth.Framework; 

public partial class World : Disposable {
    
    private static int currIndex;
    private static readonly Stack<int> resumeIndex = new();
    private static readonly object locker = new();
    private static World[] worlds = new World[4];
    public static int WorldCount { get; private set; }
    public static ReadOnlySpan<World> Worlds => worlds.AsSpan(0, WorldCount);

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

        InitArchetypes(out emptyArchetype);
        InitSpaces(out defaultSpace);
        InitEntities(Options.EntityCapacity.Index, Options.EntityCapacity.Chunk);
        InitComponents(Options);
        InitSystems(out RootSystem);
            
        singleton = CreateEntity();
    }

    protected override void OnDispose() {
        if (!SyncContext.IsMain) {
            Logger.Error("Dispose can must be call in world main thread.");
            return;
        }
        ClearEntities();
        ClearSpaces();
        ClearSystems();
        ClearArchetypes();
        ClearComponents();

        lock (locker) {
            WorldCount--;
            worlds[Index] = default!;
            resumeIndex.Push(Index);
        }
    }
    
    public static World? FindWorld(int index) {
        return index < worlds.Length ? worlds[index] : null;
    }
    
    public static bool HasWorld(int index) {
        return index < WorldCount;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public object GetService(Type serviceType) => Services.Get(serviceType);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public object? FindService(Type serviceType) => Services.Find(serviceType);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetService<T>() where T : class => Services.Get<T>();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T? FindService<T>() where T : class => Services.Find<T>();
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispatch<T>(in T e) where T: notnull => Dispatcher.Dispatch(e);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask DispatchAsync<T>(in T e) where T: notnull => Dispatcher.DispatchAsync(e);

    public void Execute<T>(in T e)  where T: notnull => Dispatcher.Dispatch(e);
    
    public ValueTask ExecuteAsync<T>(in T e)  where T: notnull => Dispatcher.DispatchAsync(e);

    public void Receive<T>(MessageContext context, T message) where T: notnull {
        Router.Dispatch(context, message);
    }

    public ValueTask ReceiveAsync<T>(MessageContext context, T message) where T: notnull {
        return Router.DispatchAsync(context, message);
    }
}