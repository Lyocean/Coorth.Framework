using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth {
    public readonly struct ActorRef : IEquatable<ActorRef> {
        
        internal readonly ActorContext Context;

        public static ActorRef Null => default;

        public bool IsNull => Context == null;

        public ActorRuntime Runtime => Context.Runtime;

        public ActorId Id => Context.Id;

        public TActor GetActor<TActor>() => Context.GetActor<TActor>();

        internal ActorRef(ActorContext context) { this.Context = context; }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Send<T>(T message) => Context.Send(message, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Send<T>(T message, ActorRef sender) => Context.Send(message, sender);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResp> Request<TReq, TResp>(TReq message) => this.Request<TReq, TResp>(message, ActorRef.Null);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResp> Request<TReq, TResp>(TReq message, ActorRef sender) => Context.Request<TReq, TResp>(message, sender);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResp> Request<TReq, TResp>(TReq message, CancellationToken cancellation) => Context.Request<TReq, TResp>(message, ActorRef.Null, cancellation);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResp> Request<TReq, TResp>(TReq message, ActorRef sender, CancellationToken cancellation) => Context.Request<TReq, TResp>(message, sender, cancellation);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResp> Request<TReq, TResp>(TReq message, TimeSpan span) => Context.Request<TReq, TResp>(message, ActorRef.Null, span);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResp> Request<TReq, TResp>(TReq message, ActorRef sender, TimeSpan span) => Context.Request<TReq, TResp>(message, sender, span);

        public bool Equals(ActorRef other) {
            return Equals(Context, other.Context);
        }

        public override bool Equals(object obj) {
            return obj is ActorRef other && Equals(other);
        }

        public override int GetHashCode() {
            return (Context != null ? Context.GetHashCode() : 0);
        }

        public static bool operator ==(ActorRef a, ActorRef b) {
            return a.Equals(b);
        }
        
        public static bool operator !=(ActorRef a, ActorRef b) {
            return !a.Equals(b);
        }
    }
}