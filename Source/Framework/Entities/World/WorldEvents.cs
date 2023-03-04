using System;

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
public readonly struct ComponentAddEvent {
    public readonly EntityId Id;
    private readonly IComponentGroup group;
    private readonly int index;

    public World World => group.World;
    public Entity Entity => World.GetEntity(Id);
    public Type Type => group.Type;
    public IComponent Component => group.Get(index);
    public ref T Get<T>() where T : struct, IComponent => ref ((ComponentGroup<T>)group).Get(index);

    internal ComponentAddEvent(EntityId id, IComponentGroup group, int index) {
        this.Id = id;
        this.group = group;
        this.index = index;
    }
}

[Event]
public readonly struct ComponentAddEvent<T> where T : IComponent {
    public readonly EntityId Id;
    private readonly ComponentGroup<T> group;
    private readonly int index;

    public World World => group.World;
    public Entity Entity => World.GetEntity(Id);
    public Type Type => group.Type;
    public T Component => group[index];
    public ref T Get() => ref group.Get(index);

    internal ComponentAddEvent(EntityId id, ComponentGroup<T> group, int index) {
        this.Id = id;
        this.group = group;
        this.index = index;
    }
}

[Event]
public readonly struct ComponentModifyEvent {
    public readonly EntityId Id;
    private readonly IComponentGroup group;
    private readonly int index;

    public World World => group.World;
    public Entity Entity => World.GetEntity(Id);
    public Type Type => group.Type;
    public IComponent Component => group.Get(index);
    public ref T Get<T>() where T : struct, IComponent => ref ((ComponentGroup<T>)group).Get(index);

    internal ComponentModifyEvent(EntityId id, IComponentGroup group, int index) {
        this.Id = id;
        this.group = group;
        this.index = index;
    }
}

[Event]
public readonly struct ComponentModifyEvent<T> where T : IComponent {
    public readonly EntityId Id;
    private readonly ComponentGroup<T> group;
    private readonly int index;

    public World World => group.World;
    public Entity Entity => World.GetEntity(Id);
    public Type Type => group.Type;
    public T Component => group[index];
    public ref T Get() => ref group.Get(index);

    internal ComponentModifyEvent(EntityId id, ComponentGroup<T> group, int index) {
        this.Id = id;
        this.group = group;
        this.index = index;
    }
}

[Event]
public readonly struct ComponentRemoveEvent {
    public readonly EntityId Id;
    private readonly IComponentGroup group;
    private readonly int index;

    public World World => group.World;
    public Type Type => group.Type;
    public Entity Entity => World.GetEntity(Id);
    public IComponent Component => group.Get(index);
    public ref T Get<T>() where T : struct, IComponent => ref ((ComponentGroup<T>)group).Get(index);

    internal ComponentRemoveEvent(EntityId id, IComponentGroup group, int index) {
        this.Id = id;
        this.group = group;
        this.index = index;
    }
}

[Event]
public readonly struct ComponentRemoveEvent<T> where T : IComponent {
    public readonly EntityId Id;
    private readonly ComponentGroup<T> group;
    private readonly int index;

    public World World => group.World;
    public Entity Entity => World.GetEntity(Id);
    public Type Type => group.Type;
    public T Component => group[index];
    public ref T Get() => ref group.Get(index);

    internal ComponentRemoveEvent(EntityId id, ComponentGroup<T> group, int index) {
        this.Id = id;
        this.group = group;
        this.index = index;
    }
}

[Event]
public readonly struct ComponentEnableEvent<T> where T : IComponent {
    public World World => group.World;
    public readonly EntityId Id;
    private readonly ComponentGroup<T> group;

    private readonly int index;
    public readonly bool IsEnable;

    public Entity Entity => World.GetEntity(Id);
    public Type Type => group.Type;
    public T Component => group[index];

    public ref T Get() => ref group.Get(index);

    internal ComponentEnableEvent(EntityId id, ComponentGroup<T> group, int index, bool enable) {
        this.Id = id;
        this.group = group;
        this.index = index;
        this.IsEnable = enable;
    }
}

[Event]
public readonly record struct SystemAddEvent(World World, Type Type, SystemBase System) {
    public readonly World World = World;
    public readonly Type Type = Type;
    public readonly SystemBase System = System;
}

[Event]
public readonly record struct SystemRemoveEvent(World World, Type Type, SystemBase System) {
    public readonly World World = World;
    public readonly Type Type = Type;
    public readonly SystemBase System = System;
}