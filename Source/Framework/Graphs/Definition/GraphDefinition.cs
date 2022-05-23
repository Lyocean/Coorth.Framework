using System;
using System.Collections.Generic;

namespace Coorth.Framework; 

public class GraphDefinition {
        
    private readonly Dictionary<Guid, NodeDefinition> nodes = new();
    public IDictionary<Guid, NodeDefinition> Nodes => nodes;

    private readonly HashSet<EdgeDefinition> edges = new();
    public ICollection<EdgeDefinition> Edges => edges;

    public int MaxNodePortCount => 16;

    public int MaxPortEdgeCount => 16;
        
    public NodeHandle<T> Create<T>(T node) where T : NodeDefinition {
        nodes.Add(node.Id, node);
        node.Setup(this);
        return new NodeHandle<T>(node.Id);
    }
        
    public NodeHandle<T> Create<T>() where T : NodeDefinition, new() {
        var node = new T();
        nodes.Add(node.Id, node);
        node.Setup(this);
        return new NodeHandle<T>(node.Id);
    }

    public bool Exists(NodeHandle handle) {
        return nodes.ContainsKey(handle.Id);
    }
        
    public bool Remove(NodeHandle handle) {
        if (!nodes.TryGetValue(handle.Id, out var node)) {
            return false;
        }
        nodes.Remove(handle.Id);
        for (var i = 0; i < node.InPorts.Count; i++) {
            var port = node.InPorts[i];
            foreach (var edge in port.Edges) {
                edge.Source.RemoveEdge(edge);
                edges.Remove(edge);
            }
        }
        for (var i = 0; i < node.OutPorts.Count; i++) {
            var port = node.OutPorts[i];
            foreach (var edge in port.Edges) {
                edge.Target.RemoveEdge(edge);
                edges.Remove(edge);
            }
        }
        return true;
    }

    public NodeDefinition GetDefinition(NodeHandle handle) => nodes[handle.Id];
    
    public NodeDefinition? FindDefinition(NodeHandle handle) => nodes.TryGetValue(handle.Id, out var node) ? node : null;
        
    public bool Connect(NodeHandle sourceHandle, int outputId, NodeHandle targetHandle, int inputId) {
        if (!nodes.TryGetValue(sourceHandle.Id, out var sourceNode)) {
            throw new KeyNotFoundException(sourceHandle.ToString());
        }
        if (outputId < 0 || sourceNode.OutPorts.Count <= outputId) {
            throw new IndexOutOfRangeException(outputId.ToString());
        }
        if (!nodes.TryGetValue(targetHandle.Id, out var targetNode)) {
            throw new KeyNotFoundException(targetHandle.ToString());
        }
        if (inputId < 0 || targetNode.InPorts.Count <= inputId) {
            throw new IndexOutOfRangeException(inputId.ToString());
        }
        var sourcePort = sourceNode.OutPorts[outputId];
        var targetPort = targetNode.InPorts[inputId];
        if (sourcePort == null || targetPort == null) {
            throw new NullReferenceException();
        }
        var edge = new EdgeDefinition (
            sourcePort,
            targetPort
        );
        sourcePort.AddEdge(edge);
        targetPort.AddEdge(edge);
        edges.Add(edge);
        return true;
    }
}