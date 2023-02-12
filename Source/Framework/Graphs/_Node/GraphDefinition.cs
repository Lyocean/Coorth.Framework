using System;
using System.Collections.Generic;

namespace Coorth.Framework; 

public class GraphDefinition {
    
    public static readonly GraphDefinition Empty = new();
    
    private readonly Dictionary<Guid, NodeDefinition> nodes = new();
    public IDictionary<Guid, NodeDefinition> Nodes => nodes;
    public int NodeCount => nodes.Count;
    
    private readonly HashSet<EdgeDefinition> edges = new();
    public ICollection<EdgeDefinition> Edges => edges;

    public int MaxNodePortCount => 16;

    public int MaxPortEdgeCount => 16;
    
    public NodeHandle<T> Create<T>(T node) where T : NodeDefinition {
        nodes.Add(node.Id, node);
        node.Graph = this;
        return new NodeHandle<T>(node.Id);
    }
    
    public NodeHandle<T> Create<T>() where T : NodeDefinition, new() {
        var node = new T();
        nodes.Add(node.Id, node);
        node.Graph = this;
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
        var edge = new EdgeDefinition ();
        sourcePort.AddEdge(edge);
        targetPort.AddEdge(edge);
        edges.Add(edge);
        return true;
    }
    
    protected virtual void Optimize() {
        
    }
    
    public GraphData Compile(GraphDefinition graphDefinition) {
        Optimize();
        return Generate(graphDefinition);
    } 
    
    protected virtual GraphData Generate(GraphDefinition graph) {
        var portCount = 0;

        //Generate Node Data
        var nodeList = new NodeData[graph.Nodes.Count];
        var nodeIndex = 0;
        foreach (var pair in graph.Nodes) {
            var nodeDefinition = pair.Value;
            nodeDefinition.Index = nodeIndex;
            nodeList[nodeIndex] = nodeDefinition.Compile(ref portCount);
            nodeIndex++;
        }

        //Generate Port Data
        var portList = new PortData[portCount];
        var portIndex = 0;
        var edgeCount = 0;
        for (var i = 0; i < nodeList.Length; i++) {
            var nodeDefinition = nodeList[i].Definition;
            nodeDefinition.Index = i;
            for (var j = 0; j < nodeDefinition.InPorts.Count; j++) {
                portList[portIndex + j] = nodeDefinition.InPorts[j].Compile(ref edgeCount);
            }
            portIndex += nodeDefinition.InPorts.Count;
            for (var j = 0; j < nodeDefinition.OutPorts.Count; j++) {
                portList[portIndex + j] =nodeDefinition.OutPorts[j].Compile(ref edgeCount);
            } 
            portIndex += nodeDefinition.OutPorts.Count;
        }
        //Generate Edge Data
        var edgeDataList = new EdgeData[edgeCount];
        var edgeIndex = 0;
        for (var i = 0; i < nodeList.Length; i++) {
            var nodeDefinition = nodeList[i].Definition;
            for (var j = 0; j < nodeDefinition.InPorts.Count; j++) {
                var portDefinition = nodeDefinition.InPorts[j];
                for (var k = 0; k < portDefinition.Edges.Count; k++) {
                    edgeDataList[edgeIndex] = portDefinition.Edges[k].Compile();
                    edgeIndex++;
                }
            }
            for (var j = 0; j < nodeDefinition.OutPorts.Count; j++) {
                var portDefinition = nodeDefinition.OutPorts[j];
                for (var k = 0; k < portDefinition.Edges.Count; k++) {
                    edgeDataList[edgeIndex] = portDefinition.Edges[k].Compile();
                    edgeIndex++;
                }
            } 
        }

        // edgeCount = 0;
        // foreach (var edgeDefinition in edges) {
        //     edgeDataList[edgeCount] = new EdgeData(
        //         sourceNode: (short) edgeDefinition.Source.Node.Index,
        //         sourcePort: (short) edgeDefinition.Source.Index,
        //         targetNode: (short) edgeDefinition.Target.Node.Index,
        //         targetPort: (short) edgeDefinition.Target.Index);
        //     edgeCount++;
        // }
        var data = new GraphData(nodeList, portList, edgeDataList);
        return data;
    }
}

public abstract class GraphDefinition<TGraph, TNode, TPort, TEdge, TContext> : GraphDefinition 
                                                                where TGraph : GraphDefinition<TGraph, TNode, TPort, TEdge, TContext>
                                                                where TNode  : NodeDefinition
                                                                where TPort  : PortDefinition 
                                                                where TEdge  : EdgeDefinition 
                                                                where TContext : IGraphContext 
{
    
    
    
}