using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Coorth {
    public abstract partial class AppFramework : IDisposable {

        private static readonly object locker = new object();
        
        private static readonly List<AppFramework> apps = new List<AppFramework>();
        
        private static AppFramework MainApp {
            get {
                lock (locker) {
                    return apps.FirstOrDefault();
                }
            }
        }

        private static void AddApp(AppFramework app) {
            lock (locker) {
                Infra.Instance.AddChild(app.services);
                app.Index = apps.Count;
                apps.Add(app);
            }
        }
        
        public static AppFramework GetApp(Guid guid) {
            lock (locker) {
                return apps.Find(app => app.Id == guid);
            }
        }
        
        public static AppFramework GetApp(string name) {
            lock (locker) {
                return apps.Find(app => app.Name == name);
            }
        }
        
        public static bool RemoveApp(AppFramework app) {
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
        
        public readonly Guid Id = Guid.NewGuid();

        public readonly string Name;

        public int Index { get; private set; }

        private volatile int disposed;
        public bool IsDisposed => disposed != 0;

        private bool isRunning;
        public bool IsRunning => isRunning;
        
        private readonly AppConfigure configure;
        
        private readonly ActorRuntime actors;

        private readonly ServiceLocator services;

        private readonly EventDispatcher dispatcher = new EventDispatcher();
        
        public AppFramework(string name, AppConfigure configure) {
            this.Name = name;
            this.configure = configure;
            this.services = new ServiceLocator(dispatcher);
            this.actors = new ActorRuntime(dispatcher, configure.Actor, services);
            
            AddApp(this);
        }

        public void Setup() {
            OnSetup();
        }

        protected virtual void OnSetup() {
            
        }
        
        public void Startup() {
            isRunning = true;
            OnStartup();
        }

        protected virtual void OnStartup() {
            
        }
        
        public void Execute<T>(in T e) {
            dispatcher.Dispatch(in e);
        }

        public void Shutdown() {
            isRunning = false;
            OnShutdown();
        }
        
        protected virtual void OnShutdown() {
        
        }
        
        public void Dispose() {
            if (Interlocked.CompareExchange(ref disposed, 1, 0) == 0) {
                RemoveApp(this);
                OnDispose(true);
                GC.SuppressFinalize(this);
            }
        }

        ~AppFramework() {
            if (Interlocked.CompareExchange(ref disposed, 1, 0) == 0) {
                RemoveApp(this);
                OnDispose(false);
            }
        }
        
        protected virtual void OnDispose(bool dispose) {
        }
    }
}