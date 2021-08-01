using System;
using System.Threading.Tasks;

namespace Coorth {
    public readonly struct SystemSubscription<TEvent> where TEvent : IEvent {
        private readonly SystemBase system;
        
        internal SystemSubscription(SystemBase system) {
            this.system = system;
        }

        public void OnEvent(Action<TEvent> action) {
            var reaction = system.CreateReaction<TEvent>();
            reaction.OnEvent(action);
        }
        
        public void OnEvent(Func<TEvent, ValueTask> action, bool continueOnCapturedContext = true) {
            var reaction = system.CreateReaction<TEvent>();
            reaction.OnEvent(e => action(e).ConfigureAwait(continueOnCapturedContext));
        }

        public void OnMatch(EntityMatcher matcher, Action<TEvent, Entity> action) {
            var sandbox = system.Sandbox;
            var reaction = system.CreateReaction<TEvent>();
            if (matcher.Includes != null) {
                foreach (var typeId in matcher.Includes) {
                    var componentGroup = sandbox.GetComponentGroup(typeId);
                    reaction.Include(componentGroup.Type);
                }
            }
            if (matcher.Excludes != null) {
                foreach (var typeId in matcher.Excludes) {
                    var componentGroup = sandbox.GetComponentGroup(typeId);
                    reaction.Exclude(componentGroup.Type);
                }
            }

            reaction.OnEvent(e => {
                var entities = sandbox.GetEntities(matcher);
                foreach (Entity entity in entities) {
                    action(e, entity);
                }
            });
        }
        
        #region Event_Comp_1

        public void ForEach<TComponent>(Action<TEvent, TComponent> action) where TComponent: IComponent {
            var sandbox = system.Sandbox;
            var reaction = system.CreateReaction<TEvent>();
            reaction.Include<TComponent>();
            reaction.OnEvent(e => {
                ComponentCollection<TComponent> components = sandbox.GetComponents<TComponent>();
                components.ForEach(e, action);
            });
        }
        
        public void ForEach<TComponent>(Action<TEvent, Entity, TComponent> action) where TComponent : IComponent {
            var sandbox = system.Sandbox;
            var reaction = system.CreateReaction<TEvent>();
            reaction.Include<TComponent>();
            reaction.OnEvent(e => {
                ComponentCollection<TComponent> components = sandbox.GetComponents<TComponent>();
                components.ForEach(e, action);
            });
        }
        
        #endregion
        
        #region Event_Comp_2
        
        public void ForEach<TComponent1, TComponent2>(Action<TEvent, TComponent1, TComponent2> action) where TComponent1 : IComponent where TComponent2: IComponent {
            var sandbox = system.Sandbox;
            var reaction = system.CreateReaction<TEvent>();
            var matcher = Matcher.Include<TComponent1, TComponent2>();
            
            reaction.Include<TComponent1>();
            reaction.Include<TComponent2>();

            if (sandbox.GetBinding<TComponent1>().HasDependency<TComponent2>()) {
                reaction.OnEvent(e => {
                    ComponentCollection<TComponent1, TComponent2> components = sandbox.GetComponents<TComponent1, TComponent2>();
                    components.ForEach(e, action);
                });
            }
            else {
                reaction.OnEvent(e => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(e, action);
                });
            }
        }
        
        public void ForEach<TComponent1, TComponent2>(Action<TEvent, Entity, TComponent1, TComponent2> action) where TComponent1 : IComponent where TComponent2: IComponent {
            var sandbox = system.Sandbox;
            var reaction = system.CreateReaction<TEvent>();
            var matcher = Matcher.Include<TComponent1, TComponent2>();
            
            reaction.Include<TComponent1>();
            reaction.Include<TComponent2>();
            
            if (sandbox.GetBinding<TComponent1>().HasDependency<TComponent2>()) {
                reaction.OnEvent(e => {
                    ComponentCollection<TComponent1, TComponent2> components = sandbox.GetComponents<TComponent1, TComponent2>();
                    components.ForEach(e, action);
                });
            }
            else {
                reaction.OnEvent(e => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(e, action);
                });
            }
        }
        
        #endregion

        #region Event_Comp_3

        public void ForEach<TComponent1, TComponent2, TComponent3>(Action<TEvent, TComponent1, TComponent2, TComponent3> action) where TComponent1 : IComponent where TComponent2: IComponent where TComponent3: IComponent {
            var sandbox = system.Sandbox;
            var reaction = system.CreateReaction<TEvent>();
            var matcher = Matcher.Include<TComponent1, TComponent2, TComponent3>();
            
            reaction.Include<TComponent1>();
            reaction.Include<TComponent2>();
            reaction.Include<TComponent3>();

            if (sandbox.GetBinding<TComponent1>().HasDependency<TComponent2>() && sandbox.GetBinding<TComponent1>().HasDependency<TComponent3>()) {
                reaction.OnEvent(e => {
                    ComponentCollection<TComponent1, TComponent2, TComponent3> components = sandbox.GetComponents<TComponent1, TComponent2, TComponent3>();
                    components.ForEach(e, action);
                });
            }
            else {
                reaction.OnEvent(e => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(e, action);
                });
            }
        }
        
        public void ForEach<TComponent1, TComponent2, TComponent3>(Action<TEvent, Entity, TComponent1, TComponent2, TComponent3> action) where TComponent1 : class, IComponent where TComponent2: class, IComponent where TComponent3: IComponent {
            var sandbox = system.Sandbox;
            var reaction = system.CreateReaction<TEvent>();
            var matcher = Matcher.Include<TComponent1, TComponent2, TComponent3>();
            
            reaction.Include<TComponent1>();
            reaction.Include<TComponent2>();
            reaction.Include<TComponent3>();
            
            if (sandbox.GetBinding<TComponent1>().HasDependency<TComponent2>() && sandbox.GetBinding<TComponent1>().HasDependency<TComponent3>()) {
                reaction.OnEvent(e => {
                    ComponentCollection<TComponent1, TComponent2, TComponent3> components = sandbox.GetComponents<TComponent1, TComponent2, TComponent3>();
                    components.ForEach(e, action);
                });
            }
            else {
                reaction.OnEvent(e => {
                    EntityCollection entities = sandbox.GetEntities(matcher);
                    entities.ForEach(e, action);
                });
            }
        }

        #endregion
    }
}