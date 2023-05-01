using System;
using System.Collections.Generic;


namespace Coorth.Framework;

[AttributeUsage(AttributeTargets.Class)]
public class SystemAttribute : Attribute {
}

public abstract partial class SystemBase : ServiceNode<SystemBase> {

    #region Common

    private World? world;
    public World World => world ?? throw new NullReferenceException();

    public new SystemRoot Root => root as SystemRoot ?? throw new NullReferenceException();
    
    protected override Dispatcher Dispatcher => World.Dispatcher;

    protected override IServiceLocator Services => World.Services;

    private readonly List<ISystemSubscription> subscriptions = new();

    public IReadOnlyList<ISystemSubscription> Subscriptions => subscriptions;

    protected T Singleton<T>() where T : IComponent => World.Singleton<T>();

    protected T Singleton<T>(Func<Entity, T> provider) where T : IComponent, new() => World.Singleton().Offer(provider);

    public void Setup(World value) => world = value;

    protected ComponentBinding<T> BindComponent<T>() where T : IComponent => World.BindComponent<T>();

    protected ComponentBinding<T> BindComponent<T>(IComponentFactory<T> factory) where T : IComponent {
        var binding = World.BindComponent<T>();
        binding.SetFactory(factory);
        return binding;
    }

    #endregion

    #region Hierarchy
    
    protected sealed override void OnChildAdd(SystemBase child) {
        child.Setup(World);
        World.OnSystemAdd(child.Key, child);
    }
    
    protected sealed override void OnChildRemove(SystemBase child) {
        World.OnSystemRemove(child.Key);
    }

    #endregion
    
}
