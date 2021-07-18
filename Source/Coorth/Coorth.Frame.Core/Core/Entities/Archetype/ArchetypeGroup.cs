using System;
using System.Collections.Generic;
using System.Linq;


namespace Coorth {
    public class ArchetypeGroup {
        
        private readonly Sandbox sandbox;
        public Sandbox Sandbox => sandbox;

        public readonly EntityMatcher matcher;

        private readonly HashSet<Archetype> archetypes = new HashSet<Archetype>();

        private Archetype[] archetypeArray;

        internal Archetype[] Archetypes {
            get {
                if (archetypeArray == null) {
                    archetypeArray = archetypes.ToArray() ;
                }
                return archetypeArray;
            }
        }
        
        public ArchetypeGroup(Sandbox sandbox, EntityMatcher matcher) {
            this.sandbox = sandbox;
            this.matcher = matcher;
        }

        internal void Match(Archetype archetype) {
            if (matcher.Match(sandbox, archetype)) {
                if (archetypes.Add(archetype)) {
                    archetypeArray = null;
                }
            }
        }
        
    }
}
