using System;
using System.Collections.Generic;
using System.Text;

namespace Coorth {
    internal class ArchetypeDefinition {

        #region Fields

        private readonly Sandbox sandbox;
        
        private ChunkList<int> entities;

        public int EntityCount;

        public int EntityCapacity;
        
        private readonly Dictionary<int, ArchetypeDefinition> links = new Dictionary<int, ArchetypeDefinition>();

        private int reusing;
        
        public readonly int Flag;

        public readonly HashSet<int> Components;

        public readonly int[] Types;
        
        public readonly ComponentMask Mask;

        public readonly int ComponentCapacity;
        
        public int ComponentCount => Types.Length;

        public ArchetypeDefinition(Sandbox sandbox) {
            this.sandbox = sandbox;
            this.Components = new HashSet<int>();
            this.Mask = new ComponentMask(0);
            this.ComponentCapacity = 2;
            this.Flag = 0;
            this.entities = new ChunkList<int>(sandbox.ArchetypeCapacity.Index, sandbox.ArchetypeCapacity.Chunk);
            this.Types = Array.Empty<int>();
        }
        
        public ArchetypeDefinition(Sandbox sandbox, int flag, HashSet<int> components, ComponentMask mask) {
            this.sandbox = sandbox;
            var capacity = (int) ((uint) (components.Count - 1 + 2) >> 1) << 1;

            this.Components = components;

            this.Types = new int[components.Count];
            int position = 0;
            foreach (var typeId in components) {
                this.Types[position] = typeId;
                position++;
            }
            Array.Sort(this.Types, (a, b) => a - b);

            this.Mask = mask;
            this.ComponentCapacity = capacity;
            this.Flag = flag;
            
            this.entities = new ChunkList<int>(sandbox.ArchetypeCapacity.Index, sandbox.ArchetypeCapacity.Chunk);
        }

        #endregion

        #region Entity

        public int AddEntity(int entityIndex) {
            var position = -reusing - 1;
            EntityCount++;
            if (position >= 0) {
                reusing = entities[position];
                entities.Set(position, entityIndex + 1);
            }
            else {
                position = entities.Count;
                entities.Add(entityIndex + 1);
                EntityCapacity++;
            }
            
            return position;
        }

        public void RemoveEntity(int position) {
            entities[position] = reusing;
            reusing = -(position+1);
            EntityCount--;
            
        }

        public int GetEntity(int position) {
            return entities[position] - 1;
        }

        internal ChunkList<int> GetEntities() {
            return entities;
        }

        #endregion
        
        #region Component

        public ArchetypeDefinition AddComponent(int type) {
            if (links.TryGetValue(type, out var archetype)) {
                return archetype;
            }
            var capacity = (int) ((uint) (Components.Count + 2) >> 1) << 1;

            var components = new HashSet<int>();
            foreach (var typeId in Components) {
                components.Add(typeId);
            }
            components.Add(type);

            var mask = new ComponentMask(Mask, capacity);
            mask.Set(type, true);
            
            var flag = Flag ^ (1 << type);
            
            archetype = new ArchetypeDefinition(sandbox, flag, components, mask);
            
            links[type] = archetype;
            archetype.links[type] = this;
            
            sandbox.OnAddArchetype(archetype);

            return archetype;
        }

        public ArchetypeDefinition AddComponent(Type type) {
            var typeId = Sandbox.ComponentTypeIds[type];
            return AddComponent(typeId);
        }
        
        public ArchetypeDefinition AddComponent<T>() where T : IComponent {
            var typeId = ComponentType<T>.TypeId;
            return AddComponent(typeId);
        }
        
        public bool HasComponent(int type) {
            return Components.Contains(type);
        }
        
        public ArchetypeDefinition RemoveComponent(int type) {
            if (links.TryGetValue(type, out var archetype)) {
                return archetype;
            }
            var capacity = (int) ((uint) Components.Count >> 1) << 1;
            var components = new HashSet<int>();
            foreach (var typeId in Components) {
                if (typeId != type) {
                    components.Add(typeId);
                }
            }
            
            var mask = new ComponentMask(Mask, capacity);
            mask.Set(type, false);
            
            var flag = Flag ^ (1 << type);

            archetype = new ArchetypeDefinition(sandbox, flag, components, mask);

            links[type] = archetype;
            archetype.links[type] = this;

            sandbox.OnAddArchetype(archetype);
            return archetype;
        }

        public bool ComponentsEquals(ArchetypeDefinition other) {
            if (ComponentCount != other.ComponentCount) {
                return false;
            }
            foreach (var typeId in other.Components) {
                if (!Components.Contains(typeId)) {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region ToString

        private string? toString;
        
        public string GetComponentNames() {
            if (toString != null) {
                return toString;
            }
            var builder = new StringBuilder();
            foreach (var type in Types) {
                var group = sandbox.GetComponentGroup(type);
                builder.Append(group.Type.Name).Append(", ");
            }
            builder.Remove(builder.Length - 1, 1);
            toString = builder.ToString();
            return toString;
        }

        public override string ToString() {
            return $"ArchetypeDefinition:{{{ComponentCount} - [{GetComponentNames()}]}}";
        }
        
        #endregion
    }
    
}