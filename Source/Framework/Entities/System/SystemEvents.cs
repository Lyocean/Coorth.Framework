using System;

namespace Coorth.Framework; 

[Event]
public readonly record struct SystemAddEvent(World World, Type Type, SystemBase System) {
    public readonly World World = World;
    public readonly Type Type = Type;
    public readonly SystemBase System = System;
}

[Event]
public readonly record struct SystemActiveEvent(World World, Type Type, SystemBase System) {
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