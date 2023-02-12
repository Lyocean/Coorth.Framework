using System.Collections.Generic;
using System.Linq;

namespace Coorth.Framework; 

public sealed class ArchetypeGroup {
        
    public readonly World World;

    public readonly ArchetypeMatcher Matcher;

    private readonly HashSet<ArchetypeDefinition> archetypes = new();

    private ArchetypeDefinition[]? archetypeArray;

    internal ArchetypeDefinition[] Archetypes => archetypeArray ?? archetypes.ToArray();

    public ArchetypeGroup(World world, ArchetypeMatcher matcher) {
        World = world;
        Matcher = matcher;
    }

    internal void Match(ArchetypeDefinition archetype) {
        if (!Matcher.Match(archetype)) {
            return;
        }
        if (archetypes.Add(archetype)) {
            archetypeArray = null;
        }
    }
}