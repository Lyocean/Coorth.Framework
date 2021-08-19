using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Coorth {
    public abstract class AppFrame : Disposable, ISupportInitialize {

        #region Static

        private static readonly List<AppFrame> apps = new List<AppFrame>();

        public static AppFrame Main { get => apps.Count > 0 ? apps[0] : default; set => ChangeMainApp(apps.IndexOf(value)); }

        private static void ChangeMainApp(int index) {
            if (index <= 0 || index >= apps.Count) {
                return;
            }
            var temp = apps[0];
            apps[0] = apps[index];
            apps[index] = temp;
            apps[0].IsMain = true;
            apps[index].IsMain = false;
        }
        
        #endregion
        
        #region Fields

        public readonly AppConfig Config;

        private bool isRunning = false;

        public bool IsRunning => isRunning;

        public bool IsMain { get; private set; }

        public int MainThreadId { get; private set; }

        protected readonly EventDispatcher Dispatcher = new EventDispatcher();

        public readonly ActorContainer Actors;
        
        public Infra Infra => Infra.Instance;
        public ServiceContainer Services => Infra.Services;
        
        public World CurrentWorld {get=> World.Current; private set=>World.Current = value; }

        private readonly List<World> worlds = new List<World>();
        public IReadOnlyList<World> Worlds => worlds;

        public RootModule Module { get; private set; }

        #endregion

        #region Lifecycle

        protected AppFrame(AppConfig config = null) {
            this.Config = config ?? AppConfig.Default;
            this.Actors = new ActorContainer(this.Dispatcher, this.Config);

            this.Module = new RootModule(Services);

            if (apps.Count == 0) {
                this.IsMain = true;
            }
            apps.Add(this);
        }

        public AppFrame AsMain() {
            Main = this;
            return this;
        }
        
        public virtual void Setup() {
            
            SetupInfra();
            SetupWorld();
            SetupModule();
            
            OnSetup();
        }

        protected virtual void OnSetup() { }

        public virtual System.Threading.Tasks.Task Load() { return Task.CompletedTask; }
        
        public virtual void BeginInit() {
            Execute(new EventAppBeginInit());
        }

        public virtual void EndInit() {
            Execute(new EventAppEndInit());
        }

        public bool Startup() {
            if (isRunning) {
                return false;
            }
            isRunning = true;
            MainThreadId = Thread.CurrentThread.ManagedThreadId;
            OnStartup();
            Dispatcher.Execute(new EventAppStartup(MainThreadId));
            return true;
        }
        
        protected virtual void OnStartup() { }
        
        public void Execute<T>(in T e) where T: IEvent {
            if (IsDisposed) {
                return;
            }
            //AppFrame Events
            try {
                Dispatcher.Execute(e);
            }
            catch (Exception ex) {
                LogUtil.Exception(ex);
            }
            //Infrastructure Events
            if (IsMain) {
                try {
                    Infra.Dispatcher.Execute(e);
                }
                catch (Exception ex) {
                    LogUtil.Exception(ex);
                }
            }
            //World Events
            foreach (World world in worlds) {
                try {
                    world.Dispatcher.Execute(e);
                } catch (Exception ex) {
                    LogUtil.Exception(ex);
                }
            }
            //Module Events
            try {
                Module.Dispatcher.Execute(e);
            } catch (Exception ex) {
                LogUtil.Exception(ex);
            }
        }

        public void Shutdown() {
            if (!isRunning) {
                return;
            }
            isRunning = false;
            Dispatcher.Execute(new EventAppShutdown());
            OnShutdown();
        }
        
        protected virtual void OnShutdown() { }
        
        protected override void Dispose(bool dispose) {
            Shutdown();
            OnDispose();
        }
        
        protected virtual void OnDispose() { }
        
        #endregion
        
        #region Infra

        protected virtual void SetupInfra() { }

        #endregion
        
        #region World
        
        public World CreateWorld(WorldConfig config) {
            var world = new World(this, config);
            worlds.Add(world);
            if (CurrentWorld == null) {
                CurrentWorld = world;
            }
            if (IsRunning) {
                CurrentWorld.Execute(new EventAppStartup(MainThreadId));
            }
            return world;
        }

        protected virtual WorldConfig GetDefaultWorldConfig() {
            return new WorldConfig();
        }
        
        protected virtual void SetupWorld() {
            var config = GetDefaultWorldConfig();
            var world = CreateWorld(config);

            var sandbox = world.Active;
            sandbox.AddSystem<CoreSystems>();
        }
        
        #endregion

        #region Module

        protected virtual void SetupModule() { }

        #endregion

    }

}
