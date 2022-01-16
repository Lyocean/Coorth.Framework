using System;

namespace Coorth {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Enum)]
    public class StoreContractAttribute : Attribute {
        
        public Type Base;

        public int Index;

        public readonly Guid Guid;
        
        public Type Serializer;
        
        public StoreContractAttribute() {
        }
        
        public StoreContractAttribute(string guid) {
            this.Guid = !string.IsNullOrEmpty(guid) ? Guid.Parse(guid) : Guid.Empty;
        }
    }
}