using System;
using System.Threading.Tasks;

namespace Coorth {
    public interface IActor {
        void OnActive();
        Task OnActiveAsync();
        void Execute(in ActorMail e);
        Task ExecuteAsync(in ActorMail e);
        void DeActive();
        Task OnDeActiveAsync();
    }

    public abstract class Actor : Disposable, IActor {
        internal ActorContext Context { get; set; }
        
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
    

    

}
