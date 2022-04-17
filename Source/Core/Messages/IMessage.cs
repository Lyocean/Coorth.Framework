using System;
using System.Runtime.Serialization;

namespace Coorth {
    
    public interface IMessage {
    }
    
    public interface IRequest : IMessage {
    }

    public interface IResponse : IMessage {
    }
    
    [Message, DataContract]
    public sealed partial class ErrorMessage : IResponse {
        
        [DataMember(Order = 1)]
        public int ErrorCode { get; private set; }
        
        [DataMember(Order = 2)]
        public string ErrorInfo { get; private set; }

        public ErrorMessage(int errorCode, string errorInfo) {
            this.ErrorCode = errorCode;
            this.ErrorInfo = errorInfo;
        }
    }
}