using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Coorth.Collections;

namespace Coorth.Framework; 

internal class ArchetypeDefinition {
    
    private readonly Sandbox sandbox;
    
    private ChunkList<int> entities;

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

    public ArchetypeDefinition(Sandbox s) {
        sandbox = s;
        Components = new HashSet<int>();
        Mask = new ComponentMask(0);
        ComponentCapacity = 2;
        Flag = 0;
        entities = new ChunkList<int>(sandbox.ArchetypeCapacity.Index, sandbox.ArchetypeCapacity.Chunk);
        Types = Array.Empty<int>();
    }

    private ArchetypeDefinition(Sandbox s, int flag, HashSet<int> components, ComponentMask mask) {
        sandbox = s;

        Components = components;
        Types = components.ToArray();
        Array.Sort(Types, (a, b) => a - b);

        Mask = mask;
        ComponentCapacity = (int) ((uint) (components.Count - 1 + 2) >> 1) << 1;
        Flag = flag;
        entities = new ChunkList<int>(sandbox.ArchetypeCapacity.Index, sandbox.ArchetypeCapacity.Chunk);
    }
    
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

    public int GetEntity(int position) {
        return entities[position] - 1;
    }
    
    //TODO: IndexDict 缓存池/共享内存

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EntityCreate(ref EntityContext context) {
        context.LocalIndex = AddEntityIndex(context.Index);
        context.Archetype = this;
        context.Components = new IndexDict<int>(ComponentCapacity);
        
        var entity = context.GetEntity(sandbox);
        foreach (var typeId in Types) {
            var componentGroup = sandbox.GetComponentGroup(typeId);
            var componentIndex = componentGroup.AddComponent(entity);
            context[typeId] = componentIndex;
            componentGroup.OnComponentAdd(entity.Id, componentIndex);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EntityClone(ref EntityContext srcContext, ref EntityContext dstContext) {
        dstContext.LocalIndex = AddEntityIndex(dstContext.Index);
        dstContext.Archetype = this;
        dstContext.Components = new IndexDict<int>(ComponentCapacity);
        
        foreach (var pair in srcContext.Components) {
            var componentGroup = sandbox.GetComponentGroup(pair.Key);
            var srcComponentIndex = pair.Value;
            var entity = dstContext.GetEntity(sandbox);
            dstContext.Components[pair.Value] = componentGroup.CloneComponent(entity, srcComponentIndex);
        }
        foreach (var pair in dstContext.Components) {
            var componentGroup = sandbox.GetComponentGroup(pair.Key);
            var dstComponentIndex = pair.Value;
            componentGroup.OnComponentAdd(dstContext.Id, dstComponentIndex);
        }
    }

    public void EntityMoveTo(ref EntityContext context, ArchetypeDefinition target) {
        RemoveEntityIndex(context.LocalIndex);
        context.LocalIndex = target.AddEntityIndex(context.Index);
        context.Archetype = target;
    }

    public void EntityRemove(ref EntityContext context, ArchetypeDefinition empty) {
        RemoveEntityIndex(context.LocalIndex);
        context.LocalIndex = 0;
        context.Archetype = empty;
        context.Components.Clear();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnAddComponent(ref EntityContext entity, int type, int index) {
        var archetype = entity.Archetype;
        archetype.RemoveEntityIndex(entity.LocalIndex);

        archetype = archetype.AddComponent(type);
        
        entity.Archetype = archetype;
        entity.LocalIndex = archetype.AddEntityIndex(entity.Index);
        
        entity.Components.Add(type, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnRemoveComponent(ref EntityContext entity, int type) {
        var archetype = entity.Archetype;
        archetype.RemoveEntityIndex(entity.LocalIndex);

        archetype = archetype.RemoveComponent(type);
        entity.Archetype = archetype;
        entity.LocalIndex = archetype.AddEntityIndex(entity.Index);
        entity.Components.Remove(type);
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
            
        archetype = new ArchetypeDefinition(sandbox, flag, components, mask);
            
        links[type] = archetype;
        archetype.links[type] = this;
            
        sandbox.OnAddArchetype(archetype);

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

        archetype = new ArchetypeDefinition(sandbox, flag, components, mask);

        links[type] = archetype;
        archetype.links[type] = this;

        sandbox.OnAddArchetype(archetype);
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

    private string GetComponentNames() {
        if (toString != null) {
            return toString;
        }
        var builder = new StringBuilder();
        foreach (var type in Types) {
            var group = sandbox.GetComponentGroup(type);
            builder.Append(group.Type.Name).Append(", ");
        }
        builder.Remove(builder.Length - 1, 1);
        toString = builder.ToString();
        return toString;
    }

    public override string ToString() {
        return $"ArchetypeDefinition:{{{ComponentCount} - [{GetComponentNames()}]}}";
    }
}