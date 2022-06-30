using System;
using System.Collections.Generic;
using System.Linq;

namespace Coorth.Framework; 

public partial class Sandbox {

    #region Fields

    public SystemRoot RootSystem { get; }

    private readonly Dictionary<Type, SystemBase> systems = new();

    public int SystemCount => systems.Count;
        
    private void InitSystems() {
        var binding = BindSystem<SystemRoot>();
        RootSystem.Setup(this, binding);
        OnSystemAdd(typeof(SystemRoot), RootSystem);
        RootSystem.SetActive(false);
    }

    private void ClearSystems() {
        var keys = systems.Keys.ToList();
        foreach (var key in keys) {
            RemoveSystem(key);
        }
        systems.Clear();
    }
        
    public void SetActive(bool active) {
        this.RootSystem.SetActive(active);
    }

    #endregion

    #region Binding

    private static readonly Dictionary<Type, SystemBinding> systemBindings = new();
        
    public SystemBinding<T> BindSystem<T>() where T : SystemBase {
        var type = typeof(T);
        if (systemBindings.TryGetValue(type, out var binding)) {
            return (SystemBinding<T>)binding;
        }
        binding = new SystemBinding<T>();
        systemBindings.Add(type, binding);
        return (SystemBinding<T>)binding;
    }

    #endregion
        
    #region Add & Get & Remove
        
    internal void OnSystemAdd<T>(Type key, T system) where T : SystemBase {
        systems.Add(key, system);
        Dispatch(new EventSystemAdd(this, key, system));
        Dispatch(new EventSystemAdd<T>(this, key, system));
    }

    internal bool OnSystemRemove<T>(Type key) where T : SystemBase {
        if (!systems.TryGetValue(key, out var system)) {
            return false;
        }
        Dispatch(new EventSystemRemove(this, key, system));
        Dispatch(new EventSystemRemove<T>(this, key, system));
        return systems.Remove(key);
    }
        
    internal static SystemBinding? GetSystemBinding(Type type, bool reflection) {
        if (systemBindings.TryGetValue(type, out var binding)) {
            return binding;
        }
        if (!reflection) {
            return null;
        }
        binding = Activator.CreateInstance(typeof(SystemBinding<>).MakeGenericType(type)) as SystemBinding;
        if (binding == null) {
            return null;
        }
        systemBindings.Add(type, binding);
        return binding;
    }
        
    public SystemBase AddSystem(Type type, bool reflection = false) {
        var binding = GetSystemBinding(type, reflection);
        if (binding == null) {
            throw new NotBindException(type);
        }
        return binding.AddSystem(RootSystem);
    }
        
    public T AddSystem<T>(T system) where T : SystemBase {
        BindSystem<T>();
        return RootSystem.AddSystem(system);
    }
        
    public T AddSystem<T>() where T : SystemBase, new() {
            
        var system = Activator.CreateInstance<T>();
        return RootSystem.AddSystem(system);
    }

    public Reaction<T> AddAction<T>(Action<T> action) where T : class, IEvent {
        var reaction = Dispatcher.Subscribe(action);
        return reaction;
    }

    public T OfferSystem<T>() where T : SystemBase, new() {
        return GetSystem<T>() ?? AddSystem<T>();
    }

    public bool HasSystem<T>() where T : SystemBase {
        return HasSystem(typeof(T));
    }

    public bool HasSystem(Type type) {
        return systems.ContainsKey(type);
    }

    public T GetSystem<T>() where T : SystemBase {
        return systems.TryGetValue(typeof(T), out var system) ? (T)system : throw new KeyNotFoundException(typeof(T).Name);
    }
        
    public T? TryGetSystem<T>() where T : SystemBase {
        return systems.TryGetValue(typeof(T), out var system) ? (T)system : throw new KeyNotFoundException(typeof(T).Name);
    }

    public SystemBase GetSystem(Type type) {
        return systems.TryGetValue(type, out var system) ? system : throw new KeyNotFoundException(type.Name);
    }
        
    public SystemBase? TryGetSystem(Type type) {
        return systems.TryGetValue(type, out var system) ? system : default;
    }
        
    public SystemBase[] GetSystems() {
        return systems.Values.ToArray();
    }

    public void ActiveSystem<T>(bool active) where T : SystemBase {
        ActiveSystem(typeof(T), active);
    }
        
    public void ActiveSystem(Type type, bool active) {
        var system = GetSystem(type);
        system?.SetActive(active);
    }
        
    public bool RemoveSystem(SystemBase system) {
        if (!systems.ContainsValue(system)) {
            return false;
        }
        RemoveSystem(system.Key);
        return true;
    }

    public bool RemoveSystem<T>() where T : SystemBase {
        var key = typeof(T);
        return RemoveSystem(key);
    }
        
    public bool RemoveSystem(Type key) {
        if (!systems.TryGetValue(key, out var system)) {
            return false;
        }
        if (ReferenceEquals(system, RootSystem)) {
            return false;
        }
        system.Parent?.RemoveSystem(key);
        return true;
    }

    public SystemScope BeginScope<T>() where T : SystemBase, new() {
        var system = AddSystem<T>();
        return new SystemScope(system);
    }

    #endregion
        
}