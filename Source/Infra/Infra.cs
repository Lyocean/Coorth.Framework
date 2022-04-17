using System;
using System.Threading;

namespace Coorth {
    public partial class Infra : IServiceLocator {
        
        public ServiceLocator Services => services;
        
        private Infra() { }

        public void AddChild(ServiceLocator child) {
            services.AddChild(child);
        }
        
        public void RemoveChild(ServiceLocator child) {
            services.RemoveChild(child);
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