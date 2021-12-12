namespace Coorth {
    public interface IComponent {
    }
    
    public interface IComponent<in TP1> : IComponent, ISetup<TP1> {
    }
    
    public interface IComponent<in TP1, in TP2> : IComponent, ISetup<TP1, TP2> {
    }
    
    public interface IComponent<in TP1, in TP2, in TP3> : IComponent, ISetup<TP1, TP2, TP3> {
    }

    public interface IComponent<in TP1, in TP2, in TP3, in TP4> : IComponent, ISetup<TP1, TP2, TP3, TP4> {
    }
    
    public interface IComponent<in TP1, in TP2, in TP3, in TP4, in TP5> : IComponent, ISetup<TP1, TP2, TP3, TP4, TP5> {
    }
    
    public interface IRefComponent : IComponent {
        Entity Entity { get; }
        void OnAttach(in Entity entity);
        void OnDetach();
    }
    
    public abstract class Component : IRefComponent {
        
        public Entity Entity { get; private set; }
        
        protected Sandbox Sandbox => Entity.Sandbox;

        void IRefComponent.OnAttach(in Entity entity) {
            this.Entity = entity;
        }
        void IRefComponent.OnDetach() {
            this.Entity = Entity.Null;
        }
    }

    public static class ComponentExtension {
        public static ComponentPtr<T> Wrap<T>(this T component) where T : IRefComponent {
            return component.Entity.Wrap<T>();
        }
    }
}
