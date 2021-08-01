using System;
using System.Numerics;

namespace Coorth.Maths {
    public readonly struct Bounding : IEquatable<Bounding> {
        
        public readonly Vector3 Position;
        
        public readonly float Radius;

        public readonly Vector3 HalfExtend;

        public Vector3 Extend => HalfExtend * 2;

        public Bounding(Vector3 position, Vector3 halfExtend) {
            this.Position = position;
            this.HalfExtend = halfExtend;
            this.Radius = (float) Math.Sqrt(halfExtend.X * halfExtend.X + 
                                            halfExtend.Y * halfExtend.Y +
                                            halfExtend.Z * halfExtend.Z);
        }

        // public ContainmentType Contains(ref Bounding bounding) {
        //     Vector3 center = bounding.Position;
        //     float radius = bounding.Radius;
        //     
        //     var lengthSquared = (Position - bounding.Position).LengthSquared();
        //     if (lengthSquared > (Radius + bounding.Radius) * (Radius + bounding.Radius)) {
        //         return ContainmentType.Disjoint;
        //     }
        //
        //     return ContainmentType.Contains;
        // }
        
        public bool Equals(Bounding other) {
            return Position.Equals(other.Position) && Radius.Equals(other.Radius) && HalfExtend.Equals(other.HalfExtend);
        }

        public override bool Equals(object obj) {
            return obj is Bounding other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                return ((Position.GetHashCode() * 397 ^ HalfExtend.GetHashCode()) * 397) ^ Radius.GetHashCode();
            }
        }

        public static bool operator ==(Bounding left, Bounding right) {
            return left.Equals(right);
        }

        public static bool operator !=(Bounding left, Bounding right) {
            return !(left == right);
        }

        public override string ToString() {
            return $"Bounding(Position:{Position}, Radius:{Radius}, Extend:{Extend})";
        }
    }
}