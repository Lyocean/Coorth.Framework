using System;
using System.Collections;
using System.Collections.Generic;

namespace Coorth.Common {
    [Component, StoreContract("C30A217A-4660-402A-A993-BA4820389F0B")]
    public struct HierarchyComponent : IComponent {
        
        public Entity Entity { get; private set; }

        public Sandbox Sandbox => Entity.Sandbox;

        private EntityId parentId;

        private int count;
        
        private EntityId headId, tailId;

        private EntityId prevId, nextId;
        
        public int Count => count;
        
        public Entity ParentEntity => Sandbox.GetEntity(parentId);

        public ref HierarchyComponent ParentHierarchy => ref Sandbox.GetComponent<HierarchyComponent>(parentId);

        public EnumerableEntities GetChildrenEntities() => new EnumerableEntities(this.Entity.Sandbox, headId);
        
        public EnumerableHierarchies GetChildrenHierarchies() => new EnumerableHierarchies(this.Entity.Sandbox, headId);
        
        public void OnAdd(Entity entity) {
            this.Entity = entity;
        }
        
        public void OnRemove() {
            if (parentId.IsNotNull) {
                ref var hierarchy = ref Sandbox.GetComponent<HierarchyComponent>(parentId);
                hierarchy._RemoveChild(ref this);
                parentId = EntityId.Null;
            }
            this.Entity = Entity.Null;
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
            if (this.tailId.IsNull) {
                this.headId = hierarchy.Entity.Id;
                this.tailId = this.headId;
            }
            else {
                ref var tailHierarchy = ref Sandbox.GetComponent<HierarchyComponent>(this.tailId);
                hierarchy.prevId = this.tailId;
                tailHierarchy.nextId = hierarchy.Entity.Id;
                this.tailId = hierarchy.Entity.Id;
            }
        }
        
        public void AddChild(in Entity child) {
            AddChild(ref child.Offer<HierarchyComponent>());
        }

        public void AddChild(ref HierarchyComponent child) {
            if (child.parentId == this.Entity.Id) {
                return;
            }
            if (child.parentId.IsNotNull) {
                ref var hierarchy = ref Sandbox.GetComponent<HierarchyComponent>(child.parentId);
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
                ref var hierarchy = ref Sandbox.GetComponent<HierarchyComponent>(child.parentId);
                hierarchy._RemoveChild(ref child);
            }
            count++;
            child.parentId = this.Entity.Id;
            if (count == 1) {
                this.headId = child.Entity.Id;
                this.tailId = this.headId;
            }
            else {
                var id = this.headId;
                ref var prev = ref Sandbox.GetComponent<HierarchyComponent>(id);
                ref var next = ref Sandbox.GetComponent<HierarchyComponent>(prev.nextId);
                while (position > 0) {
                    id = prev.nextId;
                    prev = next;
                    next = ref Sandbox.GetComponent<HierarchyComponent>(id);
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
            var id = this.headId;
            ref var hierarchy = ref Sandbox.GetComponent<HierarchyComponent>(id);
            for (var i = 1; i < position; i++) {
                id = hierarchy.nextId;
                hierarchy = ref Sandbox.GetComponent<HierarchyComponent>(id);
            }
            return ref hierarchy;
        }

        private bool _RemoveChild(ref HierarchyComponent hierarchy) {
            var childId = hierarchy.Entity.Id;
            hierarchy.parentId = EntityId.Null;
            if (this.headId == childId) {
                this.headId = hierarchy.nextId;
            }
            if (this.tailId == childId) {
                this.tailId = hierarchy.prevId;
            }
            if (!hierarchy.prevId.IsNull) {
                ref var prev= ref Sandbox.GetComponent<HierarchyComponent>(hierarchy.prevId);
                prev.nextId = hierarchy.nextId;
                hierarchy.prevId = EntityId.Null;
            }
            if (!hierarchy.nextId.IsNull) {
                ref var next= ref Sandbox.GetComponent<HierarchyComponent>(hierarchy.nextId);
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
                ref var hierarchy = ref Sandbox.GetComponent<HierarchyComponent>(id);
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
            if (parent.Id == this.parentId) {
                return;
            }
            if (!this.parentId.IsNull) {
                ref var oldParentHierarchy = ref Sandbox.GetComponent<HierarchyComponent>(this.parentId);
                oldParentHierarchy._RemoveChild(ref this);
            }
            ref var newParentHierarchy = ref parent.Offer<HierarchyComponent>();
            newParentHierarchy.AddChild(ref this);
        }

        public struct HierarchyEnumerator : IEnumerator<ComponentPtr<HierarchyComponent>> {
            private readonly Sandbox sandbox;
            private readonly EntityId headId;
            private EntityId currentId;
            private EntityId nextId;

            public HierarchyEnumerator(Sandbox sandbox, EntityId headId) {
                this.sandbox = sandbox;
                this.headId = headId;
                this.currentId = headId;
                this.nextId = headId;
            }

            public bool MoveNext() {
                if(currentId.IsNull) {
                    return false;
                }
                currentId = nextId;
                ref var hierarchy = ref sandbox.GetComponent<HierarchyComponent>(currentId);
                nextId = hierarchy.nextId;
                return true;
            }

            public void Reset() { currentId = headId; }

            public ComponentPtr<HierarchyComponent> Current => sandbox.PtrComponent<HierarchyComponent>(currentId);

            object IEnumerator.Current => Current;

            public void Dispose() { }
        }

        public struct EntityEnumerator : IEnumerator<Entity> {
            private readonly Sandbox sandbox;
            private readonly EntityId headId;
            private EntityId currentId;
            private EntityId nextId;
            
            public EntityEnumerator(Sandbox sandbox, EntityId headId) {
                this.sandbox = sandbox;
                this.headId = headId;
                this.currentId = headId;
                this.nextId = headId;
            }
            
            public bool MoveNext() {
                if(nextId.IsNull) {
                    return false;
                }
                currentId = nextId;
                ref var hierarchy = ref sandbox.GetComponent<HierarchyComponent>(currentId);
                nextId = hierarchy.nextId;
                return true;
            }

            public void Reset() { currentId = headId; this.nextId = headId; }

            public Entity Current => new Entity(sandbox, currentId);

            object IEnumerator.Current => Current;

            public void Dispose() { }
        }
        
        public readonly struct EnumerableEntities : IEnumerable<Entity> {
            private readonly Sandbox sandbox;
            private readonly EntityId headId;

            public EnumerableEntities(Sandbox sandbox, EntityId headId) {
                this.sandbox = sandbox;
                this.headId = headId;
            }
            
            public EntityEnumerator GetEnumerator() => new EntityEnumerator(sandbox, headId);
            
            IEnumerator<Entity> IEnumerable<Entity>.GetEnumerator() => GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        
        public readonly struct EnumerableHierarchies : IEnumerable<ComponentPtr<HierarchyComponent>> {
            private readonly Sandbox sandbox;
            private readonly EntityId headId;

            public EnumerableHierarchies(Sandbox sandbox, EntityId headId) {
                this.sandbox = sandbox;
                this.headId = headId;
            }
            
            public HierarchyEnumerator GetEnumerator() => new HierarchyEnumerator(sandbox, headId);

            IEnumerator<ComponentPtr<HierarchyComponent>> IEnumerable<ComponentPtr<HierarchyComponent>>.GetEnumerator() => GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

    }
}