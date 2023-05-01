namespace Coorth.Framework; 

public readonly struct ComponentBinding<T> where T: IComponent {

    private readonly World world;
        
    private readonly ComponentGroup<T> group;
        
    internal ComponentBinding(World world, ComponentGroup<T> group) {
        this.world = world;
        this.group = group;
    }

    public ComponentBinding<T> AddDependency<TP1>() where TP1: IComponent{
        var componentGroup1 = world.GetComponentGroup<TP1>();
        group.AddDependency(componentGroup1);
        return this;
    }

    public bool HasDependency<TComp2>() where TComp2 : IComponent {
        return group.HasDependency(typeof(TComp2));
    }

    public void SetFactory(IComponentFactory<T> factory) {
        group.Factory = factory;
    }
}