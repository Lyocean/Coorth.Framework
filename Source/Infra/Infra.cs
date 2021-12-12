using System;
using System.Threading;

namespace Coorth {
    public class Infra : IServiceLocator {

        public static readonly Infra Instance = new Infra();
        
        private Infra() { }
        
        private static readonly ServiceLocator services = new ServiceLocator();

        public ServiceLocator Services => services;
        
        public static EventDispatcher Dispatcher => services.Dispatcher;
        
        private struct Impl<T> where T: class {
            public static T Instance;
        }
        
        public static IServiceBinding Bind(Type type) => services.Bind(type);

        public static ServiceBinding<T> Bind<T>() where T : class, new()  => services.Bind<T>();
        
        public static ServiceBinding<T> Bind<T>(Func<ServiceLocator, T> provider) where T : class  => services.Bind<T>(provider);

        public static T Offer<T>() where T : class, new() {
            if (Impl<T>.Instance!=null) {
                return Impl<T>.Instance;
            }
            var binding = services.Get<T>().Binding ?? Bind<T>().Binding;
            var instance = binding.Singleton<T>();
            Interlocked.Exchange(ref Impl<T>.Instance, instance);
            return Impl<T>.Instance;          
        }
        
        public static T Get<T>() where T : class {
            if (Impl<T>.Instance != null) {
                return Impl<T>.Instance;
            }
            var instance = services.Singleton<T>();
            if (instance != null) {
                Interlocked.Exchange(ref Impl<T>.Instance, instance);
                return Impl<T>.Instance;
            }
            else {
                throw new NotBindException();
            }
        }

        public IServiceBinding BindService(Type type) => Bind(type);

        public ServiceBinding<T> BindService<T>() where T : class, new()  => Bind<T>();
        
        public ServiceBinding<T> BindService<T>(Func<ServiceLocator, T> provider) where T : class  => Bind<T>(provider);

        public T OfferService<T>() where T : class, new() => Offer<T>();
        
        public object GetService(Type serviceType) => services.GetService(serviceType);

        public T GetService<T>() where T : class => Get<T>();

        public object Create(Type type) => services.Create(type);

        public T Create<T>() => services.Create<T>();
    }
}