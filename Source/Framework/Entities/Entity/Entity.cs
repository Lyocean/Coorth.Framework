using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Coorth.Serialize;

namespace Coorth.Framework;

[StoreContract, Guid("B210C381-5F47-45BE-A5B7-2A78B13D859A")]
public readonly record struct Entity(Sandbox Sandbox, EntityId Id) : IDisposable {

    public readonly EntityId Id = Id;

    public readonly Sandbox Sandbox = Sandbox;

    public static Entity Null => new(Sandbox.GetDefault(), EntityId.Null);

    public int Count => Sandbox.ComponentCount(Id);

    public Archetype Archetype => Sandbox.GetArchetype(Id);
        
    public IEnumerable<Type> ComponentTypes() => Sandbox.ComponentTypes(Id);

    internal ref EntityContext GetContext() => ref Sandbox.GetContext(Id.Index);
        
    public bool IsNull => Id.IsNull || Sandbox == null || !Sandbox.HasEntity(Id);

    public bool IsNotNull => Id.IsNotNull && Sandbox != null && Sandbox.HasEntity(Id);
        
    public void Add(Type type) => Sandbox.AddComponent(Id, type);
        
    public ref T Add<T>() where T : IComponent, new() => ref Sandbox.AddComponent<T>(Id);

    public ref T Add<T>(T component) where T : IComponent => ref Sandbox.AddComponent(Id, component);

    public bool TryAdd<T>() where T : IComponent => Sandbox.TryAddComponent<T>(Id);
        
    public bool Has<T>() where T : IComponent => Sandbox.HasComponent<T>(Id);

    public bool Has(Type type) => Sandbox.HasComponent(Id, type); 
        
    public ref T Get<T>() where T : IComponent => ref Sandbox.GetComponent<T>(Id);

    public T Get<T>(T defaultValue) where T : IComponent => Sandbox.TryGetComponent<T>(Id, out var component) ? component : defaultValue;

    public T? Find<T>() where T : IComponent => Sandbox.TryGetComponent<T>(Id, out var component) ? component : default;
        
    public bool TryGet<T>([MaybeNullWhen(false), NotNullWhen(true)] out T component) where T : IComponent => Sandbox.TryGetComponent(Id, out component);

    public ComponentPtr<T> Ptr<T>() where T : IComponent => Sandbox.PtrComponent<T>(Id);

    public IEnumerable<IComponent> GetAll() => Sandbox.GetAllComponents(Id);

    public ref T Offer<T>() where T : IComponent, new() => ref Sandbox.OfferComponent<T>(Id);

    public ref T Offer<T>(Func<Entity, T> provider) where T : IComponent, new() => ref Sandbox.OfferComponent(Id, provider);
        
    public ComponentPtr<T> Wrap<T>() where T : IComponent => Sandbox.GetComponentPtr<T>(Id);

    public void Modify<T>() where T : IComponent => Sandbox.ModifyComponent<T>(Id);

    public void Modify<T>(in T component) where T : IComponent => Sandbox.ModifyComponent(Id, component);

    public void Modify<T>(Action<Sandbox, T> action) where T : IComponent => Sandbox.ModifyComponent(Id, action);

    public void Modify<T>(Func<Sandbox, T, T> action) where T : IComponent => Sandbox.ModifyComponent(Id, action);

    public bool Remove<T>() where T : IComponent => Sandbox.RemoveComponent<T>(Id);

    public bool Remove(Type type) => Sandbox.RemoveComponent(Id, type);

    public bool TryRemove<T>([MaybeNullWhen(false), NotNullWhen(true)] out T component) where T : IComponent {
        if (Sandbox.TryGetComponent(Id, out component)) {
            Sandbox.RemoveComponent<T>(Id);
        }
        return false;
    }
    
    public bool IsEnable<T>() where T : IComponent => Sandbox.IsComponentEnable<T>(in Id);
        
    public bool IsEnable(Type type) => Sandbox.IsComponentEnable(in Id, type);

    public void SetEnable<T>(bool enable) where T : IComponent => Sandbox.SetComponentEnable<T>(in Id, enable);

    public void SetEnable(Type type, bool enable) => Sandbox.SetComponentEnable(in Id, type, enable);

    public void Clear() => Sandbox.ClearComponent(Id);
        
    public void Dispose() => Sandbox.DestroyEntity(Id);

    public void Write(SerializeWriter writer) => Sandbox.WriteEntity(writer, Id);
        
    public void Read(SerializeReader reader) => Sandbox.ReadEntity(reader, Id);

    public Entity CloneEntity() => Sandbox.CloneEntity(this);
    
    public static Serializer<Entity> GetSerializer() => new Serializer();
    
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