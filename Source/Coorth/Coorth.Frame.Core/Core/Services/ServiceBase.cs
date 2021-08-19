using System;

namespace Coorth {
    public abstract class ServiceBase : Disposable {

        protected ServiceContainer Services { get; private set; }

        public EventDispatcher Dispatcher => Services.Dispatcher;
        
        protected Disposables Managed;

        public void ServiceAdd(ServiceContainer services) {
            this.Services = services;
            OnAdd();
        }

        protected virtual void OnAdd() { }
        
        public void ServiceRemove() {
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