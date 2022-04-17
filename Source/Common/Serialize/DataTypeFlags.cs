using System;

namespace Coorth.Serializes {
    [Flags]
    public enum DataTypeFlags : byte {
        Auto = 0,
        Name = 1,
        Guid = 2,
        Null = 3,
    }
}