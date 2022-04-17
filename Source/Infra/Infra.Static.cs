using System;
using System.Threading;

namespace Coorth {
    public partial class Infra {

        public static readonly Infra Instance = new Infra();
        
        private static readonly ServiceLocator services = new ServiceLocator();
        
        public static EventDispatcher Dispatcher => services.Dispatcher;
        
        private struct Impl<T> where T: class {
            public static T? Inst;
        }
        
        public static IServiceBinding Bind(Type type) => services.Bind(type);

        public static IServiceBinding Bind(Type type, Type implType) => services.Bind(type);
        
        public static ServiceBinding<T> Bind<T>() where T : class, new()  => services.Bind<T>();
        
        public static ServiceBinding<T> Bind<T>(Func<ServiceLocator, T> provider) where T : class  => services.Bind<T>(provider);
        
        public static ServiceBinding<T> Bind<T>(T instance) where T : class  => services.Bind(instance);

        public static ServiceBinding<TImpl> Bind<T, TImpl>() where TImpl : T, new() => services.Bind<T, TImpl>();

        public static T Offer<T>() where T : class, new() {
            if (Impl<T>.Inst!=null) {
                return Impl<T>.Inst;
            }
            var binding = services.GetBinding<T>().Binding ?? Bind<T>().Binding;
            var instance = binding.Singleton<T>();
            Interlocked.Exchange(ref Impl<T>.Inst, instance);
            return instance;          
        }
        
        public static T Get<T>() where T : class {
            if (Impl<T>.Inst != null) {
                return Impl<T>.Inst;
            }
            var inst = services.Singleton<T>();
            if (inst != null) {
                Interlocked.Exchange(ref Impl<T>.Inst, inst);
                return Impl<T>.Inst!;
            }
            else {
                throw new NotBindException();
            }
        }

        public static void Execute<T>(in T e) {
            services.Dispatcher.Dispatch(in e);
        }
    }
}