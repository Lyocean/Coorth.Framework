using System.Collections;
using System.Collections.Generic;
using Coorth.Tasks;

namespace Coorth.Framework; 

public readonly struct EntityCollection {
    private readonly World world;
    private readonly IEnumerable<Archetype> archetypes;
    private readonly uint filter;

    public EntityCollection(World world, IEnumerable<Archetype> archetypes, uint filter) {
        this.world = world;
        this.archetypes = archetypes;
        this.filter = filter;
    }
    
    public void ForEach<TState>(in TState state, ActionI2<TState, Entity> action) {
        foreach (var archetype in archetypes) {
            for (var i = 0; i < archetype.ChunkCount; i++) {
                ref var chunk = ref archetype.GetChunk(i);
                for (var j = 0; j < chunk.Count; j++) {
                    var span = chunk.Get(j);
                    ref var context = ref world.GetContext(span[0]);
                    if((context.Flags & filter) != filter) {
                        continue;
                    }
                    var entity = world.Cast(in context);
                    action(in state, in entity);
                }
            }
        }
    }
    
    public void ForEach<TState>(in TState state, TaskExecutor executor, ActionI2<TState, Entity> action) {
        foreach (var archetype in archetypes) {
            for (var i = 0; i < archetype.ChunkCount; i++) {
                var job = new QueryJob<TState>() {
                    Archetype = archetype,
                    Index = i,
                    Count = 1,
                    State = state,
                    Filter = filter,
                    Action = action,
                };
                executor.Queue(job);
            }
        }
    }
    
    public Enumerator GetEnumerator() => new(world, archetypes, filter);

    public struct Enumerator {
        private readonly World world;
        private readonly IEnumerable<Archetype> archetypes;
        private readonly uint filter;

        public Entity Current { get; private set; }

        public Enumerator(World world, IEnumerable<Archetype> archetypes, uint filter) {
            this.world = world;
            this.archetypes = archetypes;
            this.filter = filter;
        }

        public bool MoveNext() {

            return false;
        }

        
        public void Reset() { }
        
        public void Dispose() { }
    }
}