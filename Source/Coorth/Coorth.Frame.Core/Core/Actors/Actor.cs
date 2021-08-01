using System;
using System.Threading.Tasks;

namespace Coorth {
    public interface IActor {
        ActorContext Context { get; set; }
        
        void Execute(in ActorMail e);
        Task ExecuteAsync(in ActorMail e);

        void OnActive();
        void DeActive();
        
        Task OnActiveAsync();
        Task OnDeActiveAsync();

    }

    public abstract class Actor : Disposable, IActor {
        public ActorContext Context { get; set; }
        
        protected ActorRef Self => Context.Ref;
        
        public ActorId Id => Context.Id;
        
        protected ActorContainer Container => Context.Container;

        internal void Initial(ActorContext context) {
            this.Context = context;
        }

        public virtual void OnActive() { }

        public virtual Task OnActiveAsync() => Task.CompletedTask;
        
        public virtual void Execute(in ActorMail e) { }
        
        public virtual Task ExecuteAsync(in ActorMail e) {
            return Task.CompletedTask;
        }
        
        public virtual void DeActive() { }

        public virtual Task OnDeActiveAsync() => Task.CompletedTask;

        protected void Subscribe<TEvent>(Action<TEvent> action) where TEvent : IEvent {
            
        }
    }
    
    public readonly struct ActorId : IEquatable<ActorId> {
        private readonly Guid id;

        public bool IsNull => id == Guid.Empty;
        
        private ActorId(Guid key) { id = key; }

        public static ActorId New() { return new ActorId(Guid.NewGuid()); }
        
        public bool Equals(ActorId other) { return this.id == other.id; }

        public static bool operator ==(ActorId a, ActorId b) { return a.Equals(b); }

        public static bool operator !=(ActorId a, ActorId b) { return !a.Equals(b); }

        public override bool Equals(object obj) { return obj is ActorId actorId && actorId.Equals(this); }

        public override int GetHashCode() { return id.GetHashCode(); }

        public override string ToString() { return $"[ActorId]: {id.ToString()}"; }

        public string ToShortString() { return id.ToString(); }
    }
    
    public class ActionActor : Actor {
        private readonly Func<ActorMail, Task> action;
        public ActionActor(Func<ActorMail, Task> func) {
            this.action = func;
        }

        public override Task ExecuteAsync(in ActorMail e) {
            return action.Invoke(e);
        }
    }
}
