using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Coorth.Serialize;


namespace Coorth.Framework;

public struct ComponentChunk<T> {
    
    /// <summary>
    /// Component values
    /// </summary>
    public readonly T[] Value;
    
    /// <summary>
    /// Entity index
    /// </summary>
    public readonly int[] Index;
    
    /// <summary>
    /// Component flags, 
    /// </summary>
    public readonly byte[] Flags;
    
    /// <summary>
    /// Enable index, component index less than this value is disabled 
    /// </summary>
    public int Enable = 0;

    public int Count = 0;

    public ComponentChunk(int capacity) {
        Value = new T[capacity];
        Index = new int[capacity];
        Flags = new byte[capacity];
    }
}

public abstract class ComponentGroup {

    public readonly World World;

    public readonly ComponentType Type;

    protected int count;
    public int Count => count;

    private readonly Dictionary<Type, ComponentGroup> edges = new();
    public IDictionary<Type, ComponentGroup> Edges => edges;

    protected ComponentGroup(World world, ComponentType type) {
        World = world;
        Type = type;
    }

    internal abstract int Add(int entity_index, in Entity entity);
    internal abstract void OnAdd(in Entity entity, int component_index);

    internal abstract IComponent _Get(int entity_index);
    
    internal abstract void Modify(int entity_index);
    internal abstract void OnModify(in Entity entity, int component_index);
    
    internal abstract void Remove(int component_index, in Entity entity);
    internal abstract void OnRemove(in Entity entity, int component_index);

    internal abstract int Clone(int entity_index, int component_index, in Entity entity);
    internal abstract void OnClone(in Entity entity, int component_index);

    internal abstract bool IsEnable(int component_index);
    internal abstract void SetEnable(int component_index, bool enable, in Entity entity);
    internal abstract void SetActive(int component_index, bool active, in Entity entity);

    internal abstract void Read<TReader>(TReader reader, int component_index) where TReader : ISerializeReader;
    internal abstract void Write<TWriter>(TWriter reader, int component_index) where TWriter : ISerializeWriter;
    internal abstract ComponentPack Pack(in Entity entity, int component_index);
    internal abstract int UnPack(in Entity entity, int entity_index, ComponentPack component_pack);

    internal abstract void Clear();
    
    public void AddDependency(ComponentGroup group) {
        edges.TryAdd(group.Type.Type, group);
    }
    
    public void AddDependency<T>() where T : IComponent {
        var group = World.GetComponentGroup<T>();
        edges.TryAdd(group.Type.Type, group);
    }

    public bool HasDependency(Type type) {
        return edges.ContainsKey(type);
    }
    
    public bool HasDependency<T>() {
        return edges.ContainsKey(typeof(T));
    }

    public IReadOnlyDictionary<Type, ComponentGroup> GetDependencies() {
        return edges;
    }
}

public sealed class ComponentGroup<T> : ComponentGroup where T : IComponent {

    #region Common
    
    private ComponentChunk<T>[] chunks;

    private int reusing = -1;

    public readonly int ChunkSize;
    
    private readonly bool isPinned;

    private IComponentFactory<T>? factory;

    public int ChunkCount => chunks.Length;
    

    
    internal ComponentGroup(World world) : base(world, ComponentType<T>.Value) {
        ChunkSize = (1000 * 16) / (Unsafe.SizeOf<T>() + sizeof(int) + sizeof(byte));
        chunks = Array.Empty<ComponentChunk<T>>();
        var attribute = typeof(T).GetCustomAttribute<ComponentAttribute>();
        isPinned = attribute != null && attribute.IsPinned;
        if (typeof(T).IsValueType) {
            return;
        }
        factory = new ComponentFactory<T>();
    }

    public void SetFactory(IComponentFactory<T> value) {
        factory = value;
    }
    
    #endregion

    
    #region Add

    private ref T _Add(int entity_index, out int component_index) {
        count++;
        if (reusing >= 0) {
            component_index = reusing;
            var chunk_index = component_index / ChunkSize;
            var value_index = component_index % ChunkSize;
            ref var chunk = ref chunks[chunk_index];

            reusing = chunk.Index[value_index];
            
            chunk.Index[value_index] = entity_index;
            chunk.Count++;
            return ref chunk.Value[value_index];
        }
        else {
            component_index = count-1;
            var chunk_index = component_index / ChunkSize;
            var value_index = component_index % ChunkSize;
            if (chunk_index >= chunks.Length) {
                Array.Resize(ref chunks, chunks.Length + 1);
                chunks[chunk_index] = new ComponentChunk<T>(ChunkSize);
            }
            ref var chunk = ref chunks[chunk_index];

            chunk.Index[value_index] = entity_index;
            chunk.Flags[value_index] = EntityFlags.COMPONENT_FLAG_DEFAULT;
            chunk.Count++;
            return ref chunk.Value[value_index];
        }
    }

