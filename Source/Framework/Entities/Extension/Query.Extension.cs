
using System.Runtime.CompilerServices;
using Coorth.Tasks;

namespace Coorth.Framework;

public partial class World {

    public Query Query<T0>() where T0 : IComponent {
        var matcher = Matcher.All<T0>();
        return Query(matcher);
    }

    public Query Query<T0, T1>() where T0 : IComponent where T1 : IComponent {
        var matcher = Matcher.All<T0, T1>();
        return Query(matcher);
    }

    public Query Query<T0, T1, T2>() where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        var matcher = Matcher.All<T0, T1, T2>();
        return Query(matcher);
    }

    public Query Query<T0, T1, T2, T3>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var matcher = Matcher.All<T0, T1, T2, T3>();
        return Query(matcher);
    }

    public Query Query<T0, T1, T2, T3, T4>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var matcher = Matcher.All<T0, T1, T2, T3, T4>();
        return Query(matcher);
    }

    public Query Query<T0, T1, T2, T3, T4, T5>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var matcher = Matcher.All<T0, T1, T2, T3, T4, T5>();
        return Query(matcher);
    }

    public Query Query<T0, T1, T2, T3, T4, T5, T6>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        var matcher = Matcher.All<T0, T1, T2, T3, T4, T5, T6>();
        return Query(matcher);
    }

    public Query Query<T0, T1, T2, T3, T4, T5, T6, T7>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        var matcher = Matcher.All<T0, T1, T2, T3, T4, T5, T6, T7>();
        return Query(matcher);
    }

    public Query Query<T0, T1, T2, T3, T4, T5, T6, T7, T8>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        var matcher = Matcher.All<T0, T1, T2, T3, T4, T5, T6, T7, T8>();
        return Query(matcher);
    }

    public Query Query<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        var matcher = Matcher.All<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>();
        return Query(matcher);
    }

}

public partial class Query {


    #region Component1

