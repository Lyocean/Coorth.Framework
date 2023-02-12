namespace Coorth.Framework; 

public sealed class EntityPack {
        
    public readonly ComponentPack[] Components;

    private EntityPack(int capacity) {
        Components = new ComponentPack[capacity];
    }
        
    public static EntityPack Pack(Entity entity) {
        var world = entity.World;
        ref var context = ref entity.World.GetContext(entity.Id.Index);
        var entityPack = new EntityPack(context.Count);
        var index = 0;
        foreach (var pair in context.Components) {
            var componentGroup = world.GetComponentGroup(pair.Key);
            var componentPack = componentGroup.Pack(entity, pair.Value);
            entityPack.Components[index] = componentPack;
            index++;
        }
        return entityPack;
    }

    public Entity UnPack(World world) {
        var entity = world.CreateEntity();
        for (var i = 0; i < Components.Length; i++) {
            Components[i].UnPack(world, entity);
        }
        return entity;
    }
}