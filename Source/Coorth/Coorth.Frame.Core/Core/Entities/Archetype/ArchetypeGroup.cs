using System.Collections;
using System.Collections.Generic;


namespace Coorth {
    public class ArchetypeGroup {
        
        private readonly Sandbox sandbox;

        public readonly EntityMatcher matcher;

        private readonly HashSet<Archetype> archetypes = new HashSet<Archetype>();

        internal IReadOnlyCollection<Archetype> Archetypes => archetypes;
        
        public ArchetypeGroup(Sandbox sandbox, EntityMatcher matcher) {
            this.sandbox = sandbox;
            this.matcher = matcher;
        }

        internal void Match(Archetype archetype) {
            if (matcher.Match(sandbox, archetype)) {
                archetypes.Add(archetype);
            }
        }
    }
}