    public void ForEach<TState, T0>(in TState state, ActionI2R1<TState, Entity, T0> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent {
        var world = World;
        var group0 = World.GetComponentGroup<T0>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                ref var chunk = ref archetype.GetChunk(i);
                for (var j = 0; j < chunk.Count; j++) {
                    var span = chunk.Get(j);
                    var context = world.GetContext(span[0]);
                    if((context.Flags & filter) != filter) {
                        continue;    
                    }
                    var entity = World.Cast(in context);
                    ref var component0 = ref group0.Get(span[offset0]);
                    action(in state, entity, ref component0);
                }
            }
        }
    }

    public void ForEach<TState, T0>(in TState state, TaskExecutor executor, ActionI2R1<TState, Entity, T0> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent {
        var group0 = World.GetComponentGroup<T0>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                var job = new QueryJob<TState, T0>() {
                    Archetype = archetype,
                    Index = i,
                    Count = 1,
                    State = state,
                    Filter = filter,
                    Action = action,
                };
                job.Group0 = group0;
                job.Offset0 = offset0;
                executor.Queue(job);
            }
        }
    }

    #endregion


    #region Component2

    public void ForEach<TState, T0, T1>(in TState state, ActionI2R2<TState, Entity, T0, T1> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent {
        var world = World;
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                ref var chunk = ref archetype.GetChunk(i);
                for (var j = 0; j < chunk.Count; j++) {
                    var span = chunk.Get(j);
                    var context = world.GetContext(span[0]);
                    if((context.Flags & filter) != filter) {
                        continue;    
                    }
                    var entity = World.Cast(in context);
                    ref var component0 = ref group0.Get(span[offset0]);
                    ref var component1 = ref group1.Get(span[offset1]);
                    action(in state, entity, ref component0, ref component1);
                }
            }
        }
    }

    public void ForEach<TState, T0, T1>(in TState state, TaskExecutor executor, ActionI2R2<TState, Entity, T0, T1> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent {
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                var job = new QueryJob<TState, T0, T1>() {
                    Archetype = archetype,
                    Index = i,
                    Count = 1,
                    State = state,
                    Filter = filter,
                    Action = action,
                };
                job.Group0 = group0;
                job.Offset0 = offset0;
                job.Group1 = group1;
                job.Offset1 = offset1;
                executor.Queue(job);
            }
        }
    }

    #endregion


    #region Component3

    public void ForEach<TState, T0, T1, T2>(in TState state, ActionI2R3<TState, Entity, T0, T1, T2> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        var world = World;
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        var group2 = World.GetComponentGroup<T2>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            var offset2 = archetype.Offset[group2.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                ref var chunk = ref archetype.GetChunk(i);
                for (var j = 0; j < chunk.Count; j++) {
                    var span = chunk.Get(j);
                    var context = world.GetContext(span[0]);
                    if((context.Flags & filter) != filter) {
                        continue;    
                    }
                    var entity = World.Cast(in context);
                    ref var component0 = ref group0.Get(span[offset0]);
                    ref var component1 = ref group1.Get(span[offset1]);
                    ref var component2 = ref group2.Get(span[offset2]);
                    action(in state, entity, ref component0, ref component1, ref component2);
                }
            }
        }
    }

    public void ForEach<TState, T0, T1, T2>(in TState state, TaskExecutor executor, ActionI2R3<TState, Entity, T0, T1, T2> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        var group2 = World.GetComponentGroup<T2>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            var offset2 = archetype.Offset[group2.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                var job = new QueryJob<TState, T0, T1, T2>() {
                    Archetype = archetype,
                    Index = i,
                    Count = 1,
                    State = state,
                    Filter = filter,
                    Action = action,
                };
                job.Group0 = group0;
                job.Offset0 = offset0;
                job.Group1 = group1;
                job.Offset1 = offset1;
                job.Group2 = group2;
                job.Offset2 = offset2;
                executor.Queue(job);
            }
        }
    }

    #endregion


    #region Component4

    public void ForEach<TState, T0, T1, T2, T3>(in TState state, ActionI2R4<TState, Entity, T0, T1, T2, T3> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = World;
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        var group2 = World.GetComponentGroup<T2>();
        var group3 = World.GetComponentGroup<T3>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            var offset2 = archetype.Offset[group2.Type];
            var offset3 = archetype.Offset[group3.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                ref var chunk = ref archetype.GetChunk(i);
                for (var j = 0; j < chunk.Count; j++) {
                    var span = chunk.Get(j);
                    var context = world.GetContext(span[0]);
                    if((context.Flags & filter) != filter) {
                        continue;    
                    }
                    var entity = World.Cast(in context);
                    ref var component0 = ref group0.Get(span[offset0]);
                    ref var component1 = ref group1.Get(span[offset1]);
                    ref var component2 = ref group2.Get(span[offset2]);
                    ref var component3 = ref group3.Get(span[offset3]);
                    action(in state, entity, ref component0, ref component1, ref component2, ref component3);
                }
            }
        }
    }

    public void ForEach<TState, T0, T1, T2, T3>(in TState state, TaskExecutor executor, ActionI2R4<TState, Entity, T0, T1, T2, T3> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        var group2 = World.GetComponentGroup<T2>();
        var group3 = World.GetComponentGroup<T3>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            var offset2 = archetype.Offset[group2.Type];
            var offset3 = archetype.Offset[group3.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                var job = new QueryJob<TState, T0, T1, T2, T3>() {
                    Archetype = archetype,
                    Index = i,
                    Count = 1,
                    State = state,
                    Filter = filter,
                    Action = action,
                };
                job.Group0 = group0;
                job.Offset0 = offset0;
                job.Group1 = group1;
                job.Offset1 = offset1;
                job.Group2 = group2;
                job.Offset2 = offset2;
                job.Group3 = group3;
                job.Offset3 = offset3;
                executor.Queue(job);
            }
        }
    }

    #endregion


    #region Component5

    public void ForEach<TState, T0, T1, T2, T3, T4>(in TState state, ActionI2R5<TState, Entity, T0, T1, T2, T3, T4> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = World;
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        var group2 = World.GetComponentGroup<T2>();
        var group3 = World.GetComponentGroup<T3>();
        var group4 = World.GetComponentGroup<T4>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            var offset2 = archetype.Offset[group2.Type];
            var offset3 = archetype.Offset[group3.Type];
            var offset4 = archetype.Offset[group4.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                ref var chunk = ref archetype.GetChunk(i);
                for (var j = 0; j < chunk.Count; j++) {
                    var span = chunk.Get(j);
                    var context = world.GetContext(span[0]);
                    if((context.Flags & filter) != filter) {
                        continue;    
                    }
                    var entity = World.Cast(in context);
                    ref var component0 = ref group0.Get(span[offset0]);
                    ref var component1 = ref group1.Get(span[offset1]);
                    ref var component2 = ref group2.Get(span[offset2]);
                    ref var component3 = ref group3.Get(span[offset3]);
                    ref var component4 = ref group4.Get(span[offset4]);
                    action(in state, entity, ref component0, ref component1, ref component2, ref component3, ref component4);
                }
            }
        }
    }

    public void ForEach<TState, T0, T1, T2, T3, T4>(in TState state, TaskExecutor executor, ActionI2R5<TState, Entity, T0, T1, T2, T3, T4> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        var group2 = World.GetComponentGroup<T2>();
        var group3 = World.GetComponentGroup<T3>();
        var group4 = World.GetComponentGroup<T4>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            var offset2 = archetype.Offset[group2.Type];
            var offset3 = archetype.Offset[group3.Type];
            var offset4 = archetype.Offset[group4.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                var job = new QueryJob<TState, T0, T1, T2, T3, T4>() {
                    Archetype = archetype,
                    Index = i,
                    Count = 1,
                    State = state,
                    Filter = filter,
                    Action = action,
                };
                job.Group0 = group0;
                job.Offset0 = offset0;
                job.Group1 = group1;
                job.Offset1 = offset1;
                job.Group2 = group2;
                job.Offset2 = offset2;
                job.Group3 = group3;
                job.Offset3 = offset3;
                job.Group4 = group4;
                job.Offset4 = offset4;
                executor.Queue(job);
            }
        }
    }

    #endregion


    #region Component6

    public void ForEach<TState, T0, T1, T2, T3, T4, T5>(in TState state, ActionI2R6<TState, Entity, T0, T1, T2, T3, T4, T5> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = World;
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        var group2 = World.GetComponentGroup<T2>();
        var group3 = World.GetComponentGroup<T3>();
        var group4 = World.GetComponentGroup<T4>();
        var group5 = World.GetComponentGroup<T5>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            var offset2 = archetype.Offset[group2.Type];
            var offset3 = archetype.Offset[group3.Type];
            var offset4 = archetype.Offset[group4.Type];
            var offset5 = archetype.Offset[group5.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                ref var chunk = ref archetype.GetChunk(i);
                for (var j = 0; j < chunk.Count; j++) {
                    var span = chunk.Get(j);
                    var context = world.GetContext(span[0]);
                    if((context.Flags & filter) != filter) {
                        continue;    
                    }
                    var entity = World.Cast(in context);
                    ref var component0 = ref group0.Get(span[offset0]);
                    ref var component1 = ref group1.Get(span[offset1]);
                    ref var component2 = ref group2.Get(span[offset2]);
                    ref var component3 = ref group3.Get(span[offset3]);
                    ref var component4 = ref group4.Get(span[offset4]);
                    ref var component5 = ref group5.Get(span[offset5]);
                    action(in state, entity, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5);
                }
            }
        }
    }

    public void ForEach<TState, T0, T1, T2, T3, T4, T5>(in TState state, TaskExecutor executor, ActionI2R6<TState, Entity, T0, T1, T2, T3, T4, T5> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        var group2 = World.GetComponentGroup<T2>();
        var group3 = World.GetComponentGroup<T3>();
        var group4 = World.GetComponentGroup<T4>();
        var group5 = World.GetComponentGroup<T5>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            var offset2 = archetype.Offset[group2.Type];
            var offset3 = archetype.Offset[group3.Type];
            var offset4 = archetype.Offset[group4.Type];
            var offset5 = archetype.Offset[group5.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                var job = new QueryJob<TState, T0, T1, T2, T3, T4, T5>() {
                    Archetype = archetype,
                    Index = i,
                    Count = 1,
                    State = state,
                    Filter = filter,
                    Action = action,
                };
                job.Group0 = group0;
                job.Offset0 = offset0;
                job.Group1 = group1;
                job.Offset1 = offset1;
                job.Group2 = group2;
                job.Offset2 = offset2;
                job.Group3 = group3;
                job.Offset3 = offset3;
                job.Group4 = group4;
                job.Offset4 = offset4;
                job.Group5 = group5;
                job.Offset5 = offset5;
                executor.Queue(job);
            }
        }
    }

    #endregion


    #region Component7

    public void ForEach<TState, T0, T1, T2, T3, T4, T5, T6>(in TState state, ActionI2R7<TState, Entity, T0, T1, T2, T3, T4, T5, T6> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        var world = World;
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        var group2 = World.GetComponentGroup<T2>();
        var group3 = World.GetComponentGroup<T3>();
        var group4 = World.GetComponentGroup<T4>();
        var group5 = World.GetComponentGroup<T5>();
        var group6 = World.GetComponentGroup<T6>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            var offset2 = archetype.Offset[group2.Type];
            var offset3 = archetype.Offset[group3.Type];
            var offset4 = archetype.Offset[group4.Type];
            var offset5 = archetype.Offset[group5.Type];
            var offset6 = archetype.Offset[group6.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                ref var chunk = ref archetype.GetChunk(i);
                for (var j = 0; j < chunk.Count; j++) {
                    var span = chunk.Get(j);
                    var context = world.GetContext(span[0]);
                    if((context.Flags & filter) != filter) {
                        continue;    
                    }
                    var entity = World.Cast(in context);
                    ref var component0 = ref group0.Get(span[offset0]);
                    ref var component1 = ref group1.Get(span[offset1]);
                    ref var component2 = ref group2.Get(span[offset2]);
                    ref var component3 = ref group3.Get(span[offset3]);
                    ref var component4 = ref group4.Get(span[offset4]);
                    ref var component5 = ref group5.Get(span[offset5]);
                    ref var component6 = ref group6.Get(span[offset6]);
                    action(in state, entity, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6);
                }
            }
        }
    }

    public void ForEach<TState, T0, T1, T2, T3, T4, T5, T6>(in TState state, TaskExecutor executor, ActionI2R7<TState, Entity, T0, T1, T2, T3, T4, T5, T6> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        var group2 = World.GetComponentGroup<T2>();
        var group3 = World.GetComponentGroup<T3>();
        var group4 = World.GetComponentGroup<T4>();
        var group5 = World.GetComponentGroup<T5>();
        var group6 = World.GetComponentGroup<T6>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            var offset2 = archetype.Offset[group2.Type];
            var offset3 = archetype.Offset[group3.Type];
            var offset4 = archetype.Offset[group4.Type];
            var offset5 = archetype.Offset[group5.Type];
            var offset6 = archetype.Offset[group6.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                var job = new QueryJob<TState, T0, T1, T2, T3, T4, T5, T6>() {
                    Archetype = archetype,
                    Index = i,
                    Count = 1,
                    State = state,
                    Filter = filter,
                    Action = action,
                };
                job.Group0 = group0;
                job.Offset0 = offset0;
                job.Group1 = group1;
                job.Offset1 = offset1;
                job.Group2 = group2;
                job.Offset2 = offset2;
                job.Group3 = group3;
                job.Offset3 = offset3;
                job.Group4 = group4;
                job.Offset4 = offset4;
                job.Group5 = group5;
                job.Offset5 = offset5;
                job.Group6 = group6;
                job.Offset6 = offset6;
                executor.Queue(job);
            }
        }
    }

    #endregion


    #region Component8

    public void ForEach<TState, T0, T1, T2, T3, T4, T5, T6, T7>(in TState state, ActionI2R8<TState, Entity, T0, T1, T2, T3, T4, T5, T6, T7> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        var world = World;
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        var group2 = World.GetComponentGroup<T2>();
        var group3 = World.GetComponentGroup<T3>();
        var group4 = World.GetComponentGroup<T4>();
        var group5 = World.GetComponentGroup<T5>();
        var group6 = World.GetComponentGroup<T6>();
        var group7 = World.GetComponentGroup<T7>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            var offset2 = archetype.Offset[group2.Type];
            var offset3 = archetype.Offset[group3.Type];
            var offset4 = archetype.Offset[group4.Type];
            var offset5 = archetype.Offset[group5.Type];
            var offset6 = archetype.Offset[group6.Type];
            var offset7 = archetype.Offset[group7.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                ref var chunk = ref archetype.GetChunk(i);
                for (var j = 0; j < chunk.Count; j++) {
                    var span = chunk.Get(j);
                    var context = world.GetContext(span[0]);
                    if((context.Flags & filter) != filter) {
                        continue;    
                    }
                    var entity = World.Cast(in context);
                    ref var component0 = ref group0.Get(span[offset0]);
                    ref var component1 = ref group1.Get(span[offset1]);
                    ref var component2 = ref group2.Get(span[offset2]);
                    ref var component3 = ref group3.Get(span[offset3]);
                    ref var component4 = ref group4.Get(span[offset4]);
                    ref var component5 = ref group5.Get(span[offset5]);
                    ref var component6 = ref group6.Get(span[offset6]);
                    ref var component7 = ref group7.Get(span[offset7]);
                    action(in state, entity, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7);
                }
            }
        }
    }

    public void ForEach<TState, T0, T1, T2, T3, T4, T5, T6, T7>(in TState state, TaskExecutor executor, ActionI2R8<TState, Entity, T0, T1, T2, T3, T4, T5, T6, T7> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        var group2 = World.GetComponentGroup<T2>();
        var group3 = World.GetComponentGroup<T3>();
        var group4 = World.GetComponentGroup<T4>();
        var group5 = World.GetComponentGroup<T5>();
        var group6 = World.GetComponentGroup<T6>();
        var group7 = World.GetComponentGroup<T7>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            var offset2 = archetype.Offset[group2.Type];
            var offset3 = archetype.Offset[group3.Type];
            var offset4 = archetype.Offset[group4.Type];
            var offset5 = archetype.Offset[group5.Type];
            var offset6 = archetype.Offset[group6.Type];
            var offset7 = archetype.Offset[group7.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                var job = new QueryJob<TState, T0, T1, T2, T3, T4, T5, T6, T7>() {
                    Archetype = archetype,
                    Index = i,
                    Count = 1,
                    State = state,
                    Filter = filter,
                    Action = action,
                };
                job.Group0 = group0;
                job.Offset0 = offset0;
                job.Group1 = group1;
                job.Offset1 = offset1;
                job.Group2 = group2;
                job.Offset2 = offset2;
                job.Group3 = group3;
                job.Offset3 = offset3;
                job.Group4 = group4;
                job.Offset4 = offset4;
                job.Group5 = group5;
                job.Offset5 = offset5;
                job.Group6 = group6;
                job.Offset6 = offset6;
                job.Group7 = group7;
                job.Offset7 = offset7;
                executor.Queue(job);
            }
        }
    }

    #endregion


    #region Component9

    public void ForEach<TState, T0, T1, T2, T3, T4, T5, T6, T7, T8>(in TState state, ActionI2R9<TState, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        var world = World;
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        var group2 = World.GetComponentGroup<T2>();
        var group3 = World.GetComponentGroup<T3>();
        var group4 = World.GetComponentGroup<T4>();
        var group5 = World.GetComponentGroup<T5>();
        var group6 = World.GetComponentGroup<T6>();
        var group7 = World.GetComponentGroup<T7>();
        var group8 = World.GetComponentGroup<T8>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            var offset2 = archetype.Offset[group2.Type];
            var offset3 = archetype.Offset[group3.Type];
            var offset4 = archetype.Offset[group4.Type];
            var offset5 = archetype.Offset[group5.Type];
            var offset6 = archetype.Offset[group6.Type];
            var offset7 = archetype.Offset[group7.Type];
            var offset8 = archetype.Offset[group8.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                ref var chunk = ref archetype.GetChunk(i);
                for (var j = 0; j < chunk.Count; j++) {
                    var span = chunk.Get(j);
                    var context = world.GetContext(span[0]);
                    if((context.Flags & filter) != filter) {
                        continue;    
                    }
                    var entity = World.Cast(in context);
                    ref var component0 = ref group0.Get(span[offset0]);
                    ref var component1 = ref group1.Get(span[offset1]);
                    ref var component2 = ref group2.Get(span[offset2]);
                    ref var component3 = ref group3.Get(span[offset3]);
                    ref var component4 = ref group4.Get(span[offset4]);
                    ref var component5 = ref group5.Get(span[offset5]);
                    ref var component6 = ref group6.Get(span[offset6]);
                    ref var component7 = ref group7.Get(span[offset7]);
                    ref var component8 = ref group8.Get(span[offset8]);
                    action(in state, entity, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8);
                }
            }
        }
    }

    public void ForEach<TState, T0, T1, T2, T3, T4, T5, T6, T7, T8>(in TState state, TaskExecutor executor, ActionI2R9<TState, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        var group2 = World.GetComponentGroup<T2>();
        var group3 = World.GetComponentGroup<T3>();
        var group4 = World.GetComponentGroup<T4>();
        var group5 = World.GetComponentGroup<T5>();
        var group6 = World.GetComponentGroup<T6>();
        var group7 = World.GetComponentGroup<T7>();
        var group8 = World.GetComponentGroup<T8>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            var offset2 = archetype.Offset[group2.Type];
            var offset3 = archetype.Offset[group3.Type];
            var offset4 = archetype.Offset[group4.Type];
            var offset5 = archetype.Offset[group5.Type];
            var offset6 = archetype.Offset[group6.Type];
            var offset7 = archetype.Offset[group7.Type];
            var offset8 = archetype.Offset[group8.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                var job = new QueryJob<TState, T0, T1, T2, T3, T4, T5, T6, T7, T8>() {
                    Archetype = archetype,
                    Index = i,
                    Count = 1,
                    State = state,
                    Filter = filter,
                    Action = action,
                };
                job.Group0 = group0;
                job.Offset0 = offset0;
                job.Group1 = group1;
                job.Offset1 = offset1;
                job.Group2 = group2;
                job.Offset2 = offset2;
                job.Group3 = group3;
                job.Offset3 = offset3;
                job.Group4 = group4;
                job.Offset4 = offset4;
                job.Group5 = group5;
                job.Offset5 = offset5;
                job.Group6 = group6;
                job.Offset6 = offset6;
                job.Group7 = group7;
                job.Offset7 = offset7;
                job.Group8 = group8;
                job.Offset8 = offset8;
                executor.Queue(job);
            }
        }
    }

    #endregion


    #region Component10

    public void ForEach<TState, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(in TState state, ActionI2R10<TState, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        var world = World;
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        var group2 = World.GetComponentGroup<T2>();
        var group3 = World.GetComponentGroup<T3>();
        var group4 = World.GetComponentGroup<T4>();
        var group5 = World.GetComponentGroup<T5>();
        var group6 = World.GetComponentGroup<T6>();
        var group7 = World.GetComponentGroup<T7>();
        var group8 = World.GetComponentGroup<T8>();
        var group9 = World.GetComponentGroup<T9>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            var offset2 = archetype.Offset[group2.Type];
            var offset3 = archetype.Offset[group3.Type];
            var offset4 = archetype.Offset[group4.Type];
            var offset5 = archetype.Offset[group5.Type];
            var offset6 = archetype.Offset[group6.Type];
            var offset7 = archetype.Offset[group7.Type];
            var offset8 = archetype.Offset[group8.Type];
            var offset9 = archetype.Offset[group9.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                ref var chunk = ref archetype.GetChunk(i);
                for (var j = 0; j < chunk.Count; j++) {
                    var span = chunk.Get(j);
                    var context = world.GetContext(span[0]);
                    if((context.Flags & filter) != filter) {
                        continue;    
                    }
                    var entity = World.Cast(in context);
                    ref var component0 = ref group0.Get(span[offset0]);
                    ref var component1 = ref group1.Get(span[offset1]);
                    ref var component2 = ref group2.Get(span[offset2]);
                    ref var component3 = ref group3.Get(span[offset3]);
                    ref var component4 = ref group4.Get(span[offset4]);
                    ref var component5 = ref group5.Get(span[offset5]);
                    ref var component6 = ref group6.Get(span[offset6]);
                    ref var component7 = ref group7.Get(span[offset7]);
                    ref var component8 = ref group8.Get(span[offset8]);
                    ref var component9 = ref group9.Get(span[offset9]);
                    action(in state, entity, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9);
                }
            }
        }
    }

    public void ForEach<TState, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(in TState state, TaskExecutor executor, ActionI2R10<TState, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action, uint filter = EntityFlags.ENTITY_ACTIVE_MASK) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        var group0 = World.GetComponentGroup<T0>();
        var group1 = World.GetComponentGroup<T1>();
        var group2 = World.GetComponentGroup<T2>();
        var group3 = World.GetComponentGroup<T3>();
        var group4 = World.GetComponentGroup<T4>();
        var group5 = World.GetComponentGroup<T5>();
        var group6 = World.GetComponentGroup<T6>();
        var group7 = World.GetComponentGroup<T7>();
        var group8 = World.GetComponentGroup<T8>();
        var group9 = World.GetComponentGroup<T9>();
        foreach (var archetype in archetypes) {
            var offset0 = archetype.Offset[group0.Type];
            var offset1 = archetype.Offset[group1.Type];
            var offset2 = archetype.Offset[group2.Type];
            var offset3 = archetype.Offset[group3.Type];
            var offset4 = archetype.Offset[group4.Type];
            var offset5 = archetype.Offset[group5.Type];
            var offset6 = archetype.Offset[group6.Type];
            var offset7 = archetype.Offset[group7.Type];
            var offset8 = archetype.Offset[group8.Type];
            var offset9 = archetype.Offset[group9.Type];
            for (var i = 0; i < archetype.ChunkCount; i++) {
                var job = new QueryJob<TState, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>() {
                    Archetype = archetype,
                    Index = i,
                    Count = 1,
                    State = state,
                    Filter = filter,
                    Action = action,
                };
                job.Group0 = group0;
                job.Offset0 = offset0;
                job.Group1 = group1;
                job.Offset1 = offset1;
                job.Group2 = group2;
                job.Offset2 = offset2;
                job.Group3 = group3;
                job.Offset3 = offset3;
                job.Group4 = group4;
                job.Offset4 = offset4;
                job.Group5 = group5;
                job.Offset5 = offset5;
                job.Group6 = group6;
                job.Offset6 = offset6;
                job.Group7 = group7;
                job.Offset7 = offset7;
                job.Group8 = group8;
                job.Offset8 = offset8;
                job.Group9 = group9;
                job.Offset9 = offset9;
                executor.Queue(job);
            }
        }
    }

    #endregion

}

