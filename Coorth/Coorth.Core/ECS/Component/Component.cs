namespace Coorth.ECS {
    public interface IComponent {
    }

    public interface IRefComponent : IComponent {
        Entity Entity { get; set; }
    }

    public class Component : IRefComponent {

        public Entity Entity { get; set; }

        public EcsContainer Container => Entity.Container;
    }

}
