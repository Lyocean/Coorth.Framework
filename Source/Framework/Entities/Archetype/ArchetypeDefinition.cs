using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Coorth.Collections;

namespace Coorth.Framework; 

public struct ArchetypeChunk {
    
    public int Count;
    
    public byte[] Bytes;
    
}

internal class ArchetypeDefinition {
    
    private const int MIN_CHUNK_SIZE = 4096;

    private readonly World world;

    public int EntityCount;

    public int EntityCapacity;
    
    private int reusing;

    private readonly Dictionary<int, ArchetypeDefinition> links = new();
        

    public readonly HashSet<int> Components;

    public readonly int[] Types;
        
    public readonly int Flag;

    public readonly ComponentMask Mask;
    
    public int ComponentCount => Types.Length;
    
    private ArchetypeChunk[] chunks;
    
    private readonly int chunkCapacity;
    
    private readonly int entitySize;
    
    public ArchetypeDefinition(World s) {
        world = s;
        Components = new HashSet<int>();
        Mask = new ComponentMask(0);
        Flag = 0;
        Types = Array.Empty<int>();

        chunks = Array.Empty<ArchetypeChunk>();
        entitySize = sizeof(int);
        chunkCapacity = MIN_CHUNK_SIZE / entitySize;
    }

    private ArchetypeDefinition(World s, int flag, HashSet<int> components, ComponentMask mask) {
        world = s;

        Components = components;
        Types = components.ToArray();
        Array.Sort(Types, (a, b) => a - b);

        Mask = mask;
        Flag = flag;
        
        chunks = new ArchetypeChunk[] { new() { Count = 1, Bytes = new byte[MIN_CHUNK_SIZE] }};
        entitySize = sizeof(int) + (int)BitOpUtil.RoundUpToPowerOf2((uint)Types.Length) * Unsafe.SizeOf<IndexDict<int>.Entry>();
        chunkCapacity = MIN_CHUNK_SIZE / entitySize;

    }
    
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
                    Bytes = new byte[MIN_CHUNK_SIZE],
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
        var position = context.LocalIndex;
        var chunk_index = position / chunkCapacity;
        var local_index = position % chunkCapacity;
        ref var entity_chunk = ref chunks[chunk_index];
        entity_chunk.Count--;
        Unsafe.WriteUnaligned(ref entity_chunk.Bytes[local_index * entitySize], reusing);
        reusing = -(position+1);
        EntityCount--;
    }
    
    public int GetEntity(int position) {
        var chunk_index = position / chunkCapacity;
        var local_index = position % chunkCapacity;
        ref var entity_chunk = ref chunks[chunk_index];
        // return chunk.Bytes[local_index * entitySize] - 1;
        return Unsafe.ReadUnaligned<int>(ref entity_chunk.Bytes[local_index * entitySize]) - 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EntityCreate(ref EntityContext context) {
        context.Archetype = this;
        AddEntity(ref context);
        var entity = context.GetEntity(world);
        foreach (var typeId in Types) {
            var group = world.GetComponentGroup(typeId);
            var index = group.Add(entity);
            SetComponentIndex(ref context, typeId, index);
            group.OnAdd(entity.Id, index);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EntityClone(ref EntityContext srcContext, ref EntityContext dstContext) {
        dstContext.Archetype = this;
        AddEntity(ref dstContext);
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
    

    public void EntityRemove(ref EntityContext context, ArchetypeDefinition empty) {
        RemoveEntity(ref context);
        context.LocalIndex = 0;
        context.Archetype = empty;
        context.Components = IndexDict<int>.Empty;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnAddComponent(ref EntityContext context, int type, int index) {
        var archetype = context.Archetype;
        var components = context.Components;
        
        archetype.RemoveEntity(ref context);
        archetype = archetype.AddComponent(type);
        archetype.AddEntity(ref context);
        context.Archetype = archetype;
        
        components.CopyTo(context.Components);
        components.Clear();
        context.Components.Add(type, index);

    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnRemoveComponent(ref EntityContext context, int type) {
        var archetype = context.Archetype;
        var components = context.Components;

        archetype.RemoveEntity(ref context);
        archetype = archetype.RemoveComponent(type);
        context.Archetype = archetype;

        archetype.AddEntity(ref context);

        components.Remove(type);
        components.CopyTo(context.Components);
        components.Clear();
    }
    
    public ArchetypeDefinition AddComponent(int type) {
        if (links.TryGetValue(type, out var archetype)) {
            return archetype;
        }

        var components = new HashSet<int>();
        foreach (var typeId in Components) {
            components.Add(typeId);
        }
        components.Add(type);

        var capacity = (int) ((uint) (Components.Count + 2) >> 1) << 1;
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetComponentIndex(ref EntityContext context, int type) {
        return context.Components[type];
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasComponent(ref EntityContext context, int type) {
        return context.Components.ContainsKey(type);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetComponentIndex(ref EntityContext context, int type, int value) {
        context.Components[type] = value;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetComponentIndex(ref EntityContext context, int type, out int value) {
        return context.Components.TryGetValue(type, out value);
    }

}


