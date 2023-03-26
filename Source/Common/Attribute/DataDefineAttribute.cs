using System;

namespace Coorth; 

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, Inherited = false)]
public class DataDefineAttribute : Attribute {
    public readonly StoreFlags Flags;

    public DataDefineAttribute(StoreFlags flags = StoreFlags.DeclareOnly) {
        Flags = flags;
    }
}

[Flags]
public enum StoreFlags {
    DeclareOnly  = 1,
    PublicField = 1 << 1,
    PublicProperty = 1 << 2,
}