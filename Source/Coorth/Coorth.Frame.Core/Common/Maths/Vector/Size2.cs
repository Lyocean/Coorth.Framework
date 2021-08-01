using System;
using System.Runtime.InteropServices;

namespace Coorth.Maths {
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Size2 : IEquatable<Size2> {
        public int W;
        public int H;
        
        public Size2(int width, int height) {
            this.W = width;
            this.H = height;
        }

        public bool Equals(Size2 other) {
            return W == other.W && H == other.H;
        }

        public override bool Equals(object obj) {
            return obj is Size2 other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                return (W * 397) ^ H;
            }
        }
    }
}