using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Coorth.ECS {
    internal struct EntityData {
        public int Index;
        public int Version;
        public Dictionary<int, int> Components;
        public Entity Entity;
        public EntityId Id => new EntityId(Index, Version);
    }

    internal class Entities {

        private int currentId = 0;

        private Stack<int> resumeIds = new Stack<int>();
        private ChunkList<EntityData> datas;
        private Stack<Entity> resumes = new Stack<Entity>();
        private Dictionary<Type, Entity> singletons = new Dictionary<Type, Entity>();
        private int count = 0;
        public int Count => count;

        public Entities(EcsContainer container) {
            datas = new ChunkList<EntityData>(container.config.EntityCapacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref EntityData GetData(int index) {
            return ref datas[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Entity CreateEntity() {
            return resumes.TryPop(out var entity) ? entity : new Entity();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RecycleEntity(Entity entity) {
            resumes.Push(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityContext Create(EcsContainer container, Entity entity) {
            if (resumeIds.TryPop(out var index)) {
                ref var data = ref datas[index];
                var id = new EntityId(index, data.Version);
                var context = new EntityContext(id, container);
                data.Entity = entity;
                Interlocked.Increment(ref count);
                return context;
            } else {
                index = Interlocked.Increment(ref currentId);
                var id = new EntityId(index, 0);
                var context = new EntityContext(id, container);
                ref var data = ref datas.Alloc(index);
                data.Index = index;
                data.Version = 0;
                data.Components = new Dictionary<int, int>();
                data.Entity = entity;
                Interlocked.Increment(ref count);
                return context;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<EntityId> GetEntityIds() {
            for(var i =0;i< datas.Items.Length; i++) {
                if(datas.Items[i].Index != 0) {
                    yield return datas.Items[i].Id;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Singleton<T>(EcsContainer container) where T : Entity, new() {
            if(singletons.TryGetValue(typeof(T), out var entity)) {
                return (T)entity;
            }
            entity = new T();
            Create(container, entity);
            return (T)entity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Destroy(EcsContainer container, EntityId id, out Entity entity) {
            if (id.Index >= datas.Items.Length) {
                entity = null;
                return false;
            }
            ref var data = ref datas[id.Index];
            if(data.Index == id.Index && data.Version == id.Version) {
                entity = data.Entity;
                data.Entity = null;
                data.Version++;
                resumeIds.Push(id.Index);
                Interlocked.Decrement(ref count);
                return true;
            } else {
                entity = null;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Has(EntityId id) {
            if(id.Index >= datas.Items.Length) {
                return false;
            }
            ref var data = ref datas[id.Index];
            return data.Index == id.Index && data.Version == id.Version;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityContext GetContext(EcsContainer container, EntityId id) {
            if (id.Index >= datas.Items.Length) {
                return new EntityContext(EntityId.Null, null);
            }
            ref var data = ref datas[id.Index];
            if(data.Index == id.Index && data.Version == id.Version) {
                return new EntityContext(new EntityId(data.Index, data.Version), container);
            }
            return new EntityContext(EntityId.Null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Entity GetEntity(EntityId id) {
            if (id.Index >= datas.Items.Length) {
                return null;
            }
            ref var data = ref datas[id.Index];
            if (data.Index == id.Index && data.Version == id.Version) {
                return data.Entity;
            }
            return null;
        }
    }
}
