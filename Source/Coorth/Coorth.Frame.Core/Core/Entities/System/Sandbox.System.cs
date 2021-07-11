using System;
using System.Collections.Generic;
using System.Linq;


namespace Coorth {
    public partial class Sandbox {

        public SystemRoot RootSystem { get; private set; }

        private readonly Dictionary<Type, SystemBase> systems = new Dictionary<Type, SystemBase>();

        public int SystemCount => systems.Count;
        
        private void InitSystems() {
            RootSystem = new SystemRoot();
            AddSystem(RootSystem);
        }
        
        internal void OnSystemAdd<T>(Type key, T system, SystemBase parent) where T : SystemBase {
            systems.Add(key, system);
            system.SystemAdd(this, key, RootSystem);
            parent.OnChildAdd<T>(key, system);
            
            _Execute(new EventSystemAdd(this, key, system));
            _Execute(new EventSystemAdd<T>(this, key, system));
        }

        internal bool OnSystemRemove<T>(Type key) where T : SystemBase {
            if (!systems.TryGetValue(key, out var system)) {
                return false;
            }
            system.SystemRemove();
            system.Parent?.OnChildRemove(system);
            systems.Remove(key);
            
            _Execute(new EventSystemRemove(this, key, system));
            _Execute(new EventSystemRemove<T>(this, key, system));
            return true;
        }

        public T AddSystem<T>(T system) where T : SystemBase {
            OnSystemAdd<T>(typeof(T), system, RootSystem);
            return system;
        }
        
        public T AddSystem<T>() where T : SystemBase, new() {
            var system = Activator.CreateInstance<T>();
            return AddSystem(system);
        }

        public void AddAction<T>(Action<T> action) where T : class, IEvent {
            var system = OfferSystem<ActionsSystem>();
            system.Add(action);
        }

        public void RemoveAction<T>(Action<T> action) where T : class, IEvent {
            var system = OfferSystem<ActionsSystem>();
            system.Remove(action);
        }

        public T OfferSystem<T>() where T : SystemBase, new() {
            return HasSystem<T>() ? GetSystem<T>() : AddSystem<T>();
        }

        public bool HasSystem<T>() where T : SystemBase {
            return HasSystem(typeof(T));
        }

        public bool HasSystem(Type type) {
            return systems.ContainsKey(type);
        }

        public T GetSystem<T>() where T : SystemBase {
            return systems.TryGetValue(typeof(T), out var system) ? (T)system : default;
        }

        public SystemBase[] GetSystems() {
            return systems.Values.ToArray();
        }

        public bool RemoveSystem(SystemBase system) {
            if (!systems.ContainsValue(system)) {
                return false;
            }
            system.SystemRemove();
            systems.Remove(system.GetType());
            return true;
        }

        public bool RemoveSystem<T>() where T : SystemBase {
            return OnSystemRemove<T>(typeof(T));
        }
    }
}