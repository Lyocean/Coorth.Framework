using System;
using System.Collections.Generic;

namespace Coorth {
    [StoreContract("B210C381-5F47-45BE-A5B7-2A78B13D859A")]
    public readonly struct Entity : IEquatable<Entity>, ICloneable, IDisposable {

        public readonly EntityId Id;

        public readonly Sandbox Sandbox;
        
        public Entity(Sandbox sandbox, EntityId id) {
            this.Sandbox = sandbox;
            this.Id = id;
        }

        public static Entity Null => new Entity(null, EntityId.Null);

        public int Count => Sandbox.ComponentCount(Id);

        public Archetype Archetype => Sandbox.GetArchetype(Id);
        
        public IEnumerable<Type> ComponentTypes() => Sandbox.ComponentTypes(Id);

        internal ref EntityContext GetContext() => ref Sandbox.GetContext(Id.Index);
        
        public bool IsNull => Id.IsNull || Sandbox == null || !Sandbox.HasEntity(Id);

        public bool IsNotNull => Id.IsNotNull && Sandbox != null && Sandbox.HasEntity(Id);
        
        public void Add(Type type) => Sandbox.AddComponent(Id, type);
        
        public ref T Add<T>() where T : IComponent, new() => ref Sandbox.AddComponent<T>(Id);

        public ref T Add<T>(T component) where T : IComponent => ref Sandbox.AddComponent<T>(Id, component);

        public ref T Add<T, TP1>(TP1 p1) where T : IComponent<TP1>, new() => ref Sandbox.AddComponent<T, TP1>(Id, p1);

        public ref T Add<T, TP1, TP2>(TP1 p1, TP2 p2) where T : IComponent<TP1, TP2>, new() => ref Sandbox.AddComponent<T, TP1, TP2>(Id, p1, p2);

        public ref T Add<T, TP1, TP2, TP3>(TP1 p1, TP2 p2, TP3 p3) where T : IComponent<TP1, TP2, TP3>, new() => ref Sandbox.AddComponent<T, TP1, TP2, TP3>(Id, p1, p2, p3);

        public bool TryAdd<T>() where T : IComponent => Sandbox.TryAddComponent<T>(Id);
        
        public bool Has<T>() where T : IComponent => Sandbox.HasComponent<T>(Id);

        public bool Has(Type type) => Sandbox.HasComponent(Id, type); 
        
        public ref T Get<T>() where T : IComponent => ref Sandbox.GetComponent<T>(Id);

        public T Get<T>(T defaultValue) where T : IComponent => Sandbox.TryGetComponent<T>(Id, out var component) ? component : defaultValue;

        public T TryGet<T>() where T : class, IComponent  => Sandbox.TryGetComponent<T>(Id, out var component) ? component : null;

        public T? GetNullable<T>() where T : struct, IComponent  => Sandbox.TryGetComponent<T>(Id, out var component) ? component : null;

        public bool TryGet<T>(out T component) where T : IComponent => Sandbox.TryGetComponent<T>(Id, out component);

        public ComponentPtr<T> Ptr<T>() where T : IComponent => Sandbox.PtrComponent<T>(Id);

        public IEnumerable<IComponent> GetAll() => Sandbox.GetAllComponents(Id);

        public ref T Offer<T>() where T : IComponent, new() => ref Sandbox.OfferComponent<T>(Id);

        public ref T Offer<T>(Func<Entity, T> provider) where T : IComponent, new() => ref Sandbox.OfferComponent<T>(Id, provider);
        
        public ComponentPtr<T> Wrap<T>() where T : IComponent => Sandbox.GetComponentPtr<T>(Id);

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

        public bool Remove(Type type) => Sandbox.RemoveComponent(Id, type);

        public bool IsEnable<T>() where T : IComponent => Sandbox.IsComponentEnable<T>(in Id);
        
        public bool IsEnable(Type type) => Sandbox.IsComponentEnable(in Id, type);

        public void SetEnable<T>(bool enable) where T : IComponent => Sandbox.SetComponentEnable<T>(in Id, enable);

        public void SetEnable(Type type, bool enable) => Sandbox.SetComponentEnable(in Id, type, enable);

        public void Clear() => Sandbox.ClearComponent(Id);
        
        public void Dispose() => Sandbox.DestroyEntity(Id);
        
        public Entity Clone() => Sandbox.CloneEntity(this);

        object ICloneable.Clone() => Sandbox.CloneEntity(this);

        public void Write(SerializeWriter writer) => Sandbox.WriteEntity(writer, Id);
        
        public void Read(SerializeReader reader) => Sandbox.ReadEntity(reader, Id);

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
#if NET5_0_OR_GREATER
            return HashCode.Combine(Id, Sandbox);
#else
            return (Sandbox.GetHashCode() * 397) ^ Id.GetHashCode();
#endif
        }

        public override string ToString() => $"Entity({Sandbox.Index}-Id:{Id.Index}-{Id.Version})";
        
        [Serializer(typeof(Entity))]
        private class Serializer : Serializer<Entity> {
            public override void Write(SerializeWriter writer, in Entity value) {
                value.Sandbox.WriteEntity(writer, value.Id);
            }

            public override Entity Read(SerializeReader reader, Entity value) {
                if (value.IsNull) {
                    var sandbox = reader.GetContext<Sandbox>();
                    value = sandbox.CreateEntity();
                }
                value.Sandbox.ReadEntity(reader, value.Id);
                return value;
            }
        }
    }
}