using System.Threading;
using System.Threading.Tasks;

namespace Coorth {
    public class RootContext<TContext> : ActorContext<TContext> where TContext : ActorContext {
        protected override ActorDomain Domain { get; }

        internal RootContext(ActorDomain domain) : base(ActorId.New(), domain.Path) {
            this.Domain = domain;
        }

        public override TActor GetActor<TActor>() {
            return (TActor)(object)Domain;
        }

        public override void Send<T>(T message, ActorRef sender) {
            var mail = ActorMail.Create(message, Ref, sender, null);
            Receive(mail);
        }

        public override async Task<TResp> Request<TReq, TResp>(TReq message, ActorRef sender, CancellationToken cancellation = default) {
            var future = new MessageFuture<TResp>(cancellation);
            var mail = ActorMail.Create(message, Ref, sender, future);
            await Receive(mail);
            return await future.Task;
        }
        
        public override Task Receive(in ActorMail mail) {
            return Domain.ReceiveAsync(mail);
        }

        // public override void Forward(ActorRef sender, object message, MessageFuture future) {
        //     var mail = new ActorMail();
        //     Receive(mail);
        // }
    }
}