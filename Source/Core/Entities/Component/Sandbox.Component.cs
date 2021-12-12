using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Coorth {
    public partial class Sandbox {

        #region Fields

        private RawList<IComponentGroup> componentGroups;

        private (int Index, int Chunk) componentCapacity;

        internal static int ComponentTypeCount;
        
        internal static readonly ConcurrentDictionary<Type, int> ComponentTypeIds = new ConcurrentDictionary<Type, int>();
        
        private void InitComponents(int groupCapacity, int indexCapacity, int chunkCapacity) {
            componentCapacity = (indexCapacity, chunkCapacity);
            componentGroups = new RawList<IComponentGroup>(groupCapacity);
        }

        private void ClearComponents() {
            componentGroups.Clear();
        }
        
        #endregion

        #region Component Bind & Component Group

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetComponentTypeId<T>() where T: IComponent {
             return ComponentType<T>.TypeId;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsComponentBind(Type type)  {
            if (!ComponentTypeIds.TryGetValue(type, out var typeId)) {
                return false;
            }
            ref var group = ref componentGroups.Alloc(typeId);
            return group != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal IComponentGroup GetComponentGroup(int typeId) {
            ref var group = ref componentGroups.Alloc(typeId);
            return group;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IComponentGroup GetComponentGroup(Type type) {
            var typeId = ComponentTypeIds[type];
            return GetComponentGroup(typeId);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ComponentGroup<T> GetComponentGroup<T>(int typeId) where T : IComponent {
            ref var componentGroup = ref componentGroups.Alloc(typeId);
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
        
        public ComponentBinding<T> BindComponent<T>() where T : IComponent, new() {
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
            return context.Count;
        }

        public int ComponentCount<T>() where T : IComponent {
            var componentGroup = GetComponentGroup<T>();
            return componentGroup.Count;

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
            var componentGroup = GetComponentGroup<T>(typeId);
            var componentIndex = componentGroup.AddComponent(context.GetEntity(this));
            OnEntityAddComponent(ref context, typeId, componentIndex);
            ref var component = ref componentGroup.Get(componentIndex);
            componentGroup.OnComponentAdd(id, componentIndex);
            return ref component;
        }
        
        public void AddComponent(in EntityId id, Type type) {
            ref var context = ref GetContext(id.Index);
            var componentGroup = GetComponentGroup(type);
            var componentType = componentGroup.TypeId;
            var componentIndex = componentGroup.AddComponent(context.GetEntity(this));
            OnEntityAddComponent(ref context, componentType, componentIndex);
            componentGroup.OnComponentAdd(id, componentIndex);
        }

        public ref T AddComponent<T>(EntityId id, T componentValue) where T : IComponent {
            ref var context = ref GetContext(id.Index);
            var componentType = ComponentType<T>.TypeId;
            var group = GetComponentGroup<T>();
            var componentIndex = group.AddComponent(context.GetEntity(this), ref componentValue);
            OnEntityAddComponent(ref context, componentType, componentIndex);
            ref var component = ref group.Get(componentIndex);

            group.OnComponentAdd(id, componentIndex);
            return ref component;
        }

        public ref T AddComponent<T, TP1>(EntityId id, TP1 p1) where T : IComponent<TP1> {
            ref var context = ref GetContext(id.Index);
            var typeId = ComponentType<T>.TypeId;
            var componentGroup = GetComponentGroup<T>(typeId);
            var componentIndex = componentGroup.AddComponent(context.GetEntity(this));
            OnEntityAddComponent(ref context, typeId, componentIndex);
            ref var component = ref componentGroup.Get(componentIndex);            
            component.OnSetup(p1);
            componentGroup.OnComponentAdd(id, componentIndex);
            return ref component;
        }

        public ref T AddComponent<T, TP1, TP2>(EntityId id, TP1 p1, TP2 p2) where T : IComponent<TP1, TP2> {
            ref var context = ref GetContext(id.Index);
            var typeId = ComponentType<T>.TypeId;
            var componentGroup = GetComponentGroup<T>(typeId);
            var componentIndex = componentGroup.AddComponent(context.GetEntity(this));
            OnEntityAddComponent(ref context, typeId, componentIndex);
            ref var component = ref componentGroup.Get(componentIndex);  
            component.OnSetup(p1, p2);
            componentGroup.OnComponentAdd(id, componentIndex);
            return ref component;
        }

        public ref T AddComponent<T, TP1, TP2, TP3>(EntityId id, TP1 p1, TP2 p2, TP3 p3) where T : IComponent<TP1, TP2, TP3> {
            ref var context = ref GetContext(id.Index);
            var typeId = ComponentType<T>.TypeId;
            var componentGroup = GetComponentGroup<T>(typeId);
            var componentIndex = componentGroup.AddComponent(context.GetEntity(this));
            OnEntityAddComponent(ref context, typeId, componentIndex);
            ref var component = ref componentGroup.Get(componentIndex);  
            component.OnSetup(p1, p2, p3);
            componentGroup.OnComponentAdd(id, componentIndex);
            return ref component;
        }
        
        public bool TryAddComponent<T>(EntityId id) where T : IComponent {
            ref var context = ref GetContext(id.Index);
            var typeId = ComponentType<T>.TypeId;
            if (context.Has(typeId)) {
                return false;
            }
            var componentGroup = GetComponentGroup<T>(typeId);
            var componentIndex = componentGroup.AddComponent(context.GetEntity(this));
            OnEntityAddComponent(ref context, typeId, componentIndex);
            componentGroup.OnComponentAdd(id, componentIndex);
            return true;
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
            return ref group.Get(context[typeId]);
        }

        public bool TryGetComponent<T>(EntityId id, out T component) where T : IComponent {
            var typeId = ComponentType<T>.TypeId;
            ref var context = ref GetContext(id.Index);
            if (context.TryGet(typeId, out var index)) {
                var group = GetComponentGroup<T>(typeId);
                component = group[index];
                return true;
            }
            else {
                component = default;
                return false;
            }
        }

        public void ModifyComponent<T>(in EntityId id, T componentValue) where T : IComponent {
            var typeId = ComponentType<T>.TypeId;
            ref var context = ref GetContext(id.Index);
            var group = GetComponentGroup<T>();
            var index = context[typeId];
            
            group[index] = componentValue;
            
            group.OnComponentModify(id, index);
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
        private ref T _OfferComponent<T>(EntityId id, Func<Entity, T> provider) where T : IComponent {
            var typeId = ComponentType<T>.TypeId;
            ref var context = ref GetContext(id.Index);
            var componentGroup = GetComponentGroup<T>(typeId);
            if (context.TryGet(typeId, out var index)) {
                return ref componentGroup.Get(index);
            }
            if (provider == null) {
                index = componentGroup.AddComponent(context.GetEntity(this));
            }
            else {
                var value = provider(GetEntity(id));
                index = componentGroup.AddComponent(context.GetEntity(this), ref value);
            }
            OnEntityAddComponent(ref context, typeId, index);
            ref var component = ref componentGroup.Get(index);
            componentGroup.OnComponentAdd(id, index);
            return ref component;
        }
        
        public ref T OfferComponent<T>(EntityId id) where T : IComponent {
            return ref _OfferComponent<T>(id, null);
        }

        public ref T OfferComponent<T>(EntityId id, Func<Entity, T> provider) where T : IComponent {
            return ref _OfferComponent(id, provider);
        }

        #endregion

        #region Modify
        
        public void ModifyComponent<T>(in EntityId id, Action<Sandbox, T> action = null) where T : IComponent {
            var typeId = ComponentType<T>.TypeId;
            ref var context = ref GetContext(id.Index);
            var group = GetComponentGroup<T>();
            var index = context[typeId];
            
            action?.Invoke(this, group[index]);
            
            group.OnComponentModify(id, index);
        }

        public void ModifyComponent<T>(in EntityId id, Func<Sandbox, T, T> action) where T : IComponent {
            var typeId = ComponentType<T>.TypeId;
            ref var context = ref GetContext(id.Index);
            var group = GetComponentGroup<T>();
            var index = context[typeId];
            
            group[index] = action.Invoke(this, group[index]);
            
            group.OnComponentModify(id, index);
        }

        #endregion

        #region Remove

        public bool RemoveComponent<T>(in EntityId id) where T : IComponent {
            var typeId = ComponentType<T>.TypeId;
            ref var context = ref GetContext(id.Index);
            if (context.TryGet(typeId, out var index)) {
                var group = GetComponentGroup<T>();
                
                //Remove Event
                group.OnRemoveComponent(id, index);
                
                //Remove Data
                group.RemoveComponent(context.GetEntity(this), index);
                OnEntityRemoveComponent(ref context, typeId);
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
                group.OnRemoveComponent(id, index);
                
                //Remove Data
                group.RemoveComponent(context.GetEntity(this), index);
                OnEntityRemoveComponent(ref context, typeId);
                return true;
            }
            else {
                return false;
            }
        }

        public void ClearComponent(in EntityId id) {
            ref var context = ref GetContext(id.Index);
            var entity = context.GetEntity(this);
            foreach (var pair in context.Components) {
                var group = GetComponentGroup(pair.Key);
                group.OnRemoveComponent(id, pair.Value);
            }
            foreach (var pair in context.Components) {
                var group = GetComponentGroup(pair.Key);
                group.RemoveComponent(in entity, pair.Value);
            }
            context.Archetype.RemoveEntity(context.Group);
            context.Archetype = emptyArchetype;
            context.Components.Clear();
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
            return group.IsComponentEnable(index);
        }

        public void SetComponentEnable<T>(in EntityId id, bool enable) where T: IComponent {
            ref var context = ref contexts.Ref(id.Index);
            if (context.Version != id.Version) {
                return;
            }
            var group = GetComponentGroup<T>();
            var index = context.Get(group.TypeId);
            group.SetComponentEnable(id, index, enable);
        }

        public bool IsComponentEnable(in EntityId id, Type type) {
            ref var context = ref contexts.Ref(id.Index);
            if (context.Version != id.Version) {
                return false;
            }
            var group = GetComponentGroup(type);
            if (group == null) {
                return false;
            }
            var index = context.Get(group.TypeId);
            return group.IsComponentEnable(index);
        }
        
        public void SetComponentEnable(in EntityId id, Type type, bool enable) {
            ref var context = ref contexts.Ref(id.Index);
            if (context.Version != id.Version) {
                return;
            }
            var group = GetComponentGroup(type);
            if (group == null) {
                return;
            }
            var index = context.Get(group.TypeId);
            group.SetComponentEnable(id, index, enable);
        }
        #endregion
    }
}