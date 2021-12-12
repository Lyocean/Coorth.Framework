using System;
using System.Runtime.InteropServices;

namespace Coorth.Maths {
    [StoreContract("ECBCA6D3-1B8B-4AE2-B8BD-24AA1A2A54D4")]
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Int2 : IEquatable<Int2> {
        public int X;

        public int Y;

        public static readonly Int2 Zero;

        public static readonly Int2 UnitX = new Int2(1, 0);

        public static readonly Int2 UnitY = new Int2(0, 1);

        public static readonly Int2 One = new Int2(1, 1);

        public static readonly Int2 MinValue = new Int2(int.MinValue);

        public static readonly Int2 MaxValue = new Int2(int.MaxValue);

        public Int2(int value) {
            X = value;
            Y = value;
        }

        public Int2(int x, int y) {
            X = x;
            Y = y;
        }

        public static Int2 operator +(Int2 l, Int2 r) {
            return new Int2(l.X + r.X, l.Y + r.Y);
        }

        public static Int2 operator -(Int2 l, Int2 r) {
            return new Int2(l.X - r.X, l.Y - r.Y);
        }

        public static Int2 operator *(Int2 l, int r) {
            return new Int2(l.X * r, l.Y * r);
        }

        public static Int2 operator *(int l, Int2 r) {
            return new Int2(l * r.X, l * r.Y);
        }

        public bool Equals(Int2 other) {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }

            if (obj.GetType() != this.GetType()) {
                return false;
            }

            return Equals((Int2) obj);
        }

        public static bool operator ==(Int2 left, Int2 right) {
            return left.Equals(right);
        }

        public static bool operator !=(Int2 left, Int2 right) {
            return !(left == right);
        }

        public override int GetHashCode() {
#if NET5_0_OR_GREATER
                return HashCode.Combine(X, Y);
#else
            unchecked {
                return (X * 397) ^ Y;
            }
#endif
        }

        public override string ToString() {
            return $"Int2({X}, {Y})";
        }
    }
}