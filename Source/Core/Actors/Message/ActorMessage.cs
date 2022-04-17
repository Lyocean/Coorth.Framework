using System;
using System.Runtime.Serialization;

namespace Coorth {
    [Serializable, System.Runtime.Serialization.DataContract]
    public class ActorMessage : IMessage {

        [System.Runtime.Serialization.DataMember(Order = 0)]
        public readonly ActorId Sender;
        
        [System.Runtime.Serialization.DataMember(Order = 1)]
        public readonly ActorId Target;
        
        [System.Runtime.Serialization.DataMember(Order = 2)]
        public readonly IMessage Message;

        public ActorMessage(ActorId target, ActorId sender, IMessage message) {
            Sender = sender;
            Target = target;
            Message = message;
        }

        public bool ValidateMessage() {
            return Message != null;
        }
    }
    
    [Serializable, System.Runtime.Serialization.DataContract]
    public class ActorRequest : IRequest {
        
        [System.Runtime.Serialization.DataMember(Order = 0)]
        public readonly ActorId Sender;
        
        [System.Runtime.Serialization.DataMember(Order = 1)]
        public readonly ActorId Target;
        
        [System.Runtime.Serialization.DataMember(Order = 2)]
        public readonly IRequest Request;
        
        public ActorRequest(ActorId target, ActorId sender, IRequest request) {
            this.Request = request;
        }
    }
    
    [Serializable, System.Runtime.Serialization.DataContract]
    public class ActorResponse : IResponse {
        
        [System.Runtime.Serialization.DataMember(Order = 0)]
        public readonly ActorId Sender;
        
        [System.Runtime.Serialization.DataMember(Order = 1)]
        public readonly ActorId Target;
        
        [System.Runtime.Serialization.DataMember(Order = 2)]
        public readonly IResponse Response;
        
        public ActorResponse(ActorId target, ActorId sender, IResponse response) {
            this.Response = response;
        }
    }
}