using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Coorth.Maths {
    [DataContract, Guid("D1519E05-A09A-485A-8EEB-49C2C668ACF3")]
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Int3 : IEquatable<Int3> {
        
        public int X;
        
        public int Y;
        
        public int Z;
        
        public static readonly Int3 Zero;

        public static readonly Int3 One = new Int3(1, 1, 1);

        public static readonly Int3 UnitX = new Int3(1, 0, 0);
        
        public static readonly Int3 UnitY = new Int3(0, 1, 0);

        public static readonly Int3 UnitZ = new Int3(0, 0, 1);

        public static readonly Int3 MinValue = new Int3(int.MinValue);
        
        public static readonly Int3 MaxValue = new Int3(int.MaxValue);

        public Int3(int value) {
            X = value;
            Y = value;
            Z = value;
        }
       
        public Int3(int x, int y, int z) {
            X = x;
            Y = y;
            Z = z;
        }
        
        public static Int3 operator +(Int3 l, Int3 r) {
            return new Int3(l.X + r.X, l.Y + r.Y, l.Z + r.Z);
        }
        
        public static Int3 operator -(Int3 l, Int3 r) {
            return new Int3(l.X - r.X, l.Y - r.Y, l.Z - r.Z);
        }
        
        public static Int3 operator *(Int3 l, int r) {
            return new Int3(l.X * r, l.Y * r, l.Z * r);
        }
        
        public static Int3 operator *(int l, Int3 r) {
            return new Int3(l * r.X, l * r.Y, l * r.Z);
        }
        
        public readonly bool Equals(Int3 other) {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override readonly bool Equals(object? obj) {
            return obj is Int3 other && Equals(other);
        }

        public static bool operator ==(Int3 left, Int3 right) {
            return left.Equals(right);
        }

        public static bool operator !=(Int3 left, Int3 right) {
            return !(left == right);
        }
        
        public override readonly int GetHashCode() {
            return HashCode.Combine(X, Y, Z);
        }

        public override readonly string ToString() {
            return $"Int3({X}, {Y}, {Z})";
        }
    }
}