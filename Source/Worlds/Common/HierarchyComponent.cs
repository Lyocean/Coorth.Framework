using System;
using System.Runtime.CompilerServices;


namespace Coorth.Framework; 

[Serializable, DataDefine]
[Component(IsPinned = true), Guid("C30A217A-4660-402A-A993-BA4820389F0B")]
public partial struct HierarchyComponent : IComponent {

    private Entity entity;
    [DataMember]
    private int self;
    [DataMember]
    private int parent;
    [DataMember]
    private int head, tail;
    [DataMember]
    private int prev, next;
    [DataMember]
    private int count;
    [DataMember]
    private uint flags;
    
    public Entity Entity => entity;
    public World World => entity.World;
    public int Count => count;
    
    private ComponentGroup<HierarchyComponent> Group => entity.World.GetComponentGroup<HierarchyComponent>();

    public bool HasParent => parent != 0;
    public ref HierarchyComponent Parent => ref World.GetComponentGroup<HierarchyComponent>().Get(parent - 1);
    public Entity ParentEntity => parent != 0 ? Parent.Entity : Entity.Null;

    public EnumerableHierarchies Children => new(World, head);

    public HierarchyEnumerator GetEnumerator() => new(World, head);
    
    public EnumerableEntities ChildrenEntities => new(Entity.World, head);
    
    public void OnSetup(Entity e) {
        entity = e;
        self = e.Index<HierarchyComponent>() + 1;
    }

    public void OnClear() {
        var group = Group;
        if (count > 0) {
            var curr = head;
            while (curr != 0) {
                ref var curr_hierarchy = ref group.Get(curr - 1);
                curr = curr_hierarchy.next;
                curr_hierarchy.entity.Dispose();
            }
        }
        
        if (parent != 0) {
            ref var parent_hierarchy = ref group.Get(parent - 1);
            parent_hierarchy._RemoveChild(ref this);
            parent = 0;
            entity = Entity.Null; 
        }
    }

    public void SetFlags(ushort value) {
        flags = value;
    }
    
    public void SetFlags(int position, bool value) {
        if (value) {
            flags |= (1u << position);
        }
        else {
            flags &= (~(1u << position));
        }
    }
    
    public void SetParent(in Entity? value) {
        if (value == null || value.Value == Entity.Null) {
            if (parent == 0) {
                return;
            }
            ref var old_parent = ref Group.Get(parent - 1);
            old_parent._RemoveChild(ref this);
        }
        else {
            if (parent != 0) {
                ref var old_parent = ref Group.Get(parent - 1);
                old_parent._RemoveChild(ref this);
            }
            ref var new_parent = ref value.Value.Offer<HierarchyComponent>();
            new_parent._AddChild(ref this);
        }
    }
    