#if NET7_0_OR_GREATER
[SkipLocalsInit]
#endif
public struct QueryJob<TState, T0> : IQueryJob where T0 : IComponent {
    public Archetype Archetype;
    public int Index;
    public int Count;
    public TState State;
    public uint Filter;
    public ActionI2R1<TState, Entity, T0> Action;
        
    public ComponentGroup<T0> Group0;
    public int Offset0;

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
                ref var component0 = ref Group0.Get(span[Offset0]);
                Action(in State, in entity, ref component0);
            }
        }
    }
}

#if NET7_0_OR_GREATER
[SkipLocalsInit]
#endif
public struct QueryJob<TState, T0, T1> : IQueryJob where T0 : IComponent where T1 : IComponent {
    public Archetype Archetype;
    public int Index;
    public int Count;
    public TState State;
    public uint Filter;
    public ActionI2R2<TState, Entity, T0, T1> Action;
        
    public ComponentGroup<T0> Group0;
    public ComponentGroup<T1> Group1;
    public int Offset0;
    public int Offset1;

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
                ref var component0 = ref Group0.Get(span[Offset0]);
                ref var component1 = ref Group1.Get(span[Offset1]);
                Action(in State, in entity, ref component0, ref component1);
            }
        }
    }
}

#if NET7_0_OR_GREATER
[SkipLocalsInit]
#endif
public struct QueryJob<TState, T0, T1, T2> : IQueryJob where T0 : IComponent where T1 : IComponent where T2 : IComponent {
    public Archetype Archetype;
    public int Index;
    public int Count;
    public TState State;
    public uint Filter;
    public ActionI2R3<TState, Entity, T0, T1, T2> Action;
        
