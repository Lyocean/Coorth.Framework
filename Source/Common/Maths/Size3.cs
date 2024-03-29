﻿using System;
using System.Runtime.InteropServices;

namespace Coorth.Maths; 

[DataDefine(DataFlags.PubField), Guid("DE608657-DB40-4EAB-AA16-AA31AE1CFA97")]
[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
public partial record struct Size3(int X, int Y, int Z) {
        
    public int X = X;
        
    public int Y = Y;
        
    public int Z = Z;

    public readonly int W => X;
    
    public readonly int H => Y;
    
    public readonly int D => Z;
}