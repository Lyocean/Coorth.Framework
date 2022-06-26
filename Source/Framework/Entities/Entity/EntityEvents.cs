namespace Coorth.Framework; 

[Event]
public readonly struct EventEntityCreate {
        
    public readonly Entity Entity;
        
    public EntityId Id => Entity.Id;

    public Sandbox Sandbox => Entity.Sandbox;

    internal EventEntityCreate(Entity entity) {
        Entity = entity;
    }
}

[Event]
public readonly struct EventEntityRemove {
 
    public readonly Entity Entity;
        
    public EntityId Id => Entity.Id;

    public Sandbox Sandbox => Entity.Sandbox;
        
    internal EventEntityRemove(Entity entity) {
        Entity = entity;
    }
}