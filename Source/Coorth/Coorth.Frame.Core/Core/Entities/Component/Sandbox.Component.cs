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
        
        #endregion

        #region Component Bind & Component Group

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetComponentTypeId<T>() where T: IComponent {
             return ComponentGroup<T>.TypeId;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal IComponentGroup GetComponentGroup(int typeId) {
            ref var group = ref componentGroups.Alloc(typeId);
            return group;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal IComponentGroup GetComponentGroup(Type type) {
            var typeId = ComponentTypeIds[type];
            return GetComponentGroup(typeId);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ComponentGroup<T> GetComponentGroup<T>(int typeId) where T : IComponent {
            ref var componentGroup = ref componentGroups.Alloc(typeId);
            if (componentGroup == null) {
                componentGroup = new ComponentGroup<T>(this, componentCapacity.Index, componentCapacity.Chunk);
            }
            return (ComponentGroup<T>)componentGroup;
        }

        internal ComponentGroup<T> GetComponentGroup<T>() where T : IComponent {
            var typeId = ComponentGroup<T>.TypeId;
            return GetComponentGroup<T>(typeId);
        }

        public ComponentCollection<T> GetComponents<T>()  where T : IComponent {
            return new ComponentCollection<T>(this);
        }
        
        public ComponentBinding<T> BindComponent<T>() where T : IComponent, new() {
            var group = GetComponentGroup<T>();
            return new ComponentBinding<T>(this, group);
        }
        
        public ComponentBinding<T> BindComponent<T>(ComponentCreator<T> creator) where T : IComponent {
            var group = GetComponentGroup<T>();
            group.Creator = creator;
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ref T _AddComponent<T>(int entityIndex, out ComponentGroup<T> componentGroup, out int componentIndex) where T : IComponent {
            ref var context = ref GetContext(entityIndex);
            var componentType = ComponentGroup<T>.TypeId;
            componentGroup = GetComponentGroup<T>(componentType);
            componentIndex = componentGroup.AddComponent(context.GetEntity(this));
            OnEntityAddComponent(ref context, componentType, componentIndex);
            ref var component = ref componentGroup.Ref(componentIndex);
            return ref component;
        }

        public void ReadComponent(Entity entity, Type type, IComponentSerializer serializer) {
            ref var context = ref GetContext(entity.Id.Index);
            var componentGroup = GetComponentGroup(type);
            var componentIndex = componentGroup.AddComponent(entity);
            componentGroup.ReadComponent(serializer, componentIndex);
            OnEntityAddComponent(ref context, componentGroup.Id, componentIndex);
            componentGroup.OnComponentAdd(entity.Id, componentIndex);
        }
        
        public void ReadComponent(EntityId id, Type type, ISerializer serializer, ReadOnlySpan<byte> data) { 
            ref var context = ref contexts[id.Index];
            var componentGroup = GetComponentGroup(type);
            
            var componentIndex = componentGroup.AddComponent(context.GetEntity(this));
            // componentGroup.ReadComponent(serializer, componentIndex);

             // serializer.ReadUInt32();
        }
        
        public void WriteComponent(int typeId, int componentIndex, IComponentSerializer serializer) {
            var componentGroup = GetComponentGroup(typeId);
            componentGroup.WriteComponent(serializer, componentIndex);
        }


        
        public T AddComponent<T>(in EntityId id) where T : IComponent {
            ref var component = ref _AddComponent<T>(id.Index, out var componentGroup, out var componentIndex);
            componentGroup.OnComponentAdd(id, componentIndex);
            return component;
        }
        
        public void AddComponent(in EntityId id, Type type) {
            ref var context = ref GetContext(id.Index);
            var componentGroup = GetComponentGroup(type);
            var componentType = componentGroup.Id;
            var componentIndex = componentGroup.AddComponent(context.GetEntity(this));
            OnEntityAddComponent(ref context, componentType, componentIndex);
            componentGroup.OnComponentAdd(id, componentIndex);
        }

        public T AddComponent<T>(in EntityId id, T componentValue) where T : IComponent {
            ref var context = ref GetContext(id.Index);
            var componentType = ComponentGroup<T>.TypeId;
            var group = GetComponentGroup<T>();
            var componentIndex = group.AddComponent(context.GetEntity(this), ref componentValue);
            OnEntityAddComponent(ref context, componentType, componentIndex);
            ref var component = ref group.Ref(componentIndex);

            group.OnComponentAdd(id, componentIndex);
            return component;
        }

        public T AddComponent<T, TP1>(in EntityId id, TP1 p1) where T : IComponent<TP1> {
            ref var component = ref _AddComponent<T>(id.Index, out var group, out var componentIndex);
            component.OnAwake(p1);
            group.OnComponentAdd(id, componentIndex);
            return component;
        }

        public T AddComponent<T, TP1, TP2>(in EntityId id, TP1 p1, TP2 p2) where T : IComponent<TP1, TP2> {
            ref var component = ref _AddComponent<T>(id.Index, out var group, out var componentIndex);
            component.OnAwake(p1, p2);
            group.OnComponentAdd(id, componentIndex);
            return component;
        }

        public T AddComponent<T, TP1, TP2, TP3>(in EntityId id, TP1 p1, TP2 p2, TP3 p3) where T : IComponent<TP1, TP2, TP3> {
            ref var component = ref _AddComponent<T>(id.Index, out var group, out var componentIndex);
            component.OnAwake(p1, p2, p3);
            group.OnComponentAdd(id, componentIndex);
            return component;
        }
        
        #endregion

        #region Has

        public bool HasComponent<T>(in EntityId id) where T : IComponent {
            ref var context = ref GetContext(id.Index);
            return context.Has(ComponentGroup<T>.TypeId);
        }

        public bool HasComponent(in EntityId id, Type type) {
            if (ComponentTypeIds.TryGetValue(type, out var typeId)) {
                ref var context = ref GetContext(id.Index);
                return context.Has(typeId);
            }
            return false;
        }
         
        #endregion
        
        #region Get & Ref & Modify
        
        private ref T RefComponent<T>(in int index) where T : IComponent {
            var typeId = ComponentGroup<T>.TypeId;
            ref var context = ref GetContext(index);
            var group = GetComponentGroup<T>();
            return ref group.Ref(context[typeId]);
        }
        
        public ref T RefComponent<T>(EntityId id) where T : IComponent {
            var typeId = ComponentGroup<T>.TypeId;
            ref var context = ref GetContext(id.Index);
            var group = GetComponentGroup<T>();
            return ref group.Ref(context[typeId]);
        }
        
        public T GetComponent<T>(in EntityId id) where T : IComponent {
            var typeId = ComponentGroup<T>.TypeId;
            ref var context = ref GetContext(id.Index);
            var group = GetComponentGroup<T>();
            return group.Ref(context[typeId]);
        }

        public bool TryGetComponent<T>(in EntityId id, out T component) where T : IComponent {
            var typeId = ComponentGroup<T>.TypeId;
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
            var typeId = ComponentGroup<T>.TypeId;
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
        
        public T OfferComponent<T>(in EntityId id) where T : IComponent {
            if (TryGetComponent<T>(id, out var component)) {
                return component;
            }
            return AddComponent<T>(id);
        }

        public T OfferComponent<T>(in EntityId id, Func<EntityId, Entity, T> provider) where T : IComponent {
            if (TryGetComponent<T>(id, out var component)) {
                return component;
            }
            component = provider(id, GetEntity(id));
            return AddComponent<T>(id, component);
        }

        #endregion

        #region Modify
        
        public void ModifyComponent<T>(in EntityId id, Action<Sandbox, T> action = null) where T : IComponent {
            var typeId = ComponentGroup<T>.TypeId;
            ref var context = ref GetContext(id.Index);
            var group = GetComponentGroup<T>();
            var index = context[typeId];
            
            action?.Invoke(this, group[index]);
            
            group.OnComponentModify(id, index);
        }

        public void ModifyComponent<T>(in EntityId id, Func<Sandbox, T, T> action) where T : IComponent {
            var typeId = ComponentGroup<T>.TypeId;
            ref var context = ref GetContext(id.Index);
            var group = GetComponentGroup<T>();
            var index = context[typeId];
            
            group[index] = action.Invoke(this, group[index]);
            
            group.OnComponentModify(id, index);
        }

        #endregion

        #region Remove

        public bool RemoveComponent<T>(in EntityId id) where T : IComponent {
            var typeId = ComponentGroup<T>.TypeId;
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
            var typeId = group.Id;
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
            foreach (var pair in context.Components) {
                var group = GetComponentGroup(pair.Key);
                group.OnRemoveComponent(id, pair.Value);
            }
            context.Archetype.RemoveEntity(context.Group);
            context.Archetype = emptyArchetype;
            context.Components.Clear();
        }

        #endregion

        #region Wrap

        public ComponentWrap<T> WrapComponent<T>(EntityId id) where T : IComponent {
            var typeId = ComponentGroup<T>.TypeId;
            ref var context = ref GetContext(id.Index);
            var group = GetComponentGroup<T>(typeId);
            var index = context.Components[typeId];
            return new ComponentWrap<T>(id, group, index);
        }

        #endregion
    }
}