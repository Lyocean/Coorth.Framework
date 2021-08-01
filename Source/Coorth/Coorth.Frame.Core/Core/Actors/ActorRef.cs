using System.Threading.Tasks;

namespace Coorth {
    public sealed class ActorRef {
        
        public readonly ActorContext Context;

        public IActor Actor => Context.Actor;

        public ActorContainer Container => Context.Container;

        public ActorId Id => Context.Id;

        internal ActorRef(ActorContext context) {
            this.Context = context;
        }
        
        public void Send<T>(T message) => this.Send(message, null);

        public async Task<TResp> Request<TReq, TResp>(TReq message) => await this.Request<TReq, TResp>(message, null);

        public void Send<T>(T message, ActorRef sender) => Context.Send(message, sender);

        public Task<TResp> Request<TReq, TResp>(TReq message, ActorRef sender) => Context.Request<TReq, TResp>(message, sender);

                
    }
}