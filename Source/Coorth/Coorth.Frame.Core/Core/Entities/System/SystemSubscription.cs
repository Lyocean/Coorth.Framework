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
   
            var archetypeGroup = sandbox.GetArchetypeGroup(matcher);
            reaction.OnEvent(e => {
                foreach (Archetype archetype in archetypeGroup.Archetypes) {
                    for (var i = 0; i < archetype.EntityCount; i++) {
                        var index = archetype.GetEntity(i);
                        var entity = sandbox.GetEntity(index);
                        action(e, entity);
                    }
                }
            });
        }
        
        #region Event_Comp_1

        public void ForEach<TComponent>(Action<TEvent, TComponent> action) where TComponent: IComponent {
            var sandbox = system.Sandbox;
            var reaction = system.CreateReaction<TEvent>();
            reaction.Include<TComponent>();
            reaction.OnEvent(e => {
                var componentGroup = sandbox.GetComponentGroup<TComponent>();
                for (var i = 0; i < componentGroup.Count; i++) {
                    ref var component = ref componentGroup.components[i];
                    action(e, component);
                }
            });
        }
        
        public void ForEach<TComponent>(Action<TEvent, Entity, TComponent> action) where TComponent : IComponent {
            var sandbox = system.Sandbox;
            var reaction = system.CreateReaction<TEvent>();
            reaction.Include<TComponent>();
            reaction.OnEvent(e => {
                var componentGroup = sandbox.GetComponentGroup<TComponent>();
                for (var i = 0; i < componentGroup.Count; i++) {
                    ref var component = ref componentGroup.components[i];
                    var entity = sandbox.GetEntity(componentGroup.mapping[i]);
                    action(e, entity, component);
                }
            });
        }
        
        #endregion
        
        #region Event_Comp_2
        
        public void ForEach<TComponent1, TComponent2>(Action<TEvent, TComponent1, TComponent2> action) where TComponent1 : IComponent where TComponent2: IComponent {
            var sandbox = system.Sandbox;
            var reaction = system.CreateReaction<TEvent>();
            var matcher = Matcher.Include<TComponent1, TComponent2>();
            var archetypeGroup = sandbox.GetArchetypeGroup(matcher);
            
            reaction.Include<TComponent1>();
            reaction.Include<TComponent2>();

            reaction.OnEvent(e => {
                var componentGroup1 = sandbox.GetComponentGroup<TComponent1>();
                var componentGroup2 = sandbox.GetComponentGroup<TComponent2>();
                foreach (Archetype archetype in archetypeGroup.Archetypes) {
                    for (var i = 0; i < archetype.EntityCount; i++) {
                        ref var context = ref sandbox.GetContext(archetype.GetEntity(i));
                        ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                        ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                        
                        action(e, component1, component2);
                    }
                }
            });
        }
        
        public void ForEach<TComponent1, TComponent2>(Action<TEvent, Entity, TComponent1, TComponent2> action) where TComponent1 : IComponent where TComponent2: IComponent {
            var sandbox = system.Sandbox;
            var reaction = system.CreateReaction<TEvent>();
            var matcher = Matcher.Include<TComponent1, TComponent2>();
            var archetypeGroup = sandbox.GetArchetypeGroup(matcher);
            
            reaction.Include<TComponent1>();
            reaction.Include<TComponent2>();
            
            reaction.OnEvent(e => {
                var componentGroup1 = sandbox.GetComponentGroup<TComponent1>();
                var componentGroup2 = sandbox.GetComponentGroup<TComponent2>();
                foreach (Archetype archetype in archetypeGroup.Archetypes) {
                    for (var i = 0; i < archetype.EntityCount; i++) {
                        var entityIndex = archetype.GetEntity(i);
                        ref var context = ref sandbox.GetContext(entityIndex);
                        ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                        ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                        
                        action(e, context.GetEntity(sandbox), component1, component2);
                    }
                }
            });
        }
        
        #endregion

        #region Event_Comp_3

        public void ForEach<TComponent1, TComponent2, TComponent3>(Action<TEvent, TComponent1, TComponent2, TComponent3> action) where TComponent1 : IComponent where TComponent2: IComponent where TComponent3: IComponent {
            var sandbox = system.Sandbox;
            var reaction = system.CreateReaction<TEvent>();
            var matcher = Matcher.Include<TComponent1, TComponent2, TComponent3>();
            var archetypeGroup = sandbox.GetArchetypeGroup(matcher);
            
            reaction.Include<TComponent1>();
            reaction.Include<TComponent2>();
            reaction.Include<TComponent3>();

            reaction.OnEvent(e => {
                var componentGroup1 = sandbox.GetComponentGroup<TComponent1>();
                var componentGroup2 = sandbox.GetComponentGroup<TComponent2>();
                var componentGroup3 = sandbox.GetComponentGroup<TComponent3>();

                foreach (Archetype archetype in archetypeGroup.Archetypes) {

                    for (var i = 0; i < archetype.EntityCount; i++) {
                        ref var context = ref sandbox.GetContext(archetype.GetEntity(i));
                        ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                        ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                        ref var component3 = ref componentGroup3.Ref(context.Get(componentGroup3.Id));

                        action(e, component1, component2, component3);
                    }
                }
            });
        }
        
        public void ForEach<TComponent1, TComponent2, TComponent3>(Action<TEvent, Entity, TComponent1, TComponent2, TComponent3> action) where TComponent1 : class, IComponent where TComponent2: class, IComponent where TComponent3: IComponent {
            var sandbox = system.Sandbox;
            var reaction = system.CreateReaction<TEvent>();
            var matcher = Matcher.Include<TComponent1, TComponent2, TComponent3>();
            var archetypeGroup = sandbox.GetArchetypeGroup(matcher);
            
            reaction.Include<TComponent1>();
            reaction.Include<TComponent2>();
            reaction.Include<TComponent3>();
            
            reaction.OnEvent(e => {
                var componentGroup1 = sandbox.GetComponentGroup<TComponent1>();
                var componentGroup2 = sandbox.GetComponentGroup<TComponent2>();
                var componentGroup3 = sandbox.GetComponentGroup<TComponent3>();

                foreach (Archetype archetype in archetypeGroup.Archetypes) {

                    for (var i = 0; i < archetype.EntityCount; i++) {
                        ref var context = ref sandbox.GetContext(archetype.GetEntity(i));
                        ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                        ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                        ref var component3 = ref componentGroup3.Ref(context.Get(componentGroup3.Id));

                        action(e, context.GetEntity(sandbox), component1, component2, component3);
                    }
                }
            });
        }

        #endregion
    }
}