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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute<T>(T e, TaskExecutor executor, Action<T, Entity> action) {
        var sandbox = archetypeGroup.Sandbox;
        foreach (var archetype in archetypeGroup.Archetypes) {
            executor.For((e, archetype, sandbox), 0, archetype.EntityCount,(state, i) => {
                var index = state.archetype.GetEntity(i);
                var entity = state.sandbox.GetEntity(index);
                action(state.e, entity);
            });
        }
    }

    public struct Enumerator : IEnumerator<Entity> {
        private readonly Sandbox sandbox; 
        private readonly ArchetypeDefinition[] archetypes;
        private int archetypeIndex;
        private int index;
            
        public Enumerator(ArchetypeGroup archetypeGroup){
            sandbox = archetypeGroup.Sandbox;
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
                    Current = sandbox.GetEntity(entityIndex);
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