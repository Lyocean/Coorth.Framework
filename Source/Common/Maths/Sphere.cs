using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Coorth.Maths {
    [DataContract, Guid("82EF8D53-2DE2-401E-A910-DAC37A03A46F")]
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Sphere : IEquatable<Sphere>, IBounding {
        
        public Vector3 Center;
        
        public float Radius;

        public Sphere(Vector3 center, float radius) {
            this.Center = center;
            this.Radius = radius;
        }
        
        public readonly bool Equals(Sphere other) {
            return Center.Equals(other.Center) && Radius.Equals(other.Radius);
        }

        public override readonly bool Equals(object? obj) {
            return obj is Sphere other && Equals(other);
        }

        public override readonly int GetHashCode() {
            return HashCode.Combine(Center, Radius);
        }

        public static bool operator ==(Sphere left, Sphere right) {
            return left.Equals(right);
        }

        public static bool operator !=(Sphere left, Sphere right) {
            return !(left == right);
        }

        public override readonly string ToString() {
            return $"Sphere(Center:{Center}, Radius:{Radius})";
        }
    }
}