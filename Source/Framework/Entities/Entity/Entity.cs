using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Coorth.Collections;
using Coorth.Serialize;

namespace Coorth.Framework;

[Guid("B210C381-5F47-45BE-A5B7-2A78B13D859A")]
public readonly record struct Entity(World World, EntityId Id) : IDisposable {
    public readonly EntityId Id = Id;

    public readonly World World = World;

    public static Entity Null => new(null!, EntityId.Null);

    public int Count => World.ComponentCount(Id);

    public Archetype Archetype => World.GetArchetype(Id);

    EntityContext Context => World.GetContext(Id.Index);

    public IEnumerable<Type> ComponentTypes() => World.ComponentTypes(Id);

    public bool IsNull => Id.IsNull || World == null || !World.HasEntity(Id);

    public bool IsNotNull => Id.IsNotNull && World != null && World.HasEntity(Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(Type type) => World.AddComponent(Id, type);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Add<T>() where T : IComponent, new() => ref World.AddComponent<T>(Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Add<T>(T component) where T : IComponent => ref World.AddComponent(Id, component);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryAdd<T>() where T : IComponent => World.TryAddComponent<T>(Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has<T>() where T : IComponent => World.HasComponent<T>(Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(Type type) => World.HasComponent(Id, type);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Get<T>() where T : IComponent => ref World.GetComponent<T>(Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Get<T>(T defaultValue) where T : IComponent =>
        World.TryGetComponent<T>(Id, out var component) ? component : defaultValue;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T? Find<T>() where T : IComponent => World.TryGetComponent<T>(Id, out var component) ? component : default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGet<T>([MaybeNullWhen(false), NotNullWhen(true)] out T component) where T : IComponent =>
        World.TryGetComponent(Id, out component);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IComponent> GetAll() => World.GetAllComponents(Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Offer<T>() where T : IComponent, new() => ref World.OfferComponent<T>(Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Offer<T>(Func<Entity, T> provider) where T : IComponent, new() =>
        ref World.OfferComponent(Id, provider);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Modify<T>() where T : IComponent => World.ModifyComponent<T>(Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Modify<T>(in T component) where T : IComponent => World.ModifyComponent(Id, component);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Modify<T>(Action<World, T> action) where T : IComponent => World.ModifyComponent(Id, action);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Modify<T>(Func<World, T, T> action) where T : IComponent => World.ModifyComponent(Id, action);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove<T>() where T : IComponent => World.RemoveComponent<T>(Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove(Type type) => World.RemoveComponent(Id, type);

    public bool TryRemove<T>([MaybeNullWhen(false), NotNullWhen(true)] out T component) where T : IComponent {
        if (World.TryGetComponent(Id, out component)) {
            World.RemoveComponent<T>(Id);
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEnable<T>() where T : IComponent => World.IsComponentEnable<T>(in Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEnable(Type type) => World.IsComponentEnable(in Id, type);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetEnable<T>(bool enable) where T : IComponent => World.SetComponentEnable<T>(in Id, enable);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetEnable(Type type, bool enable) => World.SetComponentEnable(in Id, type, enable);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear() => World.ClearComponent(Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose() => World.DestroyEntity(Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(ISerializeWriter writer) => World.WriteEntity(writer, Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Read(ISerializeReader reader) => World.ReadEntity(reader, Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity CloneEntity() => World.CloneEntity(this);

    public override string ToString() {
        using var builder = new ValueStringBuilder(512);
        builder.Append("Entity={ ");
        builder.Append("Id=(Index:");
        builder.Append(Id.Index);
        builder.Append(", Version:");
        builder.Append(Id.Version);
        builder.Append("), ");
        builder.Append(Archetype.ToString());
        builder.Append(" }");
        return builder.ToString();
    }
}