﻿using System;

namespace Coorth.Framework; 

[Event]
public readonly struct EventComponentAdd : ISandboxEvent {
        
    public Sandbox Sandbox => group.Sandbox;
        
    public readonly EntityId Id;
        
    private readonly IComponentGroup group;
        
    private readonly int index;

    public Entity Entity => Sandbox.GetEntity(Id);

    public Type Type => group.Type;
        
    public IComponent Component => group.Get(index);

    public ref T Get<T>() where T : struct, IComponent => ref ((ComponentGroup<T>)group).Get(index);

    internal EventComponentAdd(EntityId id, IComponentGroup group, int index) {
        this.Id = id;
        this.group = group;
        this.index = index;
    }
}

[Event]
public readonly struct EventComponentAdd<T> : ISandboxEvent where T : IComponent {
        
    public Sandbox Sandbox => group.Sandbox;

    public readonly EntityId Id;
        
    private readonly ComponentGroup<T> group;
        
    private readonly int index;

    public Entity Entity => Sandbox.GetEntity(Id);
        
    public Type Type => group.Type;
        
    public T Component => group[index];

    public ref T Get() => ref group.Get(index);

    internal EventComponentAdd(EntityId id, ComponentGroup<T> group, int index) {
        this.Id = id;
        this.group = group;
        this.index = index;
    }
}

[Event]
public readonly struct EventComponentModify : ISandboxEvent {
        
    public Sandbox Sandbox => group.Sandbox;
        
    public readonly EntityId Id;
        
    private readonly IComponentGroup group;
        
    private readonly int index;

    public Entity Entity => Sandbox.GetEntity(Id);
        
    public Type Type => group.Type;
        
    public IComponent Component => group.Get(index);

    public ref T Get<T>() where T : struct, IComponent => ref ((ComponentGroup<T>)group).Get(index);

    internal EventComponentModify(EntityId id, IComponentGroup group, int index) {
        this.Id = id;
        this.group = group;
        this.index = index;
    }
}

[Event]
public readonly struct EventComponentModify<T> : ISandboxEvent where T : IComponent {
        
    public Sandbox Sandbox => group.Sandbox;
        
    public readonly EntityId Id;
        
    private readonly ComponentGroup<T> group;
        
    private readonly int index;

    public Entity Entity => Sandbox.GetEntity(Id);
        
    public Type Type => group.Type;
        
    public T Component => group[index];

    public ref T Get() => ref group.Get(index);

    internal EventComponentModify(EntityId id, ComponentGroup<T> group, int index) {
        this.Id = id;
        this.group = group;
        this.index = index;
    }
}

[Event]
public readonly struct EventComponentRemove : ISandboxEvent {
        
    public Sandbox Sandbox => group.Sandbox;
        
    public readonly EntityId Id;
        
    private readonly IComponentGroup group;
        
    private readonly int index;

    public Type Type => group.Type;
        
    public Entity Entity => Sandbox.GetEntity(Id);
        
    public IComponent Component => group.Get(index);

    public ref T Get<T>() where T : struct, IComponent => ref ((ComponentGroup<T>)group).Get(index);

    internal EventComponentRemove(EntityId id, IComponentGroup group, int index) {
        this.Id = id;
        this.group = group;
        this.index = index;
    }
}

[Event]
public readonly struct EventComponentRemove<T> : ISandboxEvent where T : IComponent {
        
    public Sandbox Sandbox => group.Sandbox;
        
    public readonly EntityId Id;
        
    private readonly ComponentGroup<T> group;
        
    private readonly int index;

    public Entity Entity => Sandbox.GetEntity(Id);
        
    public Type Type => group.Type;
        
    public T Component => group[index];
        
    public ref T Get() => ref group.Get(index);

    internal EventComponentRemove(EntityId id, ComponentGroup<T> group, int index) {
        this.Id = id;
        this.group = group;
        this.index = index;
    }
}
    
[Event]
public readonly struct EventComponentEnable<T> : ISandboxEvent where T : IComponent {
        
    public Sandbox Sandbox => group.Sandbox;
        
    public readonly EntityId Id;
        
    private readonly ComponentGroup<T> group;
        
    private readonly int index;

    public readonly bool IsEnable;

    public Entity Entity => Sandbox.GetEntity(Id);
        
    public Type Type => group.Type;
        
    public T Component => group[index];
        
    public ref T Get() => ref group.Get(index);

    internal EventComponentEnable(EntityId id, ComponentGroup<T> group, int index, bool enable) {
        this.Id = id;
        this.group = group;
        this.index = index;
        this.IsEnable = enable;
    }
}