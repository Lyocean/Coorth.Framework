using System;
using System.Collections;
using System.Collections.Generic;

namespace Coorth {
    public readonly struct EntityCollection : IEnumerable<Entity> {
        private readonly ArchetypeGroup archetypeGroup;

        public EntityCollection(ArchetypeGroup value) {
            this.archetypeGroup = value;
        }

        public void ForEach<T>(Action<T> action) where T: IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup = sandbox.GetComponentGroup<T>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component = ref componentGroup.Ref(context.Get(componentGroup.Id));
                    action(component);
                }
            }
        }

        public void ForEach<T>(Action<Entity, T> action) where T: IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup = sandbox.GetComponentGroup<T>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component = ref componentGroup.Ref(context.Get(componentGroup.Id));
                    action(context.GetEntity(sandbox), component);
                }
            }
        }

        public void ForEach<TState, T>(TState state, Action<TState, T> action) where T: IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup = sandbox.GetComponentGroup<T>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component = ref componentGroup.Ref(context.Get(componentGroup.Id));
                    action(state, component);
                }
            }
        }

        public void ForEach<TState, T>(TState state, Action<TState, Entity, T> action) where T: IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup = sandbox.GetComponentGroup<T>();
            for(var i=0; i<archetypeGroup.Archetypes.Length; i++) {
                var archetype = archetypeGroup.Archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component = ref componentGroup.Ref(context.Get(componentGroup.Id));
                    action(state, context.GetEntity(sandbox), component);
                }
            }
        }
        public void ForEach<T1, T2>(Action<T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup1 = sandbox.GetComponentGroup<T1>();
            var componentGroup2 = sandbox.GetComponentGroup<T2>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                    ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                    action(component1, component2);
                }
            }
        }

        public void ForEach<T1, T2>(Action<Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup1 = sandbox.GetComponentGroup<T1>();
            var componentGroup2 = sandbox.GetComponentGroup<T2>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                    ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                    action(context.GetEntity(sandbox), component1, component2);
                }
            }
        }

        public void ForEach<TState, T1, T2>(TState state, Action<TState, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup1 = sandbox.GetComponentGroup<T1>();
            var componentGroup2 = sandbox.GetComponentGroup<T2>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                    ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                    action(state, component1, component2);
                }
            }
        }

        public void ForEach<TState, T1, T2>(TState state, Action<TState, Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup1 = sandbox.GetComponentGroup<T1>();
            var componentGroup2 = sandbox.GetComponentGroup<T2>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                    ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                    action(state, context.GetEntity(sandbox), component1, component2);
                }
            }
        }
        public void ForEach<T1, T2, T3>(Action<T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup1 = sandbox.GetComponentGroup<T1>();
            var componentGroup2 = sandbox.GetComponentGroup<T2>();
            var componentGroup3 = sandbox.GetComponentGroup<T3>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                    ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                    ref var component3 = ref componentGroup3.Ref(context.Get(componentGroup3.Id));
                    action(component1, component2, component3);
                }
            }
        }

        public void ForEach<T1, T2, T3>(Action<Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup1 = sandbox.GetComponentGroup<T1>();
            var componentGroup2 = sandbox.GetComponentGroup<T2>();
            var componentGroup3 = sandbox.GetComponentGroup<T3>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                    ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                    ref var component3 = ref componentGroup3.Ref(context.Get(componentGroup3.Id));
                    action(context.GetEntity(sandbox), component1, component2, component3);
                }
            }
        }

        public void ForEach<TState, T1, T2, T3>(TState state, Action<TState, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup1 = sandbox.GetComponentGroup<T1>();
            var componentGroup2 = sandbox.GetComponentGroup<T2>();
            var componentGroup3 = sandbox.GetComponentGroup<T3>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                    ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                    ref var component3 = ref componentGroup3.Ref(context.Get(componentGroup3.Id));
                    action(state, component1, component2, component3);
                }
            }
        }

        public void ForEach<TState, T1, T2, T3>(TState state, Action<TState, Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup1 = sandbox.GetComponentGroup<T1>();
            var componentGroup2 = sandbox.GetComponentGroup<T2>();
            var componentGroup3 = sandbox.GetComponentGroup<T3>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                    ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                    ref var component3 = ref componentGroup3.Ref(context.Get(componentGroup3.Id));
                    action(state, context.GetEntity(sandbox), component1, component2, component3);
                }
            }
        }
        public void ForEach<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup1 = sandbox.GetComponentGroup<T1>();
            var componentGroup2 = sandbox.GetComponentGroup<T2>();
            var componentGroup3 = sandbox.GetComponentGroup<T3>();
            var componentGroup4 = sandbox.GetComponentGroup<T4>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                    ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                    ref var component3 = ref componentGroup3.Ref(context.Get(componentGroup3.Id));
                    ref var component4 = ref componentGroup4.Ref(context.Get(componentGroup4.Id));
                    action(component1, component2, component3, component4);
                }
            }
        }

        public void ForEach<T1, T2, T3, T4>(Action<Entity, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup1 = sandbox.GetComponentGroup<T1>();
            var componentGroup2 = sandbox.GetComponentGroup<T2>();
            var componentGroup3 = sandbox.GetComponentGroup<T3>();
            var componentGroup4 = sandbox.GetComponentGroup<T4>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                    ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                    ref var component3 = ref componentGroup3.Ref(context.Get(componentGroup3.Id));
                    ref var component4 = ref componentGroup4.Ref(context.Get(componentGroup4.Id));
                    action(context.GetEntity(sandbox), component1, component2, component3, component4);
                }
            }
        }

        public void ForEach<TState, T1, T2, T3, T4>(TState state, Action<TState, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup1 = sandbox.GetComponentGroup<T1>();
            var componentGroup2 = sandbox.GetComponentGroup<T2>();
            var componentGroup3 = sandbox.GetComponentGroup<T3>();
            var componentGroup4 = sandbox.GetComponentGroup<T4>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                    ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                    ref var component3 = ref componentGroup3.Ref(context.Get(componentGroup3.Id));
                    ref var component4 = ref componentGroup4.Ref(context.Get(componentGroup4.Id));
                    action(state, component1, component2, component3, component4);
                }
            }
        }

        public void ForEach<TState, T1, T2, T3, T4>(TState state, Action<TState, Entity, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup1 = sandbox.GetComponentGroup<T1>();
            var componentGroup2 = sandbox.GetComponentGroup<T2>();
            var componentGroup3 = sandbox.GetComponentGroup<T3>();
            var componentGroup4 = sandbox.GetComponentGroup<T4>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                    ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                    ref var component3 = ref componentGroup3.Ref(context.Get(componentGroup3.Id));
                    ref var component4 = ref componentGroup4.Ref(context.Get(componentGroup4.Id));
                    action(state, context.GetEntity(sandbox), component1, component2, component3, component4);
                }
            }
        }
        public void ForEach<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup1 = sandbox.GetComponentGroup<T1>();
            var componentGroup2 = sandbox.GetComponentGroup<T2>();
            var componentGroup3 = sandbox.GetComponentGroup<T3>();
            var componentGroup4 = sandbox.GetComponentGroup<T4>();
            var componentGroup5 = sandbox.GetComponentGroup<T5>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                    ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                    ref var component3 = ref componentGroup3.Ref(context.Get(componentGroup3.Id));
                    ref var component4 = ref componentGroup4.Ref(context.Get(componentGroup4.Id));
                    ref var component5 = ref componentGroup5.Ref(context.Get(componentGroup5.Id));
                    action(component1, component2, component3, component4, component5);
                }
            }
        }

        public void ForEach<T1, T2, T3, T4, T5>(Action<Entity, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup1 = sandbox.GetComponentGroup<T1>();
            var componentGroup2 = sandbox.GetComponentGroup<T2>();
            var componentGroup3 = sandbox.GetComponentGroup<T3>();
            var componentGroup4 = sandbox.GetComponentGroup<T4>();
            var componentGroup5 = sandbox.GetComponentGroup<T5>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                    ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                    ref var component3 = ref componentGroup3.Ref(context.Get(componentGroup3.Id));
                    ref var component4 = ref componentGroup4.Ref(context.Get(componentGroup4.Id));
                    ref var component5 = ref componentGroup5.Ref(context.Get(componentGroup5.Id));
                    action(context.GetEntity(sandbox), component1, component2, component3, component4, component5);
                }
            }
        }

        public void ForEach<TState, T1, T2, T3, T4, T5>(TState state, Action<TState, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup1 = sandbox.GetComponentGroup<T1>();
            var componentGroup2 = sandbox.GetComponentGroup<T2>();
            var componentGroup3 = sandbox.GetComponentGroup<T3>();
            var componentGroup4 = sandbox.GetComponentGroup<T4>();
            var componentGroup5 = sandbox.GetComponentGroup<T5>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                    ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                    ref var component3 = ref componentGroup3.Ref(context.Get(componentGroup3.Id));
                    ref var component4 = ref componentGroup4.Ref(context.Get(componentGroup4.Id));
                    ref var component5 = ref componentGroup5.Ref(context.Get(componentGroup5.Id));
                    action(state, component1, component2, component3, component4, component5);
                }
            }
        }

        public void ForEach<TState, T1, T2, T3, T4, T5>(TState state, Action<TState, Entity, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
            var sandbox = archetypeGroup.Sandbox;
            var archetypes = archetypeGroup.Archetypes;
            var componentGroup1 = sandbox.GetComponentGroup<T1>();
            var componentGroup2 = sandbox.GetComponentGroup<T2>();
            var componentGroup3 = sandbox.GetComponentGroup<T3>();
            var componentGroup4 = sandbox.GetComponentGroup<T4>();
            var componentGroup5 = sandbox.GetComponentGroup<T5>();
            for(var i=0; i<archetypes.Length; i++) {
                var archetype = archetypes[i];
                for(var j=0; j<archetype.EntityCount; j++) {
                    ref var context = ref sandbox.GetContext(archetype.GetEntity(j));
                    ref var component1 = ref componentGroup1.Ref(context.Get(componentGroup1.Id));
                    ref var component2 = ref componentGroup2.Ref(context.Get(componentGroup2.Id));
                    ref var component3 = ref componentGroup3.Ref(context.Get(componentGroup3.Id));
                    ref var component4 = ref componentGroup4.Ref(context.Get(componentGroup4.Id));
                    ref var component5 = ref componentGroup5.Ref(context.Get(componentGroup5.Id));
                    action(state, context.GetEntity(sandbox), component1, component2, component3, component4, component5);
                }
            }
        }
        public struct Enumerator : IEnumerator<Entity> {
            private readonly Archetype[] archetypes;
            private int archetypeIndex;
            private int index;
            
            public Enumerator(ArchetypeGroup archetypeGroup){
                this.archetypes = archetypeGroup.Archetypes;
                this.archetypeIndex = 0;
                this.index = 0;
                this.Current = Entity.Null;
            }         

            public bool MoveNext() {
                if(archetypes.Length == 0){
                    return false;
                }
                while(archetypeIndex < archetypes.Length) {
                    var archetype = archetypes[archetypeIndex];
                    if(index < archetype.EntityCount) {
                        var entityIndex = archetype.GetEntity(index);
                        Current = archetype.Sandbox.GetEntity(entityIndex);
                        index ++;
                        return true;
                    }
                    archetypeIndex++;
                    index = 0;
                }
                return false;
            }

            public void Reset() {
                this.archetypeIndex = 0;
                this.index = 0;
                this.Current = Entity.Null;
            }

            public Entity Current { get; private set; }

            object IEnumerator.Current => Current;

            public void Dispose() {
                
            }
        }

        public Enumerator GetEnumerator() => new Enumerator(archetypeGroup);

        IEnumerator<Entity> IEnumerable<Entity>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}