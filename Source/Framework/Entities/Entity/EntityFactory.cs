namespace Coorth.Framework; 

public abstract class EntityFactory {

    public readonly World World;

    public Archetype Archetype { get; private set; }

    protected EntityFactory(World world) {
        World = world;
    }
    
    internal void Setup() {
        var builder = World.CreateArchetype();
        OnBuild(builder);
        Archetype = builder.Compile();
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