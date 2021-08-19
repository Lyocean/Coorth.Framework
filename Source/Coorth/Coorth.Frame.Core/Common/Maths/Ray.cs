using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Coorth.Maths {
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Ray: IEquatable<Ray> {
        
        public Vector3 Position;
        
        public Vector3 Direction;

        public Ray(Vector3 position, Vector3 direction) {
            this.Position = position;
            this.Direction = direction;
        }

        public bool Equals(Ray other) {
            return Position.Equals(other.Position) && Direction.Equals(other.Direction);
        }

        public override bool Equals(object obj) {
            return obj is Ray other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                return (Position.GetHashCode() * 397) ^ Direction.GetHashCode();
            }
        }

        public override string ToString() {
            return $"Ray(Position={Position},Direction={Direction})";
        }
    }
}