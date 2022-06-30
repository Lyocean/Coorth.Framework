using System;

namespace Coorth.Framework; 

[Event]
public record EventSystemAdd(Sandbox Sandbox, Type Type, SystemBase System) : ISandboxEvent {
    public readonly Sandbox Sandbox = Sandbox;
    public readonly Type Type = Type;
    public readonly SystemBase System = System;
}

[Event]
public record EventSystemAdd<T>(Sandbox Sandbox, Type Type, SystemBase System) : ISandboxEvent {
    public readonly Sandbox Sandbox = Sandbox;
    public readonly Type Type = Type;
    public readonly SystemBase System = System;
}

[Event]
public record EventSystemRemove(Sandbox Sandbox, Type Type, SystemBase System) : ISandboxEvent {
    public readonly Sandbox Sandbox = Sandbox;
    public readonly Type Type = Type;
    public readonly SystemBase System = System;
}

[Event]
public record EventSystemRemove<T>(Sandbox Sandbox, Type Type, SystemBase System) : ISandboxEvent {
    public readonly Sandbox Sandbox = Sandbox;
    public readonly Type Type = Type;
    public readonly SystemBase System = System;
}