using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Coorth {
    public interface IServiceLocator : IServiceProvider {
        T GetService<T>() where T : class;

        object Create(Type type);
        
        T Create<T>();
    }
    
    public partial class ServiceLocator : Disposable, IServiceLocator {
     
        private readonly ConcurrentDictionary<Type, ServiceBinding> services = new ConcurrentDictionary<Type, ServiceBinding>();

        private ServiceLocator parent;
        
        private readonly List<ServiceLocator> children = new List<ServiceLocator>();
        
        public event Action<ServiceLocator, Type, object> OnCreate;

        public event Action<ServiceLocator, Type, object> OnDestroy;
        
        public readonly EventDispatcher Dispatcher;

        public ServiceLocator() {
            Dispatcher = new EventDispatcher();
        }
        
        public ServiceLocator(EventDispatcher dispatcher) {
            Dispatcher = dispatcher;
        }
        
        public void AddChild(ServiceLocator child) {
            child.parent = this;
            children.Add(child);
        }

        public void RemoveChild(ServiceLocator child) {
            child.parent = null;
            children.Remove(child);
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

        public ServiceBinding<T> Bind<T>(Func<ServiceLocator, object> provider) {
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

        protected override void OnDispose(bool dispose) {
            foreach (var pair in services) {
                pair.Value.Dispose();
            }
            services.Clear();
            foreach (var child in children) {
                child.Dispose();
            }
        }

        private ServiceBinding CreateBinding(Type type) {
            return new ServiceBinding(this, type);
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