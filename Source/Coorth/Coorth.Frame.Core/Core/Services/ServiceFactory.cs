using System;

namespace Coorth {
    public interface IServiceFactory : IServiceProvider {
        T GetService<T>() where T : class;

        object Create(Type type);
        T Create<T>();
    }
}