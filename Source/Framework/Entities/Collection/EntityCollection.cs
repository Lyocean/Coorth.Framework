using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Coorth.Tasks;

namespace Coorth.Framework; 

public readonly partial struct EntityCollection : IEnumerable<Entity> {
    
    private readonly ArchetypeGroup archetypeGroup;

    public EntityCollection(ArchetypeGroup value) {
        archetypeGroup = value;
    }

    public void Execute<T>(T e, TaskExecutor executor, Action<T, Entity> action) {
        var world = archetypeGroup.World;
        foreach (var archetype in archetypeGroup.Archetypes) {
            executor.For((e, archetype, world), 0, archetype.EntityCount,(state, i) => {
                var index = state.archetype.GetEntity(i);
                var entity = state.world.GetEntity(index);
                action(state.e, entity);
            });
        }
    }

    public struct Enumerator : IEnumerator<Entity> {
        private readonly World world; 
        private readonly ArchetypeDefinition[] archetypes;
        private int archetypeIndex;
        private int index;
            
        public Enumerator(ArchetypeGroup archetypeGroup){
            world = archetypeGroup.World;
            archetypes = archetypeGroup.Archetypes;
            archetypeIndex = 0;
            index = 0;
            Current = Entity.Null;
        }         

        public bool MoveNext() {
            if(archetypes.Length == 0){
                return false;
            }
            while(archetypeIndex < archetypes.Length) {
                var archetype = archetypes[archetypeIndex];
                if(index < archetype.EntityCount) {
                    var entityIndex = archetype.GetEntity(index);
                    Current = world.GetEntity(entityIndex);
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