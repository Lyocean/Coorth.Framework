using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Coorth.Maths {
    [DataContract, Guid("DE608657-DB40-4EAB-AA16-AA31AE1CFA97")]
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Size3 : IEquatable<Size3> {
        
        public int X;
        
        public int Y;
        
        public int Z;

        public readonly int W => X;
        public readonly int H => Y;
        public readonly int D => Z;

        public Size3(int width, int height, int depth) {
            this.X = width;
            this.Y = height;
            this.Z = depth;
        }

        public readonly bool Equals(Size3 other) {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override readonly bool Equals(object? obj) {
            return obj is Size3 other && Equals(other);
        }
        
        public static bool operator ==(Size3 left, Size3 right) {
            return left.Equals(right);
        }

        public static bool operator !=(Size3 left, Size3 right) {
            return !(left == right);
        }

        public override readonly int GetHashCode() {
            return HashCode.Combine(X, Y, Z);
        }
        
        public override readonly string ToString() {
            return $"Size3(W:{W},H:{H},Z:{Z})";
        }
    }
}