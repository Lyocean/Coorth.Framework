using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Coorth {
    public abstract class AppFrame : Disposable, ISupportInitialize {

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
        

        public readonly AppConfig Config;

        private bool isRunning = false;

        public bool IsRunning => isRunning;

        public bool IsMain { get; private set; }

        public int MainThreadId { get; private set; }

        protected readonly EventDispatcher Dispatcher = new EventDispatcher();

        public readonly ActorRuntime Actors;
        
        public Infra Infra => Infra.Instance;
        public ServiceLocator Services => Infra.Services;
        
        public World CurrentWorld {get=> World.Current; private set=>World.Current = value; }

        private readonly List<World> worlds = new List<World>();
        public IReadOnlyList<World> Worlds => worlds;

        public ModuleRoot Module { get; private set; }
        

        protected AppFrame(AppConfig config = null) {
            this.Config = config ?? AppConfig.Default;
            this.Actors = new ActorRuntime(this.Dispatcher, this.Config);

            this.Module = new ModuleRoot(Services, this.Actors, this);

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
            Dispatcher.Dispatch(new EventAppStartup(MainThreadId));
            return true;
        }
        
        protected virtual void OnStartup() { }
        
        public void Execute<T>(in T e) where T: IEvent {
            if (IsDisposed) {
                return;
            }
            //AppFrame Events
            try {
                Dispatcher.Dispatch(e);
            }
            catch (Exception ex) {
                LogUtil.Exception(ex);
            }
            //Infrastructure Events
            try {
                Infra.Dispatcher.Dispatch(e);
            }
            catch (Exception ex) {
                LogUtil.Exception(ex);
            }
            //World Events
            foreach (World world in worlds) {
                try {
                    world.Dispatcher.Dispatch(e);
                } catch (Exception ex) {
                    LogUtil.Exception(ex);
                }
            }
            //Module Events
            try {
                Module.Dispatcher.Dispatch(e);
            } catch (Exception ex) {
                LogUtil.Exception(ex);
            }
        }

        public void Shutdown() {
            if (!isRunning) {
                return;
            }
            isRunning = false;
            Dispatcher.Dispatch(new EventAppShutdown());
            OnShutdown();
        }
        
        protected virtual void OnShutdown() { }
        
        protected override void OnDispose(bool dispose) {
            Shutdown();
            OnDispose();
        }
        
        protected virtual void OnDispose() { }


        protected virtual void SetupInfra() {

        }

        
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
        }
        
        protected virtual void SetupModule() { }
        

    }

}
