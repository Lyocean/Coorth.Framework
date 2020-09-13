using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Coorth.ECS {
    public sealed class Systems : ISupportInitialize {

        public readonly EcsContainer Container;

        public Dictionary<Type, IEcsSystem> systems = new Dictionary<Type, IEcsSystem>();

        public Dictionary<Type, Dictionary<Type, IEcsSystem>> events = new Dictionary<Type, Dictionary<Type, IEcsSystem>>();

        private bool isInitial = false;

        public Systems(EcsContainer container) {
            this.Container = container;
        }

        public void BeginInit() { }

        public void EndInit() {
            isInitial = true;
        }

        public void Destroy() { }

        public T Add<T>() where T : IEcsSystem {
            var system = Activator.CreateInstance<T>();
            systems.Add(typeof(T), system);
            OnSystemAdd(system);
            return system;
        }

        public T Add<T>(T system) where T : IEcsSystem {
            systems.Add(typeof(T), system);
            OnSystemAdd(system);
            return system;
        }

        public T Get<T>() where T : IEcsSystem {
            return systems.TryGetValue(typeof(T), out var system) ? (T)system : default;
        }

        public bool Has<T>() where T : IEcsSystem {
            return systems.ContainsKey(typeof(T));
        }

        public bool Remove<T>() where T : IEcsSystem {
            OnSystemRemove<T>();
            return systems.Remove(typeof(T));
        }

        public IEnumerable<IEcsSystem> GetEventSystems<T>() where T: IEvent {
            if(events.TryGetValue(typeof(T), out var eventSystems)) {
                foreach(var (type, eventSystem) in eventSystems) {
                    yield return eventSystem;
                }
            }
        }

        private void OnSystemAdd<T>(T system) where T: IEcsSystem {
            var systemType = typeof(T);
            foreach (var type in systemType.GetInterfaces()) {
                if (type.IsGenericType && typeof(IEventSystem).IsAssignableFrom(type)) {
                    var eventTypes = type.GetGenericArguments();
                    if (eventTypes.Length == 0) {
                        continue;
                    }
                    var eventType = eventTypes[0];
                    Dictionary<Type, IEcsSystem> eventSystems;
                    if (!events.TryGetValue(eventType, out eventSystems)) {
                        eventSystems = new Dictionary<Type, IEcsSystem>();
                        events[eventType] = eventSystems;
                    }
                    eventSystems.Add(systemType, system);
                }
            }
            if(system is ISystemAdd add) {
                add.OnAdd();
            }
        }

        private void OnSystemRemove<T>() where T : IEcsSystem {
            var systemType = typeof(T);
            foreach (var type in systemType.GetInterfaces()) {
                if (type.IsGenericType && typeof(IEventSystem).IsAssignableFrom(type)) {
                    var eventTypes = type.GetGenericArguments();
                    if (eventTypes.Length == 0) {
                        continue;
                    }
                    var eventType = eventTypes[0];
                    if (events.TryGetValue(eventType, out var eventSystems)) {
                        eventSystems.Remove(systemType);
                    }
                }
            }
            var system = systems[typeof(T)];
            if (system is ISystemRemove remove) {
                remove.OnRemove();
            }
        }
    }
}
