using System;

namespace Coorth; 

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, Inherited = false)]
public class DataDefineAttribute : Attribute {
    public readonly DataFlags Flags;

    public DataDefineAttribute(DataFlags flags = DataFlags.DeclareOnly) {
        Flags = flags;
    }
}

[Flags]
public enum DataFlags {
    DeclareOnly  = 1,
    PublicField = 1 << 1,
    PublicProperty = 1 << 2,
}