using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Reflection;
using Coorth.Serialize;

namespace Coorth.Framework; 

public partial class World {

    #region Common

    private ComponentGroup?[] componentGroups = Array.Empty<ComponentGroup?>();
    
    private void SetupComponents() {
        componentGroups = new ComponentGroup[16];
    }

    private void ClearComponents() {
        foreach (var component_group in componentGroups) {
            component_group?.Clear();
        }
        componentGroups = Array.Empty<ComponentGroup>();
    }
    
    public T Singleton<T>() where T : IComponent {
        return OfferComponent<T>(Singleton().Id);
    }

    internal ComponentGroup GetComponentGroup(in ComponentType type) {
        if (type.Id >= componentGroups.Length) {
            var size = (int)BitOpUtil.RoundUpToPowerOf2((uint)type.Id + 1);
            Array.Resize(ref componentGroups, size);
        }
        var component_group = componentGroups[type.Id];
        if (component_group != null) {
            return component_group;
        }
        var group_type = typeof(ComponentGroup<>).MakeGenericType(type.Type);
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.CreateInstance | BindingFlags.NonPublic;
        var args = new object[] { this };
        component_group = Activator.CreateInstance(group_type, flags, null, args, null) as ComponentGroup;
        componentGroups[type.Id] = component_group;
        if (component_group != null) {
            return component_group;
        }
        throw new InvalidOperationException($"[Entity] Create component group failed: {group_type}");
    }
    
