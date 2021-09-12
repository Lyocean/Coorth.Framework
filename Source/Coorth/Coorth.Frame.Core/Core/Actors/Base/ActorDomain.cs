using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Coorth {
    
    public abstract class ActorDomain : Disposable, IActor {

        public string Name { get; private set; }

        public ActorContainer Container { get; private set; }

        public ServiceContainer Services => Container.Services;

        public EventDispatcher Dispatcher => Container.Dispatcher;

        public ActorPath Path;

        internal void Setup(ActorContainer container, string name) {
            this.Name = name;
            this.Container = container;
            this.Path = new ActorPath(name);
        }
        
        protected override void Dispose(bool dispose) {
            Container._RemoveDomain(this);
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