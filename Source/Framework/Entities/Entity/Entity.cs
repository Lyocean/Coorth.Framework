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

    private ref EntityContext Context => ref World.GetContext(Id.Index);

    public Archetype Archetype => Context.Archetype;

    public int Count => Archetype.ComponentCount;

    public Space Space => Context.Space;
    
    public IReadOnlyList<ComponentType> ComponentTypes => Archetype.Definition.Types;

    public bool IsNull => Id.IsNull || World == null || !World.HasEntity(Id);

    public bool IsNotNull => Id.IsNotNull && World != null && World.HasEntity(Id);
        
    public bool IsActive { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => World.IsActive(in Id); }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetActive(bool active) => World.SetActive(in Id, active);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEnable<T>() where T : IComponent => World.IsComponentEnable<T>(in Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEnable(Type type) => World.IsComponentEnable(in Id, type);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetEnable<T>(bool enable) where T : IComponent => World.SetComponentEnable<T>(in Id, enable);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetFlags(int flag, bool value) => World.SetFlags(in Id, flag, value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool GetFlags(int flag) => World.GetFlags(in Id, flag);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetEnable(Type type, bool enable) => World.SetComponentEnable(in Id, type, enable);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(Type type) => World.AddComponent(Id, type);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Add<T>() where T : IComponent, new() => ref World.AddComponent<T>(in Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Add<T>(in T component) where T : IComponent => ref World.AddComponent(in Id, in component);

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
    public bool TryGet<T>([MaybeNullWhen(false), NotNullWhen(true)] out T component) where T : IComponent => World.TryGetComponent(Id, out component);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IComponent> GetAll() => World.GetAllComponents(Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Offer<T>() where T : IComponent, new() => ref World.OfferComponent<T>(Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Offer<T>(Func<Entity, T> provider) where T : IComponent, new() => ref World.OfferComponent(Id, provider);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Index<T>() where T : IComponent, new() => World.ComponentIndex<T>(Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Modify<T>() where T : IComponent => World.ModifyComponent<T>(Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Modify<T>(in T component) where T : IComponent => World.ModifyComponent(Id, component);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Modify<T>(Action<World, T> action) where T : IComponent => World.ModifyComponent(Id, action);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Modify<T>(Func<World, T, T> action) where T : IComponent => World.ModifyComponent(Id, action);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Set<T>() where T : IComponent, new() => ref World.OfferComponent<T>(Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Set<T>(in T component) where T : IComponent => ref World.SetComponent(in Id, in component);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove<T>() where T : IComponent => World.RemoveComponent<T>(Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove(Type type) => World.RemoveComponent(Id, type);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryRemove<T>([MaybeNullWhen(false), NotNullWhen(true)] out T component) where T : IComponent {
        return World.TryRemoveComponent(in Id, out component);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity With<T>()  where T: IComponent, new() {
        Add<T>();
        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity With<T>(in T component) where T: IComponent {
        Add(in component);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear() => World.ClearComponents(in Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose() => World.DestroyEntity(in Id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity CloneEntity() => World.CloneEntity(in Id);

    public override string ToString() {
        return $"[Entity]{{ Id:{Id.Index}-{Id.Version}, Count: {Count} }}";
    }
}

[Guid("1D1AD5B0-3ACD-4666-8276-29B105324905")]
public readonly record struct EntityId(int Index, int Version) {
        
    public readonly int Index = Index;
        
    public readonly int Version = Version;

    public static EntityId Null => new(0, 0);

    private EntityId(long uid) : this((int) (uid & ~0xFFFFFFFF), (int) ((uid >> sizeof(int)) & ~0xFFFFFFFF)) { }

    public bool IsNull => Index == 0 && Version == 0;

    public bool IsNotNull => Index != 0 || Version != 0;

    public static explicit operator long(EntityId id) => ((long) id.Version << sizeof(int)) | (uint) id.Index;

    public static explicit operator EntityId(long uid) => new(uid);

    public override string ToString() => $"{{Index:{Index}, Version:{Version}}})";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ToString(ref ValueStringBuilder builder) {
        builder.Append("{Index:");
        builder.Append(Index);
        builder.Append(", Version:");
        builder.Append(Version);
        builder.Append("}");
    }

    public override int GetHashCode() => Index;
    
    [SerializeFormatter(typeof(EntityId))]
    public class Formatter : SerializeFormatter<EntityId> {
        public override void SerializeWriting(in SerializeWriter writer, scoped in EntityId value) {
            writer.WriteInt64((long)value);
        }

        public override void SerializeReading(in SerializeReader reader, scoped ref EntityId value) {
            value = (EntityId)reader.ReadInt64();
        }
    }
}