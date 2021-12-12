namespace Coorth {
    public sealed class ModuleRoot : ModuleBase {
        public override ServiceLocator Services { get; } = new ServiceLocator();

        public override EventDispatcher Dispatcher { get; } = new EventDispatcher();

        private readonly ActorContainer actors;
        public override ActorContainer Actors => actors;

        private readonly AppFrame app;
        public AppFrame App => app;

        public ModuleRoot(ServiceLocator parent, ActorContainer actors, AppFrame app) {
            parent.AddChild(Services);
            this.actors = actors;
            this.app = app;
        }
    }
}