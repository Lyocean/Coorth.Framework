using System;
using System.Numerics;

namespace Coorth.Framework;

public interface INodeContext {
    
    Entity HostEntity { get; }
    
    World World => HostEntity.World;
    
    Entity? Caster { get; }
    Entity? Target { get; }
    Span<Entity> Targets { get; }
    Vector3 Position { get; }
    Quaternion Rotation { get; }
}
