using System.Numerics;
using Coorth.Framework;

namespace Coorth.Framework; 

[Component]
public struct WorldMatrixComponent : IComponent {
    public Matrix4x4 Value;
}

[Component]
public struct PositionComponent : IComponent {
    public Vector3 Value;
}

[Component]
public struct RotationComponent : IComponent {
    public Quaternion Value;
}

[Component]
public struct ScalingComponent : IComponent {
    public Vector3 Value;
}