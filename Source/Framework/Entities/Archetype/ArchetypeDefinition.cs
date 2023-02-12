#define CHUNK_MODE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Coorth.Collections;


namespace Coorth.Framework; 

internal class ArchetypeDefinition {
    
    private readonly World world;
    
    
#if !CHUNK_MODE
    private ChunkList<int> entities;
#endif

    public int EntityCount;

    public int EntityCapacity;
    
    private int reusing;

    private readonly Dictionary<int, ArchetypeDefinition> links = new();
        
    public readonly int Flag;

    public readonly HashSet<int> Components;

    public readonly int[] Types;
        
    public readonly ComponentMask Mask;

    public readonly int ComponentCapacity;
    
    public int ComponentCount => Types.Length;

#if CHUNK_MODE
    
    private ArchetypeChunk[] chunks;
    
    private int chunkCapacity;
    
    private int entitySize;
#endif
    
    public ArchetypeDefinition(World s) {
        world = s;
        Components = new HashSet<int>();
        Mask = new ComponentMask(0);
        ComponentCapacity = 2;
        Flag = 0;
        Types = Array.Empty<int>();

#if CHUNK_MODE
        chunks = Array.Empty<ArchetypeChunk>();
        entitySize = sizeof(int);
        chunkCapacity = 4096 / entitySize;
#else
        entities = new ChunkList<int>(world.ArchetypeCapacity.Index, world.ArchetypeCapacity.Chunk);
#endif
    }

