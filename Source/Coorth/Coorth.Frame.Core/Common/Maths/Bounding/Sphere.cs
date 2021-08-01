using System;
using System.Numerics;

namespace Coorth.Maths {
    [Serializable]
    public struct Sphere : IEquatable<Sphere> {
        
        public Vector3 Center;
        
        public float Radius;

        public Sphere(Vector3 center, float radius) {
            this.Center = center;
            this.Radius = radius;
        }
        
        public bool Equals(Sphere other) {
            return Center.Equals(other.Center) && Radius.Equals(other.Radius);
        }

        public override bool Equals(object obj) {
            return obj is Sphere other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                return (Center.GetHashCode() * 397) ^ Radius.GetHashCode();
            }
        }

        public static bool operator ==(Sphere left, Sphere right) {
            return left.Equals(right);
        }

        public static bool operator !=(Sphere left, Sphere right) {
            return !(left == right);
        }

        public override string ToString() {
            return $"Sphere(Center:{Center}, Radius:{Radius})";
        }
    }
}