    public ComponentGroup<T0> Group0;
    public ComponentGroup<T1> Group1;
    public ComponentGroup<T2> Group2;
    public int Offset0;
    public int Offset1;
    public int Offset2;

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
                ref var component0 = ref Group0.Get(span[Offset0]);
                ref var component1 = ref Group1.Get(span[Offset1]);
                ref var component2 = ref Group2.Get(span[Offset2]);
                Action(in State, in entity, ref component0, ref component1, ref component2);
            }
        }
    }
}

#if NET7_0_OR_GREATER
[SkipLocalsInit]
#endif
public struct QueryJob<TState, T0, T1, T2, T3> : IQueryJob where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
    public Archetype Archetype;
    public int Index;
    public int Count;
    public TState State;
    public uint Filter;
    public ActionI2R4<TState, Entity, T0, T1, T2, T3> Action;
        
    public ComponentGroup<T0> Group0;
    public ComponentGroup<T1> Group1;
    public ComponentGroup<T2> Group2;
    public ComponentGroup<T3> Group3;
    public int Offset0;
    public int Offset1;
    public int Offset2;
    public int Offset3;

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
                ref var component0 = ref Group0.Get(span[Offset0]);
                ref var component1 = ref Group1.Get(span[Offset1]);
                ref var component2 = ref Group2.Get(span[Offset2]);
                ref var component3 = ref Group3.Get(span[Offset3]);
                Action(in State, in entity, ref component0, ref component1, ref component2, ref component3);
            }
        }
    }
}

