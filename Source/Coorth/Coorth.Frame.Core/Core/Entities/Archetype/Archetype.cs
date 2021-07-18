using System;
using System.Collections.Generic;


namespace Coorth {
    internal class Archetype {

        #region Fields

        public readonly Sandbox Sandbox;

        private ChunkList<int> entities = new ChunkList<int>();

        public int EntityCount = 0;

        private readonly Dictionary<int, Archetype> links = new Dictionary<int, Archetype>();

        private readonly Stack<int> resumeIds = new Stack<int>();
        
        public readonly int Flag;

        public readonly HashSet<int> Components;

        public readonly int[] Types;
        
        public readonly ComponentMask Mask;

        public readonly int ComponentCapacity;
        
        public int ComponentCount => Types.Length;

        public Archetype(Sandbox sandbox) {
            this.Sandbox = sandbox;
            this.Components = new HashSet<int>();
            this.Mask = new ComponentMask(0);
            this.ComponentCapacity = 2;
            this.Flag = 0;
            this.entities = new ChunkList<int>(sandbox.ArchetypeCapacity.Index, sandbox.ArchetypeCapacity.Chunk);
            this.Types = Array.Empty<int>();
        }
        
        public Archetype(Sandbox sandbox, int flag, HashSet<int> components, ComponentMask mask) {
            var capacity = (int) ((uint) (components.Count - 1 + 2) >> 1) << 1;

            this.Sandbox = sandbox;
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
            if (resumeIds.Count > 0) {
                var position = resumeIds.Pop();
                entities.Set(position, entityIndex + 1);
                EntityCount++;
                return position;
            }
            else {
                var position = entities.Count;
                entities.Add(entityIndex + 1);
                EntityCount++;
                return position;
            }
        }

        public void RemoveEntity(int position) {
            entities[position] = 0;
            resumeIds.Push(position);
            EntityCount--;
        }

        public int GetEntity(int position) {
            return entities[position] - 1;
        }

        #endregion
        
        #region Component

        public Archetype AddComponent(int type) {
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
            
            archetype = new Archetype(Sandbox, flag, components, mask);
            
            links[type] = archetype;
            archetype.links[type] = this;
            
            Sandbox.OnAddArchetype(archetype);

            return archetype;
        }

        public Archetype AddComponent(Type type) {
            var typeId = Sandbox.ComponentTypeIds[type];
            return AddComponent(typeId);
        }
        
        public Archetype AddComponent<T>() where T : IComponent {
            var componentGroup = Sandbox.GetComponentGroup<T>();
            return AddComponent(componentGroup.Id);
        }
        
        public bool HasComponent(int type) {
            return Components.Contains(type);
        }
        
        public Archetype RemoveComponent(int type) {
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

            archetype = new Archetype(Sandbox, flag, components, mask);

            links[type] = archetype;
            archetype.links[type] = this;

            Sandbox.OnAddArchetype(archetype);
            return archetype;
        }

        public bool ComponentsEquals(Archetype other) {
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
    }
    
    
    public class ArchetypeBuilder {
        
        private readonly Sandbox sandbox;

        private Archetype archetype;
        
        private bool closed;
        
        internal ArchetypeBuilder(Sandbox sandbox, Archetype archetype) {
            this.sandbox = sandbox;
            this.archetype = archetype;
        }

        public ArchetypeBuilder Add(Type type) {
            if (closed) {
                throw new InvalidOperationException("Archetype is closed.");
            }
            if (!typeof(IComponent).IsAssignableFrom(type)) {
                throw new ArgumentException("Type is not implement IComponent");
            }
            archetype = archetype.AddComponent(type);
            return this;
        }
        
        public ArchetypeBuilder Add<T>() where T : IComponent {
            if (closed) {
                throw new InvalidOperationException("Archetype is closed.");
            }
            archetype = archetype.AddComponent<T>();
            return this;
        }

        public ArchetypeCompiled Compile() {
            closed = true;
            return new ArchetypeCompiled(sandbox, archetype);
        }
    }

    public readonly struct ArchetypeCompiled {
        
        private readonly Sandbox sandbox;
        
        internal readonly Archetype archetype;

        public Sandbox Sandbox => sandbox;

        public bool IsNull => sandbox == null || archetype == null || archetype.Sandbox != sandbox;

        public bool Has<T>() where T : IComponent => archetype.HasComponent(ComponentGroup<T>.TypeId);
        
        internal ArchetypeCompiled(Sandbox sandbox, Archetype archetype) {
            this.sandbox = sandbox;
            this.archetype = archetype;
        }

        public Entity CreateEntity() {
            return sandbox.CreateEntity(archetype);
        }
        
        public void CreateEntities(Span<Entity> span, int count) {
            sandbox.CreateEntities(span, count);
        }
        
        public void CreateEntities(IList<Entity> list, int count) {
            for (var i = 0; i < count; i++) {
                list[i] = CreateEntity();
            }
        }
        
        public Entity[] CreateEntities(int count) {
            var array = new Entity[count];
            CreateEntities(array.AsSpan(), count);
            return array;
        }
    }
}