
using System;
using System.Collections;
using System.Collections.Generic;

namespace Coorth.Framework;

#region Component1
    
public readonly struct ComponentCollection<T1> : IEnumerable<(Entity, T1)> where T1 : IComponent {

    private readonly ComponentGroup<T1> group1;


    internal ComponentCollection(World world) {
        this.group1 = world.GetComponentGroup<T1>();

    }
 

    public void ForEach(Action<T1> action) {
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            action(component1);
        }
    }

    public void ForEach(Action<Entity, T1> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            action(context.GetEntity(world), component1);
        }
    }

    public void ForEach<TEvent>(in TEvent e, Action<TEvent, T1> action) {
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            action(e, component1);
        }
    }

    public void ForEach<TEvent>(in TEvent e, Action<TEvent, Entity, T1> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            action(e, context.GetEntity(world), component1);
        }
    }

    public void ForEach(EventAction<T1> action) {
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            action(in component1);
        }
    }

    public void ForEach(EventActionR<T1> action) {
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            action(ref component1);
        }
    }

    public void ForEach(EventAction<Entity, T1> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            action(context.GetEntity(world), in component1);
        }
    }

    public void ForEach(EventActionR<Entity, T1> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            action(context.GetEntity(world), ref component1);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventAction<TEvent, T1> action) {
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            action(in e, in component1);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventActionR<TEvent, T1> action) {
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            action(in e, ref component1);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventAction<TEvent, Entity, T1> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            action(in e, context.GetEntity(world), in component1);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventActionR<TEvent, Entity, T1> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            action(in e, context.GetEntity(world), ref component1);
        }
    }

    public Enumerator GetEnumerator() => new Enumerator(group1);

    IEnumerator<(Entity, T1)> IEnumerable<(Entity, T1)>.GetEnumerator() => new Enumerator(group1);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator : IEnumerator<(Entity, T1)> {
        private readonly ComponentGroup<T1> group1;

        private int index;
        private (Entity, T1) current;
        
        internal Enumerator(ComponentGroup<T1> group1) {
            this.group1 = group1;

            index = group1.separate;
            current = default;
        }

        public bool MoveNext() {
            var world = group1.World;
            while (index < group1.Count) {
                var component1 = group1.Get(index);
                var entityIndex = group1.GetEntityIndex(index);
                if(entityIndex < 0){
                    continue;
                }
                ref var context = ref world.GetContext(entityIndex);
                current = (context.GetEntity(world), component1);
                index++;
                return true;
            }

            return false;
        }

        public void Reset() {
            index = group1.separate;
            current = default;
        }

        public (Entity, T1) Current => current;

        object IEnumerator.Current => Current;

        public void Dispose() {
        }

    }
}
    
#endregion


#region Component2
    
