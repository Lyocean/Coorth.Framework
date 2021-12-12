using System.Collections.Generic;
using System.Linq;

namespace Coorth {
    public class ArchetypeGroup {
        
        public readonly Sandbox Sandbox;

        public readonly EntityMatcher Matcher;

        private readonly HashSet<ArchetypeDefinition> archetypes = new HashSet<ArchetypeDefinition>();

        private ArchetypeDefinition[] archetypeArray;

        internal ArchetypeDefinition[] Archetypes => archetypeArray ?? archetypes.ToArray();

        public ArchetypeGroup(Sandbox sandbox, EntityMatcher matcher) {
            this.Sandbox = sandbox;
            this.Matcher = matcher;
        }

        internal void Match(ArchetypeDefinition archetype) {
            if (Matcher.Match(Sandbox, archetype)) {
                if (archetypes.Add(archetype)) {
                    archetypeArray = null;
                }
            }
        }
    }
}
