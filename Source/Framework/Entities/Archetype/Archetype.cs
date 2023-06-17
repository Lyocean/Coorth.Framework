using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Coorth.Collections;


namespace Coorth.Framework;

public struct ArchetypeChunk {
    
    public readonly int[] Values;

    public readonly int Step;
    
    public int Count;
    
    public ArchetypeChunk(int capacity, int step) {
        Values = new int[capacity];
        Step = step;
        Count = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<int> Get(int index) {
        return Values.AsSpan(index * Step, Step);
    }
}

public class Archetype {

    #region Common

    public readonly int ChunkCapacity;

    public readonly World World;

    public readonly ArchetypeDefinition Definition;
    
    private ArchetypeChunk[] chunks;
    
    private int entityCount;
    public int EntityCount => entityCount;

    public int Capacity => chunks.Length * ChunkCapacity;

    public int ChunkCount => chunks.Length;
    
    private readonly Dictionary<ComponentType, Archetype> edges = new();

    public readonly int EntitySize;

    private readonly int chunkSize;
    
    public IReadOnlyList<ComponentType> Types { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Definition.Types; }

    public IReadOnlyDictionary<ComponentType, int> Offset { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Definition.Offset; }

    // private readonly List<ComponentGroup> groups = new();
    // public IReadOnlyList<ComponentGroup> Groups => groups;

    public int ComponentCount { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Definition.Types.Length; }

    public int Hash { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Definition.Hash; }

    public ComponentMask Mask { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Definition.Mask; }

    public Archetype? Next = null;
    
    internal Archetype(World world, ArchetypeDefinition definition, int chunk_size) {
        World = world;
        Definition = definition;
        EntitySize = definition.Types.Length + 1;
        ChunkCapacity = chunk_size / EntitySize;
        chunks = Array.Empty<ArchetypeChunk>();
        chunkSize = chunk_size;
        // foreach (var type in Definition.Types) {
            // var group = world.GetComponentGroup(in type);
            // groups.Add(group);
        // }
    }
    
    internal Archetype NextArchetype(in ComponentType type) {
        if (edges.TryGetValue(type, out var archetype)) {
            return archetype;
        }
        var definition = Definition.Next(in type);
        archetype = new Archetype(World, definition, chunkSize);
        edges.Add(type, archetype);
        archetype.edges[type] = this;

        World.OnArchetypeAdd(archetype);
        return archetype;
    }
    
    public ref ArchetypeChunk GetChunk(int chunk_index) {
        return ref chunks[chunk_index];
    }

    #endregion


