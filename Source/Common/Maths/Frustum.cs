using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Coorth.Maths; 

[DataDefine(DataFlags.PubField), Guid("7964BCA2-FF48-4CF3-8DB2-39EB88480A73")]
[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
public partial record struct Frustum(Plane Near, Plane Far, Plane Left, Plane Right, Plane Top, Plane Bottom) {
        
    public Plane Near = Near;
        
    public Plane Far = Far;
        
    public Plane Left = Left;
        
    public Plane Right = Right;
        
    public Plane Top = Top;
        
    public Plane Bottom = Bottom;

    public Frustum(ref Matrix4x4 matrix) : this(Plane.Normalize(new Plane(matrix.M13, matrix.M23, matrix.M33, matrix.M43)), Plane.Normalize(new Plane(matrix.M14 - matrix.M13, matrix.M24 - matrix.M23, matrix.M34 - matrix.M33, matrix.M44 - matrix.M43)), Plane.Normalize(new Plane(matrix.M14 + matrix.M11, matrix.M24 + matrix.M21, matrix.M34 + matrix.M31, matrix.M44 + matrix.M41)), Plane.Normalize(new Plane(matrix.M14 - matrix.M11, matrix.M24 - matrix.M21, matrix.M34 - matrix.M31, matrix.M44 - matrix.M41)), Plane.Normalize(new Plane(matrix.M14 - matrix.M12, matrix.M24 - matrix.M22, matrix.M34 - matrix.M32, matrix.M44 - matrix.M42)), Plane.Normalize(new Plane(matrix.M14 + matrix.M12, matrix.M24 + matrix.M22, matrix.M34 + matrix.M32, matrix.M44 + matrix.M42))) {
    }

    public readonly Plane this[int index] {
        get {
            return index switch {
                0 => Near,
                1 => Far,
                2 => Left,
                3 => Right,
                4 => Top,
                5 => Bottom,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public readonly bool Intersects(in Sphere sphere) {
        return Contains(in sphere) != ContainmentType.Disjoint;
    }
        
    public readonly bool Intersects(in Vector3 center, in float radius) {
        return InternalContains(in center, in radius) != ContainmentType.Disjoint;
    }
        
    // public readonly bool Intersects(in Cuboid cuboid) {
    //     //TODO
    //     throw new NotImplementedException();
    // }
        
    public readonly ContainmentType Contains(in Sphere sphere) {
        return InternalContains(in sphere.Center, in sphere.Radius);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private readonly ContainmentType InternalContains(in Vector3 center, in float radius) {
        var number = 0;
        for (var i = 0; i < 6; i++) {
            var plane = this[i];
            float value = plane.Normal.X * center.X + plane.Normal.Y * center.Y + plane.Normal.Z * center.Z + plane.D;
            if (value > radius) {
                return ContainmentType.Disjoint;
            }
            if (value < 0f - radius) {
                number++;
            }
        }
        if (number != 6) {
            return ContainmentType.Intersects;
        }
        return ContainmentType.Contains;
    }
    
}