using System.Collections.Generic;

namespace Coorth.Framework; 

public partial class World {
    
    private readonly Dictionary<int, List<ArchetypeDefinition>> archetypes = new();

    private readonly ArchetypeDefinition emptyArchetype;

    private readonly Dictionary<int, List<ArchetypeGroup>> componentToGroups = new();

    private readonly Dictionary<ArchetypeMatcher, ArchetypeGroup> matcherToGroups = new();

    private void InitArchetypes(out ArchetypeDefinition empty) {
        empty = new ArchetypeDefinition(this);
    }

    private void ClearArchetypes() {
        archetypes.Clear();
        componentToGroups.Clear();
        matcherToGroups.Clear();
    }

    public ArchetypeBuilder CreateArchetype() {
        return new ArchetypeBuilder(this, emptyArchetype);
    }

    public Archetype GetArchetype(in EntityId id) {
        ref var context = ref GetContext(id.Index);
        return new Archetype(this, context.Archetype);
    }

    private ArchetypeGroup GetArchetypeGroup(ArchetypeMatcher matcher) {
        if (matcherToGroups.TryGetValue(matcher, out var archetypeGroup)) {
            return archetypeGroup;
        }

        archetypeGroup = new ArchetypeGroup(this, matcher);
        matcherToGroups[matcher] = archetypeGroup;
        foreach (var compType in matcher.AllTypes) {
            if (!componentToGroups.TryGetValue(compType, out var list)) {
                list = new List<ArchetypeGroup>();
                componentToGroups[compType] = list;
            }
            list.Add(archetypeGroup);
        }

        foreach (var pair in archetypes) {
            for (var i = 0; i < pair.Value.Count; i++) {
                var archetype = pair.Value[i];
                archetypeGroup.Match(archetype);
            }
        }

        return archetypeGroup;
    }

    internal void OnAddArchetype(ArchetypeDefinition archetype) {
        if (!archetypes.TryGetValue(archetype.Flag, out var archetypeList)) {
            archetypeList = new List<ArchetypeDefinition> {archetype};
            archetypes[archetype.Flag] = archetypeList;
        }
        else {
            foreach (var item in archetypeList) {
                if (item.ComponentsEquals(archetype)) {
                    break;
                }
            }
            archetypeList.Add(archetype);
        }

        for (var i = 0; i < archetype.Types.Length; i++) {
            var typeId = archetype.Types[i];
            if (componentToGroups.TryGetValue(typeId, out var list)) {
                foreach (var archetypeGroup in list) {
                    archetypeGroup.Match(archetype);
                }
            }
        }
    }
}