using System;

namespace Coorth; 

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, Inherited = false)]
public class StoreContractAttribute : Attribute {
    
}