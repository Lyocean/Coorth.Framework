using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Coorth.Maths {
    [DataContract, Guid("1FFE09DA-3C3D-4D67-A3FB-E3DA6EA413E5")]
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TransformTRS : IEquatable<TransformTRS> {
        
        public Vector3 Position;
        
        public Quaternion Rotation;
        
        public Vector3 Scale;
        
        public TransformTRS(Vector3 position) : this(position, Quaternion.Identity, Vector3.One) {  }

        public TransformTRS(Vector3 position, Quaternion rotation) : this(position, rotation, Vector3.One) {  }
        
        public TransformTRS(Vector3 position, Quaternion rotation, Vector3 scale) {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public TransformTRS(in Matrix4x4 matrix) {
            matrix.Decompose(out Position, out Rotation, out Scale);
        }

        public static TransformTRS Identity => new TransformTRS(Vector3.Zero);

        public readonly bool Equals(TransformTRS other) {
            return Position.Equals(other.Position) && Rotation.Equals(other.Rotation) && Scale.Equals(other.Scale);
        }

        public override readonly bool Equals(object? obj) {
            return obj is TransformTRS other && Equals(other);
        }

        public static bool operator ==(in TransformTRS left, in TransformTRS right) {
            return left.Equals(right);
        }

        public static bool operator !=(in TransformTRS left, in TransformTRS right) {
            return !(left == right);
        }
        
        public override readonly int GetHashCode() {
            return HashCode.Combine(Position, Rotation, Scale);
        }

        public override readonly string ToString() {
            return $"Transform(Position:{Position},Rotation:{Rotation},Scale:{Scale})";
        }
    }
}