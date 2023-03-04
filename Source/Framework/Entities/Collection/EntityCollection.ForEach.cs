using System;

namespace Coorth.Framework;

public readonly partial struct EntityCollection {

    #region Component1

    public void ForEachComps<T>(EventAction<T> action) {
        var world = archetypeGroup.World;
    }

    public void ForEach<T1>(Action<T1> action) where T1 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                action(component1);
            }
        }
    }

    public void ForEach<T1>(Action<Entity, T1> action) where T1 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                action(context.GetEntity(world), component1);
            }
        }
    }

    public void ForEach<TEvent, T1>(in TEvent e, Action<TEvent, T1> action) where T1 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                action(e, component1);
            }
        }
    }

    public void ForEach<TEvent, T1>(in TEvent e, Action<TEvent, Entity, T1> action) where T1 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                action(e, context.GetEntity(world), component1);
            }
        }
    }

    public void ForEach<T1>(EventAction<T1> action) where T1 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                action(in component1);
            }
        }
    }

    public void ForEach<T1>(EventActionR<T1> action) where T1 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                action(ref component1);
            }
        }
    }

    public void ForEach<T1>(EventAction<Entity, T1> action) where T1 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                action(context.GetEntity(world), in component1);
            }
        }
    }

    public void ForEach<T1>(EventActionR<Entity, T1> action) where T1 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                action(context.GetEntity(world), ref component1);
            }
        }
    }

    public void ForEach<TEvent, T1>(in TEvent e, EventAction<TEvent, T1> action) where T1 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                action(in e, in component1);
            }
        }
    }

    public void ForEach<TEvent, T1>(in TEvent e, EventActionR<TEvent, T1> action) where T1 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                action(in e, ref component1);
            }
        }
    }

    public void ForEach<TEvent, T1>(in TEvent e, EventAction<TEvent, Entity, T1> action) where T1 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                action(in e, context.GetEntity(world), in component1);
            }
        }
    }

    public void ForEach<TEvent, T1>(in TEvent e, EventActionR<TEvent, Entity, T1> action) where T1 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                action(in e, context.GetEntity(world), ref component1);
            }
        }
    }


    #endregion

    #region Component2

    public void ForEach<T1, T2>(Action<T1, T2> action) where T1 : IComponent where T2 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                action(component1, component2);
            }
        }
    }

    public void ForEach<T1, T2>(Action<Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                action(context.GetEntity(world), component1, component2);
            }
        }
    }

    public void ForEach<TEvent, T1, T2>(in TEvent e, Action<TEvent, T1, T2> action) where T1 : IComponent where T2 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                action(e, component1, component2);
            }
        }
    }

    public void ForEach<TEvent, T1, T2>(in TEvent e, Action<TEvent, Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                action(e, context.GetEntity(world), component1, component2);
            }
        }
    }

    public void ForEach<T1, T2>(EventAction<T1, T2> action) where T1 : IComponent where T2 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                action(in component1, in component2);
            }
        }
    }

    public void ForEach<T1, T2>(EventActionR<T1, T2> action) where T1 : IComponent where T2 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                action(in component1, ref component2);
            }
        }
    }

    public void ForEach<T1, T2>(EventActionR2<T1, T2> action) where T1 : IComponent where T2 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                action(ref component1, ref component2);
            }
        }
    }

    public void ForEach<T1, T2>(EventAction<Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                action(context.GetEntity(world), in component1, in component2);
            }
        }
    }

    public void ForEach<T1, T2>(EventActionR<Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                action(context.GetEntity(world), in component1, ref component2);
            }
        }
    }

    public void ForEach<T1, T2>(EventActionR2<Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                action(context.GetEntity(world), ref component1, ref component2);
            }
        }
    }

    public void ForEach<TEvent, T1, T2>(in TEvent e, EventAction<TEvent, T1, T2> action) where T1 : IComponent where T2 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                action(in e, in component1, in component2);
            }
        }
    }

    public void ForEach<TEvent, T1, T2>(in TEvent e, EventActionR<TEvent, T1, T2> action) where T1 : IComponent where T2 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                action(in e, in component1, ref component2);
            }
        }
    }

    public void ForEach<TEvent, T1, T2>(in TEvent e, EventActionR2<TEvent, T1, T2> action) where T1 : IComponent where T2 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                action(in e, ref component1, ref component2);
            }
        }
    }

    public void ForEach<TEvent, T1, T2>(in TEvent e, EventAction<TEvent, Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                action(in e, context.GetEntity(world), in component1, in component2);
            }
        }
    }

    public void ForEach<TEvent, T1, T2>(in TEvent e, EventActionR<TEvent, Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                action(in e, context.GetEntity(world), in component1, ref component2);
            }
        }
    }

    public void ForEach<TEvent, T1, T2>(in TEvent e, EventActionR2<TEvent, Entity, T1, T2> action) where T1 : IComponent where T2 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                action(in e, context.GetEntity(world), ref component1, ref component2);
            }
        }
    }


    #endregion

    #region Component3

    public void ForEach<T1, T2, T3>(Action<T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(component1, component2, component3);
            }
        }
    }

    public void ForEach<T1, T2, T3>(Action<Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(context.GetEntity(world), component1, component2, component3);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3>(in TEvent e, Action<TEvent, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(e, component1, component2, component3);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3>(in TEvent e, Action<TEvent, Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(e, context.GetEntity(world), component1, component2, component3);
            }
        }
    }

    public void ForEach<T1, T2, T3>(EventAction<T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(in component1, in component2, in component3);
            }
        }
    }

    public void ForEach<T1, T2, T3>(EventActionR<T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(in component1, in component2, ref component3);
            }
        }
    }

    public void ForEach<T1, T2, T3>(EventActionR2<T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(in component1, ref component2, ref component3);
            }
        }
    }

    public void ForEach<T1, T2, T3>(EventActionR3<T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(ref component1, ref component2, ref component3);
            }
        }
    }

    public void ForEach<T1, T2, T3>(EventAction<Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(context.GetEntity(world), in component1, in component2, in component3);
            }
        }
    }

    public void ForEach<T1, T2, T3>(EventActionR<Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(context.GetEntity(world), in component1, in component2, ref component3);
            }
        }
    }

    public void ForEach<T1, T2, T3>(EventActionR2<Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(context.GetEntity(world), in component1, ref component2, ref component3);
            }
        }
    }

    public void ForEach<T1, T2, T3>(EventActionR3<Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(context.GetEntity(world), ref component1, ref component2, ref component3);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3>(in TEvent e, EventAction<TEvent, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(in e, in component1, in component2, in component3);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3>(in TEvent e, EventActionR<TEvent, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(in e, in component1, in component2, ref component3);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3>(in TEvent e, EventActionR2<TEvent, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(in e, in component1, ref component2, ref component3);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3>(in TEvent e, EventActionR3<TEvent, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(in e, ref component1, ref component2, ref component3);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3>(in TEvent e, EventAction<TEvent, Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(in e, context.GetEntity(world), in component1, in component2, in component3);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3>(in TEvent e, EventActionR<TEvent, Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(in e, context.GetEntity(world), in component1, in component2, ref component3);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3>(in TEvent e, EventActionR2<TEvent, Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(in e, context.GetEntity(world), in component1, ref component2, ref component3);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3>(in TEvent e, EventActionR3<TEvent, Entity, T1, T2, T3> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                action(in e, context.GetEntity(world), ref component1, ref component2, ref component3);
            }
        }
    }


    #endregion

    #region Component4

    public void ForEach<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(component1, component2, component3, component4);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4>(Action<Entity, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(context.GetEntity(world), component1, component2, component3, component4);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4>(in TEvent e, Action<TEvent, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(e, component1, component2, component3, component4);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4>(in TEvent e, Action<TEvent, Entity, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(e, context.GetEntity(world), component1, component2, component3, component4);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4>(EventAction<T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(in component1, in component2, in component3, in component4);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4>(EventActionR<T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(in component1, in component2, in component3, ref component4);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4>(EventActionR2<T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(in component1, in component2, ref component3, ref component4);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4>(EventActionR3<T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(in component1, ref component2, ref component3, ref component4);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4>(EventActionR4<T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(ref component1, ref component2, ref component3, ref component4);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4>(EventAction<Entity, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(context.GetEntity(world), in component1, in component2, in component3, in component4);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4>(EventActionR<Entity, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(context.GetEntity(world), in component1, in component2, in component3, ref component4);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4>(EventActionR2<Entity, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(context.GetEntity(world), in component1, in component2, ref component3, ref component4);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4>(EventActionR3<Entity, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(context.GetEntity(world), in component1, ref component2, ref component3, ref component4);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4>(EventActionR4<Entity, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(context.GetEntity(world), ref component1, ref component2, ref component3, ref component4);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4>(in TEvent e, EventAction<TEvent, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(in e, in component1, in component2, in component3, in component4);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4>(in TEvent e, EventActionR<TEvent, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(in e, in component1, in component2, in component3, ref component4);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4>(in TEvent e, EventActionR2<TEvent, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(in e, in component1, in component2, ref component3, ref component4);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4>(in TEvent e, EventActionR3<TEvent, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(in e, in component1, ref component2, ref component3, ref component4);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4>(in TEvent e, EventActionR4<TEvent, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(in e, ref component1, ref component2, ref component3, ref component4);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4>(in TEvent e, EventAction<TEvent, Entity, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(in e, context.GetEntity(world), in component1, in component2, in component3, in component4);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4>(in TEvent e, EventActionR<TEvent, Entity, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(in e, context.GetEntity(world), in component1, in component2, in component3, ref component4);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4>(in TEvent e, EventActionR2<TEvent, Entity, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(in e, context.GetEntity(world), in component1, in component2, ref component3, ref component4);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4>(in TEvent e, EventActionR3<TEvent, Entity, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(in e, context.GetEntity(world), in component1, ref component2, ref component3, ref component4);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4>(in TEvent e, EventActionR4<TEvent, Entity, T1, T2, T3, T4> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                action(in e, context.GetEntity(world), ref component1, ref component2, ref component3, ref component4);
            }
        }
    }


    #endregion

    #region Component5

    public void ForEach<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(component1, component2, component3, component4, component5);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5>(Action<Entity, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(context.GetEntity(world), component1, component2, component3, component4, component5);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4, T5>(in TEvent e, Action<TEvent, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(e, component1, component2, component3, component4, component5);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4, T5>(in TEvent e, Action<TEvent, Entity, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(e, context.GetEntity(world), component1, component2, component3, component4, component5);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5>(EventAction<T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(in component1, in component2, in component3, in component4, in component5);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5>(EventActionR<T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(in component1, in component2, in component3, in component4, ref component5);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5>(EventActionR2<T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(in component1, in component2, in component3, ref component4, ref component5);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5>(EventActionR3<T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(in component1, in component2, ref component3, ref component4, ref component5);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5>(EventActionR4<T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(in component1, ref component2, ref component3, ref component4, ref component5);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5>(EventActionR5<T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(ref component1, ref component2, ref component3, ref component4, ref component5);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5>(EventAction<Entity, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(context.GetEntity(world), in component1, in component2, in component3, in component4, in component5);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5>(EventActionR<Entity, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(context.GetEntity(world), in component1, in component2, in component3, in component4, ref component5);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5>(EventActionR2<Entity, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(context.GetEntity(world), in component1, in component2, in component3, ref component4, ref component5);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5>(EventActionR3<Entity, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(context.GetEntity(world), in component1, in component2, ref component3, ref component4, ref component5);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5>(EventActionR4<Entity, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(context.GetEntity(world), in component1, ref component2, ref component3, ref component4, ref component5);
            }
        }
    }

    public void ForEach<T1, T2, T3, T4, T5>(EventActionR5<Entity, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(context.GetEntity(world), ref component1, ref component2, ref component3, ref component4, ref component5);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4, T5>(in TEvent e, EventAction<TEvent, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(in e, in component1, in component2, in component3, in component4, in component5);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4, T5>(in TEvent e, EventActionR<TEvent, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(in e, in component1, in component2, in component3, in component4, ref component5);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4, T5>(in TEvent e, EventActionR2<TEvent, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(in e, in component1, in component2, in component3, ref component4, ref component5);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4, T5>(in TEvent e, EventActionR3<TEvent, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(in e, in component1, in component2, ref component3, ref component4, ref component5);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4, T5>(in TEvent e, EventActionR4<TEvent, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(in e, in component1, ref component2, ref component3, ref component4, ref component5);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4, T5>(in TEvent e, EventActionR5<TEvent, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(in e, ref component1, ref component2, ref component3, ref component4, ref component5);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4, T5>(in TEvent e, EventAction<TEvent, Entity, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(in e, context.GetEntity(world), in component1, in component2, in component3, in component4, in component5);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4, T5>(in TEvent e, EventActionR<TEvent, Entity, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(in e, context.GetEntity(world), in component1, in component2, in component3, in component4, ref component5);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4, T5>(in TEvent e, EventActionR2<TEvent, Entity, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(in e, context.GetEntity(world), in component1, in component2, in component3, ref component4, ref component5);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4, T5>(in TEvent e, EventActionR3<TEvent, Entity, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(in e, context.GetEntity(world), in component1, in component2, ref component3, ref component4, ref component5);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4, T5>(in TEvent e, EventActionR4<TEvent, Entity, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(in e, context.GetEntity(world), in component1, ref component2, ref component3, ref component4, ref component5);
            }
        }
    }

    public void ForEach<TEvent, T1, T2, T3, T4, T5>(in TEvent e, EventActionR5<TEvent, Entity, T1, T2, T3, T4, T5> action) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var world = archetypeGroup.World;
        var archetypes = archetypeGroup.Archetypes;
        var componentGroup1 = world.GetComponentGroup<T1>();
        var componentGroup2 = world.GetComponentGroup<T2>();
        var componentGroup3 = world.GetComponentGroup<T3>();
        var componentGroup4 = world.GetComponentGroup<T4>();
        var componentGroup5 = world.GetComponentGroup<T5>();
        for(var i = 0; i < archetypes.Length; i++) {
            var archetype = archetypes[i];
            for(var j = 0; j < archetype.EntityCapacity; j++) {
                var index = archetype.GetEntity(j);
                if(index < 0) {
                    continue;
                }
                ref var context = ref world.GetContext(index);
                ref var component1 = ref componentGroup1.Get(context.Get(componentGroup1.TypeId));
                ref var component2 = ref componentGroup2.Get(context.Get(componentGroup2.TypeId));
                ref var component3 = ref componentGroup3.Get(context.Get(componentGroup3.TypeId));
                ref var component4 = ref componentGroup4.Get(context.Get(componentGroup4.TypeId));
                ref var component5 = ref componentGroup5.Get(context.Get(componentGroup5.TypeId));
                action(in e, context.GetEntity(world), ref component1, ref component2, ref component3, ref component4, ref component5);
            }
        }
    }


    #endregion
}
