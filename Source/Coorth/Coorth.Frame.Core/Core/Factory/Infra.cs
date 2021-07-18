using System;
using System.Threading;

namespace Coorth {
    public static class Infra {
        
        private static readonly ServiceContainer container = new ServiceContainer();

        public static EventDispatcher Dispatcher => container.Dispatcher;
        
        private struct Impl<T> where T: class {
            public static T Instance;
        }
        
        public static IServiceBinding Bind(Type type) => container.Bind(type);

        public static IServiceBinding Bind<T>() where T : class, new()  => container.Bind<T>();
        
        public static IServiceBinding Bind<T>(Func<ServiceFactory, T> provider) where T : class  => container.Bind<T>(provider);

        public static T Offer<T>() where T : class, new() {
            if (Impl<T>.Instance!=null) {
                return Impl<T>.Instance;
            }
            var binding = container.Get<T>() ?? Bind<T>();
            var instance = binding.Singleton<T>();
            Interlocked.Exchange(ref Impl<T>.Instance, instance);
            return Impl<T>.Instance;          
        }
        
        public static T Get<T>() where T : class {
            if (Impl<T>.Instance != null) {
                return Impl<T>.Instance;
            }
            var instance = container.Singleton<T>();
            if (instance != null) {
                Interlocked.Exchange(ref Impl<T>.Instance, instance);
                return Impl<T>.Instance;
            }
            else {
                throw new NotBindException();
            }
        }
        
    }

    public class AppInfra {

        public IServiceBinding Bind(Type type) => Infra.Bind(type);
        
        public IServiceBinding Bind<T>() where T : class, new() => Infra.Bind<T>();

        public IServiceBinding Bind<T>(Func<ServiceFactory, T> provider) where T : class  => Infra.Bind<T>(provider);

        public T Offer<T>() where T : class, new() =>  Infra.Offer<T>();

        public static T Get<T>() where T : class => Infra.Get<T>();
    }
    
    public class NotBindException : Exception {
        
    }
}