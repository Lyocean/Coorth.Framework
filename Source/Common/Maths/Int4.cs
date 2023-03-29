using System;
using System.Runtime.InteropServices;

namespace Coorth.Maths; 

[DataDefine(DataFlags.PublicField), Guid("52A4045C-213C-4E02-9F18-89BC3AC5A03B")]
[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
public partial record struct Int4(int X, int Y, int Z, int W) {
        
    public int X = X;
        
    public int Y = Y;
        
    public int Z = Z;

    public int W = W;

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
}