namespace Coorth {
    public sealed class ModuleRoot : ModuleBase {
        
        public override AppFrame App { get; }
        
        public override ServiceLocator Services { get; }
        
        public override EventDispatcher Dispatcher { get; }
        
        public override ActorRuntime Actors { get; }

        public override LocalDomain Domain { get; }

        public ModuleRoot(AppFrame app, ServiceLocator services, EventDispatcher dispatcher, ActorRuntime actors) {
            this.App = app;
            this.Services = services;
            this.Dispatcher = dispatcher;
            this.Actors = actors;
            this.Domain = Actors.CreateDomain<LocalDomain>();
        }
    }
}