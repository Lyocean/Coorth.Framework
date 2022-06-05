using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Coorth.Collections;
using Coorth.Serialize;

namespace Coorth.Framework; 

internal interface IComponentGroup {

    Sandbox Sandbox { get; }

    int Count { get; }
        
    Type Type { get; }

    int TypeId { get; }

    int AddComponent(in Entity entity);
    void OnComponentAdd(in EntityId id, int componentIndex);
        
    IComponent Get(int index);
        
    void RemoveComponent(in Entity entity, int componentIndex);
    void OnRemoveComponent(in EntityId id, int componentIndex);

    void OnComponentModify(in EntityId id, int componentIndex);
        
    int CloneComponent(in Entity entity, int componentIndex);

    void ReadComponent(SerializeReader reader, int componentIndex);

    void WriteComponent(SerializeWriter writer, int componentIndex);

    ComponentPack PackComponent(Entity entity, int componentIndex);

    bool IsComponentEnable(int componentIndex);
        
    void SetComponentEnable(EntityId id, int componentIndex, bool enable);
}

internal class ComponentGroup<T> : IComponentGroup where T : IComponent {

    #region Fields

    public Sandbox Sandbox { get; set; }

    public IComponentFactory<T>? Factory { get; internal set; }

    //Component Value
    internal ChunkList<T> components;
        
    //Entity Index
    private ChunkList<int> mapping;

    // private int reusing = -1;

    //Disable | Enable
    internal int separate;

    public int Count { get; private set; }

    // public int Capacity { get; private set; }

    public Type Type => ComponentType<T>.Type;

    public int TypeId => ComponentType<T>.TypeId;
        
    // public bool IsPinned => ComponentType<T>.IsPinned;
        
    // public bool IsValueType => ComponentType<T>.IsValueType;

    private RawList<IComponentGroup> dependency;

    public ComponentGroup(Sandbox sandbox, int indexCapacity, int chunkCapacity) {
        this.Sandbox = sandbox;
        var attribute = ComponentType<T>.Attribute;
        if (attribute != null) {
            indexCapacity = attribute.IndexCapacity > 0 ? attribute.IndexCapacity : indexCapacity;
            chunkCapacity = attribute.ChunkCapacity > 0 ? attribute.ChunkCapacity : chunkCapacity;
        }
        this.components = new ChunkList<T>(indexCapacity, chunkCapacity);
        this.mapping = new ChunkList<int>(indexCapacity, chunkCapacity);  
            
        if (!ComponentType<T>.IsValueType) {
            Factory = new ComponentFactory<T>();
        }
    }

    #endregion

    #region Binding

    public void AddDependency(IComponentGroup componentGroup) {
        if (dependency.Values == null) {
            dependency = new RawList<IComponentGroup>(1);
        }
        if (!dependency.Values.Contains(componentGroup)) {
            dependency.Add(componentGroup);
        }
    }
        
