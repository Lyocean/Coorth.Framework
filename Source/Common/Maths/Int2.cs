using System;
using System.Runtime.InteropServices;

namespace Coorth.Maths; 

[StoreContract, Guid("ECBCA6D3-1B8B-4AE2-B8BD-24AA1A2A54D4")]
[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
public record struct Int2(int X, int Y) {

    public int X = X;

    public int Y = Y;

    public static readonly Int2 Zero;

    public static readonly Int2 UnitX = new(1, 0);

    public static readonly Int2 UnitY = new(0, 1);

    public static readonly Int2 One = new(1, 1);

    public static readonly Int2 MinValue = new(int.MinValue);

    public static readonly Int2 MaxValue = new(int.MaxValue);

    public Int2(int value) : this(value, value) {
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
}