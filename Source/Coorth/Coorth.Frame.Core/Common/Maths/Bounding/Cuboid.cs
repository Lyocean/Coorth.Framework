using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Coorth.Maths {
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Cuboid : IEquatable<Cuboid> {
        
        public float X;
        
        public float Y;
        
        public float Z;
        
        public float W;
        
        public float H;
        
        public float D;

        public Vector3 Position => new Vector3(X, Y, Z);
        
        public Vector3 Size => new Vector3(W, H, D);
        
        public Vector3 Center => new Vector3(X + W * 0.5f, Y + H * 0.5f, Z + D * 0.5f);

        public Vector3 Min => Position;
        
        public Vector3 Max => new Vector3(X + W, Y + H, Z + D);

        public Cuboid(Vector3 min, Vector3 max) {
            this.X = Math.Min(min.X, max.X);
            this.Y = Math.Min(min.Y, max.Y);
            this.Z = Math.Min(min.Z, max.Z);
            this.W = Math.Abs(max.X - min.X);
            this.H = Math.Abs(max.Y - min.Y);
            this.D = Math.Abs(max.Z - min.Z);
        }
        

        public bool Equals(Cuboid other) {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W) && H.Equals(other.H) && D.Equals(other.D);
        }

        public override bool Equals(object obj) {
            return obj is Cuboid other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                hashCode = (hashCode * 397) ^ W.GetHashCode();
                hashCode = (hashCode * 397) ^ H.GetHashCode();
                hashCode = (hashCode * 397) ^ D.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Cuboid left, Cuboid right) {
            return left.Equals(right);
        }

        public static bool operator !=(Cuboid left, Cuboid right) {
            return !(left == right);
        }
        
        public override string ToString() {
            return $"Cuboid(Center:{Center}, Size:{Size})";
        }
    }
}