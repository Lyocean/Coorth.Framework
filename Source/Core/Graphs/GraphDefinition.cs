using System;
using System.Collections.Generic;

namespace Coorth {
    public partial class GraphDefinition {
        
        private readonly Dictionary<Guid, NodeDefinition> nodes = new Dictionary<Guid, NodeDefinition>();
        
        private readonly HashSet<EdgeDefinition> edges = new HashSet<EdgeDefinition>();

        public int MaxNodePortCount => 16;

        public int MaxPortEdgeCount => 16;
        
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

        public NodeDefinition GetDefinition(NodeHandle handle) {
            return !nodes.TryGetValue(handle.Id, out var node) ? null : node;
        }
        
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
            var edge = new EdgeDefinition {
                Source = sourcePort,
                Target = targetPort
            };
            sourcePort.AddEdge(edge);
            targetPort.AddEdge(edge);
            edges.Add(edge);
            return true;
        }


    }
    
    public readonly struct GraphData {
        public readonly NodeData[] Nodes;
        public readonly PortData[] Ports;
        public readonly EdgeData[] Edges;

        public GraphData(NodeData[] nodes, PortData[] ports, EdgeData[] edges) {
            this.Nodes = nodes;
            this.Ports = ports;
            this.Edges = edges;
        }
    }
    
    
    public interface IGraphContext {
    }
}