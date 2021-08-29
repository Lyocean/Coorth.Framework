using System.Threading.Tasks;

namespace Coorth {
    public readonly struct ActorRef {
        
        internal readonly ActorContext Context;

        public static ActorRef Null => default;

        public bool IsNull => Context == null;
        
        public IActor Actor => Context.Actor;

        public ActorContainer Container => Context.Container;

        public ActorId Id => Context.Id;

        internal ActorRef(ActorContext context) {
            this.Context = context;
        }
        
        public void Send<T>(T message) => Context.Send(message, this);

        public async Task<TResp> Request<TReq, TResp>(TReq message) => await this.Request<TReq, TResp>(message, default);

        public void Send<T>(T message, ActorRef sender) => Context.Send(message, sender);

        public Task<TResp> Request<TReq, TResp>(TReq message, ActorRef sender) => Context.Request<TReq, TResp>(message, sender);

    }
}