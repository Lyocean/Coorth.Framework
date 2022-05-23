using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Coorth.Logs;

namespace Coorth.Framework; 

public partial class Sandbox : IDisposable {
        
    public readonly string Name;
    
    public int Index { get; private set; }

    public bool IsDisposed { get; private set; }

    public readonly SandboxOptions Options;

    public readonly SandboxContext Context;

    public readonly Dispatcher Dispatcher;

    public readonly IServiceLocator Services;

    public readonly ILogger Logger;
        
    public Sandbox(SandboxOptions? config = null) {
        Options = config ?? SandboxOptions.Default;
        Logger = config?.Logger ?? Coorth.Logs.Logger.Root;

        OnCreateSandbox(this);
            
        Name = Options.Name;
        Services = config?.Services ?? new ServiceLocator();
        Dispatcher = config?.Dispatcher ?? Dispatcher.Root.CreateChild();
        Context = new SandboxContext();

        InitArchetypes(Options.ArchetypeCapacity.Index, Options.ArchetypeCapacity.Chunk);
        emptyArchetype = new ArchetypeDefinition(this);

        InitEntities(Options.EntityCapacity.Index, Options.EntityCapacity.Chunk);
        InitComponents(Options.ComponentGroupCapacity, Options.ComponentDataCapacity.Index, Options.ComponentDataCapacity.Chunk);

        RootSystem = new SystemRoot();
        InitSystems();
            
        singleton = CreateEntity();
    }
        
    public void Dispose() {
        if (IsDisposed) {
            return;
        }
        ClearEntities();
        ClearComponents();
        ClearSystems();
        ClearArchetypes();
        IsDisposed = true;
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