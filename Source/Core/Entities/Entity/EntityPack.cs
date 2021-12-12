﻿namespace Coorth {
    public sealed class EntityPack {
        
        public readonly ComponentPack[] Components;

        private EntityPack(int capacity) {
            Components = new ComponentPack[capacity];
        }
        
        public static EntityPack Pack(Entity entity) {
            var sandbox = entity.Sandbox;
            ref var context = ref entity.GetContext();
            var entityAsset = new EntityPack(context.Count);
            var index = 0;
            foreach (var pair in context.Components) {
                var componentGroup = sandbox.GetComponentGroup(pair.Key);
                var componentPack = componentGroup.PackComponent(entity, pair.Value);
                entityAsset.Components[index] = componentPack;
                index++;
            }
            return entityAsset;
        }

        public Entity UnPack(Sandbox sandbox) {
            var entity = sandbox.CreateEntity();
            for (var i = 0; i < Components.Length; i++) {
                Components[i].UnPack(sandbox, entity);
            }
            return entity;
        }
    }
}