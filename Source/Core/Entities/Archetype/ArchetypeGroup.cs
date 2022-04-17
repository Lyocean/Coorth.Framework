using System.Collections.Generic;
using System.Linq;

namespace Coorth {
    public class ArchetypeGroup {
        
        public readonly Sandbox Sandbox;

        public readonly EntityMatcher Matcher;

        private readonly HashSet<ArchetypeDefinition> archetypes = new();

        private ArchetypeDefinition[]? archetypeArray;

        internal ArchetypeDefinition[] Archetypes => archetypeArray ?? archetypes.ToArray();

        public ArchetypeGroup(Sandbox sandbox, EntityMatcher matcher) {
            this.Sandbox = sandbox;
            this.Matcher = matcher;
        }

        internal void Match(ArchetypeDefinition archetype) {
            if (!Matcher.Match(Sandbox, archetype)) {
                return;
            }
            if (archetypes.Add(archetype)) {
                archetypeArray = null;
            }
        }
    }
}
