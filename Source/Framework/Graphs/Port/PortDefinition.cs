using System;
using System.Collections.Generic;

namespace Coorth.Framework; 

public class PortDefinition {

    public static readonly PortDefinition Empty = new();

    public int Index { get; private set; } = -1;

    public Type Type { get; private set; } = typeof(void);

    public NodeDefinition Node { get; private set; } = NodeDefinition.Empty;
    
    private List<EdgeDefinition>? edges;
    public IReadOnlyList<EdgeDefinition> Edges => edges ?? (IReadOnlyList<EdgeDefinition>)Array.Empty<EdgeDefinition>();

    internal void Setup(NodeDefinition node, int index) {
        Node = node;
        Index = index;
    }

    internal void AddEdge(EdgeDefinition edge) {
        edges ??= new List<EdgeDefinition>();
        edges.Add(edge);
    }
        
    internal void RemoveEdge(EdgeDefinition edge) {
        edges?.Remove(edge);
    }
    
    public PortData Compile(ref int index) {
        var data = new PortData(Definition: this, EdgeMin: (short)index, EdgeMax:(short)(index + Edges.Count - 1));
        index += Edges.Count;
        return data;
    }
}

