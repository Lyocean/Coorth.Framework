using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Coorth {
    public partial class Sandbox : Disposable {

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

        public World World { get; }

        public Sandbox( SandboxConfig config = null, 
                        World world = null, 
                        IServiceFactory services = null, 
                        EventDispatcher dispatcher = null) {
            
            lock (locking) {
                this.Id = currentId;
                sandboxes.Add(this);
                currentId++;
            }
            this.config = config ?? SandboxConfig.Default;
            this.Name = this.config.Name ?? $"Sandbox_{this.Id.ToString()}"; ;
            this.World = world;
            this.services = services ?? new ServiceContainer();
            this.Dispatcher = dispatcher ?? new EventDispatcher();

            this.InitArchetypes(this.config.ArchetypeCapacity.Index, this.config.ArchetypeCapacity.Chunk);
            this.InitEntities(this.config.EntityCapacity.Index, this.config.EntityCapacity.Chunk);
            this.InitComponents(this.config.ComponentGroupCapacity, this.config.ComponentDataCapacity.Index, this.config.ComponentDataCapacity.Chunk);
            this.InitSystems();

            singleton = CreateEntity();
        }
        
        public object GetService(Type serviceType) {
            return services.GetService(serviceType);
        }

        public T GetService<T>() where T : class {
            return services.GetService<T>();
        }

        #endregion
        
        #region Lifecycle
        
        
        
        protected override void Dispose(bool dispose) {
            _Execute(new EventSandboxDestroy());
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