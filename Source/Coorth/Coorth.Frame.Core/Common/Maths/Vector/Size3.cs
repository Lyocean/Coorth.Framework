using System;
using System.Runtime.InteropServices;

namespace Coorth.Maths {
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

        public override int GetHashCode() {
            unchecked {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ Z;
                return hashCode;
            }
        }
    }
}