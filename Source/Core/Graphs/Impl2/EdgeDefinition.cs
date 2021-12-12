namespace Coorth.Graphs.Experiment {
    public class EdgeDefinition {
        
        public PortDefinition Source;
        
        public PortDefinition Target;
        
    }
    
    public readonly struct EdgeData {
        
        public readonly short SourceNode;
        
        public readonly short SourcePort;
        
        public readonly short TargetNode;
        
        public readonly short TargetPort;

        public EdgeData(short sourceNode, short sourcePort, short targetNode, short targetPort) {
            SourceNode = sourceNode;
            SourcePort = sourcePort;
            TargetNode = targetNode;
            TargetPort = targetPort;
        }
    }
}