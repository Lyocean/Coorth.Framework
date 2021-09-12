using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth {

    public enum ActorStatus : int {
        DeActive = 0,
        IdleState = 1,
        BusyState = 2,
    }
    
    public class LocalContext : ActorContext<LocalContext> {
        
        #region Fields

        private readonly LocalDomain localDomain;
        protected override ActorDomain Domain => localDomain;

        public IActor Actor { get; private set; }
        
        public Type ActorType => Actor?.GetType();

        private volatile int status;

        public bool IsActive => status > 0;

        public readonly ActorMailbox Mailbox;

        private IActorConfig Config => Container.Config;

        public override TActor GetActor<TActor>() => (TActor) Actor;
        
        #endregion
        
        #region Lifecycle
        
        internal LocalContext(ActorId id, LocalDomain domain, IActor actor, ActorMailbox mailbox, ActorPath path) : base(id, path) {
            this.Actor = actor;
            this.Mailbox = mailbox;
            this.localDomain = domain;
        }

        protected override void Dispose(bool dispose) {
            this.localDomain.RemoveActor(Id);
        }

        protected override void OnAdd() {
            
        }

        protected override void OnRemove() {
            
        }

        #endregion

        #region Message

        public override void Send<T>(T message, ActorRef sender) {
            var mail = ActorMail.Create<T>(message, Ref, sender, null);
            Schedule(mail);
        }

        public override async Task<TResp> Request<TReq, TResp>(TReq message, ActorRef sender, CancellationToken cancellation = default) {
            var future = new MessageFuture<TResp>(cancellation);
            var mail = ActorMail.Create<TReq>(message, Ref, sender, future);
            Schedule(mail);
            var response = await future.Task;
            return response;
        }

        // public override void Forward(ActorRef sender, object message, MessageFuture future) {
        //     
        // }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Schedule(in ActorMail mail) {
            if (Interlocked.CompareExchange(ref status, (int)ActorStatus.BusyState, (int)ActorStatus.IdleState) == (int)ActorStatus.IdleState) {
                Scheduler.Schedule(Actor, mail);
                if (Mailbox.Count == 0) {
                    Interlocked.CompareExchange(ref status, (int)ActorStatus.IdleState, (int)ActorStatus.BusyState);
                }
                else {
                    Scheduler.Schedule(this);
                }
            }
            else {
                Mailbox.Enqueue(mail);
            }
        }
        
        public override Task Receive(in ActorMail e) {
            return Actor.ReceiveAsync(e);
        }
        
        public async Task Execute() {
            for (var i = 0; i < Config.ActorThroughput; i++) {
                if (Mailbox.TryDequeue(out var mail)) {
                    await Receive(mail);
                } else {
                    break;
                }
            }
            if (Mailbox.Count == 0) {
                Interlocked.CompareExchange(ref status, (int)ActorStatus.IdleState, (int)ActorStatus.BusyState);
            }
        }
        
        #endregion
    }
}