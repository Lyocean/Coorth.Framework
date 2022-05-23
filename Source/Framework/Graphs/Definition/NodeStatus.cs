﻿using System;

namespace Coorth.Framework; 

[Flags]
public enum NodeStatus {
        
    Success = 1 << 0,
        
    Failure = 1 << 1,
        
    Running = 1 << 2
}