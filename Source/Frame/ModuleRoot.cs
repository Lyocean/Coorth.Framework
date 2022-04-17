using System;

namespace Coorth {
    public sealed class ModuleRoot : Module {

        public override Type Key => typeof(ModuleRoot);

        public override ModuleRoot Root => this;

        public override AppFrame App { get; }

        public override ServiceLocator Services { get; }

        public override EventDispatcher Dispatcher { get; }

        public ActorRuntime Actors { get; }

        public LocalDomain Domain { get; }

        public ModuleRoot(AppFrame app, ServiceLocator services, EventDispatcher dispatcher, ActorRuntime actors) {
            this.App = app;
            this.Services = services;
            this.Dispatcher = dispatcher;
            this.Actors = actors;
            this.Domain = Actors.CreateDomain<LocalDomain>();
        }
    }
}