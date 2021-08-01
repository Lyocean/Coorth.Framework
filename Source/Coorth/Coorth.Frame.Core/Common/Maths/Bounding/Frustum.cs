using System;
using System.Numerics;

namespace Coorth.Maths {
    public struct Frustum {
        
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

        public Plane this[int index] {
            get {
                switch (index) {
                    case 0: return Near;
                    case 1: return Far;
                    case 2: return Left;
                    case 3: return Right;
                    case 4: return Top;
                    case 5: return Bottom;
                }
                throw new ArgumentOutOfRangeException();
            }
        }

        // public ContainmentType Contains(ref Cuboid cuboid) {
        //     
        // }

        public bool Intersects(Sphere sphere) {
            return Contains(ref sphere) != ContainmentType.Disjoint;
        }
        
        public ContainmentType Contains(ref Sphere sphere) {
            Vector3 center = sphere.Center;
            float radius = sphere.Radius;
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

        // public bool Equals(Frustum other) {
        //     return Matrix.Equals(other.Matrix) && Near.Equals(other.Near) && Far.Equals(other.Far) && Left.Equals(other.Left) && Right.Equals(other.Right) && Top.Equals(other.Top) && Bottom.Equals(other.Bottom);
        // }
        //
        // public override bool Equals(object obj) {
        //     return obj is Frustum other && Equals(other);
        // }
        //
        // public override int GetHashCode() {
        //     return HashCode.Combine(Matrix, Near, Far, Left, Right, Top, Bottom);
        // }
    }
}