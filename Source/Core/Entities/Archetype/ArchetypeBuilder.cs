using System;

namespace Coorth {
    public class ArchetypeBuilder {
        
        private readonly Sandbox sandbox;

        private ArchetypeDefinition definition;
        
        private bool closed;
        
        internal ArchetypeBuilder(Sandbox sandbox, ArchetypeDefinition definition) {
            this.sandbox = sandbox;
            this.definition = definition;
        }

        public ArchetypeBuilder Add(Type type) {
            if (closed) {
                throw new InvalidOperationException("Archetype is closed.");
            }
            if (!typeof(IComponent).IsAssignableFrom(type)) {
                throw new ArgumentException("Type is not implement IComponent");
            }

            if (!sandbox.IsComponentBind(type)) {
                throw new NotBindException(type);
            }
            definition = definition.AddComponent(type);
            return this;
        }
        
        public ArchetypeBuilder Add<T>() where T : IComponent {
            if (closed) {
                throw new InvalidOperationException("Archetype is closed.");
            }
            if (!sandbox.IsComponentBind(typeof(T))) {
                throw new NotBindException(typeof(T));
            }         
            definition = definition.AddComponent<T>();
            return this;
        }

        public Archetype Compile() {
            closed = true;
            return new Archetype(sandbox, definition);
        }
    }
}