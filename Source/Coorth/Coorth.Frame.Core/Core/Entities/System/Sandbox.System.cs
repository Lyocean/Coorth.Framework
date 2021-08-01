using System;
using System.Collections.Generic;
using System.Linq;

namespace Coorth {
    public partial class Sandbox {

        #region Fields

        public SystemRoot RootSystem { get; private set; }

        private readonly Dictionary<Type, SystemBase> systems = new Dictionary<Type, SystemBase>();

        public int SystemCount => systems.Count;
        
        private void InitSystems() {
            var binding = BindSystem<SystemRoot>();
            RootSystem = new SystemRoot();
            OnSystemAdd(typeof(SystemRoot), RootSystem, null, binding);
        }

        #endregion

        #region Binding

        private static readonly Dictionary<Type, SystemBinding> systemBindings = new Dictionary<Type, SystemBinding>();
        
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
        
        internal void OnSystemAdd<T>(Type key, T system, SystemBase parent, SystemBinding binding) where T : SystemBase {
            systems.Add(key, system);
            system.Setup(this, key, binding);
            system.SystemAdd(RootSystem);
            parent?.OnChildAdd<T>(key, system);
            
            _Execute(new EventSystemAdd(this, key, system));
            _Execute(new EventSystemAdd<T>(this, key, system));
            
            system.SystemActive();
        }

        internal bool OnSystemRemove<T>(Type key) where T : SystemBase {
            if (!systems.TryGetValue(key, out var system)) {
                return false;
            }
            system.SystemDeActive();
            
            system.SystemRemove();
            system.Parent?.OnChildRemove(system);
            systems.Remove(key);
            
            _Execute(new EventSystemRemove(this, key, system));
            _Execute(new EventSystemRemove<T>(this, key, system));
            return true;
        }
        
        internal static SystemBinding GetSystemBinding(Type type, bool reflection) {
            if (systemBindings.TryGetValue(type, out var binding)) {
                return binding;
            }
            if (reflection) {
                binding = (SystemBinding)Activator.CreateInstance(typeof(SystemBinding<>).MakeGenericType(type));
                systemBindings.Add(type, binding);
                return binding;
            }
            return null;
        }
        
        public SystemBase AddSystem(Type type, bool reflection = false) {
            var binding = GetSystemBinding(type, reflection);
            if (binding == null) {
                throw new NotBindException(type);
            }
            return binding.AddSystem(this, RootSystem);
        }
        
        public T AddSystem<T>(T system) where T : SystemBase {
            var binding = BindSystem<T>();
            OnSystemAdd(typeof(T), system, RootSystem, binding);
            return system;
        }
        
        public T AddSystem<T>() where T : SystemBase, new() {
            var system = Activator.CreateInstance<T>();
            return AddSystem(system);
        }

        public EventId AddAction<T>(Action<T> action) where T : class, IEvent {
            var system = OfferSystem<ActionsSystem>();
            return system.Add(action);
        }

        public bool RemoveAction(EventId id) {
            var system = OfferSystem<ActionsSystem>();
            return system.Remove(id);
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

        public SystemBase GetSystem(Type type) {
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
            if (active) {
                system.SystemActive();
            }
            else {
                system.SystemDeActive();
            }
        }

        
        public bool RemoveSystem(SystemBase system) {
            if (!systems.ContainsValue(system)) {
                return false;
            }

            RemoveSystem(system.Key);
            return true;
        }

        public bool RemoveSystem<T>() where T : SystemBase {
            return OnSystemRemove<T>(typeof(T));
        }
        
        public bool RemoveSystem(Type type) {
            var binding = GetSystemBinding(type, false);
            if (binding == null) {
                return false;
            }
            return binding.RemoveSystem(this);
        }

        #endregion
        
    }
}