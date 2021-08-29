using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

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
        
        internal readonly SandboxConfig Config;

        public readonly EventDispatcher Dispatcher;

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
            this.Config = config ?? SandboxConfig.Default;
            this.Name = this.Config.Name ?? $"Sandbox_{this.Id.ToString()}"; ;
            this.World = world;
            this.services = services ?? new ServiceContainer();
            this.Dispatcher = dispatcher ?? new EventDispatcher();

            this.InitArchetypes(this.Config.ArchetypeCapacity.Index, this.Config.ArchetypeCapacity.Chunk);
            this.InitEntities(this.Config.EntityCapacity.Index, this.Config.EntityCapacity.Chunk);
            this.InitComponents(this.Config.ComponentGroupCapacity, this.Config.ComponentDataCapacity.Index, this.Config.ComponentDataCapacity.Chunk);
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
        internal void _Execute<T>(in T e = default) where T : IEvent {
            Dispatcher.Execute(e);
        }
        
        public void Execute<T>(in T e = default) where T : IEvent {
            _Execute<T>(e);
        }
        
        public Task ExecuteAsync<T>(in T e = default) where T : IEvent {
            return Dispatcher.ExecuteAsync(e);
        }
        
        #endregion

        #region Read & Write

        public void ReadSandbox<TSerializer>(TSerializer serializer) where TSerializer : ISerializer {
            var archetypeCount = serializer.Read<int>();
            for (var i = 0; i < archetypeCount; i++) {
                ReadArchetypeWithEntities<TSerializer>(serializer, null);
            }
        }

        public void WriteSandbox<TSerializer>(TSerializer serializer) where TSerializer : ISerializer {
            var archetypeCount = archetypes.Sum(pair => pair.Value.Count);
            serializer.WriteValue<int>(archetypeCount);
            foreach (var pair in archetypes) {
                for (int i = 0; i < pair.Value.Count; i++) {
                    var archetype = pair.Value[i];
                    WriteArchetypeWithEntities<TSerializer>(serializer, archetype);
                }
            }
        }

        #endregion
    }
}