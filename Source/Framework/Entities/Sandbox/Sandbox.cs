using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Logs;
using Coorth.Tasks;

namespace Coorth.Framework; 

public partial class Sandbox : IDisposable {
        
    public readonly string Name;
    
    public int Index { get; private set; }
    
    public readonly SandboxOptions Options;

    public readonly TaskSyncContext Schedule;

    public readonly Dispatcher Dispatcher;
    
    public readonly IServiceLocator Services;

    public readonly ILogger Logger;
        
    private volatile int disposed;
    public bool IsDisposed => disposed != 0;
    
    public Sandbox(SandboxOptions? options = null) {
        Options = options ?? SandboxOptions.Default;
        Logger = options?.Logger ?? Coorth.Logs.Logger.Root;

        OnCreateSandbox(this);
            
        Name = Options.Name;
        Services = options?.Services ?? new ServiceLocator();
        Dispatcher = options?.Dispatcher ?? Dispatcher.Root.CreateChild();
        Schedule = options?.Schedule ?? new TaskSyncContext(Thread.CurrentThread);

        InitArchetypes(Options.ArchetypeCapacity.Index, Options.ArchetypeCapacity.Chunk);
        emptyArchetype = new ArchetypeDefinition(this);

        InitEntities(Options.EntityCapacity.Index, Options.EntityCapacity.Chunk);
        InitComponents(Options.ComponentGroupCapacity, Options.ComponentDataCapacity.Index, Options.ComponentDataCapacity.Chunk);

        RootSystem = new SystemRoot();
        InitSystems();
            
        singleton = CreateEntity();
    }

    public void Dispose() {
        if (Interlocked.CompareExchange(ref disposed, 1, 0) == 0) {
            GC.SuppressFinalize(this);
            OnDispose();
        }
    }
    
    ~Sandbox() {
        if (Interlocked.CompareExchange(ref disposed, 1, 0) == 0) {
            Logger.Error("Sandbox is not disposed.");
        }
    }

    private void OnDispose() {
        if (!Schedule.IsMain) {
            Logger.Error("Dispose can must be call in sandbox main thread.");
            return;
        }
        ClearEntities();
        ClearComponents();
        ClearSystems();
        ClearArchetypes();
        OnRemoveSandbox(this);
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
    public void Dispatch<T>(in T e)  where T: notnull => Dispatcher.Dispatch(e);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask DispatchAsync<T>(in T e) where T: notnull => Dispatcher.DispatchAsync(e);

    public void Execute<T>(in T e)  where T: notnull => Dispatcher.Dispatch(e);

    public ValueTask ExecuteAsync<T>(in T e)  where T: notnull => Dispatcher.DispatchAsync(e);
}