public readonly struct ComponentCollection<T1, T2> : IEnumerable<(Entity, T1, T2)> where T1 : IComponent where T2 : IComponent {

    private readonly ComponentGroup<T1> group1;
    private readonly ComponentGroup<T2> group2;


    internal ComponentCollection(World world) {
        this.group1 = world.GetComponentGroup<T1>();
        this.group2 = world.GetComponentGroup<T2>();

    }
 

    public void ForEach(Action<T1, T2> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            action(component1, component2);
        }
    }

    public void ForEach(Action<Entity, T1, T2> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            action(context.GetEntity(world), component1, component2);
        }
    }

    public void ForEach<TEvent>(in TEvent e, Action<TEvent, T1, T2> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            action(e, component1, component2);
        }
    }

    public void ForEach<TEvent>(in TEvent e, Action<TEvent, Entity, T1, T2> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            action(e, context.GetEntity(world), component1, component2);
        }
    }

    public void ForEach(EventAction<T1, T2> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            action(in component1, in component2);
        }
    }

    public void ForEach(EventActionR<T1, T2> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            action(in component1, ref component2);
        }
    }

    public void ForEach(EventActionR2<T1, T2> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            action(ref component1, ref component2);
        }
    }

    public void ForEach(EventAction<Entity, T1, T2> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            action(context.GetEntity(world), in component1, in component2);
        }
    }

    public void ForEach(EventActionR<Entity, T1, T2> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            action(context.GetEntity(world), in component1, ref component2);
        }
    }

    public void ForEach(EventActionR2<Entity, T1, T2> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            action(context.GetEntity(world), ref component1, ref component2);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventAction<TEvent, T1, T2> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            action(in e, in component1, in component2);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventActionR<TEvent, T1, T2> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            action(in e, in component1, ref component2);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventActionR2<TEvent, T1, T2> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            action(in e, ref component1, ref component2);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventAction<TEvent, Entity, T1, T2> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            action(in e, context.GetEntity(world), in component1, in component2);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventActionR<TEvent, Entity, T1, T2> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            action(in e, context.GetEntity(world), in component1, ref component2);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventActionR2<TEvent, Entity, T1, T2> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            action(in e, context.GetEntity(world), ref component1, ref component2);
        }
    }

    public Enumerator GetEnumerator() => new Enumerator(group1, group2);

    IEnumerator<(Entity, T1, T2)> IEnumerable<(Entity, T1, T2)>.GetEnumerator() => new Enumerator(group1, group2);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator : IEnumerator<(Entity, T1, T2)> {
        private readonly ComponentGroup<T1> group1;
        private readonly ComponentGroup<T2> group2;

        private int index;
        private (Entity, T1, T2) current;
        
        internal Enumerator(ComponentGroup<T1> group1, ComponentGroup<T2> group2) {
            this.group1 = group1;
            this.group2 = group2;

            index = group1.separate;
            current = default;
        }

        public bool MoveNext() {
            var world = group1.World;
            while (index < group1.Count) {
                var component1 = group1.Get(index);
                var entityIndex = group1.GetEntityIndex(index);
                if(entityIndex < 0){
                    continue;
                }
                ref var context = ref world.GetContext(entityIndex);
                if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                    continue;
                }
                var component2 = group2.Get(componentIndex2);
                current = (context.GetEntity(world), component1, component2);
                index++;
                return true;
            }

            return false;
        }

        public void Reset() {
            index = group1.separate;
            current = default;
        }

        public (Entity, T1, T2) Current => current;

        object IEnumerator.Current => Current;

        public void Dispose() {
        }

    }
}
    
#endregion


#region Component3
    
