namespace Coorth {
    public class EdgeDefinition {
        
        public PortDefinition Source;
        
        public PortDefinition Target;

        public EdgeData Compile() {
            return new EdgeData(
                sourceNode: (short) Source.Node.Index,
                sourcePort: (short) Source.Index,
                targetNode: (short) Target.Node.Index,
                targetPort: (short) Target.Index);
        }
    }
    
    public readonly struct EdgeData {
        
        public readonly int SourceNode;
        
        public readonly int SourcePort;
        
        public readonly int TargetNode;
        
        public readonly int TargetPort;

        public EdgeData(int sourceNode, int sourcePort, int targetNode, int targetPort) {
            SourceNode = sourceNode;
            SourcePort = sourcePort;
            TargetNode = targetNode;
            TargetPort = targetPort;
        }
    }
}