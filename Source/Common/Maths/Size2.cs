using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Coorth.Maths {
    [DataContract, Guid("61A2C677-E467-4D4B-A1AA-C84C201432C4")]
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Size2 : IEquatable<Size2> {
        
        public int W;
        
        public int H;
        
        public Size2(int width, int height) {
            this.W = width;
            this.H = height;
        }

        public readonly bool Equals(Size2 other) {
            return W == other.W && H == other.H;
        }

        public override readonly bool Equals(object? obj) {
            return obj is Size2 other && Equals(other);
        }
        
        public static bool operator ==(Size2 left, Size2 right) {
            return left.Equals(right);
        }

        public static bool operator !=(Size2 left, Size2 right) {
            return !(left == right);
        }

        public override readonly int GetHashCode() {
            return HashCode.Combine(W, H);
        }

        public override readonly string ToString() {
            return $"Size2(W:{W},H:{H})";
        }
    }
}