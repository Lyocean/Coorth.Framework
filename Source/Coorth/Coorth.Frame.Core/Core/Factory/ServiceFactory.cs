using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Coorth {
    public interface IServiceFactory : IServiceProvider {
        T GetService<T>();
    }
    
    public interface IServiceBinding {
        
        IServiceBinding ToValue(object value);
        
        IServiceBinding ToFactory(Func<ServiceFactory, object> func);
        
        object Singleton();
        
        T Singleton<T>();

        object Create();

        T Create<T>();
    }
    
    public class ServiceFactory : Disposable, IServiceFactory {
        
        private class Binding : Disposable, IServiceBinding, IDisposable {
            
            private readonly ServiceFactory factory;
            
            private readonly Type type;
            
            private Func<ServiceFactory, object> provider;
            
            private volatile object instance;
            
            public Binding(ServiceFactory c, Type t) {
                this.factory = c;
                this.type = t;
            }

            public IServiceBinding ToValue(object value) {
                Interlocked.Exchange(ref instance, value);
                return this;
            }

            public IServiceBinding ToFactory(Func<ServiceFactory, object> func) {
                Interlocked.Exchange(ref this.provider, func);
                return this;
            }
            
            public object Singleton() {
                if (instance != null) {
                    return instance;
                }
                var inst = provider.Invoke(factory);
                if (Interlocked.CompareExchange(ref instance, inst, null) == null) {
                    factory.OnCreateObject(type, instance);
                }
                return instance;
            }
            
            public T Singleton<T>() => (T) Singleton();
            
            public object Create() {
                return provider.Invoke(factory);
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
                    factory.OnDestroyObject(type, instance);
                    instance = null;
                }
            }
        }
        
        private readonly ConcurrentDictionary<Type, Binding> services = new ConcurrentDictionary<Type, Binding>();

        public event Action<ServiceFactory, Type, object> OnCreate;

        public event Action<ServiceFactory, Type, object> OnDestroy; 
        
        public IServiceBinding Bind(Type type) {
            var service = services.GetOrAdd(type, CreateBinding);
            service.ToFactory(container => Activator.CreateInstance(type));
            return service;
        }
        
        public IServiceBinding Bind<T>(T value) {
            var service = services.GetOrAdd(typeof(T), CreateBinding);
            service.ToValue(value);
            return service;
        }

        public IServiceBinding Bind<T>(Func<ServiceFactory, object> provider) {
            var service = services.GetOrAdd(typeof(T), CreateBinding);
            service.ToFactory(provider);
            return service;
        }

        public IServiceBinding Bind<T>() where T : new() {
            var service = services.GetOrAdd(typeof(T), CreateBinding);
            service.ToFactory(container => new T());
            return service;
        }

        public IServiceBinding Get(Type type) {
            return services.TryGetValue(type, out var binding) ? binding : null;
        }
        
        public IServiceBinding Get<T>() {
            return Get(typeof(T));
        }
        
        public object Singleton(Type type) {
            if (services.TryGetValue(type, out var binding)) {
                return binding.Singleton();
            }
            return null;
        }

        public T Singleton<T>() where T: class {
            if (services.TryGetValue(typeof(T), out var binding)) {
                return binding.Singleton<T>();
            }
            return default;
        }
        
        public object Create(Type type) {
            if (services.TryGetValue(type, out var binding)) {
                return binding.Create();
            }
            return Activator.CreateInstance(type);
        }

        public T Create<T>() {
            if (services.TryGetValue(typeof(T), out var binding)) {
                return binding.Create<T>();
            }
            return Activator.CreateInstance<T>();
        }

        protected override void Dispose(bool dispose) {
            foreach (var pair in services) {
                pair.Value.Dispose();
            }
            services.Clear();
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
        
        protected virtual void OnCreateObject(Type type, object instance) {
            OnCreate?.Invoke(this, type, instance);
        }

        protected virtual void OnDestroyObject(Type type, object instance) {
            OnDestroy?.Invoke(this, type, instance);
            if (instance is IDisposable disposable) {
                disposable.Dispose();
            }
        }

        public object GetService(Type serviceType) {
            return Get(serviceType).Singleton();
        }

        public T GetService<T>() {
            return Get<T>().Singleton<T>();
        }
    }

}