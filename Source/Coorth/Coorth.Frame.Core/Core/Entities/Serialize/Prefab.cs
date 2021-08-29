using System;
using System.Collections.Generic;
using System.IO;

namespace Coorth {
    
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