using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Coorth.Maths; 

[DataContract, Guid("DE608657-DB40-4EAB-AA16-AA31AE1CFA97")]
[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
public record struct Size3(int X, int Y, int Z) {
        
    public int X = X;
        
    public int Y = Y;
        
    public int Z = Z;

    public readonly int W => X;
    
    public readonly int H => Y;
    
    public readonly int D => Z;
}