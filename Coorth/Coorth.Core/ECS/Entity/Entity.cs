using System;

namespace Coorth.ECS {
    public class Entity {

        private EntityContext context;
        public EntityContext Context { get => context; internal set => context = value; }

        public EntityId Id => context.Id;

        public EcsContainer Container => context.Container;

        public int Count => Container.ComponentCount(Id);

        public T Add<T>() where T : IComponent => Container.AddComponent<T>(Id);

        public T Add<T>(T component) where T : IComponent => Container.AddComponent<T>(Id, component);

        public T Offer<T>() where T : IComponent => Container.OfferComponent<T>(Id);

        public bool Has<T>() where T : IComponent => Container.HasComponent<T>(Id);

        public T Get<T>() where T : IComponent => Container.GetComponent<T>(Id);

        public bool TryGet<T>(out T component) where T : IComponent => Container.TryGetComponent<T>(Id, out component);

        public ref T Ref<T>() where T : struct, IComponent => ref Container.RefComponent<T>(Id);

        public Entity Modify<T>(in T component) where T : IComponent { Container.ModifyComponent<T>(Id, component); return this; }

        public Entity Modify<T>(Action<EcsContainer, T> action = null) where T : IComponent { Container.ModifyComponent<T>(Id, action); return this; }

        public Entity Modify<T>(Func<EcsContainer, T, T> action) where T : IComponent { Container.ModifyComponent<T>(Id, action); return this; }

        public bool Remove<T>() where T : IComponent => Container.RemoveComponent<T>(Id);

        public void Clear() => Container.ClearComponent(Id);

        public void Destroy() => Container.DestroyEntity(Id);

        public override string ToString() => $"[Entity Id:{Id.Index}-{Id.Version} Container: {Container}]";
    }

    public struct EntityContext : IEquatable<EntityContext> {
        public readonly EntityId Id;

        public readonly EcsContainer Container;

        public EntityContext(EntityId id, EcsContainer container) {
            this.Id = id;
            this.Container = container;
        }

        public int Count => Container.ComponentCount(Id);

        public T Add<T>() where T : IComponent => Container.AddComponent<T>(Id);

        public T Add<T>(T component) where T : IComponent => Container.AddComponent<T>(Id, component);

        public bool Has<T>() where T : IComponent => Container.HasComponent<T>(Id);

        public T Get<T>() where T : IComponent => Container.GetComponent<T>(Id);

        public bool TryGet<T>(out T component) where T : IComponent => Container.TryGetComponent<T>(Id, out component);

        public ref T Ref<T>() where T : struct, IComponent => ref Container.RefComponent<T>(Id);

        public EntityContext Modify<T>(in T component) where T : IComponent { Container.ModifyComponent<T>(Id, component); return this; }

        public EntityContext Modify<T>(Action<EcsContainer, T> action = null) where T : IComponent { Container.ModifyComponent<T>(Id, action); return this; }

        public EntityContext Modify<T>(Func<EcsContainer, T, T> action) where T : IComponent { Container.ModifyComponent<T>(Id, action); return this; }

        public void Remove<T>() where T : IComponent => Container.RemoveComponent<T>(Id);

        public void Clear() => Container.ClearComponent(Id);

        public void Destroy() => Container.DestroyEntity(Id);

        public bool Equals(EntityContext other) {
            return Id == other.Id && ReferenceEquals(Container, other.Container);
        }

        public static bool operator ==(EntityContext a, EntityContext b) {
            return a.Equals(b);
        }

        public static bool operator !=(EntityContext a, EntityContext b) {
            return !a.Equals(b);
        }

        public override bool Equals(object obj) {
            return obj != null && Equals((EntityContext)obj);
        }

        public override int GetHashCode() {
            return Id.GetHashCode();
        }

        public override string ToString() {
            return $"[EntityContext Id:{Id.Index}-{Id.Version}, Container:{Container}]";
        }
    }

    public struct EntityId : IEquatable<EntityId> {

        internal readonly int Index;
        internal readonly int Version;

        public static EntityId Null => new EntityId(0, 0);


        public EntityId(int index, int version) {
            Index = index;
            Version = version;
        }

        public EntityId(long uid) {
            Index = (int)(uid & ~0xFFFFFFFF);
            Version = (int)((uid >> sizeof(int)) & ~0xFFFFFFFF);
        }

        public bool IsValidate() {
            return Index < 0;
        }

        public bool IsNull => Index == 0 && Version == 0;

        public static explicit operator long(EntityId id) {
            return (((long)id.Version) << sizeof(int)) | ((long)id.Index);
        }

        public static explicit operator EntityId(long uid) {
            return new EntityId(uid);
        }

        public static bool operator ==(EntityId a, EntityId b) {
            return a.Index == b.Index && a.Version == b.Version;
        }

