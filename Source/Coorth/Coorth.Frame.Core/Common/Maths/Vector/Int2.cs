using System;
using System.Runtime.InteropServices;

namespace Coorth.Maths {
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class Int2: IEquatable<Int2> {
        
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

        public bool Equals(Int2 other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Int2) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (X * 397) ^ Y;
            }
        }
    }
}