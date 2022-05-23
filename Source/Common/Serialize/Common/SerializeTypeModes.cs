using System;

namespace Coorth.Serialize; 

[Flags]
public enum SerializeTypeModes : byte {
    Name = 1,
    Guid = 2,
}