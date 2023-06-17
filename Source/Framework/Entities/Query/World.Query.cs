using System;
using System.Collections.Generic;

namespace Coorth.Framework;

public partial class World {
    
    private readonly Dictionary<Matcher, Query> queries = new();
    private readonly Dictionary<ComponentType, List<Query>> type2Queries = new();

    private void SetupQueries() {
        
    }

    private void ClearQueries() {
        queries.Clear();
        type2Queries.Clear();
    }

    internal void OnArchetypeAdd(Archetype archetype) {
        if (archetypes.TryGetValue(archetype.Hash, out var current)) {
            while (current.Next != null) {
                current = current.Next;
            }
            current.Next = archetype;
        }
        else {
            archetypes.Add(archetype.Hash, archetype);
        }
        for (var i = 0; i < archetype.Types.Count; i++) {
            var type = archetype.Types[i];
            if (!type2Queries.TryGetValue(type, out var list)) {
                continue;
            }
            foreach (var query in list) {
                query.Match(archetype);
            }
        }
    }
    
    public Query Query(Matcher matcher) {
        if (queries.TryGetValue(matcher, out var query)) {
            return query;
        }
        query = new Query(this, matcher);
        queries[matcher] = query;
        foreach (var type in matcher.Types) {
            if (!type2Queries.TryGetValue(type, out var list)) {
                list = new List<Query>();
                type2Queries.Add(type, list);
            }
            list.Add(query);
        }
        foreach (var (_, archetype) in archetypes) {
            var current = archetype;
            do {
                query.Match(current);
                current = current.Next;
            } while (current != null);
        }
        return query;
    }
    

}
