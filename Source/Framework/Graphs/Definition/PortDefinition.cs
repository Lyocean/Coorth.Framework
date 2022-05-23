using System.Collections.Generic;

namespace Coorth.Framework; 

public class PortDefinition {
    public PortDefinition(int index, NodeDefinition node) {
        Index = index;
        Node = node;
    }

    public int Index { get; private set; }
    public NodeDefinition Node { get; private set; }

    private List<EdgeDefinition> edges = new();
    public IReadOnlyList<EdgeDefinition> Edges => edges;

    internal void Setup(NodeDefinition node, int index) {
        Node = node;
        Index = index;
    }

    internal void AddEdge(EdgeDefinition edge) {
        edges.Add(edge);
    }
        
    internal void RemoveEdge(EdgeDefinition edge) {
        edges.Remove(edge);
    }
}