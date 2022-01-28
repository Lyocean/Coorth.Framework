
using System;

namespace Coorth {
    public partial class SystemSubscription<TEvent> {

        #region Component1

        public void ForEach<T1>(Action<T1> action) where T1 : IComponent {
            _Include<T1>();
            OnEvent((in TEvent _) => {
                ComponentCollection<T1> components = Sandbox.GetComponents<T1>();
                components.ForEach(action);
            });
        }

        public void ForEach<T1>(Action<Entity, T1> action) where T1 : IComponent {
            _Include<T1>();
            OnEvent((in TEvent _) => {
                ComponentCollection<T1> components = Sandbox.GetComponents<T1>();
                components.ForEach(action);
            });
        }

        public void ForEach<T1>(Action<TEvent, T1> action) where T1 : IComponent {
            _Include<T1>();
            OnEvent((in TEvent e) => {
                ComponentCollection<T1> components = Sandbox.GetComponents<T1>();
                components.ForEach(in e, action);
            });
        }

        public void ForEach<T1>(Action<TEvent, Entity, T1> action) where T1 : IComponent {
            _Include<T1>();
            OnEvent((in TEvent e) => {
                ComponentCollection<T1> components = Sandbox.GetComponents<T1>();
                components.ForEach(in e, action);
            });
        }

        public void ForEach<T1>(EventAction<T1> action) where T1 : IComponent {
            _Include<T1>();
            OnEvent((in TEvent _) => {
                ComponentCollection<T1> components = Sandbox.GetComponents<T1>();
                components.ForEach(action);
            });
        }

        public void ForEach<T1>(EventActionR<T1> action) where T1 : IComponent {
            _Include<T1>();
            OnEvent((in TEvent _) => {
                ComponentCollection<T1> components = Sandbox.GetComponents<T1>();
                components.ForEach(action);
            });
        }

        public void ForEach<T1>(EventAction<Entity, T1> action) where T1 : IComponent {
            _Include<T1>();
            OnEvent((in TEvent _) => {
                ComponentCollection<T1> components = Sandbox.GetComponents<T1>();
                components.ForEach(action);
            });
        }

        public void ForEach<T1>(EventActionR<Entity, T1> action) where T1 : IComponent {
            _Include<T1>();
            OnEvent((in TEvent _) => {
                ComponentCollection<T1> components = Sandbox.GetComponents<T1>();
                components.ForEach(action);
            });
        }

        public void ForEach<T1>(EventAction<TEvent, T1> action) where T1 : IComponent {
            _Include<T1>();
            OnEvent((in TEvent e) => {
                ComponentCollection<T1> components = Sandbox.GetComponents<T1>();
                components.ForEach(in e, action);
            });
        }

        public void ForEach<T1>(EventActionR<TEvent, T1> action) where T1 : IComponent {
            _Include<T1>();
            OnEvent((in TEvent e) => {
                ComponentCollection<T1> components = Sandbox.GetComponents<T1>();
                components.ForEach(in e, action);
            });
        }

        public void ForEach<T1>(EventAction<TEvent, Entity, T1> action) where T1 : IComponent {
            _Include<T1>();
            OnEvent((in TEvent e) => {
                ComponentCollection<T1> components = Sandbox.GetComponents<T1>();
                components.ForEach(in e, action);
            });
        }

        public void ForEach<T1>(EventActionR<TEvent, Entity, T1> action) where T1 : IComponent {
            _Include<T1>();
            OnEvent((in TEvent e) => {
                ComponentCollection<T1> components = Sandbox.GetComponents<T1>();
                components.ForEach(in e, action);
            });
        }

        #endregion

        #region Component2

        public void ForEach<T1, T2>(Action<T1, T2> action) where T1 : IComponent where T2 : IComponent {
            _Include<T1>();
            _Include<T2>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2> components = Sandbox.GetComponents<T1, T2>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2>(Action<Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            _Include<T1>();
            _Include<T2>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2> components = Sandbox.GetComponents<T1, T2>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2>(Action<TEvent, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            _Include<T1>();
            _Include<T2>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2> components = Sandbox.GetComponents<T1, T2>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2>(Action<TEvent, Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            _Include<T1>();
            _Include<T2>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2> components = Sandbox.GetComponents<T1, T2>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2>(EventAction<T1, T2> action) where T1 : IComponent where T2 : IComponent {
            _Include<T1>();
            _Include<T2>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2> components = Sandbox.GetComponents<T1, T2>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2>(EventActionR<T1, T2> action) where T1 : IComponent where T2 : IComponent {
            _Include<T1>();
            _Include<T2>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2> components = Sandbox.GetComponents<T1, T2>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2>(EventActionR2<T1, T2> action) where T1 : IComponent where T2 : IComponent {
            _Include<T1>();
            _Include<T2>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2> components = Sandbox.GetComponents<T1, T2>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2>(EventAction<Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            _Include<T1>();
            _Include<T2>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2> components = Sandbox.GetComponents<T1, T2>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2>(EventActionR<Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            _Include<T1>();
            _Include<T2>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2> components = Sandbox.GetComponents<T1, T2>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2>(EventActionR2<Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            _Include<T1>();
            _Include<T2>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2> components = Sandbox.GetComponents<T1, T2>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2>(EventAction<TEvent, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            _Include<T1>();
            _Include<T2>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2> components = Sandbox.GetComponents<T1, T2>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2>(EventActionR<TEvent, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            _Include<T1>();
            _Include<T2>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2> components = Sandbox.GetComponents<T1, T2>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2>(EventActionR2<TEvent, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            _Include<T1>();
            _Include<T2>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2> components = Sandbox.GetComponents<T1, T2>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2>(EventAction<TEvent, Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            _Include<T1>();
            _Include<T2>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2> components = Sandbox.GetComponents<T1, T2>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2>(EventActionR<TEvent, Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            _Include<T1>();
            _Include<T2>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2> components = Sandbox.GetComponents<T1, T2>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2>(EventActionR2<TEvent, Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            _Include<T1>();
            _Include<T2>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2> components = Sandbox.GetComponents<T1, T2>();
                    components.ForEach(in e, action);
                });
            }
        }

        #endregion

        #region Component3

        public void ForEach<T1, T2, T3>(Action<T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(Action<Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(Action<TEvent, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(Action<TEvent, Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventAction<T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR<T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR2<T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR3<T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventAction<Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR<Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR2<Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR3<Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent _) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventAction<TEvent, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR<TEvent, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR2<TEvent, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR3<TEvent, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventAction<TEvent, Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR<TEvent, Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR2<TEvent, Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR3<TEvent, Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            _Include<T1>();
            _Include<T2>();
            _Include<T3>();
            if (Sandbox.GetBinding<T1>().HasDependency<T2>() && Sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                OnEvent((in TEvent e) => {
                    EntityCollection entities = Sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = Sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        #endregion
    }
}