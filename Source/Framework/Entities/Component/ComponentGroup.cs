using System;
using System.Runtime.CompilerServices;
using Coorth.Collections;
using Coorth.Serialize;

namespace Coorth.Framework;

public interface IComponentGroup {
    World World { get; }
    int Count { get; }
    Type Type { get; }
    int TypeId { get; }

    int Add(in Entity entity);
    void OnAdd(in EntityId id, int index);

    IComponent Get(int index);

    void Remove(in Entity entity, int index);
    void OnRemove(in EntityId id, int index);

    void OnModify(in EntityId id, int index);

    int Clone(in Entity entity, int index);

    void Read(ISerializeReader reader, int index);
    void Write(ISerializeWriter writer, int index);

    ComponentPack Pack(Entity entity, int index);

    bool IsEnable(int index);
    void SetEnable(EntityId id, int index, bool enable);
}

public sealed class ComponentGroup<T> : IComponentGroup where T : IComponent {
    #region Fields

    public World World { get; }

    public IComponentFactory<T>? Factory { get; internal set; }
    
    private ChunkList<T> components;

    private ChunkList<int> mapping;

    private int reusing = -1;

    //Disable | Enable
    internal int separate;

    public int Count { get; private set; }

    public Type Type => ComponentType<T>.Type;

    public int TypeId => ComponentType<T>.TypeId;

    private bool IsPinned => ComponentType<T>.IsPinned;
    
    private ValueList<IComponentGroup> dependency = new(1);

    public ComponentGroup(World world) {
        World = world;
        var attribute = ComponentType<T>.Attribute;
        var indexCapacity = world.Options.ComponentDataCapacity.Index;
        var chunkCapacity = world.Options.ComponentDataCapacity.Chunk;
        
        if (attribute != null) {
            indexCapacity = attribute.IndexCapacity > 0 ? attribute.IndexCapacity : indexCapacity;
            chunkCapacity = attribute.ChunkCapacity > 0 ? attribute.ChunkCapacity : chunkCapacity;
        }

        components = new ChunkList<T>(indexCapacity, chunkCapacity);
        mapping = new ChunkList<int>(indexCapacity, chunkCapacity);

        if (!ComponentType<T>.IsValueType) {
            Factory = new ComponentFactory<T>();
        }
    }

    #endregion

    #region Binding

