using System;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Coorth.ECS {

    public class EcsContainer {

        #region Init

        public readonly string Name;
        public readonly int Id;
        internal EcsConfig config;

        private static int currentId = 0;

        public EcsContainer(string name = null, EcsConfig config = null) {
            this.Id = Interlocked.Increment(ref currentId);
            this.Name = name ?? this.Id.ToString();
            this.config = config ?? EcsConfig.Default;
            this.entities = new Entities(this);
            this.components = new Components(this);
            this.systems = new Systems(this);
            this.schedular = new Schedular(this);
            this.groups = new Groups(this);
        }

        public void Initial() {
            systems.BeginInit();
            schedular.Schedule(systems, new EventBeginInit());
            systems.EndInit();
            schedular.Schedule(systems, new EventEndInit());
        }

        public void Destroy() {
            schedular.Schedule(systems, new EventDestroy());
            systems.Destroy();
        }

        #endregion

        #region Entity

        internal readonly Entities entities;

        public int EntityCount => entities.Count;

        public EntityContext CreateContext() {
            var context = entities.Create(this, null);
            schedular.Schedule(systems, new EventEntityAdd(this, context.Id));
            return context;
        }

        public Entity CreateEntity() {
            var entity = entities.CreateEntity();
            var context = entities.Create(this, entity);
            entity.Context = context;
            schedular.Schedule(systems, new EventEntityAdd(this, entity.Id));
            return entity;
        }

        public T Singleton<T>() where T : Entity, new() => entities.Singleton<T>(this);
        public Entity Singleton() => entities.Singleton<Entity>(this);

        public bool HasEntity(EntityId id) {
            return entities.Has(id);
        }

        public EntityContext GetContext(EntityId id) {
            return entities.GetContext(this, id);
        }

        public Entity GetEntity(EntityId id) {
            return entities.GetEntity(id);
        }

        internal ref EntityData GetData(int index) {
            return ref entities.GetData(index);
        }

        public IEnumerable<EntityId> GetEntityIds() {
            return entities.GetEntityIds();
        }

        public void DestroyEntity(EntityId id) {
            schedular.Schedule(systems, new EventEntityRemove(this, id));
            ClearComponent(id);
            if (entities.Destroy(this, id, out var entity)) {
                if (entity != null) {
                    entities.RecycleEntity(entity);
                }
            }
        }

        public void DestroyContext(EntityContext context) => DestroyEntity(context.Id);

        public void DestroyEntity(Entity entity) => DestroyEntity(entity.Id);

        #endregion

        #region Component

        private readonly Components components;

        public int ComponentCount(in EntityId id) {
            ref var data = ref entities.GetData(id.Index);
            return data.Components.Count;
        }

        private void OnComponentAdd<T>(ref EntityData data, ref T component, IComponentGroup group, int index) where T : IComponent {
            if (component is IRefComponent refComponent) {
                refComponent.Entity = data.Entity;
            }
            schedular.Schedule(systems, new EventComponentAdd(this, data.Id, group, index));
            schedular.Schedule(systems, new EventComponentAdd<T>(this, data.Id, group, index));
        }

        private void OnComponentModify<T>(ref EntityData data, ref T component, IComponentGroup group, int index) where T : IComponent {
            if (component is IRefComponent refComponent) {
                refComponent.Entity = data.Entity;
            }
            groups.OnComponentModify<T>(ref data);
            schedular.Schedule(systems, new EventComponentModify(this, data.Id, group, index));
            schedular.Schedule(systems, new EventComponentModify<T>(this, data.Id, group, index));
        }

        internal void OnComponentRemove<T>(ref EntityData data, ref T component, IComponentGroup group, int index) where T : IComponent {
            if (component is IRefComponent refComponent) {
                refComponent.Entity = null;
            }
            schedular.Schedule(systems, new EventComponentRemove(this, data.Id, group, index));
            schedular.Schedule(systems, new EventComponentRemove<T>(this, data.Id, group, index));
            
        }

        public T AddComponent<T>(in EntityId id) where T : IComponent {
            var typeId = Components.Types<T>.Id;
            ref var data = ref entities.GetData(id.Index);
            var group = components.GetGroup<T>(typeId, this);
            var index = group.Add(id.Index);
            ref var component = ref group.Ref(index);
            data.Components[typeId] = index;
            groups.OnComponentAdd<T>(ref data);
            OnComponentAdd(ref data, ref component, group, index);
            return component;
        }

        public T AddComponent<T>(in EntityId id, T component) where T : IComponent {
            var typeId = Components.Types<T>.Id;
            ref var data = ref entities.GetData(id.Index);
            var group = components.GetGroup<T>(typeId, this);
            var index = group.Add(id.Index);
            data.Components[typeId] = index;
            groups.OnComponentAdd<T>(ref data);
            OnComponentAdd(ref data, ref component, group, index);
            return component;
        }

        public T OfferComponent<T>(in EntityId id) where T : IComponent {
            if (!HasComponent<T>(id)) {
                return GetComponent<T>(id);
            }
            return AddComponent<T>(id);
        }

        public bool HasComponent<T>(in EntityId id) where T : IComponent {
            var typeId = Components.Types<T>.Id;
            ref var data = ref entities.GetData(id.Index);
            return data.Components.ContainsKey(typeId);
        }

        public T GetComponent<T>(in EntityId id) where T : IComponent {
            var typeId = Components.Types<T>.Id;
            ref var data = ref entities.GetData(id.Index);
            var group = components.GetGroup<T>(typeId, this);
            return group[data.Components[typeId]];
        }

        public bool TryGetComponent<T>(in EntityId id, out T component) where T : IComponent {
            var typeId = Components.Types<T>.Id;
            ref var data = ref entities.GetData(id.Index);
            if (data.Components.TryGetValue(typeId, out var index)) {
                var group = components.GetGroup<T>(typeId, this);
                component = group[index];
                return true;
            } else {
                component = default;
                return false;
            }
        }

        public ref T RefComponent<T>(EntityId id) where T : struct, IComponent {
            var typeId = Components.Types<T>.Id;
            ref var data = ref entities.GetData(id.Index);
            var group = (ValComponentGroup<T>)components.GetGroup<T>(typeId, this);
            var index = data.Components[typeId];
            ref var component = ref group.Ref(index);
            OnComponentModify(ref data, ref component, group, index);
            return ref component;
        }

        public void ModifyComponent<T>(in EntityId id, T component) where T : IComponent {
            var typeId = Components.Types<T>.Id;
            ref var data = ref entities.GetData(id.Index);
            var group = components.GetGroup<T>(typeId, this);
            var index = data.Components[typeId];
            group[index] = component;
            OnComponentModify(ref data, ref component, group, index);
        }

        public void ModifyComponent<T>(in EntityId id, Action<EcsContainer, T> action = null) where T : IComponent {
            var typeId = Components.Types<T>.Id;
            ref var data = ref entities.GetData(id.Index);
            var group = components.GetGroup<T>(typeId, this);
            var index = data.Components[typeId];
            var component = group[index];
            action?.Invoke(this, component);
            OnComponentModify(ref data, ref component, group, index);
        }

        public void ModifyComponent<T>(in EntityId id, Func<EcsContainer, T, T> action) where T : IComponent {
            var typeId = Components.Types<T>.Id;
            ref var data = ref entities.GetData(id.Index);
            var group = components.GetGroup<T>(typeId, this);
            var index = data.Components[typeId];
            var component = group[index];
            component = action.Invoke(this, component);
            group[index] = component;
            OnComponentModify(ref data, ref component, group, index);
        }

        public bool RemoveComponent<T>(in EntityId id) where T : IComponent {
            var typeId = Components.Types<T>.Id;
            ref var data = ref entities.GetData(id.Index);
            if (data.Components.TryGetValue(typeId, out var index)) {
                var group = components.GetGroup<T>(typeId, this);
                group.Remove(index);
                OnComponentRemove(ref data, ref group.Ref(index), group, index);
                data.Components.Remove(group.TypeId);
                groups.OnComponentRemove<T>(ref data);
                return true;
            } else {
                return false;
            }
        }

        public void ClearComponent(in EntityId id) {
            ref var data = ref entities.GetData(id.Index);
            foreach (var (typeId, typeIndex) in data.Components) {
                var group = components.GetGroup(typeId);
                group.Remove(typeIndex);
                group.OnRemove(ref data, typeIndex);
            }
            data.Components.Clear();
        }

        public IEnumerator<T> GetComponents<T>() where T: IComponent {
            var typeId = Components.Types<T>.Id;
            var group = components.GetGroup<T>(typeId, this);
            return group.GetEnumerator();
        }

        public int ComponentCount(in Entity entity) => ComponentCount(entity.Id);

        public int ComponentCount(in EntityContext context) => ComponentCount(context.Id);

        public T AddComponent<T>(in Entity entity) where T : IComponent => AddComponent<T>(entity.Id);

        public T AddComponent<T>(in EntityContext context) where T : IComponent => AddComponent<T>(context);

        public T AddComponent<T>(in Entity entity, T component) where T : IComponent => AddComponent<T>(entity.Id, component);

        public T AddComponent<T>(in EntityContext context, T component) where T : IComponent => AddComponent<T>(context, component);

        public bool HasComponent<T>(in Entity entity) where T : IComponent => HasComponent<T>(entity.Id);

        public bool HasComponent<T>(in EntityContext context) where T : IComponent => HasComponent<T>(context.Id);

        public T GetComponent<T>(in Entity entity) where T : IComponent => GetComponent<T>(entity.Id);

        public T GetComponent<T>(in EntityContext context) where T : IComponent => GetComponent<T>(context.Id);

        public bool TryGetComponent<T>(in Entity entity, out T component) where T : IComponent => TryGetComponent<T>(entity.Id, out component);

        public bool TryGetComponent<T>(in EntityContext context, out T component) where T : IComponent => TryGetComponent<T>(context.Id, out component);

        public ref T RefComponent<T>(in Entity entity) where T : struct, IComponent => ref RefComponent<T>(entity.Id);

        public ref T RefComponent<T>(in EntityContext context) where T : struct, IComponent => ref RefComponent<T>(context.Id);

        public void ModifyComponent<T>(in Entity entity, T value) where T : IComponent => ModifyComponent<T>(entity.Id, value);

        public void ModifyComponent<T>(in EntityContext entity, T value) where T : IComponent => ModifyComponent<T>(entity.Id, value);

        public void ModifyComponent<T>(in Entity entity, Action<EcsContainer, T> action = null) where T : IComponent => ModifyComponent<T>(entity.Id, action);

        public void ModifyComponent<T>(in EntityContext context, Action<EcsContainer, T> action = null) where T : IComponent => ModifyComponent<T>(context.Id, action);

        public void ModifyComponent<T>(in Entity entity, Func<EcsContainer, T, T> action) where T : IComponent => ModifyComponent<T>(entity.Id, action);

        public void ModifyComponent<T>(in EntityContext context, Func<EcsContainer, T, T> action) where T : IComponent => ModifyComponent<T>(context.Id, action);

        public bool RemoveComponent<T>(in Entity entity) where T : IComponent => RemoveComponent<T>(entity.Id);

        public bool RemoveComponent<T>(in EntityContext context) where T : IComponent => RemoveComponent<T>(context.Id);

        public void ClearComponent(in Entity entity) => ClearComponent(entity.Id);

        public void ClearComponent(in EntityContext context) => ClearComponent(context.Id);


        #endregion

        #region System

        private readonly Systems systems;
        internal readonly Schedular schedular;

        private void OnSystemAdd<T>(Type type, T system) where T : IEcsSystem {
            system.Container = this;
            schedular.Schedule(systems, new EventSystemAdd(this, type, system));
            schedular.Schedule(systems, new EventSystemAdd<T>(this, type, system));
        }

        private void OnSystemRemove<T>(Type type, T system) where T : IEcsSystem {
            system.Container = null;
            schedular.Schedule(systems, new EventSystemRemove(this, type, system));
            schedular.Schedule(systems, new EventSystemRemove<T>(this, type, system));
        }

        public T AddSystem<T>() where T : IEcsSystem {
            var system = systems.Add<T>();
            OnSystemAdd<T>(typeof(T), system);
            return system;
        }

        public T AddSystem<T>(T system) where T : IEcsSystem {
            system = systems.Add<T>(system);
            OnSystemAdd<T>(typeof(T), system);
            return system;
        }

        public ActionSystem<T> AddSystem<T>(Action<T> action) where T : IEvent {
            var system = systems.Add<ActionSystem<T>>(new ActionSystem<T>(action));
            OnSystemAdd<ActionSystem<T>>(typeof(ActionSystem<T>), system);
            return system;
        }

        public bool HasSystem<T>() where T : IEcsSystem {
            return systems.Has<T>();
        }

        public T GetSystem<T>() where T : IEcsSystem {
            return systems.Get<T>();
        }

        public bool RemoveSystem<T>() where T : IEcsSystem {
            var system = systems.Get<T>();
            if (system != null) {
                OnSystemRemove<T>(typeof(T), system);
            }
            return systems.Remove<T>();
        }

        public void Execute<T>(T evt = default) where T : IEvent {
            schedular.Schedule<T>(systems, evt);
        }

        #endregion

        #region Group

        private readonly Groups groups;

        public EntityGroup GetGroup(IMatcher matcher) {
            return groups.GetGroup(matcher);
        }

        public bool RemoveGroup(EntityGroup group) {
            return groups.RemoveGroup(group);
        }

        public void ForEach<T>(Action<T> action) where T : class, IComponent {
            var typeId = Components.Types<T>.Id;
            var group = (RefComponentGroup<T>)components.GetGroup<T>(typeId, this);
            for (var i = 0; i < group.Count; i++) {
                if (group.actives[i] != 0) {
                    action(group.items[i]);
                }
            }
        }

        public void ForEach<T>(ComponentAction<T> action) where T : struct, IComponent {
            var typeId = Components.Types<T>.Id;
            var group = (ValComponentGroup<T>)components.GetGroup<T>(typeId, this);
            for (var i = 0; i < group.Count; i++) {
                if (group.actives[i] != 0) {
                    action(ref group.items[i]);
                }
            }
        }

        public void ForEach<T1, T2>(Action<T1, T2> action) where T1 : class, IComponent where T2 : class, IComponent {
            var typeId1 = Components.Types<T1>.Id;
            var typeId2 = Components.Types<T2>.Id;
            var group1 = (RefComponentGroup<T1>)components.GetGroup<T1>(typeId1, this);
            var group2 = (RefComponentGroup<T2>)components.GetGroup<T2>(typeId2, this);
            for (var i = 0; i < group1.Count; i++) {
                var index = group1.actives[i];
                if (index == 0) {
                    continue;
                }
                ref var data = ref entities.GetData(index);
                if (data.Components.TryGetValue(typeId2, out var compIndex2)) {
                    action(group1.items[i], group2.items[compIndex2]);
                }
            }
        }

        public void ForEach<TEvent, T1, T2>(TEvent evt, Action<TEvent, T1, T2> action) where T1 : class, IComponent where T2 : class, IComponent {
            var typeId1 = Components.Types<T1>.Id;
            var typeId2 = Components.Types<T2>.Id;
            var group1 = (RefComponentGroup<T1>)components.GetGroup<T1>(typeId1, this);
            var group2 = (RefComponentGroup<T2>)components.GetGroup<T2>(typeId2, this);
            for (var i = 0; i < group1.Count; i++) {
                var index = group1.actives[i];
                if (index == 0) {
                    continue;
                }
                ref var data = ref entities.GetData(index);
                if (data.Components.TryGetValue(typeId2, out var compIndex2)) {
                    action(evt, group1.items[i], group2.items[compIndex2]);
                }
            }
        }

        public void ForEach<T1, T2>(ComponentAction<T1, T2> action) where T1 : struct, IComponent where T2 : struct, IComponent {
            var typeId1 = Components.Types<T1>.Id;
            var typeId2 = Components.Types<T2>.Id;
            var group1 = (ValComponentGroup<T1>)components.GetGroup<T1>(typeId1, this);
            var group2 = (ValComponentGroup<T2>)components.GetGroup<T2>(typeId2, this);
            for (var i = 0; i < group1.Count; i++) {
                var index = group1.actives[i];
                if (index == 0) {
                    continue;
                }
                ref var data = ref entities.GetData(index);
                if (data.Components.TryGetValue(typeId2, out var compIndex2)) {
                    action(ref group1.items[i], ref group2.items[compIndex2]);
                }
            }
        }

        public void ForEach<T1, T2, T3>(Action<T1, T2, T3> action) where T1 : class, IComponent where T2 : class, IComponent where T3 : class, IComponent {
            var typeId1 = Components.Types<T1>.Id;
            var typeId2 = Components.Types<T2>.Id;
            var typeId3 = Components.Types<T3>.Id;
            var group1 = (ValComponentGroup<T1>)components.GetGroup<T1>(typeId1, this);
            var group2 = (ValComponentGroup<T2>)components.GetGroup<T2>(typeId2, this);
            var group3 = (ValComponentGroup<T3>)components.GetGroup<T3>(typeId3, this);
            for (var i = 0; i < group1.Count; i++) {
                var index = group1.actives[i];
                if (index == 0) {
                    continue;
                }
                ref var data = ref entities.GetData(index);
                if (data.Components.TryGetValue(typeId2, out var compIndex2)
                    && data.Components.TryGetValue(typeId3, out var compIndex3)) {
                    action(group1.items[i], group2.items[compIndex2], group3.items[compIndex3]);
                }
            }
        }

        public void ForEach<T1, T2, T3>(ComponentAction<T1, T2, T3> action) where T1 : struct, IComponent where T2 : struct, IComponent where T3 : struct, IComponent {
            var typeId1 = Components.Types<T1>.Id;
            var typeId2 = Components.Types<T2>.Id;
            var typeId3 = Components.Types<T3>.Id;
            var group1 = (ValComponentGroup<T1>)components.GetGroup<T1>(typeId1, this);
            var group2 = (ValComponentGroup<T2>)components.GetGroup<T2>(typeId2, this);
            var group3 = (ValComponentGroup<T3>)components.GetGroup<T3>(typeId3, this);
            for (var i = 0; i < group1.Count; i++) {
                var index = group1.actives[i];
                if (index == 0) {
                    continue;
                }
                ref var data = ref entities.GetData(index);
                if (data.Components.TryGetValue(typeId2, out var compIndex2)
                    && data.Components.TryGetValue(typeId3, out var compIndex3)) {
                    action(ref group1.items[i], ref group2.items[compIndex2], ref group3.items[compIndex3]);
                }
            }
        }


        #endregion

    }
}
