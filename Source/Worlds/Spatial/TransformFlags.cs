using System;

namespace Coorth.Worlds; 

[Flags]
public enum TransformFlags : byte {
    Nothing = 0, 
    Position = 1, 
    Rotation = 1 << 1, 
    Scaling = 1 << 2, 
    All = Position | Rotation | Scaling
}