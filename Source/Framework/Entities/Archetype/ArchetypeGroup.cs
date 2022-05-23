using System.Collections.Generic;
using System.Linq;

namespace Coorth.Framework; 

public class ArchetypeGroup {
        
    public readonly Sandbox Sandbox;

    public readonly EntityMatcher Matcher;

    private readonly HashSet<ArchetypeDefinition> archetypes = new();

    private ArchetypeDefinition[]? archetypeArray;

    internal ArchetypeDefinition[] Archetypes => archetypeArray ?? archetypes.ToArray();

    public ArchetypeGroup(Sandbox sandbox, EntityMatcher matcher) {
        Sandbox = sandbox;
        Matcher = matcher;
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