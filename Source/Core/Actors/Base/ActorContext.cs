using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Coorth {
    public abstract class ActorContext : Disposable {

        #region Fields

        public readonly ActorId Id;
        
        public readonly ActorRef Ref;
        
        public readonly ActorPath Path;

        protected abstract ActorDomain Domain { get; }
        
        public ActorRuntime Runtime => Domain.Runtime;

        protected ActorScheduler Scheduler => Domain.Runtime.Scheduler;

        public ActorContext Parent { get; private set; }

        public abstract TActor GetActor<TActor>();

        #endregion

        #region Lifecycle

        protected ActorContext(ActorId id, ActorPath path) {
            this.Id = id;
            this.Path = path;
            this.Ref = new ActorRef(this);
        }

        internal void _ActorAdd(ActorContext parent) {
            this.Parent = parent;
            OnAdd();
        }
        
        internal void _ActorRemove() {
            OnRemove();
            this.Parent = null;
        }

        protected virtual void OnAdd() { }

        protected virtual void OnRemove() { }

        #endregion

        #region Message

        public abstract void Send<T>(T message, ActorRef sender);

        public abstract Task<TResp> Request<TReq, TResp>(TReq message, ActorRef sender, CancellationToken cancellation = default);

        public Task<TResp> Request<TReq, TResp>(TReq message, ActorRef sender, TimeSpan timeSpan) {
            var cancellation = new CancellationTokenSource();
            cancellation.CancelAfter(timeSpan);
            return Request<TReq, TResp>(message, sender, cancellation.Token);
        }
        
        // public abstract void Forward<T>(T message, ActorRef target, ActorRef sender);

        public abstract Task Receive(in ActorMail e);

        #endregion
    }

    public abstract class ActorContext<TContext> : ActorContext where TContext : ActorContext {
        
        private volatile ConcurrentDictionary<ActorId, TContext> children;

        protected ActorContext(ActorId id, ActorPath path) : base(id, path) { }

        public bool AddChild(TContext context) {
            if (children == null) {
                Interlocked.Exchange(ref children, new ConcurrentDictionary<ActorId, TContext>());
            }
            var result = children.TryAdd(context.Id, context);
            if (result) {
                context._ActorAdd(this);
            }
            return result;
        }
        
        public TContext GetChild(ActorId id) {
            if (children == null) {
                return null;
            }
            return children.TryGetValue(id, out var context) ? context : null;
        }
        
        public bool RemoveChild(ActorId id) {
            if (children == null) {
                return false;
            }
            var result = children.TryRemove(id, out var context);
            if (result) {
                context._ActorRemove();
            }
            return result;
        }
    }
}
