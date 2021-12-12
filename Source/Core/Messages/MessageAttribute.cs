using System;

namespace Coorth {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class MessageAttribute : Attribute {
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class RpcAttribute : Attribute {
        
        public readonly ushort Id;

        public RpcAttribute(ushort id) {
            this.Id = id;
        }
    }
}