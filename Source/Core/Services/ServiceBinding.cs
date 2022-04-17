using System;
using System.Threading;

namespace Coorth {
    public interface IServiceBinding {
        
        IServiceBinding ToValue(object value);
        
        IServiceBinding ToFactory(Func<ServiceLocator, object> func);
        
        object Singleton();
        
        T Singleton<T>();

        object Create();

        T Create<T>();
    }

    
    public partial class ServiceLocator {
           
        private class ServiceBinding : Disposable, IServiceBinding {
            
            private readonly ServiceLocator locator;
            
            private readonly Type type;
            
            private Func<ServiceLocator, object> provider;
            
            private volatile object? instance;
            
            public ServiceBinding(ServiceLocator l, Type t) {
                this.locator = l;
                this.type = t;
            }

            public IServiceBinding ToValue(object value) {
                Interlocked.Exchange(ref instance, value);
                return this;
            }

            public IServiceBinding ToFactory(Func<ServiceLocator, object> func) {
                Interlocked.Exchange(ref this.provider, func);
                return this;
            }
            
            public object Singleton() {
                if (instance != null) {
                    return instance;
                }
                var inst = provider.Invoke(locator);
                if (Interlocked.CompareExchange(ref instance, inst, null) == null) {
                    locator.OnCreateObject(type, instance);
                }
                return instance;
            }
            
            public T Singleton<T>() => (T) Singleton();
            
            public object Create() {
                return provider.Invoke(locator);
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

            protected override void OnDispose(bool dispose) {
                if (instance != null) {
                    locator.OnDestroyObject(type, instance);
                    instance = null;
                }
            }
        }
        
    }

    public readonly struct ServiceBinding<T> {
        public readonly IServiceBinding Binding;

        public ServiceBinding(IServiceBinding binding) {
            this.Binding = binding;
        }

        public T Singleton() {
            return Binding.Singleton<T>();
        }
    }
}