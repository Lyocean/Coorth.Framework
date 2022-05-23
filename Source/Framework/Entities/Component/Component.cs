namespace Coorth.Framework; 

public interface IComponent { }

public interface IRefComponent : IComponent {
    Entity Entity { get; }
    void OnAttach(in Entity entity);
    void OnDetach();
}
    
public abstract class Component : IRefComponent {
    
    public Entity Entity { get; private set; }
        
    protected Sandbox Sandbox => Entity.Sandbox;

    void IRefComponent.OnAttach(in Entity entity) => Entity = entity;

    void IRefComponent.OnDetach() => Entity = Entity.Null;
}

public static class ComponentExtension {
    public static ComponentPtr<T> Wrap<T>(this T component) where T : IRefComponent {
        return component.Entity.Wrap<T>();
    }
}