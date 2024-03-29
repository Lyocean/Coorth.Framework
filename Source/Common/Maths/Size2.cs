﻿using System;
using System.Runtime.InteropServices;

namespace Coorth.Maths; 

[DataDefine(DataFlags.PubField), Guid("61A2C677-E467-4D4B-A1AA-C84C201432C4")]
[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
public partial record struct Size2(int W, int H) {
        
    public int W = W;
        
    public int H = H;
    
}