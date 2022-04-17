using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Coorth {
    public class ActorRuntime {

        public readonly EventDispatcher Dispatcher;

        public readonly IActorSetting Setting;

        public readonly ServiceLocator Services;

        private readonly List<ActorDomain> domains = new List<ActorDomain>();

        private readonly ConcurrentDictionary<ActorId, ActorContext> contexts = new ConcurrentDictionary<ActorId, ActorContext>();

        private readonly LocalDomain defaultDomain;
        
        public ActorScheduler Scheduler { get; }

        private readonly ConcurrentDictionary<ActorPath, ActorId> paths = new ConcurrentDictionary<ActorPath, ActorId>();
        
        public ActorRuntime(EventDispatcher? dispatcher = null, IActorSetting? setting = null, ServiceLocator? services = null) {
            this.Dispatcher = dispatcher ?? new EventDispatcher();
            this.Setting = setting ?? ActorSetting.Default;
            this.Services = services ?? new ServiceLocator();
            this.Scheduler = new ActorScheduler();
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
        
        public TDomain CreateDomain<TDomain>(TDomain domain, string name) where TDomain : ActorDomain, new() {
            domain ??= Activator.CreateInstance<TDomain>();
            _AddDomain(domain, name);
            (domain as ISetup)?.OnSetup();
            return domain;
        }
        
        public TDomain CreateDomain<TDomain>(string name) where TDomain : ActorDomain, new() {
            var domain = Activator.CreateInstance<TDomain>();
            return CreateDomain<TDomain>(domain, name);
        }
        
        public TDomain CreateDomain<TDomain>() where TDomain : ActorDomain, new() {
            var domain = Activator.CreateInstance<TDomain>();
            return CreateDomain<TDomain>(domain, nameof(TDomain));
        }
        
        public TDomain CreateDomain<TDomain, TArgument>(TArgument argument, string name = "") where TDomain : ActorDomain, ISetup<TArgument>, new() {
            var domain = Activator.CreateInstance<TDomain>();
            _AddDomain(domain, name);
            domain.OnSetup(argument);
            return domain;
        }

        public void DestroyDomain(ActorDomain domain) {
            domain.Dispose();
        }

        internal void OnActorContextAttach(ActorId id, ActorContext context) {
            contexts.TryAdd(id, context);
        }

        internal void OnActorContextDetach(ActorId id) {
            contexts.TryRemove(id, out var _);
        }

        public ActorRef GetRef(ActorId id) {
            if(contexts.TryGetValue(id, out var context)) {
                return context.Ref;
            }
            return ActorRef.Null;
        }
    }
}