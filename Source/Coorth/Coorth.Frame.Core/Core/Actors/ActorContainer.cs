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

        public ActorScheduler Scheduler { get; }

        private readonly ConcurrentDictionary<ActorPath, ActorId> paths = new ConcurrentDictionary<ActorPath, ActorId>();
        
        public ActorContainer(EventDispatcher dispatcher = null, IActorConfig config = null, ServiceContainer services = null) {
            this.Dispatcher = dispatcher ?? new EventDispatcher();
            this.Config = config ?? ActorConfig.Default;
            this.Services = services ?? new ServiceContainer();
            defaultDomain = CreateDomain<LocalDomain>();
        }

        private void _AddDomain(ActorDomain domain, string name) {
            domain.Setup(this, name);
            domains.Add(domain);
        }
        
        public ActorDomain GetDomain(string path) {
            return domains.Find(domain => domain.Name == path);
        }
        
        internal void _RemoveDomain(ActorDomain domain) {
            domains.Remove(domain);
        }
        
        public TDomain CreateDomain<TDomain>(TDomain domain = default, string name = "") where TDomain : ActorDomain, new() {
            domain ??= Activator.CreateInstance<TDomain>();
            _AddDomain(domain, name);
            (domain as IAwake)?.OnAwake();
            return domain;
        }
        
        public TDomain CreateDomain<TDomain, TArgument>(TArgument argument, string name = "") where TDomain : ActorDomain, IAwake<TArgument>, new() {
            var domain = Activator.CreateInstance<TDomain>();
            _AddDomain(domain, name);
            domain.OnAwake(argument);
            return domain;
        }

        public void DestroyDomain(ActorDomain domain) {
            domain.Dispose();
        }

        public ActorRef GetRef(ActorId id) {
            if(contexts.TryGetValue(id, out var context)) {
                return context.Ref;
            }
            return ActorRef.Null;
        }
    }
}