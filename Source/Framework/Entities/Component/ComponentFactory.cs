using System;

namespace Coorth.Framework; 

public interface IComponentFactory<T> {
    void Create(in Entity entity, out T component);
    void Attach(in Entity entity, ref T component);
    void Recycle(in Entity entity, ref T component);
    void Clone(in Entity entity, ref T source, out T target);
}

public sealed class ComponentFactory<T> : IComponentFactory<T> {

    public void Create(in Entity entity, out T component) {
        component = Activator.CreateInstance<T>();
        if (component is IAttachable<Entity> refComponent) {
            refComponent.OnAttach(in entity);
        }
    }
        
    public void Attach(in Entity entity, ref T component) {
        if (component is IAttachable<Entity> refComponent) {
            refComponent.OnAttach(in entity);
        }
    }
        
    public void Recycle(in Entity entity, ref T component) {
        if (component is IAttachable<Entity> refComponent) {
            refComponent.OnDetach();
        }
        component = default!;
    }

    public void Clone(in Entity entity, ref T source, out T target) {
        CloneInstance(in entity, ref source, out target);
    }
        
    public static void CloneInstance(in Entity entity, ref T source, out T target) {
        if(ComponentType<T>.IsValueType) {
            target = source;
        } else {
            if (source is ICloneable cloneable) {
                target = (T)cloneable.Clone();
            }
            else {
                target = Activator.CreateInstance<T>();
            }
        }
    }
}

public sealed class DefaultFactory<T> : IComponentFactory<T> where T : struct {
    
    private readonly T defaultValue;

    public DefaultFactory(T value) {
        defaultValue = value;
    }

    public void Create(in Entity entity, out T component) {
        component = defaultValue;
    }

    public void Attach(in Entity entity, ref T component) { }

    public void Recycle(in Entity entity, ref T component) {
        component = defaultValue;
    }

    public void Clone(in Entity entity, ref T source, out T target) {
        target = source;
    }
}