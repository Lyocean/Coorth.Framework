using System;

namespace Coorth.Serialize; 

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Enum)]
public class SerializerAttribute : Attribute {
    public Type Type { get; }
    
    public SerializerAttribute(Type type) {
        Type = type;
    }
}