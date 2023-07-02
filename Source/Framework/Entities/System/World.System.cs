using System;
using System.Collections.Generic;
using System.Linq;

namespace Coorth.Framework;

public partial class World {
    
    public readonly SystemRoot RootSystem;

    private readonly Dictionary<Type, SystemBase> systems = new();

    public int SystemCount => systems.Count;

    private void SetupSystems(out SystemRoot root) {
        root = new SystemRoot();
        RootSystem.Setup(this);
        OnSystemAdd(typeof(SystemRoot), RootSystem);
        RootSystem.SetActive(false);
    }

    private void ClearSystems() {
        RootSystem.ClearChildren();
    }

    public void SetActive(bool active) {
        RootSystem.SetActive(active);
    }
    
    internal void OnSystemAdd(Type key, SystemBase system) {
        systems.Add(key, system);
        Dispatcher.Dispatch(new SystemAddEvent(this, key, system));
    }

    internal bool OnSystemRemove(Type key) {
        if (!systems.TryGetValue(key, out var system)) {
            return false;
        }
        Dispatcher.Dispatch(new SystemRemoveEvent(this, key, system));
        return systems.Remove(key);
    }

    public SystemBase AddSystem(Type type) => RootSystem.AddChild(type);

    public T AddSystem<T>(T system) where T : SystemBase => RootSystem.AddChild(system);

    public T AddSystem<T>() where T : SystemBase, new() => RootSystem.AddChild<T>();

    public T OfferSystem<T>() where T : SystemBase, new() => RootSystem.OfferChild<T>();

    public bool HasSystem<T>() where T : SystemBase => systems.ContainsKey(typeof(T));

    public bool HasSystem(Type type) => systems.ContainsKey(type);

    public T GetSystem<T>() where T : SystemBase => (T)systems[typeof(T)];

    public SystemBase GetSystem(Type type) => systems[type];

    public T? FindSystem<T>() where T : SystemBase => systems.TryGetValue(typeof(T), out var system) ? (T)system : null;

    public SystemBase? FindSystem(Type type) => systems.TryGetValue(type, out var system) ? system : null;

    public SystemBase[] GetSystems() => systems.Values.ToArray();

    public void ActiveSystem<T>(bool active) where T : SystemBase => GetSystem(typeof(T)).SetActive(active);

    public void ActiveSystem(Type type, bool active) => GetSystem(type).SetActive(active);

    public bool RemoveSystem(SystemBase system) => RemoveSystem(system.Key);

    public bool RemoveSystem<T>() where T : SystemBase => RemoveSystem(typeof(T));

    public bool RemoveSystem(Type key) {
        if (!systems.TryGetValue(key, out var system)) {
            return false;
        }
        return !ReferenceEquals(system, RootSystem) && system.Parent.RemoveChild(key);
    }
}