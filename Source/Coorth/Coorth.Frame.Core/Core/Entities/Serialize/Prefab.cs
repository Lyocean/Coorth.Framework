using System;
using System.Collections.Generic;
using System.IO;

namespace Coorth {




    public class PrefabAsset2 {
        
        // private EntityAsset[] enities;
        // public EntityAsset Main => enities.Length > 0? enities[0] : default;
        //
        // private ReadOnlyMemory<byte> data;
        //
        // private void Load(Stream stream) {
        //     
        // }
        //
        // public PrefabCompiled Compile(Sandbox sandbox) {
        //
        //     return new PrefabCompiled(sandbox, this);
        // }
        //
        // public static PrefabAsset Create(Span<Entity> entities) {
        //     var dict = new Dictionary<Type, int>();
        //     for (int i = 0; i < entities.Length; i++) {
        //         var entity = entities[i];
        //         ref var context = ref entity.GetContext();
        //         foreach (var pair in context.Components) {
        //             var componentGroup = entity.Sandbox.GetComponentGroup(pair.Key);
        //             // dict.Add(componentGroup.Type, );
        //             // componentGroup.
        //         }
        //     }
        //     return new PrefabAsset();
        // }
    }

    public class ASerializer {
        
    }
    
    public class PrefabSerializer {

        public void ReadEntity() {
            
        }

        public void ReadComponent() {
            
        }

        // public Entity[] ReadEntities(Sandbox sandbox, SerializeReader reader) {
        //     var count = reader.ReadInt32();
        //     var entities = sandbox.CreateEntities(count);
        //     for (var i = 0; i < count; i++) {
        //         var entity = entities[i];
        //     }
        //     return entities;
        // }
        //
        // public void WriteEntities(SerializeWriter writer, Span<Entity> entities) {
        //     writer.WriteInt32(entities.Length);
        //     foreach (var entity in entities) {
        //         
        //     }
        // }


        public void WriteEntity(ref Entity entity) {
            
        }
        
        public void WriteComponent<T>(Type type, ref T component) {
            
        }
    }
    
    
    public readonly struct PrefabCompiled {

        private readonly Sandbox sandbox;

        // private readonly Archetype[] archetypes;

        private readonly PrefabAsset asset;

        public PrefabCompiled(Sandbox sandbox, PrefabAsset asset) {
            this.sandbox = sandbox;
            this.asset = asset;
            // this.archetypes = null;
        }

        // private Entity _CreateEntity(int position, int length) {
        //     var entity = sandbox.CreateEntity();
        //     for (var i = position; i < position + length; i++) {
        //         foreach (var component in components) {
        //             component.AddComponent(entity);
        //         }
        //     }
        //     return entity;
        // }
        //
        //
        //
        // public Entity Instantiate() {
        //     var entity = _CreateEntity(entities[0].Position, entities[0].Length);
        //     for (var i = 1; i < entities.Length; i++) {
        //         _CreateEntity(entities[i].Position, entities[i].Length);
        //     }
        //     return entity;
        // }
        //
        // public Entity Instantiate(Sandbox sandbox, Vector3 position) {
        //     
        // }
        //
        // public Entity Instantiate(Vector3 position, Quaternion rotation) {
        //     
        // }
        //
        // public Entity Instantiate(Vector3 position, Quaternion rotation, TransformComponent parent) {
        //                 
        // }
    }
}