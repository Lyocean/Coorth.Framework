using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Coorth {
    
    public abstract class ActorDomain : Disposable, IActor {

        public string Name { get; private set; }

        public ActorRuntime Runtime { get; private set; }

        public ServiceLocator Services => Runtime.Services;

        public EventDispatcher Dispatcher => Runtime.Dispatcher;

        public ActorPath Path;

        internal void Setup(ActorRuntime runtime, string name) {
            this.Name = name;
            this.Runtime = runtime;
            this.Path = new ActorPath(name);
        }
        
        protected override void OnDispose(bool dispose) {
            Runtime._RemoveDomain(this);
        }

        public virtual Task ReceiveAsync(in ActorMail mail) => Task.CompletedTask;
    }

    public abstract class ActorDomain<TContext> : ActorDomain where TContext : ActorContext {
        
        protected RootContext<TContext> Context { get; private set; }

        private readonly ConcurrentDictionary<ActorId, TContext> contexts = new ConcurrentDictionary<ActorId, TContext>();

        protected ActorDomain(){
            this.Context = new RootContext<TContext>(this);
        }
    }
}