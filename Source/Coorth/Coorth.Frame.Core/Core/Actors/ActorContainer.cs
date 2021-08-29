using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Coorth {
    public class ActorContainer {

        public readonly EventDispatcher Dispatcher;

        public readonly IActorConfig Config;

        public readonly ServiceContainer Services;

        private readonly List<ActorDomain> domains = new List<ActorDomain>();

        private readonly ConcurrentDictionary<ActorId, ActorContext> contexts = new ConcurrentDictionary<ActorId, ActorContext>();

        private readonly LocalDomain defaultDomain;
        
        private readonly ActorContext root;

        public ActorContainer(EventDispatcher dispatcher = null, IActorConfig config = null, ServiceContainer services = null) {
            this.Dispatcher = dispatcher ?? new EventDispatcher();
            this.Config = config ?? ActorConfig.Default;
            this.Services = services ?? new ServiceContainer();
            defaultDomain = CreateDomain<LocalDomain>();
        }

        #region Domain

        public TDomain CreateDomain<TDomain>(TDomain domain = default) where TDomain : ActorDomain, new() {
            domain = domain ?? Activator.CreateInstance<TDomain>();
            domain.Setup(this);
            domains.Add(domain);
            ((IAwake)domain).OnAwake();
            return domain;
        }
        
        public TDomain CreateDomain<TDomain, TArgument>(TArgument argument) where TDomain : ActorDomain, IAwake<TArgument>, new() {
            var domain =Activator.CreateInstance<TDomain>();
            domain.Setup(this);
            domains.Add(domain);
            ((IAwake<TArgument>)domain).OnAwake(argument);
            return domain;
        }

        internal void _RemoveDomain(ActorDomain domain) {
            domains.Remove(domain);
        }

        public void DestroyDomain(ActorDomain domain) {
            domain.Dispose();
        }

        #endregion

        #region Context

        internal ActorContext CreateContext(ActorDomain domain, ActorContext parent, Actor actor = null, string name = null, IMailbox mailbox = null) {
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

        #endregion
        
        
        
        
        #region Common

        private readonly ActorScheduler scheduler;
        public ActorScheduler Scheduler => scheduler;

        

        
        private readonly ConcurrentDictionary<ActorPath, ActorId> paths = new ConcurrentDictionary<ActorPath, ActorId>();

        #endregion

        #region Create


        

        
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

        public T CreateActor<T>() where T: Actor => root.CreateActor<T>();
        
        public ActorRef CreateRef<T>() where T: Actor => root.CreateRef<T>();

        public ActorRef CreateRef(ActorProps props, string name = null) => root.CreateRef(props, name);

        public T CreateProxy<T>() => throw new NotImplementedException();

        public ActorRef GetRef(ActorPath path) {
            if (paths.TryGetValue(path, out var id) && contexts.TryGetValue(id, out var context)) {
                return context.Ref;
            }
            return default;
        }

        public ActorRef GetRef(string path) => GetRef(new ActorPath(path));

        #endregion
    }
    

}