    public bool HasDependency(Type otherType) {
        if (dependency.IsNull) {
            return false;
        }
        for (var i = 0; i < dependency.Count; i++) {
            if (dependency[i].Type == otherType) {
                return true;
            } 
        }
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetEntityIndex(int componentIndex) {
        return (int)(((1u << 31) - 1) & mapping[componentIndex]);
    }

    #endregion

    #region Add
        
    public int AddComponent(in Entity entity) {
        Count++;
        var componentIndex = components.Count;
        ref var component = ref components.Add();
        mapping.Add(entity.Id.Index);
        Factory?.Create(in entity, out component);
        return componentIndex;
            
        // if (reusing >= 0) {
        //     var componentIndex = reusing;
        //     reusing = mapping[reusing];
        //     ref var component = ref components.Ref(componentIndex);
        //     mapping.Set(componentIndex, entity.Id.Index);
        //     Factory?.Create(in entity, out component);
        //     return componentIndex;
        // }
        // else {
        //
        // }

    }
        
    public int AddComponent(in Entity entity, ref T componentValue) {
        Count++;
        var componentIndex = components.Count;
        ref var component = ref components.Add();
        mapping.Add(entity.Id.Index);
        component = componentValue;
        Factory?.Attach(in entity, ref component);
        return componentIndex;
            
        // if (reusing >= 0) {
        //     var componentIndex = reusing;
        //     reusing = mapping[reusing];
        //     ref var component = ref components.Ref(componentIndex);
        //     component = componentValue;
        //     mapping.Set(componentIndex, entity.Id.Index);
        //     Factory?.Attach(in entity, ref component);
        //     return componentIndex;
        // }
        // else {
        //
        // }
    }

    public void OnComponentAdd(in EntityId id, int componentIndex) {
        Sandbox.Dispatch(new EventComponentAdd(id, this, componentIndex));
        Sandbox.Dispatch(new EventComponentAdd<T>(id, this, componentIndex));
    }

    #endregion

    #region Get & Ref

    IComponent IComponentGroup.Get(int index) => components[index];
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Get(int index)=> ref components.Ref(index);

    public T this[int index] { get => components[index]; set => components[index] = value; }

    public void OnComponentModify(in EntityId id, int componentIndex) {
        Sandbox.Dispatch(new EventComponentModify(id, this, componentIndex));
        Sandbox.Dispatch(new EventComponentModify<T>(id, this, componentIndex));
    }
        
    #endregion

    #region Remove
        
    public void RemoveComponent(in Entity entity, int componentIndex) {
        Count--;
        ref var component = ref components.Ref(componentIndex);
        if (Factory != null) {
            Factory.Recycle(in entity, ref component);
        }
        else {
            component = default;
        }
        var tailIndex = components.Count-1;
        if (componentIndex != tailIndex) {
            component = components[tailIndex];
            mapping[componentIndex] = mapping[tailIndex];
            ref var tailContext = ref Sandbox.GetContext(mapping[componentIndex]);
            tailContext[TypeId] = componentIndex;
        }
        components.RemoveLast();
        mapping.RemoveLast();
    }

    public void OnRemoveComponent(in EntityId id, int componentIndex) {
        Sandbox.Dispatch(new EventComponentRemove(id, this, componentIndex));
        Sandbox.Dispatch(new EventComponentRemove<T>(id, this, componentIndex));
    }

    #endregion

    #region Clone

    public int CloneComponent(in Entity entity, int componentIndex) {
        Count = 0;
        ref var sourceComponent = ref components.Ref(componentIndex);
        _Clone(entity, ref sourceComponent, out T targetComponent);
        return AddComponent(entity, ref targetComponent);
    }

    public void ReadComponent(SerializeReader reader, int componentIndex) {
        ref var component = ref components[componentIndex];
        reader.ReadValue(out component);
    }

    public void WriteComponent(SerializeWriter writer, int componentIndex) {
        ref var component = ref components[componentIndex];
        writer.WriteValue(component);
    }

    internal void _Clone(in Entity entity, ref T sourceComponent, out T targetComponent) {
        if (Factory != null) {
            Factory.Clone(in entity, ref sourceComponent, out targetComponent);
        }
        else {
            ComponentFactory<T>.CloneInstance(in entity, ref sourceComponent, out targetComponent);
        }
    }

    public ComponentPack PackComponent(Entity entity, int componentIndex) {
        ref var component = ref components[componentIndex];
        var asset = new ComponentPack<T>();
        asset.Pack(Sandbox, entity, ref component);
        return asset;
    }
        
    #endregion

    #region Enable

    private void SwapComponent(int index1, int index2) {
            
        ref var context1 = ref Sandbox.GetContext(index1);
        ref var context2 = ref Sandbox.GetContext(index2);
        context1[TypeId] = mapping[index2];
        context2[TypeId] = mapping[index1];

        (components[index1], components[index2]) = (components[index2], components[index1]);
        (mapping[index1], mapping[index2]) = (mapping[index2], mapping[index1]);
    }
        
    public bool IsComponentEnable(int index) {
        return separate <= index && index < Count && ((1u << 31) & mapping[index]) == 0;
    }

    public void SetComponentEnable(EntityId id, int index, bool enable) {
        if (enable && index < separate) {
            SwapComponent(separate, index);
            separate--;
            OnComponentEnable(id, separate+1, true);
        }
        else if(!enable && (index >= separate)) {
            SwapComponent(separate, index);
            separate++;
            OnComponentEnable(id, separate-1, false);
        }
            
            
        // if (IsPinned) {
        //     var value = ((1u << 31) & mapping[index]) == 0;
        //     if(!value && enable) {
        //         mapping[index] = (int)(((1u << 31) - 1) & mapping[index]);
        //         OnComponentEnable(id, index, true);
        //     } else if (value && !enable) {
        //         mapping[index] = (int)((1u << 31) | mapping[index]);
        //         OnComponentEnable(id, index, false);
        //     }
        // }
        // else {
        //
        // }
    }

    private void OnComponentEnable(EntityId id, int index, bool enable) {
        Sandbox.Dispatch(new EventComponentEnable<T>(id, this, index, enable));
    }
        
    #endregion

    #region Collect

    // public void Defragment(int step) {
    //     if (reusing < 0 ) {
    //         return;
    //     }
    //     for (var i = 0; i < step; i++) {
    //         SwapComponent(reusing, Count - 1);
    //     }
    // }

    #endregion
}