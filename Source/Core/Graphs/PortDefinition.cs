using System.Collections.Generic;

namespace Coorth {
    public class PortDefinition {
        public int Index { get; internal set; }
        public NodeDefinition Node;
        public List<EdgeDefinition> Edges = new List<EdgeDefinition>();
    }
    
    public readonly struct PortData {
        public readonly short EdgeMin;
        public readonly short EdgeMax;

        public PortData(short edgeMin, short edgeMax) {
            EdgeMin = edgeMin;
            EdgeMax = edgeMax;
        }
    }
}