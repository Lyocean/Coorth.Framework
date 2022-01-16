using System.Collections.Generic;

namespace Coorth {
    public class PortDefinition {
        public int Index { get; private set; }
        public NodeDefinition Node { get; private set; }

        private List<EdgeDefinition> edges = new List<EdgeDefinition>();
        public IReadOnlyList<EdgeDefinition> Edges => edges;

        internal void Setup(NodeDefinition node, int index) {
            this.Node = node;
            this.Index = index;
        }

        internal void AddEdge(EdgeDefinition edge) {
            edges.Add(edge);
        }
        
        internal void RemoveEdge(EdgeDefinition edge) {
            edges.Remove(edge);
        }

        public PortData Compile(ref int index) {
            var data = new PortData(
                edgeMin: (short)index, 
                edgeMax:(short)(index + Edges.Count - 1));
            index += Edges.Count;
            return data;
        }
    }
    
    public readonly struct PortData {
        public readonly int EdgeMin;
        public readonly int EdgeMax;

        public PortData(int edgeMin, int edgeMax) {
            EdgeMin = edgeMin;
            EdgeMax = edgeMax;
        }
    }
}