    internal ComponentGroup<T> GetComponentGroup<T>(in ComponentType type) where T : IComponent {
        if (type.Id >= componentGroups.Length) {
            var size = (int)BitOpUtil.RoundUpToPowerOf2((uint)type.Id + 1);
            Array.Resize(ref componentGroups, size);
        }
        var component_group = componentGroups[type.Id];
        component_group ??= new ComponentGroup<T>(this);
        componentGroups[type.Id] = component_group;
        return (ComponentGroup<T>)component_group;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComponentGroup GetComponentGroup(Type type) {
        return GetComponentGroup(ComponentRegistry.Get(type));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentGroup<T> GetComponentGroup<T>() where T : IComponent {
        return GetComponentGroup<T>(in ComponentType<T>.Value);
    }
    
    public int ComponentCount(EntityId id) {
        ref var entity_context = ref entities.Get(id.Index);
        if (entity_context.Version != id.Version) {
            return 0;
        }
        return entity_context.Archetype.ComponentCount;
    }

    public int ComponentCount<T>() where T : IComponent {
        var component_group = GetComponentGroup<T>();
        return component_group.Count;
    }
    
    #endregion


    #region Add Component
    
    public ref T AddComponent<T>(scoped in EntityId id) where T : IComponent {
        ref var entity_context = ref entities.Get(in id);
        var component_type = ComponentType<T>.Value;
        var component_group = GetComponentGroup<T>(in component_type);

        Debug.Assert(!entity_context.Has(in component_type), $"[Entity] Component has exist: {typeof(T)}");

        var entity = Cast(in entity_context);
        var component_index = component_group.Add(entity_context.Index, in entity);
        Archetype.AddComponent(ref entity_context, in component_type, component_index);

        component_group.OnAdd(in entity, component_index);
        return ref component_group.Get(component_index);
    }
    
    public ref T AddComponent<T>(scoped in EntityId id, scoped in T component) where T : IComponent {
        ref var entity_context = ref entities.Get(in id);
        var component_type = ComponentType<T>.Value;
        var component_group = GetComponentGroup<T>(in component_type);

        Debug.Assert(!entity_context.Has(in component_type), $"[Entity] Component has exist: {typeof(T)}");

        var entity = Cast(in entity_context);
        var component_index = component_group.Add(entity_context.Index, in entity, in component);
        Archetype.AddComponent(ref entity_context, component_group.Type, component_index);
        
        component_group.OnAdd(in entity, component_index);
        return ref component_group.Get(component_index);
    }

    public bool TryAddComponent<T>(in EntityId id) where T : IComponent {
        ref var entity_context = ref entities.Get(id.Index);
        if (entity_context.Version != id.Version) {
            return false;
        }
        var component_type = ComponentType<T>.Value;
        if (entity_context.Has(in component_type)) {
            return false;
        }
        var component_group = GetComponentGroup<T>();
        
        var entity = Cast(in entity_context);
        var component_index = component_group.Add(entity_context.Index, in entity);
        Archetype.AddComponent(ref entity_context, component_group.Type, component_index);
        
        component_group.OnAdd(in entity, component_index);
        return true;
    }
    
    public void AddComponent(in EntityId id, Type type) {
        ref var entity_context = ref entities.Get(in id);
        var component_group = GetComponentGroup(type);

        var entity = Cast(in entity_context);
        var component_index = component_group.Add(entity_context.Index, in entity);
        Archetype.AddComponent(ref entity_context, component_group.Type, component_index);

        component_group.OnAdd(in entity, component_index);
    }
    
    #endregion

    
    #region Has Component

    public bool HasComponent<T>(in EntityId id) where T : IComponent {
        ref var entity_context = ref entities.Get(id.Index);
        return entity_context.Version == id.Version && entity_context.Has(in ComponentType<T>.Value);
    }

    public bool HasComponent(in EntityId id, Type type) {
        if (!ComponentRegistry.Types.TryGetValue(type, out var component_type)) {
            return false;
        }
        ref var entity_context = ref entities.Get(id.Index);
        return entity_context.Has(in component_type);
    }
         
    #endregion
        
    
    #region Get Component

    public ref T GetComponent<T>(scoped in EntityId id) where T : IComponent {
        ref var entity_context = ref entities.Get(in id);
        var component_type = ComponentType<T>.Value;

        Debug.Assert(entity_context.Has(in component_type), $"[Entity] Can't find component, entity:{id}, component:{typeof(T)}");

        var component_group = GetComponentGroup<T>(in component_type);
        var component_index = entity_context.Get(in component_type);
        return ref component_group.Get(component_index);
    }

    public bool TryGetComponent<T>(scoped in EntityId id, [MaybeNullWhen(false), NotNullWhen(true)]out T component) where T : IComponent {
        ref var entity_context = ref entities.TryGet(id, out var result);
        if (!result) {
            component = default;
            return false;
        }
        var component_type = ComponentType<T>.Value;
        if (!entity_context.TryGet(in component_type, out var component_index)) {
            component = default;
            return false;
        }
        var component_group = GetComponentGroup<T>(in component_type);
        component = component_group.Get(component_index);
        return true;
    }

    public void ModifyComponent<T>(in EntityId id, in T component) where T : IComponent {
        ref var entity_context = ref entities.Get(in id);
        var component_type = ComponentType<T>.Value;
        var component_group = GetComponentGroup<T>(in component_type);
        var component_index = entity_context.Get(in component_type);
        component_group.Modify(component_index, in component);
        var entity = Cast(in entity_context);
        component_group.OnModify(in entity, component_index);
    }

    public IEnumerable<IComponent> GetAllComponents(EntityId id) {
        var entity_context = entities.Get(id.Index);
        var archetype = entity_context.Archetype;
        foreach (var (type_id, offset) in archetype.Offset) {
            var component_group = GetComponentGroup(type_id);
            var entity_span = archetype.GetEntitySpan(entity_context.LocalIndex);
            var component_index = entity_span[offset];
            yield return component_group._Get(component_index);
        }
    }
        
    #endregion

    
    #region Offer Component
    
    private ref T _OfferComponent<T>(EntityId id, Func<Entity, T>? provider) where T : IComponent {
        ref var entity_context = ref entities.Get(in id);
        var component_type = ComponentType<T>.Value;
        var component_group = GetComponentGroup<T>(in component_type);
        if (entity_context.TryGet(in component_type, out var component_index)) {
            return ref component_group.Get(component_index);
        }
        var entity = Cast(in entity_context);
        if (provider == null) {
            component_index = component_group.Add(entity_context.Index, in entity);
        }
        else {
            var component_value = provider(entity);
            component_index = component_group.Add(entity_context.Index, in entity, in component_value);
        }
        Archetype.AddComponent(ref entity_context, in component_type, component_index);

        component_group.OnAdd(in entity, component_index);
        return ref component_group.Get(component_index);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T OfferComponent<T>(scoped in EntityId id) where T : IComponent => ref _OfferComponent<T>(id, null);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T OfferComponent<T>(scoped in EntityId id, Func<Entity, T> provider) where T : IComponent => ref _OfferComponent(id, provider);

    public ref T SetComponent<T>(scoped in EntityId id, in T component) where T : IComponent {
        ref var entity_context = ref entities.Get(in id);
        var component_type = ComponentType<T>.Value;
        var entity = Cast(in entity_context);
        var component_group = GetComponentGroup<T>(in component_type);
        if (entity_context.TryGet(in component_type, out var component_index)) {
            component_group.Modify(component_index, in component);
            component_group.OnModify(in entity, component_index);
            return ref component_group.Get(component_index);
        }
        component_index = component_group.Add(entity_context.Index, in entity, in component);
        Archetype.AddComponent(ref entity_context, in component_type, component_index);
        component_group.OnAdd(in entity, component_index);
        return ref component_group.Get(component_index);
    }
    
    public int ComponentIndex<T>(scoped in EntityId id) where T : IComponent {
        var component_type = ComponentType<T>.Value;
        ref var entity_context = ref entities.Get(in id);
        return entity_context.Get(in component_type);
    }
    
    #endregion

    
    #region Modify Component

    public void ModifyComponent<T>(in EntityId id, Action<World, T>? action = null) where T : IComponent {
        ref var entity_context = ref entities.Get(in id);
        var component_type = ComponentType<T>.Value;
        var component_group = GetComponentGroup<T>(in component_type);
        var component_index = entity_context.Get(in component_type);
        ref var component = ref component_group.Get(component_index);
        action?.Invoke(this, component);
        var entity = Cast(in entity_context);
        component_group.OnModify(in entity, component_index);
    }

    public void ModifyComponent<T>(in EntityId id, Func<World, T, T> action) where T : IComponent {
        ref var entity_context = ref entities.Get(in id);
        var component_type = ComponentType<T>.Value;
        var component_group = GetComponentGroup<T>(in component_type);
        var component_index = entity_context.Get(in component_type);
        ref var component = ref component_group.Get(component_index);
        component = action.Invoke(this, component);
        var entity = Cast(in entity_context);
        component_group.OnModify(in entity, component_index);
    }

    #endregion

    
    #region Remove Component

    private bool RemoveComponent(in EntityId id, ComponentGroup component_group) {
        ref var entity_context = ref entities.Get(in id);
        if (!entity_context.TryGet(in component_group.Type, out var component_index)) {
            return false;
        }
        var entity = Cast(in entity_context);
        component_group.OnRemove(in entity, component_index);
        Archetype.RemoveComponent(ref entity_context, in component_group.Type);
        component_group.Remove(component_index, in entity);
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool RemoveComponent<T>(in EntityId id) where T : IComponent {
        var component_group = GetComponentGroup<T>(in ComponentType<T>.Value);
        return RemoveComponent(in id, component_group);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool RemoveComponent(in EntityId id, Type type) {
        var component_group = GetComponentGroup(type);
        return RemoveComponent(in id, component_group);
    }

    public bool TryRemoveComponent<T>(in EntityId id, out T component) where T: IComponent {
        ref var entity_context = ref entities.Get(in id);
        var component_group = GetComponentGroup<T>(in ComponentType<T>.Value);
        if (!entity_context.TryGet(in component_group.Type, out var component_index)) {
            component = default!;
            return false;
        }
        component = component_group.Get(component_index);
        var entity = Cast(in entity_context);
        component_group.OnRemove(in entity, component_index);
        Archetype.RemoveComponent(ref entity_context, in component_group.Type);
        component_group.Remove(component_index, in entity);
        return true;
    }
    
    private void ClearComponents(ref EntityContext entity_context) {
        var archetype = entity_context.Archetype;
        var entity = Cast(in entity_context);
        var entity_span = entity_context.GetSpan();
        foreach (var (type, offset) in archetype.Offset) {
            var component_group = GetComponentGroup(type);
            var component_index = entity_span[offset];
            component_group.OnRemove(in entity, component_index);
            component_group.Remove(component_index, in entity);
        }
        Archetype.ClearComponents(ref entity_context, rootArchetype);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ClearComponents(in EntityId id) {
        ref var entity_context = ref entities.Get(id.Index);
        ClearComponents(ref entity_context);
    }

    #endregion
    

    #region Enbale Component
        
    public bool IsComponentEnable<T>(in EntityId id) where T: IComponent {
        ref var entity_context = ref entities.Get(id.Index);
        if (entity_context.Version != id.Version) {
            return false;
        }
        var component_group = GetComponentGroup<T>();
        var component_index = entity_context.Get(in component_group.Type);
        return component_group.IsEnable(component_index);
    }

    public bool IsComponentEnable(in EntityId id, Type type) {
        ref var entity_context = ref entities.Get(id.Index);
        if (entity_context.Version != id.Version) {
            return false;
        }
        var component_group = GetComponentGroup(type);
        var component_index = entity_context.Get(in component_group.Type);
        return component_group.IsEnable(component_index);
    }
    
    public void SetComponentEnable<T>(in EntityId id, bool enable) where T: IComponent {
        ref var entity_context = ref entities.Get(id.Index);
        if (entity_context.Version != id.Version) {
            return;
        }
        var component_group = GetComponentGroup<T>();
        var component_index = entity_context.Get(in component_group.Type);
        var entity = Cast(in entity_context);
        component_group.SetEnable(component_index, enable, in entity);
    }
        
    public void SetComponentEnable(in EntityId id, Type type, bool enable) {
        ref var entity_context = ref entities.Get(id.Index);
        if (entity_context.Version != id.Version) {
            return;
        }
        var component_group = GetComponentGroup(type);
        var component_index = entity_context.Get(in component_group.Type);
        var entity = Cast(in entity_context);
        component_group.SetEnable(component_index, enable, in entity);
    }
    
    #endregion
    
    
    #region Component Group
    
    public ComponentCollection<T> GetComponents<T>()  where T : IComponent {
        return new ComponentCollection<T>(GetComponentGroup<T>());
    }
        
    public ComponentGroup<T> BindComponent<T>() where T : IComponent {
        var component_group = GetComponentGroup<T>();
        return component_group;
    }
        
    public ComponentGroup<T> BindComponent<T>(IComponentFactory<T> factory) where T : IComponent {
        var component_group = GetComponentGroup<T>();
        component_group.SetFactory(factory);
        return component_group;
    }

    public ComponentGroup<T> GetBinding<T>() where T : IComponent {
        var component_group = GetComponentGroup<T>();
        return component_group;
    }

    public IEnumerable<Type> ComponentTypes(EntityId id) {
        var entity_context = entities.Get(id.Index);
        foreach (var type in entity_context.Archetype.Types) {
            var group = GetComponentGroup(in type);
            yield return group.Type.Type;
        }
    }
    
    #endregion

    
    #region Serialize Component

    public void ReadComponent<TReader>(TReader reader, in EntityId id, Type type) where TReader : ISerializeReader {
        ref var entity_context = ref entities.Get(in id);
        var component_group = GetComponentGroup(type);
        var component_index = entity_context.Get(in component_group.Type);
        component_group.Read(reader, component_index);
    }

    public void WriteComponent<TWriter>(TWriter writer, in EntityId id, Type type) where TWriter : ISerializeWriter {
        ref var entity_context = ref entities.Get(in id);
        var component_group = GetComponentGroup(type);
        component_group.Write(writer, entity_context.Get(in component_group.Type));
    }

    #endregion

}
