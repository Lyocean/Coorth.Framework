using System;

namespace Coorth {
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
                var componentAsset = componentGroup.PackComponent(entity, pair.Value);
                entityAsset.Components[index] = componentAsset;
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
            
    public abstract class ComponentPack {
        public abstract Type Type { get; }

        public abstract void UnPack(Sandbox sandbox,Entity entity);
    }

    public class ComponentPack<T> : ComponentPack where T: IComponent {
        
        public override Type Type => typeof(T);

        public T Component;

        public override void UnPack(Sandbox sandbox, Entity entity) {
            var componentGroup = sandbox.GetComponentGroup<T>();
            componentGroup._Clone(entity, ref Component, out var newComponent);
            entity.Add<T>(newComponent);
        }
        
        public void Pack(Sandbox sandbox, Entity entity, ref T component) {
            var componentGroup = sandbox.GetComponentGroup<T>();
            componentGroup._Clone(entity, ref component, out Component);
        }
    }
}