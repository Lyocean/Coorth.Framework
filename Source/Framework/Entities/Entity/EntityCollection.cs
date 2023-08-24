using System.Collections;
using System.Collections.Generic;
using Coorth.Tasks;

namespace Coorth.Framework; 

public readonly struct EntityCollection {
    private readonly World world;
    private readonly Dictionary<int, Archetype> archetypes;
    private readonly uint filter;

    public EntityCollection(World world, Dictionary<int, Archetype> archetypes, uint filter) {
        this.world = world;
        this.archetypes = archetypes;
        this.filter = filter;
    }
    
    public void ForEach<TState>(in TState state, ActionI2<TState, Entity> action) {
        foreach (var (_, archetype) in archetypes) {
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
        foreach (var (_, archetype) in archetypes) {
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

    public struct Enumerator : IEnumerator<Entity> {
        private readonly World world;
        private readonly Dictionary<int, Archetype> archetypes;
        private Dictionary<int, Archetype>.Enumerator enumerator;
        private int chunkIndex;
        private int valueIndex;
        private readonly uint filter;

        object IEnumerator.Current => Current;
        public Entity Current { get; private set; }

        public Enumerator(World w, Dictionary<int, Archetype> collection, uint f) {
            world = w;
            archetypes = collection;
            enumerator = archetypes.GetEnumerator();
            filter = f;
            chunkIndex = 0;
            valueIndex = 0;
        }

        public bool MoveNext() {
            var (_, archetype) = enumerator.Current;
            while (archetype == null) {
                if (!enumerator.MoveNext()) {
                    return false;
                }
                (_, archetype) = enumerator.Current;
            }

            while (archetype != null) {
                while (chunkIndex < archetype.ChunkCount) {
                    ref var chunk = ref archetype.GetChunk(chunkIndex);
                    while (valueIndex < chunk.Count) {
                        var span = chunk.Get(valueIndex);
                        ref var context = ref world.GetContext(span[0]);
                        valueIndex++;
                        if ((context.Flags & filter) != filter) {
                            continue;
                        }

                        var entity = world.Cast(in context);
                        Current = entity;
                        return true;
                    }

                    chunkIndex++;
                }
                if (!enumerator.MoveNext()) {
                    return false;
                }
                (_, archetype) = enumerator.Current;
            }
            return false;
        }
        
        public void Reset() {
            enumerator = archetypes.GetEnumerator();
            chunkIndex = 0;
            valueIndex = 0;
        }

        public void Dispose() {
            enumerator.Dispose();
        }
    }
}