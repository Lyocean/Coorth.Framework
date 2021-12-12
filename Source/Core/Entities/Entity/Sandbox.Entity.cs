using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Coorth {
    public partial class Sandbox {
        
        #region Common

        private ChunkList<EntityContext> contexts;

        private int reusing = -1;

        private int entityCount;

        private Entity singleton;

        public int EntityCount => entityCount;

        private void InitEntities(int indexCapacity, int chunkCapacity) {
            entityCount = 0;
            contexts = new ChunkList<EntityContext>(indexCapacity, chunkCapacity);
        }

        private void ClearEntities() {
            var list = new List<EntityId>();
            for (int i = 0; i < contexts.Count; i++) {
                list.Add(contexts[i].Id);
            }

            foreach (var id in list) {
                DestroyEntity(id);
            }
        }
        
        #endregion

        #region Create Entity

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnCreateEntity(ref EntityContext context, int index, ArchetypeDefinition archetype) {
            entityCount++;
            context.Archetype = archetype;
            context.Group = archetype.AddEntity(index);
            context.Index = index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ref EntityContext CreateContext(ArchetypeDefinition archetype) {
            if (reusing >= 0) {
                ref var context = ref contexts.Ref(reusing);
                var index = reusing;
                reusing = context.Index;
                OnCreateEntity(ref context, index, archetype);
                return ref context;
            }
            else {
                var index = entityCount;
                ref var context = ref contexts.Add();
                OnCreateEntity(ref context, index, archetype);
                context.Version = 1;
                //context.Components = new Dictionary<int, int>(archetype.ComponentCapacity);
                context.Components = new IndexDict<int>(archetype.ComponentCapacity);
                return ref context;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Entity CreateEntity(ArchetypeDefinition archetype) {
            ref var context = ref CreateContext(archetype);
            var entity = context.GetEntity(this);
            Dispatch(new EventEntityAdd(entity));
            foreach (var typeId in archetype.Types) {
                var componentGroup = GetComponentGroup(typeId);
                var componentIndex = componentGroup.AddComponent(entity);
                context[typeId] = componentIndex;
                componentGroup.OnComponentAdd(entity.Id, componentIndex);
            }

            return entity;
        }

        public Entity CreateEntity() {
            return CreateEntity(emptyArchetype);
        }

        internal void CreateEntities(Span<Entity> span, ArchetypeDefinition archetype, int count) {
            for (var i = 0; i < count; i++) {
                span[i] = CreateEntity(archetype);
            }
        }

        public void CreateEntities(Span<Entity> span, int count) {
            for (var i = 0; i < count; i++) {
                span[i] = CreateEntity();
            }
        }

        public Entity[] CreateEntities(int count) {
            var array = new Entity[count];
            CreateEntities(array.AsSpan(), count);
            return array;
        }

        public Entity Singleton() {
            if (singleton.IsNull) {
                singleton = CreateEntity();
            }

            return singleton;
        }

        #endregion

        #region Has & Get Entity

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ref EntityContext GetContext(int index) {
            return ref contexts.Ref(index);
        }

        public bool HasEntity(EntityId id) {
            if (id.Index >= contexts.Count) {
                return false;
            }
            ref var context = ref contexts.Ref(id.Index);
            return context.Index == id.Index && context.Version == id.Version;
        }

        public bool HasEntity(Entity entity) {
            return ReferenceEquals(entity.Sandbox, this) && HasEntity(entity.Id);
        }

        public Entity GetEntity(EntityId id) {
            if (HasEntity(id)) {
                return new Entity(this, id);
            }
            return Entity.Null;
        }

        public Entity GetEntity(in int index) {
            ref var context = ref contexts.Ref(index);
            return context.GetEntity(this);
        }

        public EntityCollection GetEntities(EntityMatcher matcher) {
            var archetypeGroup = GetArchetypeGroup(matcher);
            return new EntityCollection(archetypeGroup);
        }

        public IEnumerable<Entity> GetEntities() {
            var count = contexts.Count;
            for (var i = 0; i < count; i++) {
                var entity = contexts[i].GetEntity(this);
                if (!entity.IsNull) {
                    yield return entity;
                }
            }
        }

        #endregion

        #region Destroy Entity

        public bool DestroyEntity(EntityId id) {
            if (!HasEntity(id)) {
                return false;
            }

            Dispatch(new EventEntityRemove(new Entity(this, id)));
            ClearComponent(id);

            ref var context = ref contexts.Ref(id.Index);
            if (context.Index == id.Index && context.Version == id.Version) {
                context.Version++;
                context.Components.Clear();
                context.Index = reusing;
                reusing = id.Index;
                entityCount--;
                return true;
            }
            else {
                return false;
            }
        }

        public bool DestroyEntity(Entity entity) {
            if (!ReferenceEquals(entity.Sandbox, this)) {
                throw new ArgumentException($"Entity not belong to this sandbox: {entity}");
            }
            return DestroyEntity(entity.Id);
        }

        #endregion

        #region Clone Entity

        public EntityId CloneEntity(EntityId id) {
            ref var srcContext = ref GetContext(id.Index);
            ref var dstContext = ref CreateContext(srcContext.Archetype);

            foreach (var pair in srcContext.Components) {
                var componentGroup = GetComponentGroup(pair.Key);
                var srcComponentIndex = pair.Value; 
                dstContext.Components[pair.Value] = componentGroup.CloneComponent(dstContext.GetEntity(this), srcComponentIndex);
            }

            foreach (var pair in dstContext.Components) {
                var componentGroup = GetComponentGroup(pair.Key);
                var dstComponentIndex = pair.Value;
                componentGroup.OnComponentAdd(dstContext.Id, dstComponentIndex);
            }

            Dispatch(new EventEntityAdd(dstContext.GetEntity(this)));
            return dstContext.Id;
        }

        public Entity CloneEntity(Entity entity) {
            var dstId = CloneEntity(entity.Id);
            return new Entity(this, dstId);
        }

        #endregion
    }
}