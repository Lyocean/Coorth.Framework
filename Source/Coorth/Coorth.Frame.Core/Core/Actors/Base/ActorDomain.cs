using System.Threading.Tasks;

namespace Coorth {
    public abstract class ActorDomain : Disposable, IAwake {

        public ActorContainer Container { get; private set; }

        public ServiceContainer Services => Container.Services;
        
        internal void Setup(ActorContainer container) {
            this.Container = container;
        }

        void IAwake.OnAwake() {
            
        }
        
        protected override void Dispose(bool dispose) {
            Container._RemoveDomain(this);
        }

        private ActorContext CreateContext() {
            //Container.CreateContext(this, )
            return default;
        }
        
        
        
        
        
        
        
        
        
        
        public abstract ActorRef GetRef();
        
        
        public T CreateProxy<T>() {
            return default;
        }


        internal virtual Task InvokeAsync<TReq>() {
        
            return Task.CompletedTask;
        }

        internal virtual Task<TResp> InvokeAsync<TReq, TResp>() {

            return default;
        }


    }



    // public class RemoteDomain_


    
    
    
    
    
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