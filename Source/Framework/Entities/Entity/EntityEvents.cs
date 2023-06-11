namespace Coorth.Framework;

[Event]
public readonly record struct EntityCreateEvent(Entity Entity) {
    public readonly Entity Entity = Entity;
    public EntityId Id => Entity.Id;
    public World World => Entity.World;
}

[Event]
public readonly record struct EntityRemoveEvent(Entity Entity) {
    public readonly Entity Entity = Entity;
    public EntityId Id => Entity.Id;
    public World World => Entity.World;
}

[Event]
public readonly record struct EntityActiveEvent(Entity Entity, bool Active) {
    public readonly Entity Entity = Entity;
    public readonly bool Active = Active;
    public EntityId Id => Entity.Id;
    public World World => Entity.World;
}
