using System;

namespace Coorth {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Enum)]
    public class DataSerializerAttribute : Attribute {
        public Type Type { get; private set; }

        public DataSerializerAttribute(Type type) {
            this.Type = type;
        }
    }
}

