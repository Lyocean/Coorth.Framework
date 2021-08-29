namespace Coorth {

    public interface IActorMessage : IMessage {
        ActorId ActorId { get; }
        void Setup(ActorId actorId);
    }
    
    public abstract class ActorMessage : IActorMessage {
        public ActorId ActorId { get; private set; }

        public void Setup(ActorId actorId) {
            this.ActorId = actorId;
        }
        
        public virtual bool ValidateMessage() {
            return !ActorId.IsNull;
        }
    }
    
    public class MessageRpcRequest : ActorMessage {
        public byte[] Arguments { get; private set; }

        public MessageRpcRequest(ActorId actorId, byte[] arguments) {
            this.Setup(actorId);
            this.Arguments = arguments;
        }
    }

    public class MessageRpcResponse : ActorMessage {
        public byte[] Arguments { get; private set; }

        public MessageRpcResponse(ActorId actorId, byte[] arguments) {
            this.Setup(actorId);
            this.Arguments = arguments;
        }
    }
}