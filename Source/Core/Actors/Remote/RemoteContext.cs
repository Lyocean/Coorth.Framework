using System.Threading;
using System.Threading.Tasks;

namespace Coorth {
    public class RemoteContext : ActorContext<RemoteContext> {
        private readonly RemoteDomain remoteDomain;
        protected override ActorDomain Domain => remoteDomain;

        public IActorProxy Proxy;
        
        public RemoteContext(ActorId id, RemoteDomain domain, ActorPath path) : base(id, path) {
            this.remoteDomain = domain;
        }

        public override TActor GetActor<TActor>() {
            return (TActor)Proxy;
        }

        public override void Send<T>(T message, ActorRef sender) {
            remoteDomain.Send(Ref, sender, message);
        }

        public override Task<TResp> Request<TReq, TResp>(TReq message, ActorRef sender, CancellationToken cancellation = default) {
            return remoteDomain.Request<TReq, TResp>(Ref, sender, message, cancellation);
        }
        
        // public override void Forward(ActorRef sender, object message, MessageFuture future) {
        //     throw new System.NotImplementedException();
        // }
        
        public override Task Receive(in ActorMail e) {
            
            return Task.CompletedTask;
        }
    }
}