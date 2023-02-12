using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Logs;
using Coorth.Tasks;

namespace Coorth.Framework; 

public partial class World : IDisposable {
        
    public readonly string Name;

    private static int currIndex;
    public readonly int Index;
    
    public readonly WorldOptions Options;

    public readonly TaskSyncContext SyncContext;

    public readonly Dispatcher Dispatcher;

    public readonly Router<MessageContext> Router;
    
    public readonly IServiceLocator Services;

    public readonly ILogger Logger;
        
    private volatile int disposed;
    public bool IsDisposed => disposed != 0;
    
    public World(WorldOptions options) {
        Options = options;
        Logger = Options.Logger;
        
        Name = Options.Name;
        Index = ++currIndex;
        Services = options.Services;
        Dispatcher = options.Dispatcher;
        SyncContext = options.SyncContext;
        Router = new Router<MessageContext>();

        InitArchetypes(Options.ArchetypeCapacity.Index, Options.ArchetypeCapacity.Chunk, out emptyArchetype);

        InitEntities(Options.EntityCapacity.Index, Options.EntityCapacity.Chunk);
        InitComponents(Options.ComponentGroupCapacity, Options.ComponentDataCapacity.Index, Options.ComponentDataCapacity.Chunk);

        InitSystems(out RootSystem);
            
        singleton = CreateEntity();
    }

    public void Dispose() {
        if (Interlocked.CompareExchange(ref disposed, 1, 0) == 0) {
            GC.SuppressFinalize(this);
            OnDispose();
        }
    }
    
    ~World() {
        if (Interlocked.CompareExchange(ref disposed, 1, 0) == 0) {
            Logger.Error("Worlds is not disposed.");
        }
    }

    private void OnDispose() {
        if (!SyncContext.IsMain) {
            Logger.Error("Dispose can must be call in world main thread.");
            return;
        }
        ClearEntities();
        ClearComponents();
        ClearSystems();
        ClearArchetypes();
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