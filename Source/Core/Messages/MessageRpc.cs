using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Coorth {

    public interface IMessageRpcInvoke : IRequest {
        ushort Method { get; }
    }
    
    public interface IMessageRpcReturn : IResponse {
    }
    
    [System.Runtime.Serialization.DataContract]
    public class MessageRpcInvoke : IMessageRpcInvoke {
        [System.Runtime.Serialization.DataMember(Order = 1)] public ushort Method { get; set; }
        public void Invoke(MethodInfo method, object obj) {
            throw new NotImplementedException();
        }

        public MessageRpcInvoke(ushort method) {
            this.Method = method;
        }
    }
    
    [System.Runtime.Serialization.DataContract]
    public class MessageRpcInvoke<TP> : IMessageRpcInvoke {
        [System.Runtime.Serialization.DataMember(Order = 1)] public ushort Method { get; set; }
        [System.Runtime.Serialization.DataMember(Order = 2)] public TP Param { get; set; }

        public MessageRpcInvoke(ushort method, TP param) {
            this.Method = method;
            this.Param = param;
        }
    }
    
    [System.Runtime.Serialization.DataContract] 
    public class MessageRpcInvoke<TP1, TP2> : IMessageRpcInvoke {
        [System.Runtime.Serialization.DataMember(Order = 1)] public ushort Method { get; set; }
        [System.Runtime.Serialization.DataMember(Order = 2)] public TP1 Param1 { get; set; }
        [System.Runtime.Serialization.DataMember(Order = 3)] public TP2 Param2 { get; set; }
        
        public MessageRpcInvoke(ushort method, TP1 param1, TP2 param2) {
            this.Method = method;
            this.Param1 = param1;
            this.Param2 = param2;
        }
    }
    
    [System.Runtime.Serialization.DataContract]
    public class MessageRpcInvoke<TP1, TP2, TP3> : IMessageRpcInvoke {
        [System.Runtime.Serialization.DataMember(Order = 1)] public ushort Method { get; set; }
        [System.Runtime.Serialization.DataMember(Order = 2)] public TP1 Param1 { get; set; }
        [System.Runtime.Serialization.DataMember(Order = 3)] public TP2 Param2 { get; set; }
        [System.Runtime.Serialization.DataMember(Order = 4)] public TP3 Param3 { get; set; }

        public MessageRpcInvoke(ushort method, TP1 param1, TP2 param2, TP3 param3) {
            this.Method = method;
            this.Param1 = param1;
            this.Param2 = param2;
            this.Param3 = param3;
        }
    }
    
    [System.Runtime.Serialization.DataContract]
    public class MessageRpcReturn : IMessageRpcReturn {
        public static readonly MessageRpcReturn Default = new MessageRpcReturn();
    }

    [System.Runtime.Serialization.DataContract]
    public class MessageRpcReturn<T> : IMessageRpcReturn {
        [System.Runtime.Serialization.DataMember(Order = 1)] public T Value { get; set; }
        
        public MessageRpcReturn(T value) {
            this.Value = value;
        }
    }
    
}