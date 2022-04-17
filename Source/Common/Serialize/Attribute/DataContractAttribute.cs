using System;

namespace Coorth {
    // [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Enum)]
    // public class DataContractAttribute : Attribute {
    //     
    //     public Type? Base { get; set; }
    //
    //     public int Index { get; set; } = 0;
    //
    //     public Guid Guid { get; set; }
    //     
    //     public Type? Serializer { get; set; }
    //
    //     public DataContractAttribute() {
    //         this.Guid = Guid.Empty;
    //     }
    //     
    //     public DataContractAttribute(string guid) {
    //         this.Guid = !string.IsNullOrEmpty(guid) ? Guid.Parse(guid) : Guid.Empty;
    //     }
    // }
}