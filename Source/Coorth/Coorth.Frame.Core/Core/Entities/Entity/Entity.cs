using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace Coorth {
    public readonly struct Entity : IEquatable<Entity>, ICloneable {
        public readonly EntityId Id;

        public readonly Sandbox Sandbox;

        public Entity(Sandbox sandbox, EntityId id) {
            this.Sandbox = sandbox;
            this.Id = id;
        }

        public static Entity Null => new Entity(null, EntityId.Null);

        public int Count => Sandbox.ComponentCount(Id);

        public IEnumerable<Type> ComponentTypes() => Sandbox.ComponentTypes(Id);

        public bool IsNull => Id.IsNull || Sandbox == null || !Sandbox.HasEntity(Id);

        public T Add<T>() where T : IComponent, new() => Sandbox.AddComponent<T>(Id);

        public T Add<T>(T component) where T : IComponent => Sandbox.AddComponent<T>(Id, component);

        public T Add<T, TP1>(TP1 p1) where T : IComponent<TP1>, new() => Sandbox.AddComponent<T, TP1>(Id, p1);

        public T Add<T, TP1, TP2>(TP1 p1, TP2 p2) where T : IComponent<TP1, TP2>, new() =>
            Sandbox.AddComponent<T, TP1, TP2>(Id, p1, p2);

        public T Add<T, TP1, TP2, TP3>(TP1 p1, TP2 p2, TP3 p3) where T : IComponent<TP1, TP2, TP3>, new() =>
            Sandbox.AddComponent<T, TP1, TP2, TP3>(Id, p1, p2, p3);

        public T Offer<T>() where T : IComponent, new() => Sandbox.OfferComponent<T>(Id);

        public T Offer<T>(Func<EntityId, Entity, T> provider) where T : IComponent, new() =>
            Sandbox.OfferComponent<T>(Id, provider);

        public bool Has<T>() where T : IComponent => Sandbox.HasComponent<T>(Id);

        public bool Has(Type type) => Sandbox.HasComponent(Id, type);

        public T Get<T>() where T : IComponent => Sandbox.GetComponent<T>(Id);

        public T Get<T>(T defaultValue) where T : IComponent =>
            Sandbox.TryGetComponent<T>(Id, out var component) ? component : defaultValue;

        public bool TryGet<T>(out T component) where T : IComponent => Sandbox.TryGetComponent<T>(Id, out component);

        public unsafe ref T Ref<T>() where T : IComponent {
            ref var component = ref Sandbox.RefComponent<T>(Id);
            return ref Unsafe.AsRef<T>(Unsafe.AsPointer(ref component));
        }

        public ComponentWrap<T> Wrap<T>() where T : IComponent => Sandbox.WrapComponent<T>(Id);

        public IEnumerable<IComponent> GetAll() => Sandbox.GetAllComponents(Id);

        public Entity Modify<T>(in T component) where T : IComponent {
            Sandbox.ModifyComponent<T>(Id, component);
            return this;
        }

        public Entity Modify<T>(Action<Sandbox, T> action = null) where T : IComponent {
            Sandbox.ModifyComponent<T>(Id, action);
            return this;
        }

        public Entity Modify<T>(Func<Sandbox, T, T> action) where T : IComponent {
            Sandbox.ModifyComponent<T>(Id, action);
            return this;
        }

        public bool Remove<T>() where T : IComponent => Sandbox.RemoveComponent<T>(Id);

        public void Clear() => Sandbox.ClearComponent(Id);

        public void Destroy() => Sandbox.DestroyEntity(Id);

        public Entity Clone() => Sandbox.CloneEntity(this);

        object ICloneable.Clone() => Sandbox.CloneEntity(this);

        public bool Equals(Entity other) {
            return Id.Equals(other.Id) && Equals(Sandbox, other.Sandbox);
        }

        public override bool Equals(object obj) {
            return obj is Entity other && Equals(other);
        }

        public static bool operator ==(Entity a, Entity b) {
            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b) {
            return !a.Equals(b);
        }

        public override int GetHashCode() {
            return Sandbox.GetHashCode() ^ Id.GetHashCode();
            // return HashCode.Combine(Id, Sandbox);
        }

        public override string ToString() => $"Entity(Id:{Id.Index}-{Id.Version} Sandbox:{Sandbox.Id})";
    }

    public readonly struct EntityId : IEquatable<EntityId> {
        internal readonly int Index;
        internal readonly int Version;

        public static EntityId Null => new EntityId(0, 0);

        public EntityId(int index, int version) {
            Index = index;
            Version = version;
        }

        public EntityId(long uid) {
            Index = (int) (uid & ~0xFFFFFFFF);
            Version = (int) ((uid >> sizeof(int)) & ~0xFFFFFFFF);
        }

        public bool IsValidate() {
            return Index < 0;
        }

        public bool IsNull => Index == 0 && Version == 0;

        public static explicit operator long(EntityId id) {
            return (((long) id.Version) << sizeof(int)) | ((long) id.Index);
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
            return obj != null && Equals((EntityId) obj);
        }

        public override int GetHashCode() {
            return Index & (Version << 8);
        }

        public override string ToString() {
            return $"[Key:{Index}, Version:{Version}]";
        }
    }
}