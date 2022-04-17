using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Coorth.Common;

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

        public ServiceLocator Services => App.Services;

        public EventDispatcher Dispatcher => App.Dispatcher;
        
        public ActorRuntime Actors => App.Actors;

        public readonly ActorDomain ActorDomain;

        public readonly WorldModule Module;

        public World(AppFrame app, WorldConfig config, WorldModule module) {
            lock (locking) {
                this.Index = currentIndex++;
                worlds.Add(this);
            }

            this.Name = config.Name ?? $"World_{Index}";
            this.App = app;
            
            // this.Services = App.Managers;
            // this.App.Services.AddChild(this.Services);
            // this.App.Dispatcher.AddChild(this.Dispatcher);

            this.ActorDomain = this.Actors.CreateDomain<LocalDomain>();
            this.Module = module;
            this.Active = CreateSandbox(config.Sandbox);
        }

        public void SetActive(bool active) {
            for (var i = 0; i < sandboxes.Count; i++) {
                var sandbox = sandboxes[i];
                sandbox.SetActive(active);
            }
        }
        
        public World AsMain() {
            Current = this;
            return this;
        }

        public Sandbox CreateSandbox(SandboxConfig config) {
            var dispatcher = new EventDispatcher();
            var sandbox = new Sandbox(config, Services, dispatcher);
            Dispatcher.AddChild(sandbox.Dispatcher);
            sandbox.Singleton<SandboxComponent>().Setup(this);
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