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
    
    [DataContract]
    public class MessageRpcInvoke : IMessageRpcInvoke {
        [DataMember(Order = 0)] public ushort Method { get; set; }
        public void Invoke(MethodInfo method, object obj) {
            throw new NotImplementedException();
        }

        public MessageRpcInvoke(ushort method) {
            this.Method = method;
        }
    }
    
    [DataContract]
    public class MessageRpcInvoke<TP> : IMessageRpcInvoke {
        [DataMember(Order = 0)] public ushort Method { get; set; }
        [DataMember(Order = 1)] public TP Param { get; set; }

        public MessageRpcInvoke(ushort method, TP param) {
            this.Method = method;
            this.Param = param;
        }
    }
    
    [DataContract] 
    public class MessageRpcInvoke<TP1, TP2> : IMessageRpcInvoke {
        [DataMember(Order = 0)] public ushort Method { get; set; }
        [DataMember(Order = 1)] public TP1 Param1 { get; set; }
        [DataMember(Order = 2)] public TP2 Param2 { get; set; }
        
        public MessageRpcInvoke(ushort method, TP1 param1, TP2 param2) {
            this.Method = method;
            this.Param1 = param1;
            this.Param2 = param2;
        }
    }
    
    [DataContract]
    public class MessageRpcInvoke<TP1, TP2, TP3> : IMessageRpcInvoke {
        [DataMember(Order = 0)] public ushort Method { get; set; }
        [DataMember(Order = 1)] public TP1 Param1 { get; set; }
        [DataMember(Order = 2)] public TP2 Param2 { get; set; }
        [DataMember(Order = 3)] public TP3 Param3 { get; set; }

        public MessageRpcInvoke(ushort method, TP1 param1, TP2 param2, TP3 param3) {
            this.Method = method;
            this.Param1 = param1;
            this.Param2 = param2;
            this.Param3 = param3;
        }
    }
    
    [DataContract]
    public class MessageRpcReturn : IMessageRpcReturn {
        public static readonly MessageRpcReturn Default = new MessageRpcReturn();
    }

    [DataContract]
    public class MessageRpcReturn<T> : IMessageRpcReturn {
        [DataMember(Order = 0)] public T Value { get; set; }
        
        public MessageRpcReturn(T value) {
            this.Value = value;
        }
    }
    
}