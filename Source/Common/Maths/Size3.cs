using System;
using System.Runtime.InteropServices;

namespace Coorth.Maths {
    [StoreContract("DE608657-DB40-4EAB-AA16-AA31AE1CFA97")]
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Size3 : IEquatable<Size3> {
        
        public int X;
        
        public int Y;
        
        public int Z;

        public int W => X;
        public int H => Y;
        public int D => Z;

        public Size3(int width, int height, int depth) {
            this.X = width;
            this.Y = height;
            this.Z = depth;
        }

        public bool Equals(Size3 other) {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj) {
            return obj is Size3 other && Equals(other);
        }
        
        public static bool operator ==(Size3 left, Size3 right) {
            return left.Equals(right);
        }

        public static bool operator !=(Size3 left, Size3 right) {
            return !(left == right);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ Z;
                return hashCode;
            }
        }
        
        public override string ToString() {
            return $"Size3(W:{W},H:{H},Z:{Z})";
        }
    }
}