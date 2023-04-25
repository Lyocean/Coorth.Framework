using System;
using System.Collections.Generic;
using System.Linq;

namespace Coorth.Framework;

public interface IServiceCollection : IServiceProvider {
    T GetService<T>() where T : class;

    object? FindService(Type serviceType);

    T? FindService<T>() where T : class;
}

public interface IServiceLocator : IServiceCollection {

    IServiceLocator CreateChild();
    
    T AddService<T>(T value) where T: class;
    
    IServiceBinding<T> AddFactory<T>(Func<ServiceLocator, T> provider) where T: class;
    IServiceBinding<T> AddFactory<T>(Func<ServiceLocator, object, T> provider) where T: class;
    IServiceBinding<T> AddFactory<T>() where T : class, new();
    IServiceBinding<T> AddFactory<T, TImpl>() where T: class where TImpl : class, T, new();

    IServiceBinding GetBinding(Type type);
    IServiceBinding<T> GetBinding<T>() where T : class;

    
    T Get<T>() where T : class;
    
    object Get(Type type);

    object? Find(Type type);
    
    T? Find<T>() where T : class;
        
    object Create(Type type);
    T Create<T>() where T: class;

    void Clear();
}
    
public sealed class ServiceLocator : Disposable, IServiceLocator {
        
    private readonly Dictionary<Type, ServiceBinding> bindings = new();
        
    private ServiceLocator? parent;
        
    private readonly List<ServiceLocator> children = new();

    public IServiceLocator CreateChild() {
        var child = new ServiceLocator();
        child.parent = this;
        children.Add(child);
        return child;
    }

    private void RemoveChild(ServiceLocator child) {
        child.parent = null;
        children.Remove(child);
    }

    public T AddService<T>(T value) where T: class {
        var key = typeof(T);
        var binding = new ServiceSingleton<T>(key, value);
        bindings.Add(key, binding);
        return value;
    }
    
    public IServiceBinding<T> AddFactory<T>(Func<T> provider) where T: class {
        var key = typeof(T);
        var binding = new ServiceFactory<T>(key, provider);
        bindings.Add(key, binding);
        return binding;
    }
    
    public IServiceBinding<T> AddFactory<T>(Func<ServiceLocator, T> provider) where T: class {
        var key = typeof(T);
        var binding = new ServiceFactory<T>(key, provider);
        bindings.Add(key, binding);
        return binding;
    }
    
    public IServiceBinding<T> AddFactory<T>(Func<ServiceLocator, object, T> provider) where T: class {
        var key = typeof(T);
        var binding = new ServiceFactory<T>(key, provider);
        bindings.Add(key, binding);
        return binding;
    }
    
    public IServiceBinding<T> AddFactory<T>() where T : class, new() {
        var key = typeof(T);
        var binding = new ServiceFactory<T>(key, (ServiceLocator _) => Activator.CreateInstance<T>());
        bindings.Add(key, binding);
        return binding;
    }

    public IServiceBinding<T> AddFactory<T, TImpl>() where T: class where TImpl : class, T, new() {
        var key = typeof(T);
        var binding = new ServiceFactory<T>(key, Activator.CreateInstance<TImpl>);
        bindings.Add(key, binding);
        return binding;
    }
    
    public IServiceBinding GetBinding(Type type) => bindings.TryGetValue(type, out var binding) ? binding : throw new NullReferenceException();

    public IServiceBinding<T> GetBinding<T>() where T : class => (IServiceBinding<T>)GetBinding(typeof(T));

    public object GetService(Type key) {
        if (bindings.TryGetValue(key, out var binding) && binding is IServiceSingleton singleton) {
            return singleton.Value;
        }
        return parent?.GetService(key) ?? throw new NullReferenceException();
    }

    public T GetService<T>() where T : class {
        var key = typeof(T);
        if (bindings.TryGetValue(key, out var binding) && binding is ServiceSingleton<T> singleton) {
            return singleton.Value;
        }
        return parent?.GetService<T>() ?? throw new ServiceException($"Service not found: {typeof(T)}");
    }

    public object? FindService(Type serviceType) => Find(serviceType);

    public T? FindService<T>() where T : class => Find<T>();

    public T Get<T>() where T: class => GetService<T>();

    public object Get(Type type) => GetService(type);

    public object? Find(Type key) {
        if (bindings.TryGetValue(key, out var binding) && binding is IServiceSingleton singleton) {
            return singleton.Value;
        }
        return parent?.Find(key);
    }

    public T? Find<T>() where T : class {
        var key = typeof(T);
        if (bindings.TryGetValue(key, out var binding) && binding is ServiceSingleton<T> singleton) {
            return singleton.Value;
        }
        return parent?.Find<T>();
    }

    public object Create(Type type) {
        if (bindings.TryGetValue(type, out var binding) && binding is IServiceFactory factory) {
            return factory.Create(this);
        }
        if (parent != null) {
            return parent.Create(type);
        }
        return Activator.CreateInstance(type)!;
    }

    public T Create<T>() where T: class {
        if (bindings.TryGetValue(typeof(T), out var binding) && binding is ServiceFactory<T> factory) {
            return factory.Create(this);
        }
        if (parent != null) {
            return parent.Create<T>();
        }
        return Activator.CreateInstance<T>();
    }

    public T Create<T>(object param) where T: class {
        if (bindings.TryGetValue(typeof(T), out var binding) && binding is ServiceFactory<T> factory) {
            return factory.Create(this);
        }
        if (parent != null) {
            return parent.Create<T>();
        }
        return Activator.CreateInstance<T>();
    }
    
    public void Clear() {
        foreach (var binding in bindings.Values.ToArray()) {
            binding.Dispose();
        }
        bindings.Clear();
    }
    
    protected override void OnDispose() {
        foreach (var locator in children.ToArray()) {
            locator.Dispose();
        }
        children.Clear();
        Clear();
        parent?.RemoveChild(this);
    }
}