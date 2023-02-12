using System;
using System.Runtime.CompilerServices;

namespace Coorth.Framework;

[AttributeUsage(AttributeTargets.Class| AttributeTargets.Struct)]
public class ComponentAttribute : Attribute {
    public bool Singleton { get; set; }
    public bool IsPinned { get; set; }
    public int Capacity = 0;
    public int Chunk = 0;
    public int ChunkCapacity => Singleton ? 1: Chunk;
    public int IndexCapacity => Singleton ? 1: Capacity;
}

public interface IComponent {
}

public interface IAttachable<T> {
    void OnAttach(in T entity);
    void OnDetach();
}

public abstract class Component : IComponent, IAttachable<Entity> {

    public Entity Entity { get; private set; }

    protected World World { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Entity.World; }

    public virtual void OnAttach(in Entity entity) => Entity = entity;

    public virtual void OnDetach() => Entity = Entity.Null;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void Dispatch<T>(in T e) where T : notnull => World.Dispatch(in e);
}