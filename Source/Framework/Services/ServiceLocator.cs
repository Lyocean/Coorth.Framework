using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Coorth.Framework;

public interface IServiceLocator : IServiceProvider {
    IServiceBinding Bind(Type type);
    IServiceBinding Bind(Type key, Type impl);
    ServiceBinding<T> Bind<T>(T value);
    ServiceBinding<T> Bind<T>(Func<ServiceLocator, T> provider) where T: class;
    ServiceBinding<T> Bind<T>() where T : class, new();
    ServiceBinding<TImpl> Bind<T, TImpl>() where TImpl : class, T, new();

    IServiceBinding GetBinding(Type type);
    ServiceBinding<T> GetBinding<T>() where T : class;

    object Singleton(Type type);
    T Singleton<T>() where T : class;

    T Get<T>() where T : class;
    object Get(Type type);
    T GetService<T>() where T : class;

    object? Find(Type type);
    T? Find<T>() where T : class;
        
    object Create(Type type);
    T Create<T>();

    bool Remove(Type key);
    bool Remove<T>();
        
    void Clear();
}
    
public sealed class ServiceLocator : Disposable, IServiceLocator {
        
    private readonly ConcurrentDictionary<Type, ServiceBinding> bindings = new();
        
    private ServiceLocator? parent;
        
    private readonly List<ServiceLocator> children = new();

    public ServiceLocator CreateChild() {
        var child = new ServiceLocator();
        AddChild(child);
        return child;
    }
        
    public void AddChild(ServiceLocator child) {
        child.parent = this;
        children.Add(child);
    }

    public void RemoveChild(ServiceLocator child) {
        child.parent = null;
        children.Remove(child);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ServiceBinding CreateBinding(Type type) => new ServiceBinding(this, type);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ServiceBinding OfferBinding(Type key) {
        var binding = bindings.GetOrAdd(key, CreateBinding);
        return binding;
    }
        
    public IServiceBinding Bind(Type type) {
        var binding = OfferBinding(type);
        binding.ToFactory(_ => Activator.CreateInstance(type)!);
        return binding;
    }

    public IServiceBinding Bind(Type key, Type impl) {
        var binding = OfferBinding(key);
        binding.ToFactory(_ => Activator.CreateInstance(impl)!);
        return binding;
    }

    public ServiceBinding<T> Bind<T>(T value) {
        var binding = OfferBinding(typeof(T));
        binding.ToValue(value!);
        return new ServiceBinding<T>(binding);
    }

    public ServiceBinding<T> Bind<T>(Func<ServiceLocator, T> provider) where T: class {
        var binding = OfferBinding(typeof(T));
        binding.ToFactory(provider);
        return new ServiceBinding<T>(binding);
    }

    public ServiceBinding<T> Bind<T>() where T : class, new() {
        var binding = OfferBinding(typeof(T));
        binding.ToFactory(_ => new T());
        return new ServiceBinding<T>(binding);
    }

    public ServiceBinding<TImpl> Bind<T, TImpl>() where TImpl : class, T, new() {
        var binding = OfferBinding(typeof(T));
        binding.ToFactory(_ => new TImpl());
        return new ServiceBinding<TImpl>(binding);
    }
        
    public IServiceBinding GetBinding(Type type) => bindings.TryGetValue(type, out var binding) ? binding : throw new NullReferenceException();

    public ServiceBinding<T> GetBinding<T>() where T : class => new ServiceBinding<T>(GetBinding(typeof(T)));

    public object Singleton(Type type) {
        if (bindings.TryGetValue(type, out var binding)) {
            return binding.Singleton();
        }
        return parent?.GetService(type) ?? throw new NullReferenceException();
    }

    public T Singleton<T>() where T: class {
        if (bindings.TryGetValue(typeof(T), out var binding)) {
            return binding.Singleton<T>();
        }
        return parent?.GetService<T>() ?? throw new NullReferenceException();
    }

    public T Get<T>() where T: class => Singleton<T>();

    public object Get(Type type) => Singleton(type);
        
    public object? Find(Type type) => bindings.TryGetValue(type, out var binding) ? binding.Find() : parent?.Find(type);

    public T? Find<T>() where T : class => bindings.TryGetValue(typeof(T), out var binding) ? binding.Find<T>() : parent?.Find<T>();

    public object Create(Type type) {
        if (bindings.TryGetValue(type, out var binding)) {
            return binding.Create();
        }
        if (parent != null) {
            return parent.Create(type);
        }
        return Activator.CreateInstance(type)!;
    }

    public T Create<T>() {
        if (bindings.TryGetValue(typeof(T), out var binding)) {
            return binding.Create<T>();
        }
        return parent == null ? Activator.CreateInstance<T>() : parent.Create<T>();
    }

    public bool Remove(Type key) {
        if (bindings.TryGetValue(key, out var binding)) {
            binding.Dispose();
            return bindings.Remove(key, out binding);
        }
        return false;
    }

    public bool Remove<T>() => Remove(typeof(T));

    protected override void OnDispose(bool disposing) {
        foreach (var child in children) {
            child.Dispose();
        }
        children.Clear();
        foreach (var pair in bindings) {
            pair.Value.Dispose();
        }
        bindings.Clear();
    }

    public object GetService(Type serviceType) => Singleton(serviceType);

    public T GetService<T>() where T : class => Singleton<T>();
        
    public void Clear() {
        foreach (var (_, binding) in bindings) {
            binding.Dispose();
        }
        bindings.Clear();
    }
}