    private void _AddChild(ref HierarchyComponent child) {
        if (child.parent == self) {
            return;
        }
        if (child.parent != 0) {
            ref var parent_hierarchy = ref Group.Get(child.parent - 1);
            parent_hierarchy._RemoveChild(ref child);            
        }
        count++;
        child.parent = self;
        if (tail == 0) {
            head = child.self;
            tail = head;
        }
        else {
            ref var tail_hierarchy = ref Group.Get(tail - 1);
            child.prev = tail;
            tail_hierarchy.next = child.self;
            tail = child.self;
        }
        entity.Modify<HierarchyComponent>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddChild(Entity child) {
        _AddChild(ref child.Offer<HierarchyComponent>());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddChild(ref HierarchyComponent child) {
        _AddChild(ref child);
    }

    public ref HierarchyComponent GetChild(int position) {
        if (position < 0 || position > count) {
            throw new ArgumentOutOfRangeException();
        }
        var group = Group;
        var curr = head;
        ref var hierarchy = ref group.Get(curr - 1);
        for (var i = 1; i < position; i++) {
            curr = hierarchy.next;
            hierarchy = ref group.Get(curr - 1);
        }
        return ref hierarchy;
    }

    private bool _RemoveChild(ref HierarchyComponent child) {
        if (child.parent != self) {
            return false;
        }
        child.parent = 0;
        var group = Group;
        if (head == child.self) {
            head = child.next;
        }
        if (tail == child.self) {
            tail = child.prev;
        }
        if (child.prev != 0) {
            ref var prev_hierarchy = ref group.Get(prev - 1);
            prev_hierarchy.next = child.next;
            child.prev = 0;
        }
        if (child.next != 0) {
            ref var next_hierarchy = ref group.Get(next - 1);
            next_hierarchy.prev = child.prev;
            child.next = 0;
        }        
        count--;
        entity.Modify<HierarchyComponent>();
        return true;
    }

    public bool RemoveChild(int position) {
        if (position < 0 || position > count) {
            throw new ArgumentOutOfRangeException();
        }
        ref var hierarchy = ref GetChild(position);
        return _RemoveChild(ref hierarchy);
    }
    
    public bool RemoveChild(ref HierarchyComponent child) {
        return _RemoveChild(ref child);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InsertChild(in Entity child, int position) {
        InsertChild(ref child.Offer<HierarchyComponent>(), position);
    }
    
    public void InsertChild(ref HierarchyComponent child, int position) {
        if (position < 0 || position > count) {
            throw new ArgumentOutOfRangeException();
        }
        if (child.parent == self) {
            return;
        }
        var group = Group;
        if (child.parent == 0) {
            ref var parent_hierarchy = ref group.Get(child.parent - 1);
            parent_hierarchy._RemoveChild(ref child);
        }
        count++;
        child.parent = self;
        if (count == 1) {
            head = child.self;
            tail = head;
        }
        else {
            var curr = head;
            ref var prev_hierarchy = ref group.Get(curr - 1);
            ref var next_hierarchy = ref group.Get(curr - 1);
            while (position > 0) {
                curr = prev_hierarchy.next;
                prev_hierarchy = next_hierarchy;
                next_hierarchy = ref group.Get(curr - 1);
                position--;
            }
            prev_hierarchy.next = child.self;
            child.prev = prev_hierarchy.self;
            child.next = next_hierarchy.self;
            next_hierarchy.prev = child.self;
        }
        Entity.Modify<HierarchyComponent>();
    }

    public void SetSiblingIndex(int index) {
        if (parent == 0 || index < 0 || index >= count) {
            return;
        }
        var group = Group;
        ref var parent_hierarchy = ref group.Get(parent - 1);
        ref var target_hierarchy = ref parent_hierarchy.GetChild(index);
        if (self == target_hierarchy.self) {
            return;
        }
        (target_hierarchy.next, next) = (next, target_hierarchy.next);
        (target_hierarchy.prev, prev) = (prev, target_hierarchy.prev);
    }

    public int GetSiblingIndex() {
        if (parent == 0) {
            return -1;
        }
        var group = Group;
        ref var parent_hierarchy = ref group.Get(parent - 1);
        var curr = parent_hierarchy.head;
        ref var curr_hierarchy = ref group.Get(curr - 1);
        for (var i = 0; i < parent_hierarchy.count; i++) {
            if (curr_hierarchy.self == self) {
                return i;
            }
            curr = curr_hierarchy.next;
            if (curr == -1) {
                return -1;
            }
            curr_hierarchy = ref group.Get(curr - 1);
        }
        return  -1;
    }
    
    public readonly ref struct EnumerableHierarchies  {
        private readonly World world;
        private readonly int head;

        public EnumerableHierarchies(World world, int head) {
            this.world = world; 
            this.head = head;
        }
        
        public HierarchyEnumerator GetEnumerator() => new(world, head);
    }
    
    public ref struct HierarchyEnumerator  {
        private readonly ComponentGroup<HierarchyComponent> group;
        private readonly int head;
        private int curr;
        private int next;

        public HierarchyEnumerator(World world, int head) {
            this.group = world.GetComponentGroup<HierarchyComponent>();
            this.head = head;
            curr = next = head;
        }

        public bool MoveNext() {
            if(curr == 0) {
                return false;
            }
            curr = next;
            ref var hierarchy = ref group.Get(curr - 1);
            next = hierarchy.next;
            return true;
        }
        
        public ref HierarchyComponent Current => ref group.Get(curr - 1);

        public void Reset() {
            curr = next = head;
        }
    }
    
    public readonly ref struct EnumerableEntities {
        private readonly World world;
        private readonly int head;

        public EnumerableEntities(World world, int head) {
            this.world = world;
            this.head = head;
        }

        public EntityEnumerator GetEnumerator() => new(world, head);
    }
    
    public ref struct EntityEnumerator {
        private readonly ComponentGroup<HierarchyComponent> group;
        private readonly int head;
        private int curr;
        private int next;
            
        public EntityEnumerator(World world, int head) {
            this.group = world.GetComponentGroup<HierarchyComponent>();
            this.head = head;
            curr = next = head;
        }
 
        public bool MoveNext() {
            if(next == 0) {
                return false;
            }
            curr = next;
            ref var hierarchy = ref group.Get(curr - 1);
            next = hierarchy.next;
            return true;
        }
        
        public Entity Current => group.Get(curr - 1).entity;

        public void Reset() {
            curr = next = head;
        }
    }
}

public static class HierarchyExtension {
    
    public static void SetParent(this in Entity entity, in Entity? parent) {
        entity.Offer<HierarchyComponent>().SetParent(parent);
    }

    public static void SetSiblingIndex(this in Entity entity, int index) {
        entity.Offer<HierarchyComponent>().SetSiblingIndex(index);
    }
    
    public static int GetSiblingIndex(this in Entity entity) {
        return entity.Get<HierarchyComponent>().GetSiblingIndex();
    }

    public static Entity? FindChild(this in Entity entity, string path) {
        return _FindChild(in entity, path.AsSpan());
    }

    private static Entity? _FindChild(in Entity entity, ReadOnlySpan<char> path) {
        ref var hierarchy = ref entity.Get<HierarchyComponent>();
        var index = path.IndexOf('/');
        var prefix = path.Slice(0, index);
        foreach (ref var child_hierarchy in hierarchy) {
            var child_entity = child_hierarchy.Entity;
            ref var child_description = ref child_entity.Get<DescriptionComponent>();
            if (child_description.Name == null) {
                continue;
            }
            var child_name = child_description.Name.AsSpan();
            if (index < 0) {
                if (child_name.SequenceEqual(path)) {
                    return child_entity;
                }
            }
            else {
                if (child_name.SequenceEqual(prefix)) {
                    return _FindChild(in entity, path.Slice(index + 1));
                }
                if (child_name.SequenceEqual(path)) {
                    return child_entity;
                }
            }
        }
        return null;
    }
}