namespace Coorth {
    public abstract class ActorDomain {

        public ActorContainer Container { get; private set; }

        public ServiceContainer Services { get; private set; }

        public void Setup(ActorContainer container, ServiceContainer services) {
            this.Container = container;
            this.Services = services;
        }
        
        public abstract ActorRef GetRef();
    }

    public class LocalDomain : ActorDomain {
        public override ActorRef GetRef() {

            return default;
        } 
    }

    public class WorldDomain : LocalDomain {
        
    }

    public class RemoteDomain : ActorDomain {
        public override ActorRef GetRef() {
            
            return default;
        }
    }

    public class ActorProxy {
        public ActorRef Ref;
    }

    // public interface IServerLogin {
    //     
    // }
    //
    // public class MessageRpcInvoke : ActorMessage, IRequest {
    //     
    //     
    //     public int MsgId { get; }
    //
    //     public bool ValidateMessage() {
    //         throw new System.NotImplementedException();
    //     }
    //
    //     public void Setup(int msgId) {
    //         throw new System.NotImplementedException();
    //     }
    //     
    //     public void Encode<T>(ISerializer serializer, ref T value) {
    //         
    //     }
    // }
    //
    // public class MessageRpcReturn : IResponse {
    //     
    // }
    //
    // public class ServerLogin : ActorProxy, IServerLogin {
    //     
    //     public Task<long> Login(string account, string password) {
    //         var request = new MessageRpcInvoke();
    //         var response = await Ref.Request<MessageRpcReturn>(request);
    //         request.Encode<string>(account).Encode<string>(password);
    //         return request.Decode<long>();
    //     }
    //
    //     public void Logout(long a) {
    //         Ref.Send();
    //     }
    // }
}