#if NET7_0_OR_GREATER
[SkipLocalsInit]
#endif
public struct QueryJob<TState, T0, T1, T2, T3, T4> : IQueryJob where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
    public Archetype Archetype;
    public int Index;
    public int Count;
    public TState State;
    public uint Filter;
    public ActionI2R5<TState, Entity, T0, T1, T2, T3, T4> Action;
        
    public ComponentGroup<T0> Group0;
    public ComponentGroup<T1> Group1;
    public ComponentGroup<T2> Group2;
    public ComponentGroup<T3> Group3;
    public ComponentGroup<T4> Group4;
    public int Offset0;
    public int Offset1;
    public int Offset2;
    public int Offset3;
    public int Offset4;

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
                ref var component0 = ref Group0.Get(span[Offset0]);
                ref var component1 = ref Group1.Get(span[Offset1]);
                ref var component2 = ref Group2.Get(span[Offset2]);
                ref var component3 = ref Group3.Get(span[Offset3]);
                ref var component4 = ref Group4.Get(span[Offset4]);
                Action(in State, in entity, ref component0, ref component1, ref component2, ref component3, ref component4);
            }
        }
    }
}

#if NET7_0_OR_GREATER
[SkipLocalsInit]
#endif
public struct QueryJob<TState, T0, T1, T2, T3, T4, T5> : IQueryJob where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
    public Archetype Archetype;
    public int Index;
    public int Count;
    public TState State;
    public uint Filter;
    public ActionI2R6<TState, Entity, T0, T1, T2, T3, T4, T5> Action;
        
    public ComponentGroup<T0> Group0;
    public ComponentGroup<T1> Group1;
    public ComponentGroup<T2> Group2;
    public ComponentGroup<T3> Group3;
    public ComponentGroup<T4> Group4;
    public ComponentGroup<T5> Group5;
    public int Offset0;
    public int Offset1;
    public int Offset2;
    public int Offset3;
    public int Offset4;
    public int Offset5;

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
                ref var component0 = ref Group0.Get(span[Offset0]);
                ref var component1 = ref Group1.Get(span[Offset1]);
                ref var component2 = ref Group2.Get(span[Offset2]);
                ref var component3 = ref Group3.Get(span[Offset3]);
                ref var component4 = ref Group4.Get(span[Offset4]);
                ref var component5 = ref Group5.Get(span[Offset5]);
                Action(in State, in entity, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5);
            }
        }
    }
}

