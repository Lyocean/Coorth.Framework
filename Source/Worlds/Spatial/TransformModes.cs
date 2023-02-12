using System;

namespace Coorth.Framework; 

[Flags]
public enum TransformModes : byte {
    Nothing = 0, 
    UseTRS = 1, 
    Immediate = 1 << 1, 
    ChangeEvent = 1 << 2, 
    All = UseTRS | Immediate | ChangeEvent
}