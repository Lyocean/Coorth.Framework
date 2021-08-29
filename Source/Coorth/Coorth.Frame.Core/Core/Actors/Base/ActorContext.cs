using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth {
    internal class ActorContext : Disposable {

        #region Actor
        
        public readonly ActorId Id;

        public readonly ActorDomain Domain;
        
        public ActorContainer Container => Domain.Container;

        public readonly ActorPath Path;

        public readonly IMailbox Mailbox;

        private ActiveState state;
        
        public IActor Actor { get; private set; }

        public Type ActorType => Actor?.GetType();
        
        public ActorContext Parent { get; private set; }
        private IActorCollection children { get; }
        
        public ActorRef Ref { get; internal set; }
        
        internal ActorContext(ActorId id, ActorDomain domain, IMailbox mailbox, ActorPath path) {
            this.Id = id;
            this.Path = path;
            this.Domain = domain;
            this.Mailbox = mailbox;
        }

        public void Send<T>(T message, ActorRef sender) {
            Mailbox.Post<T>(Ref, sender, message);
            if (state.IsActive) {
                Mailbox.Execute(Container.Scheduler);
            }
        }

        public async Task<TResp> Request<TReq, TResp>(TReq message, ActorRef sender, CancellationTokenSource cancellation = null) {
            var future = new ActorFuture<TResp>(cancellation);
            Mailbox.Request<TReq>(Ref, sender, message, future);
            if (state.IsActive) {
                Mailbox.Execute(Container.Scheduler);
            }
            var response = await future.Task;
            return response;
        }

        internal void Forward(ActorRef sender, object message, ActorFuture future) {
            Mailbox.Request(Ref, sender, message, future);
            if (state.IsActive) {
                Mailbox.Execute(Container.Scheduler);
            }
        }
        
        internal Task Receive(ActorMail e) {
            Actor?.Execute(e);
            if (Actor != null) {
                return Actor?.ExecuteAsync(e);
            }
            return Task.CompletedTask;
        }
        
        #endregion


        #region Create

        private IActorCollection GetChildren() {
            if (children != null) {
                return children;
            }
            if (Actor != null && Actor is IActorCollection collection) {
                return collection;
            }
            collection = new ActorCollection();
            return collection;
        }

        public void SetActive(bool active) {
            if (!state.IsActive && active) {
                ActiveContext();
            }
            else if(state.IsActive && !active) {
                DeActiveContext(false);
            }
        }

        private async void ActiveContext() {
            if (Actor == null) {
                return;
            }
            Actor.OnActive();
            await Actor.OnActiveAsync();
            Container.OnActorOnActive(this, Actor);
            state.Enter();
            Mailbox.Execute(Container.Scheduler);
        }
        
        private async void DeActiveContext(bool destroy) {
            Mailbox.Execute(Container.Scheduler);
            if (Actor == null) {
                return;
            }
            Actor.DeActive();
            await Actor.OnDeActiveAsync();
            Container.OnActorDeActive(this, Actor);
            state.Exit();
            if (destroy) {
                (Actor as IDisposable)?.Dispose();
                Container.OnActorDestroy(this, Actor);
            }
        }
        
        public ActorContext CreateChild(Actor actor, string name = null) {
            var mailbox = new ActorMailbox(Container.Config);
            var context = Container.CreateContext(Domain, this, actor, name, mailbox);
            context.Actor = actor;
            context.Parent = this;
            
            var collection = GetChildren();
            collection.AddChild(this, context, actor);
            Container.OnActorCreate(context, actor);
            context.ActiveContext();
            return context;
        }
        
        public bool DestroyChild(ActorContext context) {
            var collection = GetChildren();
            if (collection.RemoveChild(this, context)) {
                var actor = context.Actor;
                if (actor != null) {
                    context.DeActiveContext(true);
                }
                return true;
            }
            else {
                return false;
            }
        }
        
        public T CreateActor<T>(string name = null) where T : Actor {
            var actor = Domain.Services.Create<T>() ?? Activator.CreateInstance<T>();
            var context = CreateChild(actor, name);
            return (T)context.Actor;
        }

        public ActorRef CreateRef<T>(string name = null) where T : Actor {
            var actor = Domain.Services.Create<T>() ?? Activator.CreateInstance<T>();
            var context = CreateChild(actor, name);
            return context.Ref;
        }

        public ActorRef CreateRef(ActorProps props, string name = null)  {
            var actor = props.CreateActor();
            var context = CreateChild(actor, name);
            return context.Ref;
        }
        
        #endregion
        
    }

    internal interface IActorCollection {
        void AddChild<T>(ActorContext parent, ActorContext context, T actor) where T : Actor; 
        
        bool RemoveChild(ActorContext parent, ActorContext child); 
    }

    internal class ActorCollection : IActorCollection {

        private readonly ConcurrentDictionary<ActorId, ActorRef> children = new ConcurrentDictionary<ActorId, ActorRef>();

        public void AddChild<T>(ActorContext parent, ActorContext context, T actor) where T : Actor {
            children.TryAdd(context.Id, context.Ref);
        }

        public bool RemoveChild(ActorContext parent, ActorContext child) {
            return children.TryRemove(child.Id, out _);
        }
    }

    public struct ActiveState {
        private volatile int status;

        public bool IsActive => status == 1;

        public ActiveState(bool active = false) {
            status = active ? 1 : 0;
        }
        
        public bool Enter() {
            return Interlocked.CompareExchange(ref status, 1, 0) == 0;
        }

        public bool Exit() {
            return Interlocked.CompareExchange(ref status, 0, 1) == 1;
        }
    }
}