#if NET7_0_OR_GREATER
[SkipLocalsInit]
#endif
public struct QueryJob<TState, T0, T1, T2, T3, T4, T5, T6> : IQueryJob where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
    public Archetype Archetype;
    public int Index;
    public int Count;
    public TState State;
    public uint Filter;
    public ActionI2R7<TState, Entity, T0, T1, T2, T3, T4, T5, T6> Action;
        
    public ComponentGroup<T0> Group0;
    public ComponentGroup<T1> Group1;
    public ComponentGroup<T2> Group2;
    public ComponentGroup<T3> Group3;
    public ComponentGroup<T4> Group4;
    public ComponentGroup<T5> Group5;
    public ComponentGroup<T6> Group6;
    public int Offset0;
    public int Offset1;
    public int Offset2;
    public int Offset3;
    public int Offset4;
    public int Offset5;
    public int Offset6;

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
                ref var component0 = ref Group0.Get(span[Offset0]);
                ref var component1 = ref Group1.Get(span[Offset1]);
                ref var component2 = ref Group2.Get(span[Offset2]);
                ref var component3 = ref Group3.Get(span[Offset3]);
                ref var component4 = ref Group4.Get(span[Offset4]);
                ref var component5 = ref Group5.Get(span[Offset5]);
                ref var component6 = ref Group6.Get(span[Offset6]);
                Action(in State, in entity, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6);
            }
        }
    }
}

