using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Coorth.Collections;

namespace Coorth.Framework; 

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
        for (var i = 0; i < contexts.Count; i++) {
            list.Add(contexts[i].Id);
        }
        foreach (var id in list) {
            DestroyEntity(id);
        }
    }
        
    #endregion

    #region Create Entity

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ref EntityContext CreateContext() {
        if (reusing >= 0) {
            ref var context = ref contexts.Ref(reusing);
            (reusing, context.Index) = (context.Index, reusing);
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
        Dispatch(new EventEntityCreate(entity));
        return entity;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Entity CloneEntity(EntityId id) {
        ref var srcContext = ref GetContext(id.Index);
        ref var dstContext = ref CreateContext();
        var archetype = srcContext.Archetype;
        archetype.EntityClone(ref srcContext, ref dstContext);
        var dstEntity = dstContext.GetEntity(this);
        Dispatch(new EventEntityCreate(dstEntity));
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

    public bool HasEntity(Entity entity) => ReferenceEquals(entity.Sandbox, this) && HasEntity(entity.Id);

    public Entity GetEntity(EntityId id) => HasEntity(id) ? new Entity(this, id) : Entity.Null;

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
        if (id.Index >= contexts.Count) {
            return false;
        }
        ref var context = ref contexts.Ref(id.Index);
        if (context.Index != id.Index || context.Version != id.Version) {
            return false;
        }

        var entity = new Entity(this, id);
        Dispatch(new EventEntityRemove(entity));
        
        _ClearComponents(ref context, in entity);
        context.Archetype.EntityRemove(ref context, emptyArchetype);
        
        context.Version++;
        context.Index = reusing;
        reusing = id.Index;
        entityCount--;
        return true;
    }

    public bool DestroyEntity(Entity entity) {
        if (!ReferenceEquals(entity.Sandbox, this)) {
            throw new ArgumentException($"Entity not belong to this sandbox: {entity}");
        }
        return DestroyEntity(entity.Id);
    }

    #endregion
}