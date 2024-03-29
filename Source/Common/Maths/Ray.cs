﻿using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Coorth.Maths; 

[DataDefine(DataFlags.PubField), Guid("9DB02E7C-732B-42F3-A762-FF847927A2BA")]
[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
public partial record struct Ray(Vector3 Position, Vector3 Direction) {
        
    public Vector3 Position = Position;
        
    public Vector3 Direction = Direction;
}