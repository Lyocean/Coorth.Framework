using System;

namespace Coorth {
    public class MessageAttribute : Attribute {
        
    }
    
    public class RpcAttribute : Attribute {
        public ushort Id;

        public RpcAttribute(ushort id) {
            this.Id = id;
        }
    }
}