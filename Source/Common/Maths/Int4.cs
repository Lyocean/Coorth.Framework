using System;
using System.Runtime.InteropServices;

namespace Coorth.Maths {
    [StoreContract("52A4045C-213C-4E02-9F18-89BC3AC5A03B")]
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

        public static Int4 operator +(Int4 l, Int4 r) {
            return new Int4(l.X + r.X, l.Y + r.Y, l.Z + r.Z, l.W + r.W);
        }
        
        public static Int4 operator -(Int4 l, Int4 r) {
            return new Int4(l.X - r.X, l.Y - r.Y, l.Z - r.Z, l.W - r.W);
        }
        
        public static Int4 operator *(Int4 l, int r) {
            return new Int4(l.X * r, l.Y * r, l.Z * r, l.W * r);
        }
        
        public static Int4 operator *(int l, Int4 r) {
            return new Int4(l * r.X, l * r.Y, l * r.Z, l * r.W);
        }
        
        public bool Equals(Int4 other) {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }

        public override bool Equals(object obj) {
            return obj is Int4 other && Equals(other);
        }
        
        public static bool operator ==(Int4 left, Int4 right) {
            return left.Equals(right);
        }

        public static bool operator !=(Int4 left, Int4 right) {
            return !(left == right);
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