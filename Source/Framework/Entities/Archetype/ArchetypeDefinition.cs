using System;
using System.Collections.Generic;
using System.Threading;

namespace Coorth.Framework;

public class ArchetypeDefinition {

    public readonly int Id;

    public readonly ComponentType[] Types;

    public readonly Dictionary<ComponentType, ArchetypeDefinition> Edges;

    public readonly Dictionary<ComponentType, int> Index = new();

    public readonly ComponentMask Mask;

    public readonly int Hash;

    public readonly int Size;

    private static int currentId;

    public int ComponentCount => Types.Length;

    private static readonly Dictionary<int, ArchetypeDefinition> definitions = new();
    public IReadOnlyDictionary<int, ArchetypeDefinition> Definitions => definitions;

    public static readonly ArchetypeDefinition Empty = new(Array.Empty<ComponentType>());

    private ArchetypeDefinition(ComponentType[] types) {
        Id = Interlocked.Increment(ref currentId);
        Types = types;
        Edges = new Dictionary<ComponentType, ArchetypeDefinition>();
        Mask = ComponentRegistry.ComputeMask(types.AsSpan());
        Hash = ComponentRegistry.ComputeHash(types.AsSpan());
        Size = sizeof(int) + ComponentRegistry.ComputeSize(types.AsSpan());
        for (var i = 0; i < types.Length; i++) {
            Index.Add(types[i], i + 1);
        }
        definitions.Add(Hash, this);
    }
    
    internal ArchetypeDefinition Next(in ComponentType type) {
        if (Edges.TryGetValue(type, out var definition)) {
            return definition;
        }
        definition = new ArchetypeDefinition(ComponentRegistry.Combine(Types, type));
        Edges[type] = definition;
        definition.Edges[type] = this;
        return definition;
    }
}