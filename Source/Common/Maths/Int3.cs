using System;
using System.Runtime.InteropServices;

namespace Coorth.Maths; 

[DataDefine(StoreFlags.PublicField), Guid("D1519E05-A09A-485A-8EEB-49C2C668ACF3")]
[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
public partial record struct Int3(int X, int Y, int Z) {
        
    public int X = X;
        
    public int Y = Y;
        
    public int Z = Z;
        
    public static readonly Int3 Zero;

    public static readonly Int3 One = new(1, 1, 1);

    public static readonly Int3 UnitX = new(1, 0, 0);
        
    public static readonly Int3 UnitY = new(0, 1, 0);

    public static readonly Int3 UnitZ = new(0, 0, 1);

    public static readonly Int3 MinValue = new(int.MinValue);
        
    public static readonly Int3 MaxValue = new(int.MaxValue);

    public Int3(int value) : this(value, value, value) {
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
    
    public static implicit operator Int3((int x, int y, int z) value) {
        return new Int3(value.x, value.y, value.z);
    }
}