#if NET7_0_OR_GREATER
[SkipLocalsInit]
#endif
public struct QueryJob<TState, T0, T1, T2, T3, T4, T5, T6, T7> : IQueryJob where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
    public Archetype Archetype;
    public int Index;
    public int Count;
    public TState State;
    public uint Filter;
    public ActionI2R8<TState, Entity, T0, T1, T2, T3, T4, T5, T6, T7> Action;
        
    public ComponentGroup<T0> Group0;
    public ComponentGroup<T1> Group1;
    public ComponentGroup<T2> Group2;
    public ComponentGroup<T3> Group3;
    public ComponentGroup<T4> Group4;
    public ComponentGroup<T5> Group5;
    public ComponentGroup<T6> Group6;
    public ComponentGroup<T7> Group7;
    public int Offset0;
    public int Offset1;
    public int Offset2;
    public int Offset3;
    public int Offset4;
    public int Offset5;
    public int Offset6;
    public int Offset7;

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
                ref var component0 = ref Group0.Get(span[Offset0]);
                ref var component1 = ref Group1.Get(span[Offset1]);
                ref var component2 = ref Group2.Get(span[Offset2]);
                ref var component3 = ref Group3.Get(span[Offset3]);
                ref var component4 = ref Group4.Get(span[Offset4]);
                ref var component5 = ref Group5.Get(span[Offset5]);
                ref var component6 = ref Group6.Get(span[Offset6]);
                ref var component7 = ref Group7.Get(span[Offset7]);
                Action(in State, in entity, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7);
            }
        }
    }
}

