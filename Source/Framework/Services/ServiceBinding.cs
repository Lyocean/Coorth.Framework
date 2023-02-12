using System;
using Coorth.Logs;

namespace Coorth.Framework; 

public interface IServiceBinding {
    
}

public interface IServiceBinding<T> : IServiceBinding where T: class {
    
}

public abstract class ServiceBinding : IDisposable, IServiceBinding {

    private readonly Type type;

    protected ServiceBinding(Type t) {
        type = t;
    }
    public abstract void Dispose();
}

public interface IServiceSingleton {
    object Value { get; }
}

public sealed class ServiceSingleton<T> : ServiceBinding, IServiceBinding<T>, IServiceSingleton where T: class {
    
    public readonly T Value;
    
    object IServiceSingleton.Value => Value;

    public ServiceSingleton(Type key, T value) : base(key) {
        Value = value;
    }

    public override void Dispose() {
        if (Value is IDisposable disposable) {
            disposable.Dispose();
        }
    }
}

public interface IServiceFactory {
    object Create(ServiceLocator locator);
}

public sealed class ServiceFactory<T> : ServiceBinding, IServiceBinding<T>, IServiceFactory where T: class {

    public readonly Delegate Provider;
    
    public ServiceFactory(Type key, Delegate provider) : base(key) {
        Provider = provider;
    }

    public override void Dispose() { }

    object IServiceFactory.Create(ServiceLocator locator) {
        return ((Func<ServiceLocator, T>)Provider).Invoke(locator);
    }
    
    public T Create(ServiceLocator locator) {
        return Provider switch {
            Func<T> func => func(),
            Func<ServiceLocator, T> func2 => func2(locator),
            _ => throw new InvalidCastException()
        };
    }
    
    public T Create(ServiceLocator locator, object param) {
        return ((Func<ServiceLocator, object, T>)Provider).Invoke(locator, param);
    }
}
