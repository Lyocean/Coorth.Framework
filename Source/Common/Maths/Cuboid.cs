using System;
using System.Diagnostics.Contracts;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Coorth.Maths {
    [StoreContract("C22D1436-F4A0-4D64-BEC1-899551022A2C")]
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Cuboid : IEquatable<Cuboid>, IBounding {
        
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

        public float MinX => X;
        
        public float MinY => Y;

        public float MinZ => Z;

        public float MaxX => X + W;
        
        public float MaxY => Y + H;

        public float MaxZ => Z + D;
        
        public Vector3 Max => new Vector3(X + W, Y + H, Z + D);

        public Vector3 Extent => new Vector3(W * 0.5f, H * 0.5f, D * 0.5f);

        public Cuboid(Vector3 min, Vector3 max) {
            this.X = Math.Min(min.X, max.X);
            this.Y = Math.Min(min.Y, max.Y);
            this.Z = Math.Min(min.Z, max.Z);
            this.W = Math.Abs(max.X - min.X);
            this.H = Math.Abs(max.Y - min.Y);
            this.D = Math.Abs(max.Z - min.Z);
        }

        public bool Intersects(in Cuboid other) {
            return (MinX <= other.MaxX && other.MinX <= MaxX) &&
                   (MinY <= other.MaxY && other.MinY <= MaxY) &&
                   (MinZ <= other.MaxZ && other.MinZ <= MaxZ);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Intersects(in Sphere sphere) {
            return Intersects(in sphere.Center, in sphere.Radius);
        }

        public bool Intersects(in Vector3 center, in float radius) {
            var vector = Vector3.Clamp(center, Min, Max);
            var distance = Vector3.DistanceSquared(center, vector);
            return distance <= radius * radius;
        }

        public ContainmentType Contains(in Vector3 position) {
            if (MinX <= position.X && MaxX >= position.X &&
                MinY <= position.Y && MaxY >= position.Y &&
                MinZ <= position.Z && MaxZ >= position.Z) {
                return ContainmentType.Contains;
            }
            return ContainmentType.Disjoint;
        }
        
        public ContainmentType Contains(in Cuboid other) {
            if (MaxX < other.MinX || MinX > other.MaxX ||
                MaxY < other.MinY || MinY > other.MaxY ||
                MaxZ < other.MinZ || MinZ > other.MaxZ) {
                return ContainmentType.Disjoint;
            }
            if (MinX <= other.X && other.MaxX <= MaxX && 
                MinY <= other.Y && other.MaxY <= MaxY &&
                MinZ <= other.Z && other.MaxZ <= MaxZ) {
                return ContainmentType.Contains;
            }
            return ContainmentType.Intersects;
        }
        
        public ContainmentType Contains(in Sphere sphere) {
            var vector = Vector3.Clamp(sphere.Center, Min, Max);
            var distance = Vector3.DistanceSquared(sphere.Center, vector);

            if (distance > sphere.Radius * sphere.Radius) {
                return ContainmentType.Disjoint;
            }
            if (((X + sphere.Radius <= sphere.Center.X) && (sphere.Center.X <= Max.X - sphere.Radius) && (Max.X - Min.X > sphere.Radius)) &&
                ((Y + sphere.Radius <= sphere.Center.Y) && (sphere.Center.Y <= Max.Y - sphere.Radius) && (Max.Y - Min.Y > sphere.Radius)) &&
                ((Z + sphere.Radius <= sphere.Center.Z) && (sphere.Center.Z <= Max.Z - sphere.Radius) && (Max.Z - Min.Z > sphere.Radius))) {
                return ContainmentType.Contains;
            }
            return ContainmentType.Intersects;
        }
        
        public bool Equals(Cuboid other) {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W) &&
                   H.Equals(other.H) && D.Equals(other.D);
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