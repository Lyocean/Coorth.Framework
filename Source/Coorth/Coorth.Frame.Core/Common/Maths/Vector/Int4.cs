using System;
using System.Runtime.InteropServices;

namespace Coorth.Maths {
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Int4 : IEquatable<Int4> {
        public int X;
        
        public int Y;
        
        public int Z;

        public int W;

        public Int4(int x, int y, int z, int w) {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public bool Equals(Int4 other) {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }

        public override bool Equals(object obj) {
            return obj is Int4 other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ Z;
                hashCode = (hashCode * 397) ^ W;
                return hashCode;
            }
        }
        
        public override string ToString() {
            return $"Int3({X}, {Y}, {Z}, {W})";
        }
    }
}