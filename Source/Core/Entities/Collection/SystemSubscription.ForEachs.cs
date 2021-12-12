
using System;

namespace Coorth {
    public readonly partial struct SystemSubscription<TEvent> where TEvent : IEvent {

        #region Component1

        public void ForEach<T1>(Action<T1> action) where T1 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.OnEvent((in TEvent _) => {
                ComponentCollection<T1> components = sandbox.GetComponents<T1>();
                components.ForEach(action);
            });
        }

        public void ForEach<T1>(Action<Entity, T1> action) where T1 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.OnEvent((in TEvent _) => {
                ComponentCollection<T1> components = sandbox.GetComponents<T1>();
                components.ForEach(action);
            });
        }

        public void ForEach<T1>(Action<TEvent, T1> action) where T1 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.OnEvent((in TEvent e) => {
                ComponentCollection<T1> components = sandbox.GetComponents<T1>();
                components.ForEach(in e, action);
            });
        }

        public void ForEach<T1>(Action<TEvent, Entity, T1> action) where T1 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.OnEvent((in TEvent e) => {
                ComponentCollection<T1> components = sandbox.GetComponents<T1>();
                components.ForEach(in e, action);
            });
        }

        public void ForEach<T1>(EventAction<T1> action) where T1 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.OnEvent((in TEvent _) => {
                ComponentCollection<T1> components = sandbox.GetComponents<T1>();
                components.ForEach(action);
            });
        }

        public void ForEach<T1>(EventActionR<T1> action) where T1 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.OnEvent((in TEvent _) => {
                ComponentCollection<T1> components = sandbox.GetComponents<T1>();
                components.ForEach(action);
            });
        }

        public void ForEach<T1>(EventAction<Entity, T1> action) where T1 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.OnEvent((in TEvent _) => {
                ComponentCollection<T1> components = sandbox.GetComponents<T1>();
                components.ForEach(action);
            });
        }

        public void ForEach<T1>(EventActionR<Entity, T1> action) where T1 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.OnEvent((in TEvent _) => {
                ComponentCollection<T1> components = sandbox.GetComponents<T1>();
                components.ForEach(action);
            });
        }

        public void ForEach<T1>(EventAction<TEvent, T1> action) where T1 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.OnEvent((in TEvent e) => {
                ComponentCollection<T1> components = sandbox.GetComponents<T1>();
                components.ForEach(in e, action);
            });
        }

        public void ForEach<T1>(EventActionR<TEvent, T1> action) where T1 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.OnEvent((in TEvent e) => {
                ComponentCollection<T1> components = sandbox.GetComponents<T1>();
                components.ForEach(in e, action);
            });
        }

        public void ForEach<T1>(EventAction<TEvent, Entity, T1> action) where T1 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.OnEvent((in TEvent e) => {
                ComponentCollection<T1> components = sandbox.GetComponents<T1>();
                components.ForEach(in e, action);
            });
        }

        public void ForEach<T1>(EventActionR<TEvent, Entity, T1> action) where T1 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.OnEvent((in TEvent e) => {
                ComponentCollection<T1> components = sandbox.GetComponents<T1>();
                components.ForEach(in e, action);
            });
        }

        #endregion

        #region Component2

        public void ForEach<T1, T2>(Action<T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2> components = sandbox.GetComponents<T1, T2>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2>(Action<Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2> components = sandbox.GetComponents<T1, T2>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2>(Action<TEvent, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2> components = sandbox.GetComponents<T1, T2>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2>(Action<TEvent, Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2> components = sandbox.GetComponents<T1, T2>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2>(EventAction<T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2> components = sandbox.GetComponents<T1, T2>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2>(EventActionR<T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2> components = sandbox.GetComponents<T1, T2>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2>(EventActionR2<T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2> components = sandbox.GetComponents<T1, T2>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2>(EventAction<Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2> components = sandbox.GetComponents<T1, T2>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2>(EventActionR<Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2> components = sandbox.GetComponents<T1, T2>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2>(EventActionR2<Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2> components = sandbox.GetComponents<T1, T2>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2>(EventAction<TEvent, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2> components = sandbox.GetComponents<T1, T2>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2>(EventActionR<TEvent, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2> components = sandbox.GetComponents<T1, T2>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2>(EventActionR2<TEvent, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2> components = sandbox.GetComponents<T1, T2>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2>(EventAction<TEvent, Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2> components = sandbox.GetComponents<T1, T2>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2>(EventActionR<TEvent, Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2> components = sandbox.GetComponents<T1, T2>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2>(EventActionR2<TEvent, Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>()) {
                var matcher = Matcher.Include<T1, T2>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2> components = sandbox.GetComponents<T1, T2>();
                    components.ForEach(in e, action);
                });
            }
        }

        #endregion

        #region Component3

        public void ForEach<T1, T2, T3>(Action<T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(Action<Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(Action<TEvent, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(Action<TEvent, Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventAction<T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR<T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR2<T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR3<T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventAction<Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR<Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR2<Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR3<Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent _) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(action);
                });
            } else {
                reaction.OnEvent((in TEvent _) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventAction<TEvent, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR<TEvent, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR2<TEvent, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR3<TEvent, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventAction<TEvent, Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR<TEvent, Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR2<TEvent, Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        public void ForEach<T1, T2, T3>(EventActionR3<TEvent, Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var reaction = system.CreateReaction<TEvent>();
            var sandbox = system.Sandbox;
            reaction.Include<T1>();
            reaction.Include<T2>();
            reaction.Include<T3>();
            if (sandbox.GetBinding<T1>().HasDependency<T2>() && sandbox.GetBinding<T1>().HasDependency<T3>()) {
                var matcher = Matcher.Include<T1, T2, T3>();
                reaction.OnEvent((in TEvent e) => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(in e, action);
                });
            } else {
                reaction.OnEvent((in TEvent e) => {
                    ComponentCollection<T1, T2, T3> components = sandbox.GetComponents<T1, T2, T3>();
                    components.ForEach(in e, action);
                });
            }
        }

        #endregion
    }
}