    private ArchetypeDefinition(World s, int flag, HashSet<int> components, ComponentMask mask) {
        world = s;

        Components = components;
        Types = components.ToArray();
        Array.Sort(Types, (a, b) => a - b);

        Mask = mask;
        // (int)BitOpUtil.RoundUpToPowerOf2((uint) components.Count);
        ComponentCapacity = (int) ((uint) (components.Count - 1 + 2) >> 1) << 1;
        Flag = flag;

#if CHUNK_MODE
        chunks = new ArchetypeChunk[] { new() { Capacity = 1, Count = 1, Bytes = new byte[4096] }};
        entitySize = sizeof(int) + (int)BitOpUtil.RoundUpToPowerOf2((uint)Types.Length) * Unsafe.SizeOf<IndexDict<int>.Entry>();
        chunkCapacity = 4096 / entitySize;
#else
        entities = new ChunkList<int>(world.ArchetypeCapacity.Index, world.ArchetypeCapacity.Chunk);
#endif
    }
    
#if CHUNK_MODE

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AddEntity(ref EntityContext context) {
        var position = -reusing - 1;
        int chunk_index, local_index;
        if (position >= 0) {
            chunk_index = position / chunkCapacity;
            local_index = position % chunkCapacity;
        }
        else {
            position = EntityCount;
            chunk_index = position / chunkCapacity;
            local_index = position % chunkCapacity;
            if (chunk_index >= chunks.Length) {
                Array.Resize(ref chunks, chunks.Length == 0 ? 1 : chunk_index + 1);
                chunks[chunk_index] = new ArchetypeChunk() {
                    Count = 0,
                    Bytes = new byte[4096],
                };
            }
            EntityCapacity++;
        }
        ref var entity_chunk = ref chunks[chunk_index];
        entity_chunk.Count++;
        context.LocalIndex = position;
        Unsafe.WriteUnaligned(ref entity_chunk.Bytes[local_index * entitySize], context.Index + 1);
        // context.LocalIndex = Unsafe.ReadUnaligned<int>(ref entity_chunk.Bytes[local_index * entitySize]);
        context.Components = new IndexDict<int>(new Memory<byte>(entity_chunk.Bytes, local_index * entitySize + sizeof(int), entitySize - sizeof(int)));
        
        EntityCount++;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void RemoveEntity(ref EntityContext context) {
        var position = context.Index;
        var chunk_index = position / chunkCapacity;
        var local_index = position % chunkCapacity;
        ref var entity_chunk = ref chunks[chunk_index];
        entity_chunk.Count--;
        Unsafe.WriteUnaligned(ref entity_chunk.Bytes[local_index * entitySize], reusing);
        reusing = -(position+1);
        EntityCount--;
    }
    
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int AddEntityIndex(int entityIndex) {
        var position = -reusing - 1;
        EntityCount++;
        if (position >= 0) {
            reusing = entities[position];
            entities.Set(position, entityIndex + 1);
        }
        else {
            position = entities.Count;
            entities.Add(entityIndex + 1);
            EntityCapacity++;
        }
        return position;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void RemoveEntityIndex(int position) {
        entities[position] = reusing;
        reusing = -(position+1);
        EntityCount--;
    }
#endif
    public int GetEntity(int position) {
#if CHUNK_MODE
        var chunk_index = position / chunkCapacity;
        var local_index = position % chunkCapacity;
        ref var entity_chunk = ref chunks[chunk_index];
        // return chunk.Bytes[local_index * entitySize] - 1;
        return Unsafe.ReadUnaligned<int>(ref entity_chunk.Bytes[local_index * entitySize]) - 1;
#else
        return entities[position] - 1;
#endif
    }

    //TODO: IndexDict 缓存池/共享内存
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EntityCreate(ref EntityContext context) {
        context.Archetype = this;
#if CHUNK_MODE
        AddEntity(ref context);
#else
        context.LocalIndex = AddEntityIndex(context.Index);
        context.Components = new IndexDict<int>(ComponentCapacity);
#endif
        var entity = context.GetEntity(world);
        foreach (var typeId in Types) {
            var componentGroup = world.GetComponentGroup(typeId);
            var componentIndex = componentGroup.Add(entity);
            context[typeId] = componentIndex;
            componentGroup.OnAdd(entity.Id, componentIndex);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EntityClone(ref EntityContext srcContext, ref EntityContext dstContext) {
        dstContext.Archetype = this;
#if CHUNK_MODE
        AddEntity(ref dstContext);
#else
        dstContext.LocalIndex = AddEntityIndex(dstContext.Index);
        dstContext.Components = new IndexDict<int>(ComponentCapacity);
#endif
        foreach (var pair in srcContext.Components) {
            var componentGroup = world.GetComponentGroup(pair.Key);
            var srcComponentIndex = pair.Value;
            var entity = dstContext.GetEntity(world);
            dstContext.Components[pair.Value] = componentGroup.Clone(entity, srcComponentIndex);
        }
        foreach (var pair in dstContext.Components) {
            var componentGroup = world.GetComponentGroup(pair.Key);
            var dstComponentIndex = pair.Value;
            componentGroup.OnAdd(dstContext.Id, dstComponentIndex);
        }
    }

//     public void EntityMoveTo(ref EntityContext context, ArchetypeDefinition target) {
// #if CHUNK_MODE
//         RemoveEntity(ref context);
//         target.AddEntity(ref context);
//         context.Archetype = target;
// #else
//         RemoveEntityIndex(context.LocalIndex);
//         context.LocalIndex = target.AddEntityIndex(context.Index);
//         context.Archetype = target;
// #endif
//     }

    public void EntityRemove(ref EntityContext context, ArchetypeDefinition empty) {
#if CHUNK_MODE
        RemoveEntity(ref context);
        context.LocalIndex = 0;
        context.Archetype = empty;
        context.Components = IndexDict<int>.Empty;
#else
        RemoveEntityIndex(context.LocalIndex);
        context.LocalIndex = 0;
        context.Archetype = empty;
        context.Components.Clear();
#endif
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnAddComponent(ref EntityContext context, int type, int index) {
#if CHUNK_MODE
        var archetype = context.Archetype;
        var components = context.Components;
        
        archetype.RemoveEntity(ref context);
        archetype = archetype.AddComponent(type);
        archetype.AddEntity(ref context);
        context.Archetype = archetype;
        
        components.CopyTo(context.Components);
        components.Clear();
        context.Components.Add(type, index);
#else
        var archetype = context.Archetype;
        archetype.RemoveEntityIndex(context.LocalIndex);

        archetype = archetype.AddComponent(type);
        
        context.Archetype = archetype;
        context.LocalIndex = archetype.AddEntityIndex(context.Index);
        
        context.Components.Add(type, index);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnRemoveComponent(ref EntityContext context, int type) {
#if CHUNK_MODE
        var archetype = context.Archetype;
        var components = context.Components;

        archetype.RemoveEntity(ref context);
        archetype = archetype.RemoveComponent(type);
        context.Archetype = archetype;

        archetype.AddEntity(ref context);

        components.Remove(type);
        components.CopyTo(context.Components);
        components.Clear();
#else
        var archetype = context.Archetype;
        archetype.RemoveEntityIndex(context.LocalIndex);

        archetype = archetype.RemoveComponent(type);
        context.Archetype = archetype;
        context.LocalIndex = archetype.AddEntityIndex(context.Index);
        context.Components.Remove(type);
#endif
    }
    
    public ArchetypeDefinition AddComponent(int type) {
        if (links.TryGetValue(type, out var archetype)) {
            return archetype;
        }
        var capacity = (int) ((uint) (Components.Count + 2) >> 1) << 1;

        var components = new HashSet<int>();
        foreach (var typeId in Components) {
            components.Add(typeId);
        }
        components.Add(type);

        var mask = new ComponentMask(Mask, capacity);
        mask.Set(type, true);
            
        var flag = Flag ^ (1 << (type % 32));
            
        archetype = new ArchetypeDefinition(world, flag, components, mask);
            
        links[type] = archetype;
        archetype.links[type] = this;
            
        world.OnAddArchetype(archetype);

        return archetype;
    }

    public bool HasComponent(int type) {
        return Components.Contains(type);
    }
        
    public ArchetypeDefinition RemoveComponent(int type) {
        if (links.TryGetValue(type, out var archetype)) {
            return archetype;
        }
        var capacity = (int) ((uint) Components.Count >> 1) << 1;
        var components = new HashSet<int>();
        foreach (var typeId in Components) {
            if (typeId != type) {
                components.Add(typeId);
            }
        }
            
        var mask = new ComponentMask(Mask, capacity);
        mask.Set(type, false);
            
        var flag = Flag ^ (1 << (type % 32));

        archetype = new ArchetypeDefinition(world, flag, components, mask);

        links[type] = archetype;
        archetype.links[type] = this;

        world.OnAddArchetype(archetype);
        return archetype;
    }

    public bool ComponentsEquals(ArchetypeDefinition other) {
        if (ComponentCount != other.ComponentCount) {
            return false;
        }
        foreach (var typeId in other.Components) {
            if (!Components.Contains(typeId)) {
                return false;
            }
        }
        return true;
    }
    
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
            var group = world.GetComponentGroup(type);
            builder.Append(group.Type.Name);
            builder.Append(", ");
        }
        builder.Append(" ] }");
        toString = builder.ToString();
        return toString;
    }
}

public struct ArchetypeChunk {
    
    public int Count;
    
    public byte[] Bytes;

    public int Capacity;
    
    
}
