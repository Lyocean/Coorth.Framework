using System;

namespace Coorth.Framework; 

public interface IComponentFactory<T> {
    void Create(in Entity entity, out T component);
    void Attach(in Entity entity, ref T component);
    void Recycle(in Entity entity, ref T? component);
    void Clone(in Entity entity, ref T source, out T target);
}
    
public class ComponentFactory<T> : IComponentFactory<T> {

    public virtual void Create(in Entity entity, out T component) {
        component = Activator.CreateInstance<T>();
        if (component is IRefComponent refComponent) {
            refComponent.OnAttach(in entity);
        }
    }
        
    public virtual void Attach(in Entity entity, ref T component) {
        if (component is IRefComponent refComponent) {
            refComponent.OnAttach(in entity);
        }
    }
        
    public virtual void Recycle(in Entity entity, ref T? component) {
        if (component is IRefComponent refComponent) {
            refComponent.OnDetach();
        }
        component = default;
    }

    public virtual void Clone(in Entity entity, ref T source, out T target) {
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