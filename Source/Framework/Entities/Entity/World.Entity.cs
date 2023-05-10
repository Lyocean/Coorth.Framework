using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Coorth.Collections;

namespace Coorth.Framework; 

public partial class World {
        
    #region Common
    
    private ChunkList<EntityContext> contexts;

    private int reusingIndex = -1;

    // private int reusingCount;

    private int entityCount;

    private Entity singleton;

    public int EntityCount => entityCount;

    private void InitEntities(int indexCapacity, int chunkCapacity) {
        entityCount = 0;
        contexts = new ChunkList<EntityContext>(indexCapacity, chunkCapacity);
    }

    private void ClearEntities() {
        var id = Singleton().Id;
        for (var i = 0; i < contexts.Count; i++) {
            ref var context = ref contexts.Ref(i);
            if (context.Id == id) {
                continue;
            }
            _DestroyEntity(ref context);
        }
    }

    public void Clear() {
        ClearEntities();
    }

    #endregion

    #region Create Entity
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ref EntityContext CreateContext(int spaceId) {
        if (reusingIndex >= 0) {
            ref var context = ref contexts.Ref(reusingIndex);
            (reusingIndex, context.Index) = (context.Index, reusingIndex);
            entityCount++;
            context.Version = -context.Version;
            context.SpaceIndex = spaceId >= 0? spaceId : defaultSpace.Id;
            return ref context;
        }
        else {
            ref var context = ref contexts.Add();
            context.Index = entityCount++;
            context.Version = 1;
            context.SpaceIndex = spaceId >= 0? spaceId : defaultSpace.Id;
            return ref context;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Entity CreateEntity(ArchetypeDefinition archetype, Space? space = null) {
        ref var context = ref CreateContext(space?.Id ?? -1);
        archetype.EntityCreate(ref context);
        var entity = context.GetEntity(this);
        AddEntityToSpace(context.SpaceIndex, entity.Id);
        Dispatch(new EntityCreateEvent(entity));
        return entity;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Entity CloneEntity(EntityId id) {
        ref var srcContext = ref GetContext(id.Index);
        ref var dstContext = ref CreateContext(srcContext.SpaceIndex);
        var archetype = srcContext.Archetype;
        archetype.EntityClone(ref srcContext, ref dstContext);
        var dstEntity = dstContext.GetEntity(this);
        AddEntityToSpace(dstContext.SpaceIndex, dstEntity.Id);
        Dispatch(new EntityCreateEvent(dstEntity));
        return dstEntity;
    }

    internal void CreateEntities(ArchetypeDefinition archetype, Span<Entity> span, Space? space = null) {
        for (var i = 0; i < span.Length; i++) {
            span[i] = CreateEntity(archetype, space);
        }
    }
    
    internal void CreateEntities(ArchetypeDefinition archetype, IList<Entity> list, int start, int count, Space? space = null) {
        for (var i = start; i < count; i++) {
            list[i] = CreateEntity(archetype, space);
        }
    }

    internal void CreateEntities(ArchetypeDefinition archetype, IList<Entity> list, int count, Space? space = null) {
        for (var i = 0; i < count; i++) {
            list.Add(CreateEntity(archetype, space));
        }
    }
    
    public Entity CreateEntity(Space? space = null) => CreateEntity(emptyArchetype, space);
    
    public Entity CloneEntity(Entity entity) => CloneEntity(entity.Id);
    
    public void CreateEntities(Span<Entity> span, Space? space = null) => CreateEntities(emptyArchetype, span, space);

    public Entity[] CreateEntities(int count, Space? space = null) {
        var array = new Entity[count];
        CreateEntities(array.AsSpan(), space);
        return array;
    }

    public void CreateEntities(IList<Entity> list, int start, int count, Space? space = null) => CreateEntities(emptyArchetype, list, start, count, space);

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
        // context.Index
        if (context.Version < 0) {
            return;
        }
        
        var entity = context.GetEntity(this);
        Dispatch(new EntityRemoveEvent(entity));
        
        _ClearComponents(ref context, in entity);
        context.Archetype.EntityRemove(ref context, emptyArchetype);
        RemoveEntityFromSpace(context.SpaceIndex, entity.Id);
        context.Version = -(context.Version + 1);
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