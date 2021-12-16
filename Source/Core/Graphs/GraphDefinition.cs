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
            return new NodeHandle<T>();
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
                    edge.Source.Edges.Remove(edge);
                    edges.Remove(edge);
                }
            }
            for (var i = 0; i < node.OutPorts.Count; i++) {
                var port = node.OutPorts[i];
                foreach (var edge in port.Edges) {
                    edge.Target.Edges.Remove(edge);
                    edges.Remove(edge);
                }
            }
            return true;
        }
        
        public bool Connect(NodeHandle sourceHandle, int outputId, NodeHandle targetHandle, int inputId) {
            if (!nodes.TryGetValue(sourceHandle.Id, out var sourceNode)) {
                return false;
            }
            if (outputId < 0 || sourceNode.OutPorts.Count <= outputId) {
                return false;
            }
            if (!nodes.TryGetValue(targetHandle.Id, out var targetNode)) {
                return false;
            }
            if (inputId < 0 || targetNode.InPorts.Count <= inputId) {
                return false;
            }
            var sourcePort = sourceNode.InPorts[outputId];
            var targetPort = targetNode.InPorts[inputId];
            var edge = new EdgeDefinition {
                Source = sourcePort,
                Target = targetPort
            };
            sourcePort.Edges.Add(edge);
            targetPort.Edges.Add(edge);
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
}