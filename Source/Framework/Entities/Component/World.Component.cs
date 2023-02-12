using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Coorth.Framework; 

public partial class World {

    #region Fields

    private IComponentGroup?[] componentGroups = Array.Empty<IComponentGroup?>();

    private (int Index, int Chunk) componentCapacity;

    internal static int ComponentTypeCount;
        
    internal static readonly ConcurrentDictionary<Type, int> ComponentTypeIds = new();
        
    private void InitComponents(int groupCapacity, int indexCapacity, int chunkCapacity) {
        componentCapacity = (indexCapacity, chunkCapacity);
        componentGroups = new IComponentGroup[groupCapacity];
    }

    private void ClearComponents() {
        Array.Clear(componentGroups, 0, componentGroups.Length);
    }
        
    #endregion

    #region Component Add & Component Group

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetComponentTypeId<T>() where T: IComponent => ComponentType<T>.TypeId;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsComponentBind(Type type) {
        if (!ComponentTypeIds.TryGetValue(type, out var typeId)) {
            return false;
        }
        return typeId < componentGroups.Length && componentGroups[typeId] != null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal IComponentGroup GetComponentGroup(int typeId) {
        if (typeId >= componentGroups.Length) {
            throw new NullReferenceException();
        }
        var group = componentGroups[typeId];
        if (group != null) {
            return group;
        }
        throw new NullReferenceException();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IComponentGroup GetComponentGroup(Type type) => GetComponentGroup(ComponentTypeIds[type]);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ResizeComponentGroup(int minSize) {
        var size = (int)BitOpUtil.RoundUpToPowerOf2((uint)minSize);
        var array = new IComponentGroup?[size];
        componentGroups.CopyTo(array.AsSpan(0, componentGroups.Length));
        componentGroups = array;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComponentGroup<T> GetComponentGroup<T>(int typeId) where T : IComponent {
        if (typeId >= componentGroups.Length) {
            ResizeComponentGroup(typeId + 1);
        }
        ref var componentGroup = ref componentGroups[typeId];
        componentGroup ??= new ComponentGroup<T>(this, componentCapacity.Index, componentCapacity.Chunk);
        return (ComponentGroup<T>)componentGroup;
    }

    internal ComponentGroup<T> GetComponentGroup<T>() where T : IComponent {
        var typeId = ComponentType<T>.TypeId;
        return GetComponentGroup<T>(typeId);
    }

    public ComponentCollection<T> GetComponents<T>()  where T : IComponent {
        return new ComponentCollection<T>(this);
    }
        
    public ComponentBinding<T> BindComponent<T>() where T : IComponent {
        var group = GetComponentGroup<T>();
        return new ComponentBinding<T>(this, group);
    }
        
    public ComponentBinding<T> BindComponent<T>(IComponentFactory<T> factory) where T : IComponent {
        var group = GetComponentGroup<T>();
        group.Factory = factory;
        return new ComponentBinding<T>(this, group);
    }

    public ComponentBinding<T> GetBinding<T>() where T : IComponent {
        var group = GetComponentGroup<T>();
        return new ComponentBinding<T>(this, group);
    }

    #endregion

    #region Count

    public int ComponentCount(in EntityId id) {
        ref var context = ref GetContext(id.Index);
        return context.Index != id.Index ? 0 : context.Count;
    }

    public int ComponentCount<T>() where T : IComponent {
        var group = GetComponentGroup<T>();
        return group.Count;
    }

    public IEnumerable<Type> ComponentTypes(EntityId id) {
        var context = GetContext(id.Index);
        foreach (var pair in context.Components) {
            var group = GetComponentGroup(pair.Key);
            yield return group.Type;
        }
    }

    #endregion

    #region Add
    
    public ref T AddComponent<T>(EntityId id) where T : IComponent {
        ref var context = ref GetContext(id.Index);
        var typeId = ComponentType<T>.TypeId;
#if DEBUG
        Debug.Assert(context.Version == id.Version);
        Debug.Assert(!context.Has(typeId), $"Component has exist: {typeof(T)}");
 #endif
        var group = GetComponentGroup<T>(typeId);
        var index = group.Add(context.GetEntity(this));
        context.Archetype.OnAddComponent(ref context, typeId, index);
        group.OnAdd(id, index);
        return ref group.Get(index);
    }
    
    public ref T AddComponent<T>(EntityId id, T component) where T : IComponent {
        ref var context = ref GetContext(id.Index);
        var typeId = ComponentType<T>.TypeId;
        Debug.Assert(context.Version == id.Version && !context.Has(typeId));
        var group = GetComponentGroup<T>();
        var index = group.Add(context.GetEntity(this), in component);
        context.Archetype.OnAddComponent(ref context, typeId, index);
        group.OnAdd(id, index);
        return ref group.Get(index);
    }

    public bool TryAddComponent<T>(in EntityId id) where T : IComponent {
        ref var context = ref GetContext(id.Index);
        var typeId = ComponentType<T>.TypeId;
        if (context.Has(typeId)) {
            return false;
        }
        var componentGroup = GetComponentGroup<T>(typeId);
        var componentIndex = componentGroup.Add(context.GetEntity(this));
        context.Archetype.OnAddComponent(ref context, typeId, componentIndex);
        componentGroup.OnAdd(id, componentIndex);
        return true;
    }
    
    public void AddComponent(in EntityId id, Type type) {
        ref var context = ref GetContext(id.Index);
        Debug.Assert(context.Version == id.Version);
        var componentGroup = GetComponentGroup(type);
        var typeId = componentGroup.TypeId;
        var componentIndex = componentGroup.Add(context.GetEntity(this));
        context.Archetype.OnAddComponent(ref context, typeId, componentIndex);
        componentGroup.OnAdd(id, componentIndex);
    }

    #endregion

    #region Has

    public bool HasComponent<T>(in EntityId id) where T : IComponent {
        ref var context = ref GetContext(id.Index);
        return context.Has(ComponentType<T>.TypeId);
    }

    public bool HasComponent(in EntityId id, Type type) {
        if (ComponentTypeIds.TryGetValue(type, out var typeId)) {
            ref var context = ref GetContext(id.Index);
            return context.Has(typeId);
        }
        return false;
    }
         
    #endregion
        
    #region Get

    public ComponentPtr<T> PtrComponent<T>(in EntityId id) where T : IComponent {
        var typeId = ComponentType<T>.TypeId;
        ref var context = ref GetContext(id.Index);
        var group = GetComponentGroup<T>();
        return new ComponentPtr<T>(group, context.Get(typeId));
    }
        
    public ref T GetComponent<T>(EntityId id) where T : IComponent {
        var typeId = ComponentType<T>.TypeId;
        ref var context = ref GetContext(id.Index);
        var group = GetComponentGroup<T>();
#if DEBUG
        Debug.Assert(context.Has(typeId), $"Can't find component, entity:{id}, component:{typeof(T)}");
#endif
        return ref group.Get(context[typeId]);
    }

    public bool TryGetComponent<T>(EntityId id, [MaybeNullWhen(false), NotNullWhen(true)]out T component) where T : IComponent {
        var typeId = ComponentType<T>.TypeId;
        ref var context = ref GetContext(id.Index);
        if (context.TryGet(typeId, out var index)) {
            var group = GetComponentGroup<T>(typeId);
            component = group[index];
            return true;
        }
        component = default;
        return false;
    }

    public void ModifyComponent<T>(in EntityId id, T componentValue) where T : IComponent {
        var typeId = ComponentType<T>.TypeId;
        ref var context = ref GetContext(id.Index);
        var group = GetComponentGroup<T>();
        var index = context[typeId];
            
        group[index] = componentValue;
            
        group.OnModify(id, index);
    }
        
    public IEnumerable<IComponent> GetAllComponents(EntityId id) {
        var context = GetContext(id.Index);
        foreach (var pair in context.Components) {
            var group = GetComponentGroup(pair.Key);
            yield return group.Get(pair.Value);
        }
    }
        
    #endregion

    #region Offer
        
    public T Singleton<T>() where T : IComponent {
        return OfferComponent<T>(Singleton().Id);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ref T _OfferComponent<T>(EntityId id, Func<Entity, T>? provider) where T : IComponent {
        var typeId = ComponentType<T>.TypeId;
        ref var context = ref GetContext(id.Index);
        var componentGroup = GetComponentGroup<T>(typeId);
        if (context.TryGet(typeId, out var index)) {
            return ref componentGroup.Get(index);
        }
        if (provider == null) {
            index = componentGroup.Add(context.GetEntity(this));
        }
        else {
            var value = provider(GetEntity(id));
            index = componentGroup.Add(context.GetEntity(this), in value);
        }
        context.Archetype.OnAddComponent(ref context, typeId, index);
        componentGroup.OnAdd(id, index);
        return ref componentGroup.Get(index);
    }
        
    public ref T OfferComponent<T>(EntityId id) where T : IComponent => ref _OfferComponent<T>(id, null);

    public ref T OfferComponent<T>(EntityId id, Func<Entity, T> provider) where T : IComponent => ref _OfferComponent(id, provider);

    #endregion

    #region Modify
        
    public void ModifyComponent<T>(in EntityId id, Action<World, T>? action = null) where T : IComponent {
        var typeId = ComponentType<T>.TypeId;
        ref var context = ref GetContext(id.Index);
        var group = GetComponentGroup<T>();
        var index = context[typeId];
            
        action?.Invoke(this, group[index]);
            
        group.OnModify(id, index);
    }

    public void ModifyComponent<T>(in EntityId id, Func<World, T, T> action) where T : IComponent {
        var typeId = ComponentType<T>.TypeId;
        ref var context = ref GetContext(id.Index);
        var group = GetComponentGroup<T>();
        var index = context[typeId];
            
        group[index] = action.Invoke(this, group[index]);
            
        group.OnModify(id, index);
    }

    #endregion

    #region Remove

    public bool RemoveComponent<T>(in EntityId id) where T : IComponent {
        var typeId = ComponentType<T>.TypeId;
        ref var context = ref GetContext(id.Index);
        if (context.TryGet(typeId, out var index)) {
            var group = GetComponentGroup<T>();
                
            //Remove Event
            group.OnRemove(id, index);
                
            //Remove Data
            group.Remove(context.GetEntity(this), index);
            context.Archetype.OnRemoveComponent(ref context, typeId);
            return true;
        }
        else {
            return false;
        }
    }
    
    public bool RemoveComponent(in EntityId id, Type type) {
        var group = GetComponentGroup(type);
        var typeId = group.TypeId;
        ref var context = ref GetContext(id.Index);
        if (context.TryGet(typeId, out var index)) {
            //Remove Event
            group.OnRemove(id, index);
                
            //Remove Data
            group.Remove(context.GetEntity(this), index);
            context.Archetype.OnRemoveComponent(ref context, typeId);
            return true;
        }
        else {
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void _ClearComponents(ref EntityContext context, in Entity entity) {
        var id = entity.Id;
        foreach (var pair in context.Components) {
            var group = GetComponentGroup(pair.Key);
            group.OnRemove(id, pair.Value);
        }
        foreach (var pair in context.Components) {
            var group = GetComponentGroup(pair.Key);
            group.Remove(in entity, pair.Value);
        }
    }
        
    public void ClearComponent(in EntityId id) {
        ref var context = ref GetContext(id.Index);
        _ClearComponents(ref context, context.GetEntity(this));
        context.Archetype.EntityRemove(ref context, emptyArchetype);
        // context.Archetype.EntityMoveTo(ref context, emptyArchetype);
        // context.Components.Clear();
    }

    #endregion

    #region Wrap

    public ComponentPtr<T> GetComponentPtr<T>(EntityId id) where T : IComponent {
        var typeId = ComponentType<T>.TypeId;
        ref var context = ref GetContext(id.Index);
        var group = GetComponentGroup<T>(typeId);
        var index = context.Components[typeId];
        return new ComponentPtr<T>(group, index);
    }

    #endregion

    #region Enbale
        
    public bool IsComponentEnable<T>(in EntityId id) where T: IComponent {
        ref var context = ref contexts.Ref(id.Index);
        if (context.Version != id.Version) {
            return false;
        }
        var group = GetComponentGroup<T>();
        var index = context.Get(group.TypeId);
        return group.IsEnable(index);
    }

    public void SetComponentEnable<T>(in EntityId id, bool enable) where T: IComponent {
        ref var context = ref contexts.Ref(id.Index);
        if (context.Version != id.Version) {
            return;
        }
        var group = GetComponentGroup<T>();
        var index = context.Get(group.TypeId);
        group.SetEnable(id, index, enable);
    }

    public bool IsComponentEnable(in EntityId id, Type type) {
        ref var context = ref contexts.Ref(id.Index);
        if (context.Version != id.Version) {
            return false;
        }
        var group = GetComponentGroup(type);
        var index = context.Get(group.TypeId);
        return group.IsEnable(index);
    }
        
    public void SetComponentEnable(in EntityId id, Type type, bool enable) {
        ref var context = ref contexts.Ref(id.Index);
        if (context.Version != id.Version) {
            return;
        }
        var group = GetComponentGroup(type);
        var index = context.Get(group.TypeId);
        group.SetEnable(id, index, enable);
    }
    #endregion
}