using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Concurrent;

namespace Coorth {
    public interface IServiceProvider: System.IServiceProvider {

    }

    public class Domain<TDomain>: IServiceProvider where TDomain : Domain<TDomain>, new() {

        public static TDomain Root = new TDomain();

        protected Domain() { }

        protected struct Service<TService> {
            public static TService Instance;
        }

        public static TService Get<TService>() where TService: class {
            var service = Service<TService>.Instance;
            if (service != null) {
                return service;
            }
            return Interlocked.CompareExchange(ref Service<TService>.Instance, Root.GetService<TService>(), null);
        }

        protected ConcurrentDictionary<Type, object> services = new ConcurrentDictionary<Type, object>();

        public object GetService(Type serviceType) {
            if(services.TryGetValue(serviceType, out var value)) {
                return value;
            }
            return services.GetOrAdd(serviceType, CreateService);
        }

        public TService GetService<TService>() {
            return (TService)GetService(typeof(TService));
        }

        protected virtual object CreateService(Type serviceType) {
            return Activator.CreateInstance(serviceType);
        }
    }
}