#if NET7_0_OR_GREATER
[SkipLocalsInit]
#endif
public struct QueryJob<TState, T0, T1, T2, T3, T4, T5, T6, T7, T8> : IQueryJob where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
    public Archetype Archetype;
    public int Index;
    public int Count;
    public TState State;
    public uint Filter;
    public ActionI2R9<TState, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> Action;
        
    public ComponentGroup<T0> Group0;
    public ComponentGroup<T1> Group1;
    public ComponentGroup<T2> Group2;
    public ComponentGroup<T3> Group3;
    public ComponentGroup<T4> Group4;
    public ComponentGroup<T5> Group5;
    public ComponentGroup<T6> Group6;
    public ComponentGroup<T7> Group7;
    public ComponentGroup<T8> Group8;
    public int Offset0;
    public int Offset1;
    public int Offset2;
    public int Offset3;
    public int Offset4;
    public int Offset5;
    public int Offset6;
    public int Offset7;
    public int Offset8;

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
                ref var component0 = ref Group0.Get(span[Offset0]);
                ref var component1 = ref Group1.Get(span[Offset1]);
                ref var component2 = ref Group2.Get(span[Offset2]);
                ref var component3 = ref Group3.Get(span[Offset3]);
                ref var component4 = ref Group4.Get(span[Offset4]);
                ref var component5 = ref Group5.Get(span[Offset5]);
                ref var component6 = ref Group6.Get(span[Offset6]);
                ref var component7 = ref Group7.Get(span[Offset7]);
                ref var component8 = ref Group8.Get(span[Offset8]);
                Action(in State, in entity, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8);
            }
        }
    }
}

#if NET7_0_OR_GREATER
[SkipLocalsInit]
#endif
public struct QueryJob<TState, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> : IQueryJob where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
    public Archetype Archetype;
    public int Index;
    public int Count;
    public TState State;
    public uint Filter;
    public ActionI2R10<TState, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> Action;
        
    public ComponentGroup<T0> Group0;
    public ComponentGroup<T1> Group1;
    public ComponentGroup<T2> Group2;
    public ComponentGroup<T3> Group3;
    public ComponentGroup<T4> Group4;
    public ComponentGroup<T5> Group5;
    public ComponentGroup<T6> Group6;
    public ComponentGroup<T7> Group7;
    public ComponentGroup<T8> Group8;
    public ComponentGroup<T9> Group9;
    public int Offset0;
    public int Offset1;
    public int Offset2;
    public int Offset3;
    public int Offset4;
    public int Offset5;
    public int Offset6;
    public int Offset7;
    public int Offset8;
    public int Offset9;

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
                ref var component0 = ref Group0.Get(span[Offset0]);
                ref var component1 = ref Group1.Get(span[Offset1]);
                ref var component2 = ref Group2.Get(span[Offset2]);
                ref var component3 = ref Group3.Get(span[Offset3]);
                ref var component4 = ref Group4.Get(span[Offset4]);
                ref var component5 = ref Group5.Get(span[Offset5]);
                ref var component6 = ref Group6.Get(span[Offset6]);
                ref var component7 = ref Group7.Get(span[Offset7]);
                ref var component8 = ref Group8.Get(span[Offset8]);
                ref var component9 = ref Group9.Get(span[Offset9]);
                Action(in State, in entity, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9);
            }
        }
    }
}

