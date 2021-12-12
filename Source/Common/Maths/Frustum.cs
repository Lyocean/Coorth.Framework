using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Coorth.Maths {
    [StoreContract("7964BCA2-FF48-4CF3-8DB2-39EB88480A73")]
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Frustum : IEquatable<Frustum>, IBounding {
        
        public Plane Near;
        
        public Plane Far;
        
        public Plane Left;
        
        public Plane Right;
        
        public Plane Top;
        
        public Plane Bottom;

        public Frustum(Plane near, Plane far, Plane left, Plane right, Plane top, Plane bottom) {
            this.Near   = near;
            this.Far    = far;
            this.Left   = left;
            this.Right  = right;
            this.Top    = top;
            this.Bottom = bottom;
        }
        
        public Frustum(ref Matrix4x4 matrix) {
            this.Left   = Plane.Normalize(new Plane(matrix.M14 + matrix.M11, matrix.M24 + matrix.M21, matrix.M34 + matrix.M31, matrix.M44 + matrix.M41));
            this.Right  = Plane.Normalize(new Plane(matrix.M14 - matrix.M11, matrix.M24 - matrix.M21, matrix.M34 - matrix.M31, matrix.M44 - matrix.M41));
            
            this.Top    = Plane.Normalize(new Plane(matrix.M14 - matrix.M12, matrix.M24 - matrix.M22, matrix.M34 - matrix.M32, matrix.M44 - matrix.M42));
            this.Bottom = Plane.Normalize(new Plane(matrix.M14 + matrix.M12, matrix.M24 + matrix.M22, matrix.M34 + matrix.M32, matrix.M44 + matrix.M42));
            
            this.Near   = Plane.Normalize(new Plane(matrix.M13, matrix.M23, matrix.M33, matrix.M43));
            this.Far    = Plane.Normalize(new Plane(matrix.M14 - matrix.M13, matrix.M24 - matrix.M23, matrix.M34 - matrix.M33, matrix.M44 - matrix.M43));
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

        public bool Equals(Frustum other) {
            return Near.Equals(other.Near) && Far.Equals(other.Far) 
                && Left.Equals(other.Left) && Right.Equals(other.Right) 
                && Top.Equals(other.Top) && Bottom.Equals(other.Bottom);
        }
        
        public override bool Equals(object obj) {
            return obj is Frustum other && Equals(other);
        }
        
        public static bool operator ==(Frustum left, Frustum right) {
            return left.Equals(right);
        }

        public static bool operator !=(Frustum left, Frustum right) {
            return !(left == right);
        }
        
        public override int GetHashCode() {
            unchecked {
                return (Near.GetHashCode() * 397) ^ (Far.GetHashCode() * 397) ^ (Left.GetHashCode() * 397) ^ (Right.GetHashCode() * 397) ^ (Top.GetHashCode() * 397) ^ (Bottom.GetHashCode() * 397);      
            }
        }

        public override string ToString() {
            return $"Frustum(Near:{Near},Far:{Far},Left:{Left},Right:{Right},Top:{Top},Bottom:{Bottom})";
        }
    }
}