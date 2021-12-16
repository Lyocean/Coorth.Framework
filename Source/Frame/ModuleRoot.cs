namespace Coorth {
    public sealed class ModuleRoot : ModuleBase {
        public override ServiceLocator Services { get; } = new ServiceLocator();

        public override EventDispatcher Dispatcher { get; } = new EventDispatcher();

        private readonly ActorRuntime actors;
        public override ActorRuntime Actors => actors;

        private readonly AppFrame app;
        public AppFrame App => app;

        public ModuleRoot(ServiceLocator parent, ActorRuntime actors, AppFrame app) {
            parent.AddChild(Services);
            this.actors = actors;
            this.app = app;
        }
    }
}