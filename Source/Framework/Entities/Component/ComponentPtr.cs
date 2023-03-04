namespace Coorth.Framework;

public readonly struct ComponentPtr<T> where T : IComponent {
    
    private readonly ComponentGroup<T> group;

    private readonly int index;

    public World World => group.World;

    public ref T Get() => ref group.Get(index);

    public Entity Entity => World.GetEntity(group.GetEntityIndex(index));

    internal ComponentPtr(ComponentGroup<T> group, int index) {
        this.group = group;
        this.index = index;
    }
}