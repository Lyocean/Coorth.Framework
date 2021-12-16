using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Coorth {
    public class World : Disposable, ISupportInitialize {

        #region Static

        public static World Current;

        private static RawList<World> worlds = new RawList<World>(1);

        public static IReadOnlyList<World> Worlds => worlds.Values;

        private static readonly object locking = new object();

        private static int currentIndex = 0;
        
        #endregion

        public Guid Id = Guid.NewGuid();

        public readonly AppFrame App;

        public readonly int Index;

        public WorldActor Actor;
        
        public readonly Sandbox Active;

        private RawList<Sandbox> sandboxes = new RawList<Sandbox>(1);

        public readonly string Name;

        public readonly ServiceLocator Services;

        public readonly EventDispatcher Dispatcher;
        
        public ActorRuntime Actors => App.Actors;

        public readonly ActorDomain ActorDomain;

        public World(AppFrame app, WorldConfig config) {
            lock (locking) {
                this.Index = currentIndex++;
                worlds.Add(this);
            }

            this.Name = config.Name ?? $"World_{Index}";
            this.Dispatcher = new EventDispatcher();
            this.Services = new ServiceLocator(this.Dispatcher);
            this.App = app;
            
            this.App.Services.AddChild(this.Services);

            this.ActorDomain = this.Actors.CreateDomain<LocalDomain>();

            this.Active = CreateSandbox(config.Sandbox);
        }

        public World AsMain() {
            Current = this;
            return this;
        }

        public Sandbox CreateSandbox(SandboxConfig config) {
            var dispatcher = new EventDispatcher();
            Dispatcher.AddChild(dispatcher);
            var sandbox = new Sandbox(config, Services, dispatcher);
            sandbox.Singleton<WorldComponent>().Setup(this);
            return sandbox;
        }

        public void BeginInit() {
        }

        public void EndInit() {
            
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute<T>(in T e) where T : IEvent {
            Dispatcher.Dispatch(in e);
        }

        protected override void OnDispose(bool dispose) {
            for (var i = 0; i < sandboxes.Count; i++) {
                sandboxes[i].Dispose();                
            }       
            this.Services.Dispose();
            this.Dispatcher.Dispose();
        }
    }
}