        public static bool operator !=(EntityId a, EntityId b) {
            return a.Index != b.Index || a.Version != b.Version;
        }

        public bool Equals(EntityId other) {
            return Index == other.Index && Version == other.Version;
        }

        public override bool Equals(object obj) {
            return obj != null && Equals((EntityId)obj);
        }

        public override int GetHashCode() {
            return Index & (Version << 8);
        }

        public override string ToString() {
            return $"[Key:{Index}, Version:{Version}]";
        }
    }

    public static class EntityExtension {

        public static (T1, T2) Add<T1, T2>(this Entity entity) where T1 : IComponent
                                                                where T2 : IComponent {
            var c1 = entity.Add<T1>();
            var c2 = entity.Add<T2>();
            return (c1, c2);
        }

        public static (T1, T2, T3) Add<T1, T2, T3>(this Entity entity) where T1 : IComponent
                                                                 where T2 : IComponent
                                                                 where T3 : IComponent {
            var c1 = entity.Add<T1>();
            var c2 = entity.Add<T2>();
            var c3 = entity.Add<T3>();
            return (c1, c2, c3);
        }

        public static (T1, T2, T3, T4) Add<T1, T2, T3, T4>(this Entity entity) where T1 : IComponent
                                                                where T2 : IComponent
                                                                where T3 : IComponent
                                                                where T4 : IComponent {
            var c1 = entity.Add<T1>();
            var c2 = entity.Add<T2>();
            var c3 = entity.Add<T3>();
            var c4 = entity.Add<T4>();
            return (c1, c2, c3, c4);
        }

        public static (T1, T2, T3, T4, T5) Add<T1, T2, T3, T4, T5>(this Entity entity) where T1 : IComponent
                                                                where T2 : IComponent
                                                                where T3 : IComponent
                                                                where T4 : IComponent
                                                                where T5 : IComponent {
            var c1 = entity.Add<T1>();
            var c2 = entity.Add<T2>();
            var c3 = entity.Add<T3>();
            var c4 = entity.Add<T4>();
            var c5 = entity.Add<T5>();
            return (c1, c2, c3, c4, c5);
        }

        public static bool Has<T1, T2>(this Entity entity) where T1 : IComponent
                                                                where T2 : IComponent {
            return entity.Has<T1>() && entity.Has<T2>();
        }

        public static bool Has<T1, T2, T3>(this Entity entity) where T1 : IComponent
                                                                where T2 : IComponent
                                                                where T3 : IComponent {
            return entity.Has<T1>() && entity.Has<T2>() && entity.Has<T3>();
        }

        public static bool Has<T1, T2, T3, T4>(this Entity entity) where T1 : IComponent
                                                                where T2 : IComponent
                                                                where T3 : IComponent
                                                                where T4 : IComponent {
            return entity.Has<T1>() && entity.Has<T2>() && entity.Has<T3>() && entity.Has<T4>();
        }

        public static bool Has<T1, T2, T3, T4, T5>(this Entity entity) where T1 : IComponent
                                                  where T2 : IComponent
                                                  where T3 : IComponent
                                                  where T4 : IComponent
                                                  where T5 : IComponent {
            return entity.Has<T1>() && entity.Has<T2>() && entity.Has<T3>() && entity.Has<T4>() && entity.Has<T5>();
        }

        public static bool Remove<T1, T2>(this Entity entity) where T1 : IComponent
                                                  where T2 : IComponent {
            var result1 = entity.Remove<T1>();
            var result2 = entity.Remove<T2>();
            return result1 && result2;
        }

        public static bool Remove<T1, T2, T3>(this Entity entity) where T1 : IComponent
                                                  where T2 : IComponent
                                                  where T3 : IComponent {
            var result1 = entity.Remove<T1>();
            var result2 = entity.Remove<T2>();
            var result3 = entity.Remove<T3>();
            return result1 && result2 && result3;
        }

        public static bool Remove<T1, T2, T3, T4>(this Entity entity) where T1 : IComponent
                                                 where T2 : IComponent
                                                 where T3 : IComponent
                                                 where T4 : IComponent {
            var result1 = entity.Remove<T1>();
            var result2 = entity.Remove<T2>();
            var result3 = entity.Remove<T3>();
            var result4 = entity.Remove<T4>();
            return result1 && result2 && result3 && result4;
        }

        public static bool Remove<T1, T2, T3, T4, T5>(this Entity entity) where T1 : IComponent
                                                where T2 : IComponent
                                                where T3 : IComponent
                                                where T4 : IComponent
                                                where T5 : IComponent {
            var result1 = entity.Remove<T1>();
            var result2 = entity.Remove<T2>();
            var result3 = entity.Remove<T3>();
            var result4 = entity.Remove<T4>();
            var result5 = entity.Remove<T5>();
            return result1 && result2 && result3 && result4 && result5;
        }
    }
}
