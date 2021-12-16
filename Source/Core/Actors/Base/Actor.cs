using System.Threading.Tasks;

namespace Coorth {
    public interface IActor {
        Task ReceiveAsync(in ActorMail e);
    }

    public abstract class Actor : Disposable, IActor {
        internal LocalContext Context { get; set; }
        
        protected ActorRef Self => Context.Ref;
        
        public ActorId Id => Context.Id;
        
        protected ActorRuntime Runtime => Context.Runtime;
        
        protected EventDispatcher Dispatcher => Runtime.Dispatcher;

        public virtual Task ReceiveAsync(in ActorMail e) {
            return Task.CompletedTask;
        }
    }
}
