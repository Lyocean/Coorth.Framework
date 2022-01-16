namespace Coorth {
    public abstract class EntityFactory {
        
        public Sandbox Sandbox { get; private set; }

        public Archetype Archetype { get; private set; }

        internal void Setup(Sandbox sandbox) {
            this.Sandbox = sandbox;
            var builder = Sandbox.CreateArchetype();
            OnBuild(builder);
            this.Archetype = builder.Compile();
        }

        protected abstract void OnBuild(ArchetypeBuilder builder);
        
        public Entity Create() {
            var entity = Archetype.CreateEntity();
            OnCreate(entity);
            return entity;
        }

        protected virtual void OnCreate(Entity entity) {
            
        }
        
        public bool Recycle(Entity entity) {
            OnRecycle(entity);
            entity.Dispose();
            return true;
        }

        protected virtual void OnRecycle(Entity entity) {
            
        }
    }
}