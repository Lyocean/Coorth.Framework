using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Coorth {
    public class World : Disposable, ISupportInitialize {

        #region Static

        public static World Runtime;

        private static RawList<World> worlds = new RawList<World>(1);

        public static IReadOnlyList<World> Worlds => worlds.Values;

        private static readonly object locking = new object();

        private static int currentIndex = 0;
        
        #endregion

        public Guid Id = Guid.NewGuid();

        public readonly AppFrame App;

        public readonly int Index;

        public readonly Sandbox Active;

        private RawList<Sandbox> sandboxes = new RawList<Sandbox>(1);

        public readonly string Name;

        public readonly ServiceContainer Services;

        public readonly EventDispatcher Dispatcher;
        
        public ActorContainer Actors => App.Actors;

        public readonly ActorDomain ActorDomain;

        public World(AppFrame app, WorldConfig config) {
            lock (locking) {
                this.Index = currentIndex++;
                worlds.Add(this);
            }

            this.Name = config.Name ?? $"World_{Index}";
            this.Dispatcher = new EventDispatcher();
            this.Services = new ServiceContainer(this.Dispatcher);
            this.App = app;
            
            this.App.Dispatcher.AddChild(this.Dispatcher);
            this.App.Services.AddChild(this.Services);

            this.ActorDomain = this.Actors.CreateDomain<WorldDomain>(Services);

            this.Active = CreateSandbox(config.Sandbox);
        }

        public Sandbox CreateSandbox(SandboxConfig config) {
            var dispatcher = new EventDispatcher();
            Dispatcher.AddChild(dispatcher);
            var sandbox = new Sandbox(config, this, Services, dispatcher);
            return sandbox;
        }

        public void BeginInit() {
        }

        public void EndInit() {
            
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute<T>(in T e) where T : IEvent {
            Dispatcher.Execute(e);
        }

        protected override void Dispose(bool dispose) {
            for (var i = 0; i < sandboxes.Count; i++) {
                sandboxes[i].Dispose();                
            }       
            this.Services.Dispose();
            this.Dispatcher.Dispose();
        }
    }
}