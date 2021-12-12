using System;
using System.Collections.Generic;
using System.Linq;

namespace Coorth.Graphs.Experiment {
    public class NodeDefinition {
        public Guid Id { get; } = Guid.NewGuid();

        public int Index { get; internal set; }
        
        public List<PortDefinition> InPorts = new List<PortDefinition>();

        public List<PortDefinition> OutPorts = new List<PortDefinition>();

        public int InDegree => InPorts.Sum(port => port.Edges.Count);

        public int OutDegree => OutPorts.Sum(port => port.Edges.Count);
    }
    
    public readonly struct NodeData {
        public readonly NodeDefinition Definition;
        public readonly short InputMin;
        public readonly short InputMax;
        public readonly short OutputMin;
        public readonly short OutputMax;

        public NodeData(NodeDefinition definition, short inputMin, short inputMax, short outputMin, short outputMax) {
            this.Definition = definition;
            this.InputMin = inputMin;
            this.InputMax = inputMax;
            this.OutputMin = outputMin;
            this.OutputMax = outputMax;
        }
    }
}