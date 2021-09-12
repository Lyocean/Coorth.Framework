using System;

namespace Coorth {
    
    public interface IMessage {
        // bool ValidateMessage();
    }
    
    
    public interface IRequest : IMessage {

    }

    public interface IResponse : IMessage {

    }

    public class ErrorMessage : IResponse {
        public int MsgId { get; private set; }
        public string Error { get; set; }


        
        public bool ValidateMessage() {
            return MsgId != 0 && !string.IsNullOrEmpty(Error);
        }
    }
}