public readonly struct ComponentCollection<T1, T2, T3> : IEnumerable<(Entity, T1, T2, T3)> where T1 : IComponent where T2 : IComponent where T3 : IComponent {

    private readonly ComponentGroup<T1> group1;
    private readonly ComponentGroup<T2> group2;
    private readonly ComponentGroup<T3> group3;


    internal ComponentCollection(World world) {
        this.group1 = world.GetComponentGroup<T1>();
        this.group2 = world.GetComponentGroup<T2>();
        this.group3 = world.GetComponentGroup<T3>();

    }
 

    public void ForEach(Action<T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(component1, component2, component3);
        }
    }

    public void ForEach(Action<Entity, T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(context.GetEntity(world), component1, component2, component3);
        }
    }

    public void ForEach<TEvent>(in TEvent e, Action<TEvent, T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(e, component1, component2, component3);
        }
    }

    public void ForEach<TEvent>(in TEvent e, Action<TEvent, Entity, T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(e, context.GetEntity(world), component1, component2, component3);
        }
    }

    public void ForEach(EventAction<T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(in component1, in component2, in component3);
        }
    }

    public void ForEach(EventActionR<T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(in component1, in component2, ref component3);
        }
    }

    public void ForEach(EventActionR2<T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(in component1, ref component2, ref component3);
        }
    }

    public void ForEach(EventActionR3<T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(ref component1, ref component2, ref component3);
        }
    }

    public void ForEach(EventAction<Entity, T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(context.GetEntity(world), in component1, in component2, in component3);
        }
    }

    public void ForEach(EventActionR<Entity, T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(context.GetEntity(world), in component1, in component2, ref component3);
        }
    }

    public void ForEach(EventActionR2<Entity, T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(context.GetEntity(world), in component1, ref component2, ref component3);
        }
    }

    public void ForEach(EventActionR3<Entity, T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(context.GetEntity(world), ref component1, ref component2, ref component3);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventAction<TEvent, T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(in e, in component1, in component2, in component3);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventActionR<TEvent, T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(in e, in component1, in component2, ref component3);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventActionR2<TEvent, T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(in e, in component1, ref component2, ref component3);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventActionR3<TEvent, T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(in e, ref component1, ref component2, ref component3);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventAction<TEvent, Entity, T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(in e, context.GetEntity(world), in component1, in component2, in component3);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventActionR<TEvent, Entity, T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(in e, context.GetEntity(world), in component1, in component2, ref component3);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventActionR2<TEvent, Entity, T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(in e, context.GetEntity(world), in component1, ref component2, ref component3);
        }
    }

    public void ForEach<TEvent>(in TEvent e, EventActionR3<TEvent, Entity, T1, T2, T3> action) {
        var world = group1.World;
        for(var i = group1.separate; i< group1.Count; i++) {
            ref var component1 = ref group1.Get(i);
            ref var context = ref world.GetContext(group1.GetEntityIndex(i));
            if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                continue;
            }
            ref var component2 = ref group2.Get(componentIndex2);
            if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                continue;
            }
            ref var component3 = ref group3.Get(componentIndex3);
            action(in e, context.GetEntity(world), ref component1, ref component2, ref component3);
        }
    }

    public Enumerator GetEnumerator() => new Enumerator(group1, group2, group3);

    IEnumerator<(Entity, T1, T2, T3)> IEnumerable<(Entity, T1, T2, T3)>.GetEnumerator() => new Enumerator(group1, group2, group3);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator : IEnumerator<(Entity, T1, T2, T3)> {
        private readonly ComponentGroup<T1> group1;
        private readonly ComponentGroup<T2> group2;
        private readonly ComponentGroup<T3> group3;

        private int index;
        private (Entity, T1, T2, T3) current;
        
        internal Enumerator(ComponentGroup<T1> group1, ComponentGroup<T2> group2, ComponentGroup<T3> group3) {
            this.group1 = group1;
            this.group2 = group2;
            this.group3 = group3;

            index = group1.separate;
            current = default;
        }

        public bool MoveNext() {
            var world = group1.World;
            while (index < group1.Count) {
                var component1 = group1.Get(index);
                var entityIndex = group1.GetEntityIndex(index);
                if(entityIndex < 0){
                    continue;
                }
                ref var context = ref world.GetContext(entityIndex);
                if (!context.TryGet(group2.TypeId, out var componentIndex2)) {
                    continue;
                }
                var component2 = group2.Get(componentIndex2);
                if (!context.TryGet(group3.TypeId, out var componentIndex3)) {
                    continue;
                }
                var component3 = group3.Get(componentIndex3);
                current = (context.GetEntity(world), component1, component2, component3);
                index++;
                return true;
            }

            return false;
        }

        public void Reset() {
            index = group1.separate;
            current = default;
        }

        public (Entity, T1, T2, T3) Current => current;

        object IEnumerator.Current => Current;

        public void Dispose() {
        }

    }
}
    
#endregion


    public static class ComponentCollectionExtension {

         public static ComponentCollection<T1, T2> GetComponents<T1, T2>(this World world) where T1 : IComponent where T2 : IComponent {
             return new ComponentCollection<T1, T2>(world);
         }   

         public static ComponentCollection<T1, T2, T3> GetComponents<T1, T2, T3>(this World world) where T1 : IComponent where T2 : IComponent where T3 : IComponent {
             return new ComponentCollection<T1, T2, T3>(world);
         }   
}