    public void AddDependency(IComponentGroup componentGroup) {
        if (!dependency.Contains(componentGroup)) {
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

    public ValueList<IComponentGroup> GetDependencies() {
        return dependency;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetEntityIndex(int componentIndex) {
        return (int)(((1u << 31) - 1) & (uint)mapping[componentIndex]);
    }

    #endregion

    #region Add

    public int Add(in Entity entity) {
        Count++;
        int index;
        if (reusing < 0) {
            index = components.Count;
            ref var component = ref components.Add();
            mapping.Add(entity.Id.Index);
            Factory?.Create(in entity, out component);
        }
        else {
            index = reusing;
            reusing = mapping[reusing];
            ref var component = ref components.Ref(index);
            mapping.Set(index, entity.Id.Index);
            Factory?.Create(in entity, out component);
        }
        return index;
    }

    public int Add(in Entity entity, in T value) {
        Count++;
        int index;
        // ref var component = ref Unsafe.NullRef<T>();
        if (reusing < 0) {
            index = components.Count;
            ref var component = ref components.Add();
            mapping.Add(entity.Id.Index);
            component = value;
            Factory?.Attach(in entity, ref component);
        }
        else {
            index = reusing;
            reusing = mapping[reusing];
            ref var component = ref components.Ref(index);
            mapping.Set(index, entity.Id.Index);
            component = value;
            Factory?.Attach(in entity, ref component);
        }
        return index;
    }

    public void OnAdd(in EntityId id, int index) {
        World.Dispatch(new ComponentAddEvent(id, this, index));
        World.Dispatch(new ComponentAddEvent<T>(id, this, index));
    }

    #endregion

    #region Get & Ref

    IComponent IComponentGroup.Get(int index) => components[index];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Get(int index) => ref components.Ref(index);

    public T this[int index] {
        get => components[index];
        set => components[index] = value;
    }

    public void OnModify(in EntityId id, int index) {
        World.Dispatch(new ComponentModifyEvent(id, this, index));
        World.Dispatch(new ComponentModifyEvent<T>(id, this, index));
    }

    #endregion

    #region Remove

    public void Remove(in Entity entity, int index) {
        Count--;
        ref var component = ref components.Ref(index);
        if (Factory != null) {
            Factory.Recycle(in entity, ref component);
        }
        else {
            component = default!;
        }

        if (IsPinned) {
            mapping[index] = reusing;
            reusing = index;
        }
        else {
            var tail = components.Count - 1;
            if (index != tail) {
                component = ref components[tail];
                mapping[index] = mapping[tail];
                ref var context = ref World.GetContext(mapping[index]);
                context[TypeId] = index;
            }
            
            components.RemoveLast();
            mapping.RemoveLast();
        }
    }

    public void OnRemove(in EntityId id, int index) {
        World.Dispatcher.Dispatch(new ComponentRemoveEvent(id, this, index));
        World.Dispatcher.Dispatch(new ComponentRemoveEvent<T>(id, this, index));
    }

    #endregion

    #region Clone

    public int Clone(in Entity entity, int index) {
        Count = 0;
        ref var sourceComponent = ref components.Ref(index);
        _Clone(entity, ref sourceComponent, out T targetComponent);
        return Add(entity, in targetComponent);
    }

    public void Read(ISerializeReader reader, int index) {
        ref var component = ref components[index];
        reader.ReadValue(ref component!);
    }

    public void Write(ISerializeWriter writer, int index) {
        ref var component = ref components[index];
        writer.WriteValue(in component);
    }

    internal void _Clone(in Entity entity, ref T sourceComponent, out T targetComponent) {
        if (Factory != null) {
            Factory.Clone(in entity, ref sourceComponent, out targetComponent);
        }
        else {
            ComponentFactory<T>.CloneInstance(in entity, ref sourceComponent, out targetComponent);
        }
    }

    public ComponentPack Pack(Entity entity, int index) {
        ref var component = ref components[index];
        var pack = new ComponentPack<T>();
        pack.Pack(World, entity, ref component);
        return pack;
    }

    #endregion

    #region Enable

    private void SwapComponent(int index1, int index2) {
        if (index1 == index2) {
            return;
        }
        ref var context1 = ref World.GetContext(index1);
        ref var context2 = ref World.GetContext(index2);
        context1[TypeId] = mapping[index2];
        context2[TypeId] = mapping[index1];

        (components[index1], components[index2]) = (components[index2], components[index1]);
        (mapping[index1], mapping[index2]) = (mapping[index2], mapping[index1]);
    }

    public bool IsEnable(int index) {
        return separate <= index && index < Count && ((1u << 31) & mapping[index]) == 0;
    }

    public void SetEnable(EntityId id, int index, bool enable) {
        if (IsPinned) {
            var value = ((1u << 31) & mapping[index]) == 0;
            if(!value && enable) {
                mapping[index] = (int)(((1u << 31) - 1) & mapping[index]);
                OnComponentEnable(id, index, true);
            } else if (value && !enable) {
                mapping[index] = (int)((1u << 31) | mapping[index]);
                OnComponentEnable(id, index, false);
            }
        }
        else {
            if (enable && index < separate) {
                SwapComponent(separate, index);
                separate--;
                OnComponentEnable(id, separate + 1, true);
            }
            else if (!enable && (index >= separate)) {
                SwapComponent(separate, index);
                separate++;
                OnComponentEnable(id, separate - 1, false);
            }

        }
    }

    private void OnComponentEnable(EntityId id, int index, bool enable) {
        World.Dispatch(new ComponentEnableEvent<T>(id, this, index, enable));
    }

    #endregion

}