    internal override int Add(int entity_index, in Entity entity) {
        ref var component = ref _Add(entity_index, out var component_index);
        factory?.Create(in entity, out component);
        return component_index;
    }

    internal int Add(int entity_index, in Entity entity, in T value) {
        ref var component = ref _Add(entity_index, out var component_index);
        component = value;
        factory?.Attach(in entity, ref component);
        return component_index;
    }

    internal override void OnAdd(in Entity entity, int component_index) {
        World.Dispatcher.Dispatch(new ComponentAddEvent(entity, this, component_index));
        World.Dispatcher.Dispatch(new ComponentAddEvent<T>(entity, this, component_index));
    }

    #endregion

    
    #region Get

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal override IComponent _Get(int entity_index) {
        ref T component = ref Get(entity_index);
        return component;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ref T Get(int component_index) {
        var chunk_index = component_index / ChunkSize;
        var value_index = component_index % ChunkSize;
        ref var chunk = ref chunks[chunk_index];
        return ref chunk.Value[value_index];
    }

    public int GetEntity(int component_index) {
        var chunk_index = component_index / ChunkSize;
        var valueIndex = component_index % ChunkSize;
        ref var chunk = ref chunks[chunk_index];
        var value = chunk.Index[valueIndex];
        return value;
    }

    public ref ComponentChunk<T> GetChunk(int chunk_index) {
        return ref chunks[chunk_index];
    }
    
    #endregion

    
    #region Modify

    internal override void Modify(int entity_index) {
        
    }
    
    internal void Modify(int componentIndex, in T component) {
        var chunk_index = componentIndex / ChunkSize;
        var value_index = componentIndex % ChunkSize;
        ref var chunk = ref chunks[chunk_index];
        chunk.Value[value_index] = component;
    }
    
    internal override void OnModify(in Entity entity, int component_index) {
        World.Dispatcher.Dispatch(new ComponentModifyEvent(entity, this, component_index));
        World.Dispatcher.Dispatch(new ComponentModifyEvent<T>(entity, this, component_index));
    }

    #endregion

    
    #region Remove

    internal override void Remove(int component_index, in Entity entity) {
        var chunk_index = component_index / ChunkSize;
        var value_index = component_index % ChunkSize;
        ref var chunk = ref chunks[chunk_index];
        ref var index = ref chunk.Index[value_index];
        ref var value = ref chunk.Value[value_index];
        
 
        if (factory != null) {
            factory.Recycle(in entity, ref value);
        } else {
            value = default!;
        }
        
        if (isPinned) {
            index = reusing;
            reusing = component_index;
        }
        else if(value_index != chunk.Count - 1) {
            ref var tail_index = ref chunk.Index[ChunkSize - 1];
            ref var tail_value = ref chunk.Value[ChunkSize - 1];
            
            (index, tail_index) = (tail_index, 0);
            (value, tail_value) = (tail_value, value);

            World.OnComponentMove(tail_index, in Type, component_index);
        }
        
        count--;
        chunk.Count--;

    }

    internal override void OnRemove(in Entity entity, int component_index) {
        World.Dispatch(new ComponentRemoveEvent(entity, this, component_index));
        World.Dispatch(new ComponentRemoveEvent<T>(entity, this, component_index));
    }

    #endregion

    
    #region Clone


    internal override int Clone(int entity_index, int component_index, in Entity entity) {
        ref var source = ref Get(component_index);
        ref var target = ref _Add(entity_index, out var targetIndex);
        if (factory != null) {
            factory.Clone(in entity, in source, out target);
        } else {
            ComponentFactory<T>.CloneInstance(in entity, in source, out target);
        }
        return targetIndex;
    }
    
    internal override void OnClone(in Entity entity, int component_index) {
        World.Dispatch(new ComponentAddEvent(entity, this, component_index));
        World.Dispatch(new ComponentAddEvent<T>(entity, this, component_index));
    }

    private void CloneComponent(in Entity entity, in T source_component, out T target_component) {
        if (factory != null) {
            factory.Clone(in entity, in source_component, out target_component);
        }
        else {
            ComponentFactory<T>.CloneInstance(in entity, in source_component, out target_component);
        }
    }


    #endregion

    
    #region Enable

    internal override bool IsEnable(int component_index) {
        var chunk_index = component_index / ChunkSize;
        var value_index = component_index % ChunkSize;
        ref var chunk = ref chunks[chunk_index];
        return chunk.Enable <= value_index && EntityFlags.IsComponentEnable(chunk.Flags[value_index]);
    }
    
    internal override void SetEnable(int component_index, bool enable, in Entity entity) {
        var chunk_index = component_index / ChunkSize;
        var value_index = component_index % ChunkSize;
        ref var chunk = ref chunks[chunk_index];
        ref var flags = ref chunk.Flags[value_index];
        var prev_value = EntityFlags.IsComponentEnable(flags);
        EntityFlags.SetComponentEnable(ref flags, enable);
        var curr_value = EntityFlags.IsComponentEnable(flags);
        if (prev_value == curr_value) {
            return;
        }
        OnComponentEnable(in entity, component_index, curr_value);
        if (isPinned) {
            return;
        }
        var dst_comp_idx = chunk_index * ChunkSize + chunk.Enable;
        SwapComponent(ref chunk, component_index, value_index, dst_comp_idx, chunk.Enable);
        if (curr_value) {
            chunk.Enable++;
        }
        else {
            chunk.Enable--;
        }
    }
    
    internal override void SetActive(int component_index, bool active, in Entity entity) {
        var chunk_index = component_index / ChunkSize;
        var value_index = component_index % ChunkSize;
        ref var chunk = ref chunks[chunk_index];
        ref var flags = ref chunk.Flags[value_index];
        var prev_value = EntityFlags.IsComponentEnable(flags);
        EntityFlags.SetComponentActive(ref flags, active);
        var curr_value = EntityFlags.IsComponentEnable(flags);
        if (prev_value == curr_value) {
            return;
        }
        OnComponentEnable(in entity, component_index, curr_value);
        if (isPinned) {
            return;
        }
        var dst_comp_idx = chunk_index * ChunkSize + chunk.Enable;
        SwapComponent(ref chunk, component_index, value_index, dst_comp_idx, chunk.Enable);
        if (curr_value) {
            chunk.Enable++;
        }
        else {
            chunk.Enable--;
        }
    }

    private void OnComponentEnable(in Entity entity, int component_index, bool enable) {
        World.Dispatch(new ComponentEnableEvent<T>(entity, this, component_index, enable));
    }

    private void SwapComponent(ref ComponentChunk<T> chunk, int src_comp_idx, int src_val_idx, int dst_comp_idx, int dst_val_idx) {
        var src_entity_index = chunk.Index[src_val_idx];
        var dst_entity_index = chunk.Index[dst_val_idx];

        (chunk.Value[src_val_idx], chunk.Value[dst_val_idx]) = (chunk.Value[dst_val_idx], chunk.Value[src_val_idx]);
        (chunk.Index[src_val_idx], chunk.Index[dst_val_idx]) = (chunk.Index[dst_val_idx], chunk.Index[src_val_idx]);
        (chunk.Flags[src_val_idx], chunk.Flags[dst_val_idx]) = (chunk.Flags[dst_val_idx], chunk.Flags[src_val_idx]);

        World.OnComponentMove(src_entity_index, in Type, src_comp_idx);
        World.OnComponentMove(dst_entity_index, in Type, dst_comp_idx);
    }
    
    #endregion

    
    #region Read Write Pack

    internal override void Read<TReader>(TReader reader, int component_index) {
        ref var component = ref Get(component_index);
        reader.ReadValue(ref component!);
    }

    internal override void Write<TWriter>(TWriter writer, int component_index) {
        ref var component = ref Get(component_index);
        writer.WriteValue(in component);
    }
    
    internal override ComponentPack Pack(in Entity entity, int component_index) {
        ref var component = ref Get(component_index);
        CloneComponent(in entity, in component, out var cloned_component);
        var pack = new ComponentPack<T>(cloned_component);
        return pack;
    }

    internal override int UnPack(in Entity entity, int entity_index, ComponentPack component_pack) {
        var pack = (ComponentPack<T>)component_pack;
        CloneComponent(in entity, in pack.Component, out var cloned_component);
        return Add(entity_index, in entity, in cloned_component);
    }

    internal override void Clear() {
        chunks = Array.Empty<ComponentChunk<T>>();
    }

    #endregion

}
