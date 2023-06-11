using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Coorth.Serialize;


namespace Coorth.Framework; 

internal struct EntityContext {
    
    public int Index;

    public int Version;
    
    /// <summary> Index in archetype </summary>
    public int LocalIndex;

    /// <summary> Index in space </summary>
    public int SpaceIndex;

    public Archetype Archetype;

    public Space Space;

    public uint Flags;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(in ComponentType type) => Archetype.Offset.ContainsKey(type);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Get(in ComponentType type) => Archetype.GetComponent(LocalIndex, in type);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGet(in ComponentType type, out int component_index) => Archetype.TryGetComponent(LocalIndex, in type, out component_index);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(in ComponentType type, int component_index) => Archetype.SetComponent(LocalIndex, in type, component_index);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<int> GetSpan() => Archetype.GetEntitySpan(LocalIndex);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetFlags(int flag, bool value) => Flags = value ? (Flags | (1u << flag)) : (Flags & ~(1u << flag));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool GetFlags(int flag) => (Flags & (~(1u << flag))) != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsActive() => EntityFlags.IsEntityActive(Flags);
}

internal readonly struct EntityChunk {
    public readonly EntityContext[] Values;

    public EntityChunk(int capacity) {
        Values = new EntityContext[capacity];
    }
}

internal struct EntityTable {
    
    internal EntityChunk[] chunks;

    public int Count;

    public const int CHUNK_SIZE = 4096;

    private int reusing;

    public int Capacity => chunks.Length * CHUNK_SIZE;

