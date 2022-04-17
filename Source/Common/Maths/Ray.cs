using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Coorth.Maths {
    [DataContract, Guid("9DB02E7C-732B-42F3-A762-FF847927A2BA")]
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Ray: IEquatable<Ray> {
        
        public Vector3 Position;
        
        public Vector3 Direction;

        public Ray(Vector3 position, Vector3 direction) {
            this.Position = position;
            this.Direction = direction;
        }

        public readonly bool Equals(Ray other) {
            return Position.Equals(other.Position) && Direction.Equals(other.Direction);
        }

        public override readonly bool Equals(object? obj) {
            return obj is Ray other && Equals(other);
        }

        public static bool operator ==(Ray left, Ray right) {
            return left.Equals(right);
        }

        public static bool operator !=(Ray left, Ray right) {
            return !(left == right);
        }
        
        public override readonly int GetHashCode() {
            return HashCode.Combine(Position, Direction);
        }

        public override readonly string ToString() {
            return $"Ray(Position:{Position},Direction:{Direction})";
        }
    }
}