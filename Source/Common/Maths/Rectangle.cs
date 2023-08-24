using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Coorth.Maths; 

[DataDefine(DataFlags.PubField), Guid("62442A87-EBF5-44D7-89EF-A7BE8C4E4095")]
[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
public partial record struct Rectangle(float X, float Y, float W, float H) {
        
    public float X = X;
        
    public float Y = Y;
        
    public float W = W;
        
    public float H = H;

    public readonly Vector2 Min => new(X, Y);

    public readonly Vector2 Max => new(X + W, Y + H);

    public readonly Vector2 Size => new(W, H);

    public readonly float XMin => X;
    
    public readonly float XMax => X + W;
    
    public readonly float YMin => Y;
    
    public readonly float YMax => Y + H;
        
    public static readonly Rectangle Empty;

    public readonly bool Contains(Vector2 position) {
        return (position.X >= X && position.Y >= Y) && (position.X <= X + W && position.Y <= Y + H);
    }
}