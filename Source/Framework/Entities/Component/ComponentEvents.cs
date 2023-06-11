using System;


namespace Coorth.Framework;

[Event]
public readonly record struct ComponentAddEvent(Entity Entity, ComponentGroup Group, int Index) {
    public readonly Entity Entity = Entity;
    public readonly ComponentGroup Group = Group;
    public readonly int Index = Index;

    public EntityId Id => Entity.Id;
    public World World => Entity.World;
    public Type Type => Group.Type.Type;
    public IComponent Component => Group._Get(Index);
    public ref T Get<T>() where T : IComponent => ref ((ComponentGroup<T>)Group).Get(Index);
}

[Event]
public readonly record struct ComponentAddEvent<T>(Entity Entity, ComponentGroup<T> Group, int Index) where T : IComponent {
    public readonly Entity Entity = Entity;
    public readonly ComponentGroup<T> Group = Group;
    public readonly int Index = Index;

    public EntityId Id => Entity.Id;
    public World World => Entity.World;
    public Type Type => Group.Type.Type;
    public ref T Component => ref Group.Get(Index);
    public ref T Get() => ref Group.Get(Index);
}

[Event]
public readonly record struct ComponentModifyEvent(Entity Entity, ComponentGroup Group, int Index) {
    public readonly Entity Entity = Entity;
    public readonly ComponentGroup Group = Group;
    public readonly int Index = Index;

    public EntityId Id => Entity.Id;
    public World World => Entity.World;
    public Type Type => Group.Type.Type;
    public IComponent Component => Group._Get(Index);
    public ref T Get<T>() where T : IComponent => ref ((ComponentGroup<T>)Group).Get(Index);
}

[Event]
public readonly record struct ComponentModifyEvent<T>(Entity Entity, ComponentGroup<T> Group, int Index) where T : IComponent {
    public readonly Entity Entity = Entity;
    public readonly ComponentGroup<T> Group = Group;
    public readonly int Index = Index;

    public EntityId Id => Entity.Id;
    public World World => Entity.World;
    public Type Type => Group.Type.Type;
    public ref T Component => ref Group.Get(Index);
    public ref T Get() => ref Group.Get(Index);
}

[Event]
public readonly record struct ComponentRemoveEvent(Entity Entity, ComponentGroup Group, int Index) {
    public readonly Entity Entity = Entity;
    public readonly ComponentGroup Group = Group;
    public readonly int Index = Index;

    public EntityId Id => Entity.Id;
    public World World => Entity.World;
    public Type Type => Group.Type.Type;
    public IComponent Component => Group._Get(Index);
    public ref T Get<T>() where T : IComponent => ref ((ComponentGroup<T>)Group).Get(Index);
}

[Event]
public readonly record struct ComponentRemoveEvent<T>(Entity Entity, ComponentGroup<T> Group, int Index) where T : IComponent {
    public readonly Entity Entity = Entity;
    public readonly ComponentGroup<T> Group = Group;
    public readonly int Index = Index;

    public EntityId Id => Entity.Id;
    public World World => Entity.World;
    public Type Type => Group.Type.Type;
    public ref T Component => ref Group.Get(Index);
    public ref T Get() => ref Group.Get(Index);
}

[Event]
public readonly record struct ComponentEnableEvent<T>(Entity Entity, ComponentGroup<T> Group, int Index, bool IsEnable) where T : IComponent {
    public readonly Entity Entity = Entity;
    public readonly ComponentGroup<T> Group = Group;
    public readonly int Index = Index;
    public readonly bool IsEnable = IsEnable;

    public EntityId Id => Entity.Id;
    public World World => Entity.World;
    public Type Type => Group.Type.Type;
    public ref T Component => ref Group.Get(Index);
    public ref T Get() => ref Group.Get(Index);
}
