using System;

namespace Coorth {
    
    public interface IServiceBinding {
        
        IServiceBinding ToValue(object value);
        
        IServiceBinding ToFactory(Func<ServiceContainer, object> func);
        
        object Singleton();
        
        T Singleton<T>();

        object Create();

        T Create<T>();
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