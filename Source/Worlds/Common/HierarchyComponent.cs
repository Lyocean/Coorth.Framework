using System;
using System.Collections;
using System.Collections.Generic;


namespace Coorth.Framework; 

[Serializable, StoreContract]
[Component(IsPinned = true), Guid("C30A217A-4660-402A-A993-BA4820389F0B")]
public struct HierarchyComponent : IComponent {

    public Entity Entity { get; private set; }

    public World World => Entity.World;

    public IHierarchyNode? Node { get; set; }

    private EntityId parentId;
    private EntityId headId, tailId;
    private EntityId prevId, nextId;

    private int count;
    public int Count => count + Node?.ChildCount ?? 0;
    
    public Entity ParentEntity => World.GetEntity(parentId);

    public ref HierarchyComponent ParentHierarchy => ref World.GetComponent<HierarchyComponent>(parentId);

    public EnumerableEntities GetChildrenEntities() => new(Entity.World, headId);
    
    public EnumerableHierarchies GetChildrenHierarchies() => new(Entity.World, headId);
    
    public void OnSetup(Entity entity) => Entity = entity;

    public void OnClear() {
        if (parentId.IsNotNull && World.HasComponent<HierarchyComponent>(parentId)) {
            ref var hierarchy = ref World.GetComponent<HierarchyComponent>(parentId);
            hierarchy._RemoveChild(ref this);
            parentId = EntityId.Null;
        }
        Entity = Entity.Null;
    }

    public void SetNode(IHierarchyNode node) {
        if (Node != null) {
            throw new InvalidOperationException();
        }
        Node = node;
    }
    
    public bool IsEnableSelf() {
        return Entity.IsEnable<HierarchyComponent>();
    }

    public bool IsEnableInHierarchy() {
        var entity = Entity;
        do {
            if (!entity.IsEnable<HierarchyComponent>()) {
                return false;
            }
            ref var hierarchy = ref entity.Get<HierarchyComponent>();
            entity = hierarchy.ParentEntity;
        } while (!entity.IsNull);
        return true;
    }

    private void _AddChild(ref HierarchyComponent hierarchy) {
        count++;
        hierarchy.parentId = this.Entity.Id;
        if (tailId.IsNull) {
            headId = hierarchy.Entity.Id;
            tailId = headId;
        }
        else {
            ref var tailHierarchy = ref World.GetComponent<HierarchyComponent>(tailId);
            hierarchy.prevId = tailId;
            tailHierarchy.nextId = hierarchy.Entity.Id;
            tailId = hierarchy.Entity.Id;
        }
    }
    
    public void AddChild(in Entity child) {
        AddChild(ref child.Offer<HierarchyComponent>());
    }

    public void AddChild(ref HierarchyComponent child) {
        if (child.parentId == Entity.Id) {
            return;
        }
        if (child.parentId.IsNotNull) {
            ref var hierarchy = ref World.GetComponent<HierarchyComponent>(child.parentId);
            hierarchy._RemoveChild(ref child);
        }
        _AddChild(ref child);
        Entity.Modify<HierarchyComponent>();
    }
    
    public void InsertChild(in Entity child, int position) {
        InsertChild(ref child.Offer<HierarchyComponent>(), position);
    }

    public void InsertChild(ref HierarchyComponent child, int position) {
        if (position < 0 || position > count) {
            throw new ArgumentOutOfRangeException();
        }
        if (child.parentId == this.Entity.Id) {
            return;
        }
        if (child.parentId.IsNotNull) {
            ref var hierarchy = ref World.GetComponent<HierarchyComponent>(child.parentId);
            hierarchy._RemoveChild(ref child);
        }
        count++;
        child.parentId = this.Entity.Id;
        if (count == 1) {
            headId = child.Entity.Id;
            tailId = headId;
        }
        else {
            var id = headId;
            ref var prev = ref World.GetComponent<HierarchyComponent>(id);
            ref var next = ref World.GetComponent<HierarchyComponent>(prev.nextId);
            while (position > 0) {
                id = prev.nextId;
                prev = next;
                next = ref World.GetComponent<HierarchyComponent>(id);
                position--;
            }
            prev.nextId = child.Entity.Id;
            child.prevId = prev.Entity.Id;
            child.nextId = next.Entity.Id;
            next.prevId = child.Entity.Id;
        }
        Entity.Modify<HierarchyComponent>();
    }
    
    public ref HierarchyComponent GetChild(int position) {
        if (position < 0 || position > count) {
            throw new ArgumentOutOfRangeException();
        }
        var id = headId;
        ref var hierarchy = ref World.GetComponent<HierarchyComponent>(id);
        for (var i = 1; i < position; i++) {
            id = hierarchy.nextId;
            hierarchy = ref World.GetComponent<HierarchyComponent>(id);
        }
        return ref hierarchy;
    }

