using System.Numerics;

namespace Coorth.Maths; 

public readonly record struct RayCastHit(Vector3 Point, Vector3 Normal, float Distance, object? Collider, object? Rigidbody) {
    public readonly Vector3 Point = Point;
    public readonly Vector3 Normal = Normal;
    public readonly float Distance = Distance;
    public readonly object? Collider = Collider;
    public readonly object? Rigidbody = Rigidbody;

    public static readonly RayCastHit Null = new();
}