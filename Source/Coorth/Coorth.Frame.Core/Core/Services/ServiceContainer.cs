using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Coorth {

    public class ServiceContainer : Disposable, IServiceFactory {
        
        private class Binding : Disposable, IServiceBinding {
            
            private readonly ServiceContainer container;
            
            private readonly Type type;
            
            private Func<ServiceContainer, object> provider;
            
            private volatile object instance;
            
            public Binding(ServiceContainer c, Type t) {
                this.container = c;
                this.type = t;
            }

            public IServiceBinding ToValue(object value) {
                Interlocked.Exchange(ref instance, value);
                return this;
            }

            public IServiceBinding ToFactory(Func<ServiceContainer, object> func) {
                Interlocked.Exchange(ref this.provider, func);
                return this;
            }
            
            public object Singleton() {
                if (instance != null) {
                    return instance;
                }
                var inst = provider.Invoke(container);
                if (Interlocked.CompareExchange(ref instance, inst, null) == null) {
                    container.OnCreateObject(type, instance);
                }
                return instance;
            }
            
            public T Singleton<T>() => (T) Singleton();
            
            public object Create() {
                return provider.Invoke(container);
            }

            public T Create<T>() => (T) Create();

            public bool TryGetSingleton(out object inst) {
                if (instance == null) {
                    inst = null;
                    return false;
                }
                else {
                    inst = instance;
                    return true;
                }
            }

            protected override void Dispose(bool dispose) {
                if (instance != null) {
                    container.OnDestroyObject(type, instance);
                    instance = null;
                }
            }
        }
        
        private readonly ConcurrentDictionary<Type, Binding> services = new ConcurrentDictionary<Type, Binding>();

        private ServiceContainer parent;
        
        private readonly List<ServiceContainer> children = new List<ServiceContainer>();
        
        public event Action<ServiceContainer, Type, object> OnCreate;

        public event Action<ServiceContainer, Type, object> OnDestroy;
        
        public readonly EventDispatcher Dispatcher;

        public ServiceContainer() {
            Dispatcher = new EventDispatcher();
        }
        
        public ServiceContainer(EventDispatcher dispatcher) {
            Dispatcher = dispatcher;
        }
        
        public void AddChild(ServiceContainer child) {
            child.parent = this;
            children.Add(child);
        }
        
        public IServiceBinding Bind(Type type) {
            var service = services.GetOrAdd(type, CreateBinding);
            service.ToFactory(container => Activator.CreateInstance(type));
            return service;
        }
        
        public ServiceBinding<T> Bind<T>(T value) {
            var service = services.GetOrAdd(typeof(T), CreateBinding);
            service.ToValue(value);
            return new ServiceBinding<T>(service);
        }

        public ServiceBinding<T> Bind<T>(Func<ServiceContainer, object> provider) {
            var service = services.GetOrAdd(typeof(T), CreateBinding);
            service.ToFactory(provider);
            return new ServiceBinding<T>(service);
        }

        public ServiceBinding<T> Bind<T>() where T : new() {
            var service = services.GetOrAdd(typeof(T), CreateBinding);
            service.ToFactory(container => new T());
            return new ServiceBinding<T>(service);
        }

        public IServiceBinding Get(Type type) {
            return services.TryGetValue(type, out var binding) ? binding : null;
        }
        
        public ServiceBinding<T> Get<T>() {
            return new ServiceBinding<T>(Get(typeof(T)));
        }
        
        public object Singleton(Type type) {
            if (services.TryGetValue(type, out var binding)) {
                return binding.Singleton();
            }
            return parent?.GetService(type);
        }

        public T Singleton<T>() where T: class {
            if (services.TryGetValue(typeof(T), out var binding)) {
                return binding.Singleton<T>();
            }
            return parent?.GetService<T>();
        }
        
        public object Create(Type type) {
            if (services.TryGetValue(type, out var binding)) {
                return binding.Create();
            }
            return parent == null ? Activator.CreateInstance(type) : parent.Create(type);
        }

        public T Create<T>() {
            if (services.TryGetValue(typeof(T), out var binding)) {
                return binding.Create<T>();
            }
            return parent == null ? Activator.CreateInstance<T>() : parent.Create<T>();
        }

        protected override void Dispose(bool dispose) {
            foreach (var pair in services) {
                pair.Value.Dispose();
            }
            services.Clear();
            foreach (var child in children) {
                child.Dispose();
            }
        }

        private Binding CreateBinding(Type type) {
            return new Binding(this, type);
        }

        public IEnumerable<object> GetSingletons(bool forceResolve = false) {
            if (forceResolve) {
                foreach (var pair in services) {
                    yield return pair.Value.Singleton();
                } 
            }
            else {
                foreach (var pair in services) {
                    if (pair.Value.TryGetSingleton(out var instance)) {
                        yield return instance;
                    }
                }
            }
        }
        
        public object GetService(Type serviceType) {
            return Singleton(serviceType);
        }

        public T GetService<T>() where T : class {
            return Singleton<T>();
        }
        
        
        private void OnCreateObject(Type type, object instance) {
            if (instance is ServiceBase service) {
                service.ServiceAdd(this);
            }
            OnCreate?.Invoke(this, type, instance);
        }

        private void OnDestroyObject(Type type, object instance) {
            if (instance is ServiceBase service) {
                service.ServiceRemove();
            }
            OnDestroy?.Invoke(this, type, instance);
            if (instance is IDisposable disposable) {
                disposable.Dispose();
            }
        }
    }
}