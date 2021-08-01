using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Coorth.Maths {
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Transform : IEquatable<Transform> {
        
        public Vector3 Position;
        
        public Quaternion Rotation;
        
        public Vector3 Scale;
        
        public Transform(Vector3 position) : this(position, Quaternion.Identity, Vector3.One) {  }

        public Transform(Vector3 position, Quaternion rotation) : this(position, rotation, Vector3.One) {  }
        
        public Transform(Vector3 position, Quaternion rotation, Vector3 scale) {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public static Transform Identity => new Transform(Vector3.Zero);

        public bool Equals(Transform other) {
            return Position.Equals(other.Position) && Rotation.Equals(other.Rotation) && Scale.Equals(other.Scale);
        }

        public override bool Equals(object obj) {
            return obj is Transform other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = Position.GetHashCode();
                hashCode = (hashCode * 397) ^ Rotation.GetHashCode();
                hashCode = (hashCode * 397) ^ Scale.GetHashCode();
                return hashCode;
            }
        }
    }
}