    #region Entity

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void GetIndex(int local_index, out int chunk_index, out int value_index) {
        chunk_index = local_index / ChunkCapacity;
        value_index = (local_index % ChunkCapacity) * EntitySize;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<int> GetEntitySpan(int local_index) {
        GetIndex(local_index, out var chunk_index, out var value_index);
        ref var chunk = ref chunks[chunk_index];
        return chunk.Values.AsSpan(value_index, EntitySize);
    }
    
    internal int AddEntity(int entity_index, out Span<int> entity_span) {
        var local_index = entityCount;
        entityCount++;
        GetIndex(local_index, out var chunk_index, out var value_index);
        if (chunk_index >= chunks.Length) {
            Array.Resize(ref chunks, chunks.Length + 1);
            chunks[chunk_index] = new ArchetypeChunk(ChunkCapacity * EntitySize, EntitySize);
        }
        ref var chunk = ref chunks[chunk_index];
        chunk.Count++;
        chunk.Values[value_index] = entity_index;
        entity_span = chunk.Values.AsSpan(value_index, EntitySize);
        return local_index;
    }
    
    internal void RemoveEntity(int local_index, out Span<int> entity_span) {
        entityCount--;
        GetIndex(local_index, out var chunk_index, out var value_index);
        ref var chunk = ref chunks[chunk_index];
        if (local_index == entityCount) {
            chunk.Count--;
            entity_span = chunk.Values.AsSpan(value_index, EntitySize);
            return;
        }
        var curr_span = chunk.Values.AsSpan(value_index, EntitySize);

        GetIndex(entityCount, out var tail_chunk_index, out var tail_value_index);
        ref var tail_chunk = ref chunks[tail_chunk_index];
        var tail_span = chunk.Values.AsSpan(tail_value_index, EntitySize);

        var temp_span = (stackalloc int[EntitySize]);
        curr_span.CopyTo(temp_span);
        tail_span.CopyTo(curr_span);
        temp_span.CopyTo(tail_span);
        
        tail_chunk.Count--;
        ref var entity_context = ref World.GetContext(curr_span[0]);
        entity_context.LocalIndex = local_index;

        entity_span = tail_span;
    }

    #endregion


    #region Component

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool ContainType(in ComponentType type) {
        return Offset.ContainsKey(type);
    }

    internal static void AddComponent(ref EntityContext context, in ComponentType type, int component_index) {
        var src_archetype = context.Archetype;
        context.Archetype = context.Archetype.NextArchetype(in type);

        src_archetype.RemoveEntity(context.LocalIndex, out var src_span);
        context.LocalIndex = context.Archetype.AddEntity(src_span[0], out var dst_span);

        foreach (var (src_type, src_offset) in src_archetype.Offset) {
            var dst_offset = context.Archetype.Offset[src_type];
            dst_span[dst_offset] = src_span[src_offset];
        }
        dst_span[context.Archetype.Offset[type]] = component_index;
    }

    internal static void AddComponents(ref EntityContext context, in ComponentType[] types, Span<int> component_indexes) {
        var src_archetype = context.Archetype;
        src_archetype.RemoveEntity(context.LocalIndex, out var src_span);
        for (var i = 0; i < types.Length; i++) {
            context.Archetype = context.Archetype.NextArchetype(in types[i]);
        }
        context.LocalIndex = context.Archetype.AddEntity(src_span[0], out var dst_span);
        foreach (var (src_type, src_offset) in src_archetype.Offset) {
            var dst_offset = context.Archetype.Offset[src_type];
            dst_span[dst_offset] = src_span[src_offset];
        }
        for (var i = 0; i < types.Length; i++) {
            dst_span[context.Archetype.Offset[types[i]]] = component_indexes[i];
        }
    }

    public bool HasComponent<T>() {
        var type = ComponentType<T>.Value;
        return Definition.Offset.ContainsKey(type);
    }
    
    internal static void RemoveComponent(ref EntityContext context, in ComponentType type) {
        var src_archetype = context.Archetype;
        context.Archetype = context.Archetype.NextArchetype(in type);

        src_archetype.RemoveEntity(context.LocalIndex, out var src_span);
        context.LocalIndex = context.Archetype.AddEntity(src_span[0], out var dst_span);

        foreach (var (src_type, src_offset) in src_archetype.Offset) {
            if(src_type == type) {
                continue;
            }
            var dst_offset = context.Archetype.Offset[src_type];
            dst_span[dst_offset] = src_span[src_offset];
        }
    }

    internal static void RemoveComponents(ref EntityContext context, in ComponentType[] types) {
        var src_archetype = context.Archetype;
        for (var i = 0; i < types.Length; i++) {
            context.Archetype = context.Archetype.NextArchetype(in types[i]);
        }
        src_archetype.RemoveEntity(context.LocalIndex, out var src_span);
        context.LocalIndex = context.Archetype.AddEntity(src_span[0], out var dst_span);
        foreach (var (src_type, src_offset) in src_archetype.Offset) {
            if(types.Contains(src_type)) {
                continue;
            }
            var dst_offset = context.Archetype.Offset[src_type];
            dst_span[dst_offset] = src_span[src_offset];
        }
    }
    
    internal static void ClearComponents(ref EntityContext context, Archetype root) {
        var src_archetype = context.Archetype;
        src_archetype.RemoveEntity(context.LocalIndex, out var src_span);
        context.Archetype = root;
        context.LocalIndex = context.Archetype.AddEntity(src_span[0], out var _);
    }

    internal int GetComponent(int local_index, in ComponentType type) {
        var entity_span = GetEntitySpan(local_index);
        return entity_span[Offset[type]];
    }
    
    internal void SetComponent(int local_index, in ComponentType type, int component_index) {
        var entity_span = GetEntitySpan(local_index);
        entity_span[Offset[type]] = component_index;
    }

    internal bool TryGetComponent(int local_index, in ComponentType type, out int component_index) {
        if (!Offset.TryGetValue(type, out var offset)) {
            component_index = default;
            return false;
        }
        var entity_span = GetEntitySpan(local_index);
        component_index = entity_span[offset];
        return true;
    }
    
    internal void MoveComponent(int local_index, in ComponentType type, int component_index) {
        var entity_span = GetEntitySpan(local_index);
        entity_span[Offset[type]] = component_index;
    }

    #endregion


    #region Create

    public Entity CreateEntity() {
        return World.CreateEntity(this);
    }
    
    public Entity CreateEntity(Space space) {
        return World.CreateEntity(this, space);
    }
    
    public void CreateEntities(Span<Entity> span) {
        World.CreateEntities(this, World.MainSpace, span);
    }
    
    public void CreateEntities(Space space, Span<Entity> span) {
        World.CreateEntities(this, space, span);
    }

    public Entity[] CreateEntities(int length) {
        return World.CreateEntities(this, World.MainSpace, length);
    }
    
    public Entity[] CreateEntities(Space space, int length) {
        return World.CreateEntities(this, space, length);
    }
    
    public void CreateEntities(List<Entity> list, int length) {
        World.CreateEntities(this, World.MainSpace, list, length);
    }
    
    #endregion


    #region ToString

    private string? toString;

    public override string ToString() {
        if (toString != null) {
            return toString;
        }
        using var builder = new ValueStringBuilder(512);
        
        builder.Append("Archetype = { ");
        builder.Append("Count=");
        builder.Append(ComponentCount);
        builder.Append(", Components = [ ");
        foreach (var type in Types) {
            builder.Append(type.Type.Name);
            builder.Append(", ");
        }
        builder.Append(" ] }");
        
        toString = builder.ToString();
        return toString;
    }

    #endregion
}



