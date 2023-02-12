using System;

namespace Coorth; 

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, Inherited = false)]
public class StoreContractAttribute : Attribute {
    public readonly StoreFlags Flags;

    public StoreContractAttribute(StoreFlags flags = StoreFlags.DeclareOnly) {
        Flags = flags;
    }
}

[Flags]
public enum StoreFlags {
    DeclareOnly  = 1 << 1,
    PublicField = 1 << 2,
    PublicProperty = 1 << 3,
    
}