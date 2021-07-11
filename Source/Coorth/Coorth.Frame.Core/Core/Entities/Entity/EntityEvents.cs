namespace Coorth {
    public readonly struct EventEntityAdd : ISandboxEvent {
        
        public readonly Entity Entity;
        
        public EntityId Id => Entity.Id;

        public Sandbox Sandbox => Entity.Sandbox;

        internal EventEntityAdd(Entity entity) {
            this.Entity = entity;
        }
    }

    public readonly struct EventEntityRemove : ISandboxEvent {
        
        public readonly Entity Entity;
        
        public EntityId Id => Entity.Id;

        public Sandbox Sandbox => Entity.Sandbox;
        
        internal EventEntityRemove(Entity entity) {
            this.Entity = entity;
        }
    }
}