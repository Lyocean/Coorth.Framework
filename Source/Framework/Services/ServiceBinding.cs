using System;

namespace Coorth.Framework; 

public interface IServiceBinding {
        
    IServiceBinding ToValue(object value);

    IServiceBinding ToFactory(Func<ServiceLocator, object> func);

    object Singleton();

    T Singleton<T>();

    object Create();

    T Create<T>();
}

public class ServiceBinding : Disposable, IServiceBinding {
    private readonly ServiceLocator locator;

    private readonly Type type;

    private volatile Func<ServiceLocator, object>? provider;

    private volatile object? instance;

    private readonly object locking = new();

    public ServiceBinding(ServiceLocator l, Type t) {
        locator = l;
        type = t;
    }

    public IServiceBinding ToValue(object value) {
        instance = value;
        return this;
    }

    public IServiceBinding ToFactory(Func<ServiceLocator, object> func) {
        provider = func;
        return this;
    }

    public object Singleton() {
        if (instance != null) {
            return instance;
        }
        lock (locking) {
            if (instance != null) {
                return instance;
            }
            var inst = Create();
            instance = inst;
            return inst;
        }
    }
        
    public T Singleton<T>() => (T)Singleton();

    public object? Find() {
        if (instance != null) {
            return instance;
        }
        lock (locking) {
            if (instance != null) {
                return instance;
            }
            var inst = provider != null ? provider.Invoke(locator) : Activator.CreateInstance(type);
            instance = inst;
            return inst;
        }
    }
        
    public T? Find<T>() where T : class => Find() as T;

    public object Create() {
        if (provider != null) {
            return provider.Invoke(locator);
        }
        var value = Activator.CreateInstance(type);
        if (value != null) {
            return value;
        }
        throw new NullReferenceException();
    }

    public T Create<T>() => (T)Create();

    protected override void OnDispose(bool dispose) {
        if (instance == null) {
            return;
        }
        if (instance is IDisposable disposable) {
            disposable.Dispose();
        }
        instance = null;
    }
}

public readonly struct ServiceBinding<T> {
    public readonly IServiceBinding Binding;
    public ServiceBinding(IServiceBinding binding) => Binding = binding;
    public T Singleton() => Binding.Singleton<T>();
}