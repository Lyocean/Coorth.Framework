using System;
using System.Collections.Generic;
using System.Linq;

namespace Coorth.Framework; 

public abstract partial class SystemBase : IDisposable {

    #region Fields

#nullable disable
        
    public Sandbox Sandbox { get; private set; }
        
    protected SystemBinding Binding { get; private set; }

#nullable enable

    public bool IsDisposed { get; private set; }

    protected Dispatcher Dispatcher => Sandbox.Dispatcher;
        
    private readonly List<ISystemSubscription> subscriptions = new();

    public IReadOnlyList<ISystemSubscription> Subscriptions => subscriptions;
        
    protected T Singleton<T>() where T : IComponent => Sandbox.Singleton<T>();
        
    protected T Singleton<T>(Func<Entity, T> provider) where T : IComponent, new() => Sandbox.Singleton().Offer(provider);

    public void Setup(Sandbox sandbox, SystemBinding binding) {
        this.Sandbox = sandbox;
        this.Binding = binding;
    }
        
    public void Dispose() {
        if (IsDisposed) {
            return;
        }
        OnDispose();
    }

    private void OnDispose() {
        IsDisposed = true;
        ClearSystems();
        Parent?.RemoveSystem(Key);
    }

    #endregion

    #region Hierarchy

    public SystemBase AddSystem(Type type, bool reflection = false) {
        var binding = Sandbox.GetSystemBinding(type, reflection);
        if (binding == null) {
            throw new NotBindException(type);
        }
        return binding.AddSystem(this);
    }
        
    public T AddSystem<T>() where T : SystemBase, new() {
        var system = Activator.CreateInstance<T>();
        return AddSystem(system);
    }
        
    public T AddSystem<T>(T system) where T : SystemBase {
        var binding = Sandbox.BindSystem<T>();
        var key = typeof(T);
        system.Setup(Sandbox, binding);
        AddChild(key, system);
        Sandbox.OnSystemAdd(key, system);
        system.SetActive(true);
        return system;
    }
        
    /// <summary>
    /// 移除子系统
    /// </summary>
    /// <param name="type">子系统类型</param>
    /// <returns></returns>
    public bool RemoveSystem(Type type) {
        var binding = Sandbox.GetSystemBinding(type, false);
        return binding != null && binding.RemoveSystem(this);
    }
        
    /// <summary>
    /// 移除子系统
    /// </summary>
    /// <typeparam name="T">子系统类</typeparam>
    /// <returns>是否成功移除</returns>
    public bool RemoveSystem<T>() where T : SystemBase {
        var key = typeof(T);
        if (Children.TryGetValue(key, out var child)) {
            child.SetActive(false);
        }
        var result = Sandbox.OnSystemRemove<T>(key);
        return result && RemoveChild(key);
    }
        
    /// <summary>
    /// 清理所有子系统
    /// </summary>
    public void ClearSystems() {
        if (children == null) {
            return;
        }
        var keys = children.Keys.ToList();
        foreach (var key in keys) {
            RemoveSystem(key);
        }
        children.Clear();
    }
        
    /// <summary>
    /// 是否存在子系统
    /// </summary>
    /// <typeparam name="T">子系统类</typeparam>
    /// <returns>结果true:存在/false:不存在</returns>
    public bool HasSystem<T>() where T : SystemBase => HasSystem(typeof(T));

    /// <summary>
    /// 是否存在子系统
    /// </summary>
    /// <param name="type">子系统类型</param>
    /// <returns>结果true:存在/false:不存在</returns>
    public bool HasSystem(Type type) => Children.ContainsKey(type);
        
    /// <summary>
    /// 获取子系统，如果不存在会返回null
    /// </summary>
    /// <typeparam name="T">子系统类</typeparam>
    /// <returns>子系统实例</returns>
    public T? TryGetSystem<T>() where T : SystemBase => Children.TryGetValue(typeof(T), out var system) ? (T)system : default;
        
    public T GetSystem<T>() where T : SystemBase => Children.TryGetValue(typeof(T), out var system) ? (T)system : throw new KeyNotFoundException();

    /// <summary>
    /// 获取所有子系统
    /// </summary>
    /// <returns>子系统数组</returns>
    public SystemBase[] GetSystems() => Children.Values.ToArray();

    /// <summary>
    /// 添加子系统
    /// </summary>
    /// <typeparam name="T">子系统类</typeparam>
    /// <returns>自身实例</returns>
    public SystemBase WithSystem<T>() where T : SystemBase, new() {
        AddSystem<T>();
        return this;
    }

    /// <summary>
    /// 如果存在就获取子系统，不存在就添加子系统
    /// </summary>
    /// <typeparam name="T">子系统类</typeparam>
    /// <returns>子系统实例</returns>
    public T OfferSystem<T>() where T : SystemBase, new() {
        return TryGetSystem<T>() ?? AddSystem<T>();
    }

    #endregion
}