    private bool _RemoveChild(ref HierarchyComponent hierarchy) {
        var childId = hierarchy.Entity.Id;
        hierarchy.parentId = EntityId.Null;
        if (headId == childId) {
            headId = hierarchy.nextId;
        }
        if (tailId == childId) {
            tailId = hierarchy.prevId;
        }
        if (!hierarchy.prevId.IsNull) {
            ref var prev= ref World.GetComponent<HierarchyComponent>(hierarchy.prevId);
            prev.nextId = hierarchy.nextId;
            hierarchy.prevId = EntityId.Null;
        }
        if (!hierarchy.nextId.IsNull) {
            ref var next= ref World.GetComponent<HierarchyComponent>(hierarchy.nextId);
            next.prevId = hierarchy.prevId;
            hierarchy.nextId = EntityId.Null;
        }
        count--;
        return true;
    }
    
    public bool RemoveChild(int position) {
        if (position < 0 || position > count) {
            throw new ArgumentOutOfRangeException();
        }
        ref var hierarchy = ref GetChild(position);
        var result = _RemoveChild(ref hierarchy);
        Entity.Modify<HierarchyComponent>();
        return result;
    }
    
    public bool RemoveChild(ref HierarchyComponent child) {
        if (count == 0) {
            throw new ArgumentOutOfRangeException();
        }
        var childId = child.Entity.Id;
        for (var id = headId; id != tailId; ) {
            ref var hierarchy = ref World.GetComponent<HierarchyComponent>(id);
            if (id != childId) {
                id = hierarchy.nextId;
                continue;
            }
            var result = _RemoveChild(ref hierarchy);
            Entity.Modify<HierarchyComponent>();
            return result;
        }
        return false;
    }
    
    public bool RemoveChild(in Entity child) {
        if (child.IsNull || !child.Has<HierarchyComponent>()) {
            return false;
        }
        ref var hierarchy = ref child.Get<HierarchyComponent>(); 
        return _RemoveChild(ref hierarchy);
    }

    public void SetParent(in Entity parent) {
        if (parent.Id == parentId) {
            return;
        }
        if (!parentId.IsNull) {
            ref var oldParentHierarchy = ref World.GetComponent<HierarchyComponent>(parentId);
            oldParentHierarchy._RemoveChild(ref this);
        }
        ref var newParentHierarchy = ref parent.Offer<HierarchyComponent>();
        newParentHierarchy.AddChild(ref this);
    }

    public struct HierarchyEnumerator : IEnumerator<ComponentPtr<HierarchyComponent>> {
        private readonly World world;
        private readonly EntityId headId;
        private EntityId currentId;
        private EntityId nextId;

        public HierarchyEnumerator(World world, EntityId headId) {
            this.world = world;
            this.headId = headId;
            this.currentId = headId;
            this.nextId = headId;
        }

        public bool MoveNext() {
            if(currentId.IsNull) {
                return false;
            }
            currentId = nextId;
            ref var hierarchy = ref world.GetComponent<HierarchyComponent>(currentId);
            nextId = hierarchy.nextId;
            return true;
        }

        public void Reset() { currentId = headId; }

        public ComponentPtr<HierarchyComponent> Current => world.PtrComponent<HierarchyComponent>(currentId);

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }

    public struct EntityEnumerator : IEnumerator<Entity> {
        private readonly World world;
        private readonly EntityId headId;
        private EntityId currentId;
        private EntityId nextId;
            
        public EntityEnumerator(World world, EntityId headId) {
            this.world = world;
            this.headId = headId;
            this.currentId = headId;
            this.nextId = headId;
        }
            
        public bool MoveNext() {
            if(nextId.IsNull) {
                return false;
            }
            currentId = nextId;
            ref var hierarchy = ref world.GetComponent<HierarchyComponent>(currentId);
            nextId = hierarchy.nextId;
            return true;
        }

        public void Reset() { currentId = headId; nextId = headId; }

        public Entity Current => new(world, currentId);

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }
        
    public readonly struct EnumerableEntities : IEnumerable<Entity> {
        private readonly World world;
        private readonly EntityId headId;

        public EnumerableEntities(World world, EntityId headId) {
            this.world = world;
            this.headId = headId;
        }
            
        public EntityEnumerator GetEnumerator() => new(world, headId);
            
        IEnumerator<Entity> IEnumerable<Entity>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
        
    public readonly struct EnumerableHierarchies : IEnumerable<ComponentPtr<HierarchyComponent>> {
        private readonly World world;
        private readonly EntityId headId;

        public EnumerableHierarchies(World world, EntityId headId) {
            this.world = world;
            this.headId = headId;
        }
            
        public HierarchyEnumerator GetEnumerator() => new(world, headId);

        IEnumerator<ComponentPtr<HierarchyComponent>> IEnumerable<ComponentPtr<HierarchyComponent>>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}
