using System;

namespace Coorth {
    
    public interface IMessage {
    }
    
    public interface IRequest : IMessage {
    }

    public interface IResponse : IMessage {
    }
    
    [Message, StoreContract]
    public sealed partial class ErrorMessage : IResponse {
        
        [StoreMember(1)]
        public int ErrorCode { get; private set; }
        
        [StoreMember(2)]
        public string ErrorInfo { get; private set; }

        public ErrorMessage(int errorCode, string errorInfo) {
            this.ErrorCode = errorCode;
            this.ErrorInfo = errorInfo;
        }
    }
}