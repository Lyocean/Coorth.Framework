using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Coorth {
    public class ActorContainer {

        #region Common

        private readonly ActorContext root;

        public EventDispatcher Dispatcher { get; }

        public IActorConfig Config { get; }

        private readonly ActorScheduler scheduler;
        public ActorScheduler Scheduler => scheduler;

        
        private readonly ConcurrentDictionary<ActorId, ActorContext> contexts = new ConcurrentDictionary<ActorId, ActorContext>();

        private readonly List<ActorDomain> domains = new List<ActorDomain>();
        
        private readonly ConcurrentDictionary<ActorPath, ActorId> paths = new ConcurrentDictionary<ActorPath, ActorId>();

        
        public ActorContainer(EventDispatcher dispatcher = null, IActorConfig config = null) {
            this.Dispatcher = dispatcher ?? new EventDispatcher();
            this.Config = config ?? ActorConfig.Default;
        }

        #endregion

        #region Create

        public ActorDomain CreateDomain<TDomain>(ServiceContainer services) where TDomain : ActorDomain, new() {
            var domain = Activator.CreateInstance<TDomain>();
            domain.Setup(this, services);
            domains.Add(domain);
            return domain;
        }
        
        internal ActorContext CreateContext(ActorDomain domain, ActorContext parent, IActor actor = null, string name = null, IMailbox mailbox = null) {
            var id = ActorId.New();
            name = name ?? id.ToShortString();
            var path = parent == null ? new ActorPath(null, name) : new ActorPath(parent.Path.FullPath, name);
            var context = new ActorContext(id, domain, mailbox, path);
            context.Ref = new ActorRef(context);
            if (actor != null) {
                actor.Context = context;
            }
            return context;
        }
        
        internal void OnActorCreate(ActorContext context, IActor actor) {
            contexts.TryAdd(context.Id, context);
            paths.TryAdd(context.Path, context.Id);
        }
        
        internal void OnActorOnActive(ActorContext context, IActor actor) {
            
        }
        
        internal void OnActorDeActive(ActorContext context, IActor actor) {
            
        }
        
        internal void OnActorDestroy(ActorContext context, IActor actor) {
            contexts.TryRemove(context.Id, out _);
            paths.TryRemove(context.Path, out _);
        }

        public T CreateActor<T>() where T: class, IActor => root.CreateActor<T>();
        
        public ActorRef CreateRef<T>() where T: class, IActor => root.CreateRef<T>();

        public ActorRef CreateRef(ActorProps props, string name = null) => root.CreateRef(props, name);

        public T CreateProxy<T>() => throw new NotImplementedException();

        public ActorRef GetRef(ActorPath path) {
            if (paths.TryGetValue(path, out var id) && contexts.TryGetValue(id, out var context)) {
                return context.Ref;
            }
            return null;
        }

        public ActorRef GetRef(string path) => GetRef(new ActorPath(path));

        #endregion
    }
    
    public interface IActorConfig {
        int ActorThroughput { get; }
    }

    public class ActorConfig : IActorConfig {
        
        public static readonly ActorConfig Default = new ActorConfig();
        
        public int ActorThroughput => 100;
    }
}