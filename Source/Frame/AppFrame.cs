using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coorth {
    public abstract partial class AppFrame : Disposable, IExecutable {

        public readonly int Id;
        
        /// <summary> App唯一Id </summary>
        public readonly Guid Guid;

        /// <summary> App名称 </summary>
        public readonly string Name;

        /// <summary> App索引 </summary>
        public int Index { get; private set; }

        /// <summary> 是否是主App </summary>
        public bool IsMain => Index == 0;

        /// <summary> App配置 </summary>
        public readonly AppSetting Setting;

        public AppContext Context = new AppContext();

        public AppTime Time => Context.Time;

        private readonly EventDispatcher dispatcher;
        public EventDispatcher Dispatcher => dispatcher;

        private readonly ServiceLocator services;

        public ServiceLocator Services => services;

        public readonly ActorRuntime Actors;

        private bool isSetup;
        
        private bool isRunning;

        /// <summary> App是否正在运行中 </summary>
        public bool IsRunning => isRunning && !IsDisposed;

        private Thread mainThread;

        /// <summary> App主线程Id </summary>
        public int MainThreadId => mainThread.ManagedThreadId;

        protected AppFrame(string name, AppSetting setting) {
            this.Guid = Guid.NewGuid();
            this.Id = setting.AppId;
            this.Name = !string.IsNullOrWhiteSpace(name) ? name : "GameApp";
            this.Setting = setting;
            this.mainThread = Thread.CurrentThread;

            this.dispatcher = new EventDispatcher();
            this.services = new ServiceLocator(this.dispatcher);
            this.Actors = new ActorRuntime(this.dispatcher, this.Setting);
            this.root = new ModuleRoot(this, services, this.dispatcher, this.Actors);
            this.root.SetActive(false);
            
            AddApp(this);
        }

        public AppFrame Setup() {
            LogUtil.Info(nameof(AppFrame), nameof(Setup));
            OnSetup();
            isSetup = true;
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
            if (IsDisposed || !isSetup) {
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
            RemoveApp(this);
        }

        protected virtual void OnDestroy() {
        }
    }
}