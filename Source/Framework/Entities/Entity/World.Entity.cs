using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Coorth.Collections;

namespace Coorth.Framework; 

public partial class World {
        
    #region Common
    
    private ChunkList<EntityContext> contexts;

    private int reusingIndex = -1;

    private int reusingCount;

    private int entityCount;

    private Entity singleton;

    public int EntityCount => entityCount;

    private void InitEntities(int indexCapacity, int chunkCapacity) {
        entityCount = 0;
        contexts = new ChunkList<EntityContext>(indexCapacity, chunkCapacity);
    }

    private void ClearEntities() {
        for (var i = 0; i < contexts.Count; i++) {
            ref var context = ref contexts.Ref(i);
            _DestroyEntity(ref context);
        }
        contexts.Clear();
    }

    public void Clear() {
        ClearEntities();
    }

    #endregion

    #region Create Entity
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ref EntityContext CreateContext() {
        if (reusingIndex >= 0) {
            ref var context = ref contexts.Ref(reusingIndex);
            (reusingIndex, context.Index) = (context.Index, reusingIndex);
            entityCount++;
            return ref context;
        }
        else {
            ref var context = ref contexts.Add();
            context.Index = entityCount++;
            context.Version = 1;
            return ref context;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Entity CreateEntity(ArchetypeDefinition archetype) {
        ref var context = ref CreateContext();
        archetype.EntityCreate(ref context);
        var entity = context.GetEntity(this);
        Dispatch(new EntityCreateEvent(entity));
        return entity;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Entity CloneEntity(EntityId id) {
        ref var srcContext = ref GetContext(id.Index);
        ref var dstContext = ref CreateContext();
        var archetype = srcContext.Archetype;
        archetype.EntityClone(ref srcContext, ref dstContext);
        var dstEntity = dstContext.GetEntity(this);
        Dispatch(new EntityCreateEvent(dstEntity));
        return dstEntity;
    }

    internal void CreateEntities(ArchetypeDefinition archetype, Span<Entity> span) {
        for (var i = 0; i < span.Length; i++) {
            span[i] = CreateEntity(archetype);
        }
    }
    
    internal void CreateEntities(ArchetypeDefinition archetype, IList<Entity> list, int start, int count) {
        for (var i = start; i < count; i++) {
            list[i] = CreateEntity(archetype);
        }
    }

    internal void CreateEntities(ArchetypeDefinition archetype, IList<Entity> list, int count) {
        for (var i = 0; i < count; i++) {
            list.Add(CreateEntity(archetype));
        }
    }
    
    public Entity CreateEntity() => CreateEntity(emptyArchetype);

    public Entity CloneEntity(Entity entity) => CloneEntity(entity.Id);
    
    public void CreateEntities(Span<Entity> span) => CreateEntities(emptyArchetype, span);

    public Entity[] CreateEntities(int count) {
        var array = new Entity[count];
        CreateEntities(array.AsSpan());
        return array;
    }

    public void CreateEntities(IList<Entity> list, int start, int count) => CreateEntities(emptyArchetype, list, start, count);

    public Entity Singleton() {
        if (singleton.IsNull) {
            singleton = CreateEntity();
        }
        return singleton;
    }

    #endregion

    #region Has & Get Entity

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ref EntityContext GetContext(int index) => ref contexts.Ref(index);

    public bool HasEntity(EntityId id) {
        if (id.Index >= contexts.Count) {
            return false;
        }
        ref var context = ref contexts.Ref(id.Index);
        return context.Index == id.Index && context.Version == id.Version;
    }

    public bool HasEntity(Entity entity) => ReferenceEquals(entity.World, this) && HasEntity(entity.Id);

    public Entity GetEntity(EntityId id) => HasEntity(id) ? new Entity(this, id) : Entity.Null;


    public Entity GetEntity(in int index) {
        ref var context = ref contexts.Ref(index);
        return context.GetEntity(this);
    }

    public Entity? FindEntity(EntityId id) => HasEntity(id) ? new Entity(this, id) : null;

    public EntityCollection GetEntities(ArchetypeMatcher matcher) {
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void _DestroyEntity(ref EntityContext context) {
        var entity = context.GetEntity(this);
        Dispatch(new EntityRemoveEvent(entity));
        
        _ClearComponents(ref context, in entity);
        context.Archetype.EntityRemove(ref context, emptyArchetype);
        
        context.Version++;
        context.Index = reusingIndex;
        reusingIndex = context.Index;
        entityCount--;
    }
    
    public bool DestroyEntity(EntityId id) {
        if (id.Index >= contexts.Count) {
            return false;
        }
        ref var context = ref contexts.Ref(id.Index);
        if (context.Index != id.Index || context.Version != id.Version) {
            return false;
        }
        _DestroyEntity(ref context);
        return true;
    }

    public bool DestroyEntity(Entity entity) {
        if (!ReferenceEquals(entity.World, this)) {
            throw new ArgumentException($"Entity not belong to this world: {entity}");
        }
        return DestroyEntity(entity.Id);
    }

    #endregion
}