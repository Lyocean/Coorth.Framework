using System;

namespace Coorth; 

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, Inherited = false)]
public class DataDefineAttribute : Attribute {
    public readonly DataFlags Flags;

    public DataDefineAttribute(DataFlags flags = DataFlags.Declare) {
        Flags = flags;
    }
}

[Flags]
public enum DataFlags {
    Custom   = 0,
    Declare  = 1,
    PubField = 1 << 1,
    PubProp  = 1 << 2,
}
