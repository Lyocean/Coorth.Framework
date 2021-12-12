using System.Threading.Tasks;

namespace Coorth {
    public interface IActor {
        Task ReceiveAsync(in ActorMail e);
    }

    public abstract class Actor : Disposable, IActor {
        internal LocalContext Context { get; set; }
        
        protected ActorRef Self => Context.Ref;
        
        public ActorId Id => Context.Id;
        
        protected ActorContainer Container => Context.Container;
        
        protected EventDispatcher Dispatcher => Container.Dispatcher;

        public virtual Task ReceiveAsync(in ActorMail e) {
            return Task.CompletedTask;
        }
    }
}
