namespace Coorth.Framework;

public readonly record struct Space(World World, int Id, int Version) {
    public readonly World World = World;
    public readonly int Id = Id;
    public readonly int Version = Version;

    public bool IsValidate() => World.HasSpace(this);

    public void Destroy() => World.DestroySpace(this);
    
    public Entity CreateEntity() => World.CreateEntity(this);

    public Entity CreateEntity(Archetype archetype) => archetype.CreateEntity(this);
}