using System;
using System.Collections.Generic;

namespace Coorth {
    public readonly struct Archetype : IEquatable<Archetype> {
        
        internal readonly ArchetypeDefinition Definition;

        public readonly Sandbox Sandbox;

        public bool IsNull => Sandbox == null || Definition == null;

        public int Count => Definition?.ComponentCount ?? 0;
        
        public bool Has<T>() where T : IComponent => Definition.HasComponent(ComponentType<T>.TypeId);
        
        internal Archetype(Sandbox sandbox, ArchetypeDefinition definition) {
            this.Sandbox = sandbox;
            this.Definition = definition;
        }

        public Entity CreateEntity() {
            return Sandbox.CreateEntity(Definition);
        }
        
        public void CreateEntities(Span<Entity> span, int count) {
            for (var i = 0; i < count; i++) {
                span[i] = Sandbox.CreateEntity(Definition);
            }
        }
        
        public void CreateEntities(IList<Entity> list, int count) {
            for (var i = 0; i < count; i++) {
                list.Add(Sandbox.CreateEntity(Definition));
            }
        }
        
        public Entity[] CreateEntities(int count) {
            var array = new Entity[count];
            CreateEntities(array.AsSpan(), count);
            return array;
        }

        public static bool operator ==(in Archetype l, in Archetype r) {
            return l.Equals(r);
        }
        
        public static bool operator !=(in Archetype l, in Archetype r) {
            return !l.Equals(r);
        }
        
        public bool Equals(Archetype other) {
            return Equals(Definition, other.Definition) && Equals(Sandbox, other.Sandbox);
        }

        public override bool Equals(object? obj) {
            return obj is Archetype other && Equals(other);
        }

        public override int GetHashCode() {
            return HashCode.Combine(Definition, Sandbox);
        }

        public override string ToString() {
            return $"Archetype:{{{Definition.ComponentCount} - [{Definition.GetComponentNames()}]}}";
        }
    }
}