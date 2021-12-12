using System;
using System.Collections.Generic;
using Coorth.Common;

namespace Coorth {
    public enum PrefabMode {
        Entities,
        Archetype,
    }
    
    public class PrefabAsset : Asset {
        
        public static PrefabAsset Create(in Entity entity, SerializeWriter writer, PrefabMode mode = PrefabMode.Entities) {
            writer.BeginRoot(typeof(PrefabAsset));
            {
                writer.WriteType(writer.GetType());
                if (mode == PrefabMode.Entities) {
                    EncodeEntities(in entity, writer);
                }
                else {
                    EncodeArchetypes(in entity, writer);
                }
            }
            writer.EndRoot();

            var prefab = new PrefabAsset();
            return prefab;
        }

        private static void EncodeEntities(in Entity entity, SerializeWriter writer) {
            entity.Write(writer);
            if (!entity.TryGet(out HierarchyComponent hierarchy) && hierarchy.Count > 0) {
                return;
            }
            foreach (var child in hierarchy.GetChildrenEntities()) {
                EncodeEntities(child, writer);
            }
        }

        private static void DecodeEntities(Sandbox sandbox, SerializeReader reader) {
            var entity = sandbox.ReadEntity(reader);
            
        }

        private static void EncodeArchetypes(in Entity entity, SerializeWriter writer) {
            throw new NotImplementedException();
            var sandbox = entity.Sandbox;
            var dict = new Dictionary<ArchetypeDefinition, HashSet<Entity>>();
            SerializeEntity(entity, writer, dict);
            foreach (var pair in dict) {
                var archetype = pair.Key;
                var entities = pair.Value;
                writer.WriteTag("Archetype", 1);
                sandbox.WriteArchetype(writer, new Archetype(sandbox, archetype));
                writer.WriteTag("Archetype", 1);
            }
            sandbox.WriteEntity(writer, entity.Id);
        }
        
        private static void SerializeEntity(Entity entity, SerializeWriter writer, Dictionary<ArchetypeDefinition, HashSet<Entity>> entities) {
            var archetype = entity.GetContext().Archetype;
            if (!entities.TryGetValue(archetype, out var set)) {
                set = new HashSet<Entity>();
                entities[archetype] = set;
            }
            set.Add(entity);
            if (entity.TryGet(out HierarchyComponent hierarchy)) {
                foreach (var childEntity in hierarchy.GetChildrenEntities()) {
                    SerializeEntity(childEntity, writer, entities);
                }
            }   
        }
        
        public static PrefabAsset Create(IEnumerable<Entity> entities) {
            throw new NotImplementedException();
        }

        public struct PrefabEntry {
            public List<Type> Components;
            public List<int> Entities;
        }
    }
    
    public readonly struct Prefab {

        private readonly Sandbox sandbox;

        private readonly PrefabAsset asset;
        
        public Prefab(Sandbox sandbox, PrefabAsset asset) {
            this.sandbox = sandbox;
            this.asset = asset;
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