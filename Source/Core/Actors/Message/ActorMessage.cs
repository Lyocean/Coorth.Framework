using System;
using System.Runtime.Serialization;

namespace Coorth {
    [Serializable, DataContract]
    public class ActorMessage : IMessage {

        [DataMember(Order = 0)]
        public readonly ActorId Sender;
        
        [DataMember(Order = 1)]
        public readonly ActorId Target;
        
        [DataMember(Order = 2)]
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
    
    [Serializable, DataContract]
    public class ActorRequest : IRequest {
        
        [DataMember(Order = 0)]
        public readonly ActorId Sender;
        
        [DataMember(Order = 1)]
        public readonly ActorId Target;
        
        [DataMember(Order = 2)]
        public readonly IRequest Request;
        
        public ActorRequest(ActorId target, ActorId sender, IRequest request) {
            this.Request = request;
        }
    }
    
    [Serializable, DataContract]
    public class ActorResponse : IResponse {
        
        [DataMember(Order = 0)]
        public readonly ActorId Sender;
        
        [DataMember(Order = 1)]
        public readonly ActorId Target;
        
        [DataMember(Order = 2)]
        public readonly IResponse Response;
        
        public ActorResponse(ActorId target, ActorId sender, IResponse response) {
            this.Response = response;
        }
    }
}