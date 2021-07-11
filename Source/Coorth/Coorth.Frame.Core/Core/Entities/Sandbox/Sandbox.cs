using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Coorth {
    public partial class Sandbox : Disposable, IServiceFactory {

        #region Static

        private static RawList<Sandbox> sandboxes = new RawList<Sandbox>(4);

        public static IReadOnlyList<Sandbox> Sandboxes => sandboxes.Values;
        
        private static int currentId = 0;
        
        private static readonly object locking = new object();

        #endregion

        #region Fields

        public readonly string Name;
        
        public readonly int Id;
        
        internal readonly SandboxConfig config;

        public readonly EventDispatcher Dispatcher;

        // private readonly EventScheduler scheduler;

        private const int DEFAULT_COMPONENT_GROUP_CAPACITY = 32;

        private readonly IServiceFactory services;

        public IServiceFactory Services => services;
        
        public Sandbox(string name = null, SandboxConfig config = null, IServiceFactory services = null, EventDispatcher dispatcher = null) {
            lock (locking) {
                this.Id = currentId;
                sandboxes.Add(this);
                currentId++;
            }

            this.Name = name ?? $"Sandbox_{this.Id.ToString()}"; ;
            this.config = config ?? SandboxConfig.Default;

            this.services = services ?? new ServiceFactory();
            this.Dispatcher = dispatcher ?? new EventDispatcher();
            this.InitArchetypes();
            this.InitEntities(this.config.EntityCapacity.Index, this.config.EntityCapacity.Chunk);
            this.InitComponents(this.config.ComponentGroupCapacity, this.config.ComponentDataCapacity.Index, this.config.ComponentDataCapacity.Chunk);
            this.InitSystems();
        }
        
        public object GetService(Type serviceType) {
            return services.GetService(serviceType);
        }

        public T GetService<T>() {
            return services.GetService<T>();
        }

        #endregion
        
        #region Lifecycle
        
        protected override void Dispose(bool dispose) {
            _Execute(new EventDestroy());
            ClearEntities();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void _Execute<T>(T e = default) where T : IEvent {
            Dispatcher.Execute(e);
        }
        
        public void Execute<T>(T e = default) where T : IEvent {
            _Execute<T>(e);
        }
        
        #endregion        

    }
}