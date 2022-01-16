using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Coorth.Maths {
    [StoreContract("2B22E356-FF59-422D-8177-5E4841405314")]
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Capsule : IEquatable<Capsule> {
        
        public Vector3 Center;
        
        public float Radius;
        
        public float Height;
        
        public Capsule(Vector3 center, float height, float radius) {
            this.Center = center;
            this.Height = height;
            this.Radius = radius;
        }

        public bool Equals(Capsule other) {
            return Center.Equals(other.Center) && Radius.Equals(other.Radius) && Height.Equals(other.Height);
        }

        public override bool Equals(object obj) {
            return obj is Capsule other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = Center.GetHashCode();
                hashCode = (hashCode * 397) ^ Radius.GetHashCode();
                hashCode = (hashCode * 397) ^ Height.GetHashCode();
                return hashCode;
            }
        }
        
        public static bool operator ==(Capsule left, Capsule right) {
            return left.Equals(right);
        }

        public static bool operator !=(Capsule left, Capsule right) {
            return !(left == right);
        }
        
        public override string ToString() {
            return $"Capsule(Center:{Center}, Height:{Height}, Radius:{Radius})";
        }
    }
}