using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Coorth.Tasks;

namespace Coorth.Framework; 

public sealed partial class Query {
    public readonly World World;
    public readonly Matcher Matcher;
    private readonly HashSet<Archetype> archetypes = new();
    public IReadOnlyCollection<Archetype> Archetypes => archetypes;

    public Query(World world, Matcher matcher) {
        World = world;
        Matcher = matcher;
    }

    internal void Match(Archetype archetype) {
        if (!Matcher.Match(archetype)) {
            return;
        }
        archetypes.Add(archetype);
    }

    public EntityCollection GetEntities(uint filter) {
        return new EntityCollection(World, archetypes, filter);
    }
    
    public void Execute<TState>(in TState state, ActionI2<TState, Entity> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) {
        GetEntities(filter).ForEach(in state, action);
    }

    public void Execute<TState>(in TState state, TaskExecutor executor, ActionI2<TState, Entity> action) {
        GetEntities(EntityFlags.ENTITY_ACTIVE_MASK).ForEach(in state, executor, action);
    }

}

public interface IQueryJob : ITaskJob {
    
}

#if NET7_0_OR_GREATER
[SkipLocalsInit]
#endif
public struct QueryJob<TState> : IQueryJob {

    public Archetype Archetype;
    public int Index;
    public int Count;
    public uint Filter;
    public TState State;
    public ActionI2<TState, Entity> Action;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute() {
        var world = Archetype.World;
        for (var i = 0; i < Count; i++) {
            ref var chunk = ref Archetype.GetChunk(Index + i);
            for (var j = 0; j < chunk.Count; j++) {
                var span = chunk.Get(j);
                var context = world.GetContext(span[0]);
                if((context.Flags & Filter) != Filter) {
                    continue;    
                }
                var entity = world.Cast(in context);
                Action(in State, in entity);
            }
        }
    }
}
