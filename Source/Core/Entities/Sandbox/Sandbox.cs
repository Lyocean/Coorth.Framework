using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Coorth {
    public partial class Sandbox : IDisposable, IExecutable {
        
        public readonly string Name;

        public int Index { get; private set; }

        public bool IsDisposed { get; private set; }

        public readonly SandboxConfig Config;

        public readonly SandboxContext Context;

        public readonly EventDispatcher Dispatcher;

        public readonly IServiceLocator Services;
        
        public Sandbox( SandboxConfig? config = null, 
                        IServiceLocator? services = null, 
                        EventDispatcher? dispatcher = null) {

            OnCreateSandbox(this);
            
            this.Config = config ?? SandboxConfig.Default;
            this.Name = Config.Name;
            this.Services = services ?? new ServiceLocator();
            this.Dispatcher = dispatcher ?? new EventDispatcher();
            this.Context = new SandboxContext();

            this.InitArchetypes(Config.ArchetypeCapacity.Index, Config.ArchetypeCapacity.Chunk);
            this.emptyArchetype = new ArchetypeDefinition(this);

            this.InitEntities(Config.EntityCapacity.Index, Config.EntityCapacity.Chunk);
            this.InitComponents(Config.ComponentGroupCapacity, Config.ComponentDataCapacity.Index, Config.ComponentDataCapacity.Chunk);

            this.RootSystem = new SystemRoot();
            this.InitSystems();
            
            this.singleton = CreateEntity();
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
        
        public object GetService(Type serviceType) {
            return Services.GetService(serviceType);
        }

        public T GetService<T>() where T : class {
            return Services.GetService<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispatch<T>(in T e) {
            Dispatcher.Dispatch(e);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ValueTask DispatchAsync<T>(in T e) {
            return Dispatcher.DispatchAsync(e);
        }
        
        public void Execute<T>(in T e) {
            Dispatcher.Dispatch(e);
        }
        
        public ValueTask ExecuteAsync<T>(in T e) {
            return Dispatcher.DispatchAsync(e);
        }
    }
}