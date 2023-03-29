using System.Numerics;
using Coorth.Framework;
using Coorth.Maths;

namespace Coorth.Framework; 

[Component]
public struct LocalMatrixComponent : IComponent {
    
    public Matrix4x4 Value;
    
    public Vector3 Position { get => Value.Translation; set => Value.Translation = value; }
    
    public Quaternion Rotation { get => Value.GetRotation(); set => Value.SetRotation(value); }
    
    public Vector3 Scaling { get => Value.GetScale(); set => Value.SetScale(value); }
}

[Component]
public struct WorldMatrixComponent : IComponent {
    
    public Matrix4x4 Value;
    
    public Vector3 Position { get => Value.Translation; set => Value.Translation = value; }
    
    public Quaternion Rotation { get => Value.GetRotation(); set => Value.SetRotation(value); }
    
    public Vector3 Scaling { get => Value.GetScale(); set => Value.SetScale(value); }
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
