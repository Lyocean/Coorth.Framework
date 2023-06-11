using System;
using System.Runtime.CompilerServices;

namespace Coorth.Framework;

[AttributeUsage(AttributeTargets.Class| AttributeTargets.Struct)]
public class ComponentAttribute : Attribute {
    public bool Singleton { get; set; }
    public bool IsPinned { get; set; }
}

public interface IComponent {
}

public interface IAttachable<T> {
    void OnAttach(in T entity);
    void OnDetach();
}

public abstract class Component : IComponent, IAttachable<Entity> {

    public Entity Entity {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get; 
        [MethodImpl(MethodImplOptions.AggressiveInlining)] private set;
    }

    protected World World {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Entity.World;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual void OnAttach(in Entity entity) => Entity = entity;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual void OnDetach() => Entity = Entity.Null;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void Dispatch<T>(in T e) where T : notnull => World.Dispatch(in e);
}