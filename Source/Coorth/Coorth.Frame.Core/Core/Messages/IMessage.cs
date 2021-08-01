using System;

namespace Coorth {
    
    public interface IMessage {

        bool ValidateMessage();
    }
    
    
    public interface IRequest : IMessage {
        int MsgId { get; }
        void Setup(int msgId);
    }

    public interface IResponse : IMessage {
        int MsgId { get; }
        void Setup(int msgId);
    }

    public class ErrorMessage : IResponse {
        public int MsgId { get; private set; }
        public string Error { get; set; }

        void IResponse.Setup(int msgId) {
            this.MsgId = msgId;
        }
        
        public bool ValidateMessage() {
            return MsgId != 0 && !string.IsNullOrEmpty(Error);
        }
    }
}