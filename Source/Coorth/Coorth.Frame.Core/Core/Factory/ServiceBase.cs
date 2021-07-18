using System;

namespace Coorth {
    public abstract class ServiceBase {

        protected ServiceContainer Services { get; private set; }

        protected EventDispatcher Dispatcher => Services.Dispatcher;
        
        protected Disposables Managed;

        public void SystemAdd(ServiceContainer services) {
            this.Services = services;
            OnAdd();
        }

        protected virtual void OnAdd() { }
        
        public void SystemRemove() {
            OnRemove();
            Managed.Dispose();
            Services = null;
        }
        
        protected virtual void OnRemove() { }

        public void Subscribe<T>(Action<T> action) where T : IEvent {
            var reaction = Dispatcher.Subscribe(action);
            Managed.Add(reaction);
        }
    }
}