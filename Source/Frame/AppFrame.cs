using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coorth {
    public abstract partial class AppFrame : Disposable {
        #region Static

        private static readonly object locker = new object();

        private static readonly List<AppFrame> apps = new List<AppFrame>();

        private static AppFrame MainApp {
            get {
                lock (locker) {
                    return apps.FirstOrDefault();
                }
            }
        }

        private static void AddApp(AppFrame app) {
            lock (locker) {
                Infra.Instance.AddChild(app.services);
                app.Index = apps.Count;
                apps.Add(app);
            }
        }

        public static AppFrame GetApp(Guid guid) {
            lock (locker) {
                return apps.Find(app => app.Id == guid);
            }
        }

        public static AppFrame GetApp(string name) {
            lock (locker) {
                return apps.Find(app => app.Name == name);
            }
        }

        public static bool RemoveApp(AppFrame app) {
            lock (locker) {
                if (!apps.Remove(app)) {
                    return false;
                }

                Infra.Instance.RemoveChild(app.services);
                for (var i = 0; i < apps.Count; i++) {
                    apps[i].Index = i;
                }

                return true;
            }
        }

        #endregion

        #region Fields & Lifecycle

        /// <summary> App唯一Id </summary>
        public readonly Guid Id;

        /// <summary> App名称 </summary>
        public readonly string Name;

        /// <summary> App索引 </summary>
        public int Index { get; private set; }

        /// <summary> 是否是主App </summary>
        public bool IsMain => Index == 0;

        /// <summary> App配置 </summary>
        public readonly AppConfig Config;

        private readonly EventDispatcher dispatcher;
        public EventDispatcher Dispatcher => dispatcher;

        private readonly ServiceLocator services;

        public ServiceLocator Managers => services;

        public readonly ActorRuntime Actors;

        private readonly ModuleRoot root;

        private bool isRunning;

        /// <summary> App是否正在运行中 </summary>
        public bool IsRunning => isRunning && !IsDisposed;

        private Thread mainThread;

        /// <summary> App主线程Id </summary>
        public int MainThreadId => mainThread.ManagedThreadId;

        protected AppFrame(string name, AppConfig config = null, Guid id = default) {
            this.Id = id == default ? Guid.NewGuid() : id;
            this.Name = !string.IsNullOrWhiteSpace(name) ? name : "GameApp";
            this.Config = config ?? AppConfig.Default;

            this.dispatcher = new EventDispatcher();
            this.services = new ServiceLocator(this.dispatcher);
            this.Actors = new ActorRuntime(this.dispatcher, this.Config);
            this.root = new ModuleRoot(this, services, this.dispatcher, this.Actors);
            this.root.SetActive(false);

            apps.Add(this);
        }

        public AppFrame Setup() {
            LogUtil.Info(nameof(AppFrame), nameof(Setup));
            OnSetup();
            return this;
        }

        protected virtual void OnSetup() {
        }

        public Task Load() {
            LogUtil.Info(nameof(AppFrame), nameof(Load));
            return OnLoad();
        }

        protected virtual Task OnLoad() {
            return Task.CompletedTask;
        }

        public void Init() {
            LogUtil.Info(nameof(AppFrame), nameof(Init));
            dispatcher.Dispatch(new EventAppBeginInit());
            OnInit();
            dispatcher.Dispatch(new EventAppEndInit());
        }

        protected virtual void OnInit() {
        }

        public void Startup() {
            if (IsRunning) {
                return;
            }

            LogUtil.Info(nameof(AppFrame), nameof(Startup));
            isRunning = true;
            mainThread = Thread.CurrentThread;
            root.SetActive(true);
            OnStartup();
            dispatcher.Dispatch(new EventAppStartup(MainThreadId));
        }

        protected virtual void OnStartup() {
        }

        public void Execute<T>(in T e) {
            if (IsDisposed || !IsRunning) {
                return;
            }

            OnExecute(in e);
#if DEBUG
            dispatcher.Dispatch(e);
#else
            try {
                dispatcher.Dispatch(e);
            }
            catch (Exception ex) {
                LogUtil.Exception(ex);
            }
#endif
        }

        protected virtual void OnExecute<T>(in T e) {
        }

        public void Shutdown() {
            if (!IsRunning) {
                return;
            }

            LogUtil.Info(nameof(AppFrame), nameof(Shutdown));
            dispatcher.Dispatch(new EventAppShutdown());
            OnShutdown();
            root.SetActive(false);
            isRunning = false;
        }

        protected virtual void OnShutdown() {
        }

        protected override void OnDispose(bool dispose) {
            LogUtil.Info(nameof(AppFrame), nameof(OnDispose));
            Shutdown();
            OnDestroy();
        }

        protected virtual void OnDestroy() {
        }

        #endregion
    }
}