    public EntityTable() {
        Count = 0;
        reusing = -1;
        chunks = new[] { new EntityChunk(CHUNK_SIZE) };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ref EntityContext Add(int entity_index) {
        var chunk_index = entity_index / CHUNK_SIZE;
        var value_index = entity_index % CHUNK_SIZE;
        if (chunk_index >= chunks.Length) {
            Array.Resize(ref chunks, chunks.Length + 1);
            chunks[chunk_index] = new EntityChunk(CHUNK_SIZE);
        }
        ref var chunk = ref chunks[chunk_index];
        return ref chunk.Values[value_index];
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref EntityContext Create() {
        if (reusing >= 0) {
            var index = reusing;
            ref var entity_context = ref Get(index);
            reusing = entity_context.Index;
            entity_context.Index = index;
            entity_context.Version = -entity_context.Version;
            Count++;
            return ref entity_context;
        }
        else {
            var index = Count;
            ref var entity_context = ref Add(index);
            entity_context.Index = index;
            entity_context.Version = 1;
            Count++;
            return ref entity_context;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref EntityContext Get(int entity_index) {
        var chunk_index = entity_index / CHUNK_SIZE;
        var value_index = entity_index % CHUNK_SIZE;
        ref var chunk = ref chunks[chunk_index];
        return ref chunk.Values[value_index];
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref EntityContext Get(in EntityId id) {
        var chunk_index = id.Index / CHUNK_SIZE;
        var value_index = id.Index % CHUNK_SIZE;
        ref var chunk = ref chunks[chunk_index];
        ref var value = ref chunk.Values[value_index];
        System.Diagnostics.Debug.Assert(value.Version == id.Version, $"[Entity] Entity version error: {id}");
        return ref value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(in EntityId id) {
        var index = id.Index;
        if (index >= Count) {
            return false;
        }
        ref var entity = ref Get(index);
        return entity.Version == id.Version;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref EntityContext TryGet(in EntityId id, out bool result) {
        var index = id.Index;
        if (index >= Count) {
            result = false;
            return ref Unsafe.NullRef<EntityContext>();
        }
        ref var entity_context = ref Get(index);
        result = (entity_context.Version == id.Version);
        return ref entity_context;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove(ref EntityContext entity_context) {
        entity_context.Version = -(entity_context.Version + 1);
        entity_context.Index = reusing;
        reusing = entity_context.Index;
        Count--;
    }    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove(in EntityId id) {
        var index = id.Index;
        if (index >= Count) {
            return false;
        }
        ref var entity = ref Get(index);
        if (entity.Version != id.Version) {
            return false;
        }
        entity.Version = -(entity.Version + 1);
        entity.Index = reusing;
        reusing = entity.Index;
        Count--;
        return false;
    }
}

public partial class World {


    #region Entity Common

    private EntityTable entities;

    private Entity singleton;

    public int EntityCount => entities.Count;

    private void SetupEntities() {
        entities = new EntityTable();
        singleton = CreateEntity(rootArchetype, rootSpace);
    }

    private void ClearEntities() {
        for (var i = 1; i < entities.Count; i++) {
            var chunk_index = entities.Count / EntityTable.CHUNK_SIZE;
            var value_index = entities.Count % EntityTable.CHUNK_SIZE;
            ref var entity_chunk = ref entities.chunks[chunk_index];
            ref var entity_context = ref entity_chunk.Values[value_index];
            if (entity_context.Version <= 0) {
                continue;
            }

            DestroyEntity(ref entity_context);
        }
    }

    [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Entity Cast(in EntityContext entity_context) => new(this, new EntityId(entity_context.Index, entity_context.Version));

    [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Singleton() => singleton;

    [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ref EntityContext GetContext(int entity_index) => ref entities.Get(entity_index);

    public bool IsActive(in EntityId id) {
        ref var entity_context = ref entities.Get(id.Index);
        if (entity_context.Version != id.Version) {
            return false;
        }
        return entity_context.IsActive();
    }

    public void SetFlags(in EntityId id, int flag, bool value) {
        if (flag > EntityFlags.FLAG_MAX_INDEX) {
            throw new ArgumentOutOfRangeException(nameof(flag),
                $"[Entity] flag can not larger than: {EntityFlags.FLAG_MAX_INDEX}");
        }

        ref var entity_context = ref entities.Get(id.Index);
        if (entity_context.Version != id.Version) {
            return;
        }
        entity_context.SetFlags(flag, value);
    }

    public bool GetFlags(in EntityId id, int flag) {
        if (flag > EntityFlags.FLAG_MAX_INDEX) {
            return false;
        }
        ref var entity_context = ref entities.Get(id.Index);
        if (entity_context.Version != id.Version) {
            return false;
        }
        return entity_context.GetFlags(flag);
    }

    public void SetActive(in EntityId id, bool active) {
        ref var entity_context = ref entities.Get(id.Index);
        if (entity_context.Version != id.Version) {
            return;
        }
        var old_value = entity_context.IsActive();
        EntityFlags.SetEntityActive(ref entity_context.Flags, active);
        var new_value = entity_context.IsActive();
        if (old_value == new_value) {
            return;
        }
        Dispatch(new EntityActiveEvent(Cast(in entity_context), active));
    }

    public void OnParentActive(in EntityId id, bool active) {
        ref var entity_context = ref entities.Get(id.Index);
        if (entity_context.Version != id.Version) {
            return;
        }
        var old_value = entity_context.IsActive();
        EntityFlags.SetParentActive(ref entity_context.Flags, active);
        var new_value = entity_context.IsActive();
        if (old_value == new_value) {
            return;
        }
        Dispatch(new EntityActiveEvent(Cast(in entity_context), active));
    }

    #endregion


    #region Create Entity

    public Entity CreateEntity(Archetype archetype, Space space) {
        ref var entity_context = ref entities.Create();

        entity_context.Archetype = archetype;
        entity_context.LocalIndex = archetype.AddEntity(entity_context.Index, out var offsets);

        entity_context.Space = space;
        entity_context.SpaceIndex = space.AddEntity(entity_context.Index);

        entity_context.Flags = EntityFlags.ENTITY_FLAG_DEFAULT;

        var entity = Cast(in entity_context);
        foreach (var type in archetype.Types) {
            var component_group = GetComponentGroup(in type);
            component_group.Add(entity_context.Index, in entity);
        }

        Dispatcher.Dispatch(new EntityCreateEvent(entity));

        foreach (var (type_id, component_offset) in archetype.Offset) {
            var component_group = GetComponentGroup(type_id);
            component_group.OnAdd(in entity, offsets[component_offset]);
        }

        return entity;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity CreateEntity(Archetype archetype) {
        return CreateEntity(archetype, mainSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity CreateEntity(Space space) {
        return CreateEntity(rootArchetype, space);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity CreateEntity() {
        return CreateEntity(rootArchetype, mainSpace);
    }

    public void CreateEntities(Archetype archetype, Space space, Span<Entity> span) {
        for (var i = 0; i < span.Length; i++) {
            span[i] = CreateEntity(archetype, space);
        }
    }

    public void CreateEntities(Archetype archetype, Space space, IList<Entity> list, int length) {
        for (var i = 0; i < length; i++) {
            var entity = CreateEntity(archetype, space);
            list.Add(entity);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity[] CreateEntities(Archetype archetype, Space space, int count) {
        var array = new Entity[count];
        CreateEntities(archetype, space, array.AsSpan());
        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CreateEntities(Archetype archetype, Span<Entity> span) {
        CreateEntities(archetype, mainSpace, span);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CreateEntities(Archetype archetype, IList<Entity> list, int count) {
        CreateEntities(archetype, mainSpace, list, count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity[] CreateEntities(Archetype archetype, int count) {
        return CreateEntities(archetype, mainSpace, count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CreateEntities(Space space, Span<Entity> span) {
        CreateEntities(rootArchetype, space, span);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CreateEntities(Space space, IList<Entity> list, int count) {
        CreateEntities(rootArchetype, space, list, count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity[] CreateEntities(Space space, int count) {
        return CreateEntities(rootArchetype, space, count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CreateEntities(Span<Entity> span) {
        CreateEntities(rootArchetype, mainSpace, span);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CreateEntities(IList<Entity> list, int count) {
        CreateEntities(rootArchetype, mainSpace, list, count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity[] CreateEntities(int count) {
        return CreateEntities(rootArchetype, mainSpace, count);
    }

    #endregion


    #region Has Entity

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasEntity(in EntityId id) => entities.Has(in id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasEntity(in Entity entity) => ReferenceEquals(entity.World, this) && entities.Has(in entity.Id);

    #endregion


    #region Get Entity

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity GetEntity(in EntityId id) {
        if (id.Index >= entities.Count) {
            throw new ArgumentException($"[Entity] Can't find entity:{id}");
        }

        ref var entity_context = ref entities.Get(id.Index);
        if (id.Version != entity_context.Version) {
            throw new ArgumentException($"[Entity] Can't find entity:{id}");
        }

        return new Entity(this, id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity GetEntity(in int entity_index) {
        ref var entity_context = ref entities.Get(entity_index);
        return Cast(in entity_context);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity? FindEntity(in EntityId id) {
        if (id.Index >= entities.Count) {
            return null;
        }

        ref var entity_context = ref entities.Get(id.Index);
        if (id.Version != entity_context.Version) {
            return null;
        }

        return new Entity(this, id);
    }

    public EntityCollection GetEntities() {
        return new EntityCollection(this, this.archetypes.Values, 0);
    }

    #endregion


    #region Destroy Entity

    private void DestroyEntity(ref EntityContext entity_context) {
        var entity = Cast(in entity_context);
        Dispatch(new EntityRemoveEvent(entity));
        ClearComponents(ref entity_context);

        entity_context.Archetype.RemoveEntity(entity_context.LocalIndex, out _);
        entity_context.Archetype = rootArchetype;
        entity_context.LocalIndex = -1;

        entity_context.Space.RemoveEntity(entity_context.SpaceIndex);
        entity_context.Space = default!;
        entity_context.SpaceIndex = -1;

        entities.Remove(ref entity_context);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void DestroyEntity(int entity_index) {
        ref var entity_context = ref entities.Get(entity_index);
        DestroyEntity(ref entity_context);
    }

    public bool DestroyEntity(in EntityId id) {
        ref var entity_context = ref entities.TryGet(in id, out var result);
        if (!result) {
            return false;
        }
        DestroyEntity(ref entity_context);
        return true;
    }

    #endregion


    #region Clone Entity

    private Entity _CloneEntity(ref EntityContext src_context, Space space) {
        var archetype = src_context.Archetype;
        ref var entity_context = ref entities.Create();

        var src_span = archetype.GetEntitySpan(src_context.LocalIndex);

        entity_context.Archetype = archetype;
        entity_context.LocalIndex = archetype.AddEntity(entity_context.Index, out var entity_span);

        entity_context.Space = space;
        entity_context.SpaceIndex = space.AddEntity(entity_context.Index);

        entity_context.Flags = src_context.Flags;
        
        var entity = Cast(entity_context);

        foreach (var (type, offset) in archetype.Offset) {
            var component_group = GetComponentGroup(type);
            var source_index = src_span[offset];
            var entity_index = component_group.Clone(src_context.Index, source_index, in entity);
            entity_span[offset] = entity_index;
        }

        Dispatcher.Dispatch(new EntityCreateEvent(entity));

        foreach (var (type_id, component_offset) in archetype.Offset) {
            var component_group = GetComponentGroup(type_id);
            component_group.OnClone(in entity, entity_span[component_offset]);
        }

        return entity;
    }

    public Entity CloneEntity(in EntityId id, Space space) {
        ref var entity_context = ref entities.TryGet(in id, out var result);
        if (!result) {
            throw new ArgumentException($"[Entity] Can't find entity:{id}");
        }
        return _CloneEntity(ref entity_context, space);
    }

    public Entity CloneEntity(in EntityId id) {
        ref var entity_context = ref entities.TryGet(in id, out var result);
        if (!result) {
            throw new ArgumentException($"[Entity] Can't find entity:{id}");
        }
        return _CloneEntity(ref entity_context, entity_context.Space);
    }

    #endregion


    #region Pack Entity

    public EntityPack PackEntity(in EntityId id) {
        ref var entity_context = ref entities.Get(id.Index);
        if (id.Version != entity_context.Version) {
            throw new ArgumentException($"[Entity] Entity has destroyed: {id}");
        }

        var archetype = entity_context.Archetype;
        var entity = Cast(in entity_context);
        var entity_pack = new EntityPack(archetype.Definition);
        var entity_span = archetype.GetEntitySpan(entity_context.LocalIndex);
        var index = 0;
        foreach (var (type, offset) in archetype.Offset) {
            var component_group = GetComponentGroup(type);
            var component_index = entity_span[offset];
            var component_pack = component_group.Pack(entity, component_index);
            entity_pack.Components[index++] = component_pack;
        }

        return entity_pack;
    }

    public Entity UnPackEntity(EntityPack pack, Space space) {
        var archetype = CreateArchetype(pack.Archetype);

        ref var entity_context = ref entities.Create();

        entity_context.Archetype = archetype;
        entity_context.LocalIndex = archetype.AddEntity(entity_context.Index, out var components);

        entity_context.Space = space;
        entity_context.SpaceIndex = space.AddEntity(entity_context.Index);

        entity_context.Flags = 0;

        var entity = Cast(in entity_context);
        foreach (var component_pack in pack.Components) {
            var component_group = GetComponentGroup(component_pack.Type);
            var component_index = component_group.UnPack(in entity, entity_context.Index, component_pack);
            components[archetype.Offset[component_group.Type]] = component_index;
        }

        Dispatcher.Dispatch(new EntityCreateEvent(entity));

        foreach (var (type_id, component_offset) in archetype.Offset) {
            var component_group = GetComponentGroup(type_id);
            component_group.OnAdd(in entity, components[component_offset]);
        }

        return entity;
    }

    #endregion


    #region Serialize Entity

    public void ReadEntity<TReader>(TReader reader) where TReader : ISerializeReader {
        
    }
    
    public void WriteEntity<TWriter>(TWriter writer, EntityId id) where TWriter : ISerializeWriter {
        writer.WriteInt64((long)id);
    }

    #endregion
}



//
// public void ReadEntity(ISerializeReader reader, EntityId entityId) {
//     reader.BeginData(typeof(Entity));
//     var count = reader.ReadField<int>(nameof(Entity.Count), 1);
//     reader.ReadTag(nameof(Archetype.Components), 2);
//     reader.BeginDict(typeof(Type), typeof(IComponent), out var _);
//     for (var i = 0; i < count; i++) {
//         var type = reader.ReadKey<Type>();
//         ReadComponent(reader, entityId, type);
//     }
//
//     reader.EndDict();
//     reader.EndData();
// }
//
// public Entity ReadEntity(ISerializeReader reader) {
//     var entity = CreateEntity();
//     ReadEntity(reader, entity.Id);
//     return entity;
// }
//
// public void WriteEntity(ISerializeWriter writer, in EntityId entityId) {
//     writer.BeginData<Entity>(3);
//     {
//         ref var context = ref entities.Get(entityId.Index);
//         
//         writer.WriteField(nameof(Entity.Id), 1, entityId);
//         writer.WriteField(nameof(Entity.Count), 2, context.Count);
//         
//         writer.WriteTag(nameof(Archetype.Components), 3);
//         writer.BeginData<IComponent>((short)context.Archetype.Types.Length);
//         for (var i = 0; i < context.Archetype.Types.Length; i++) {
//             var typeId = context.Archetype.Types[i];
//             var group = GetComponentGroup(typeId);
//             var index = context.Get(typeId);
//             writer.WriteTag(group.Type.Name, i);
//             group.Write(writer, index);
//         }
//         writer.EndData();
//     }
//     writer.EndData();
// }