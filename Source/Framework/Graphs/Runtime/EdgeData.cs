namespace Coorth.Framework; 

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
    
    public static EdgeData Compile(EdgeDefinition definition) {
        return new EdgeData(
            sourceNode: (short) definition.Source.Node.Index,
            sourcePort: (short) definition.Source.Index,
            targetNode: (short) definition.Target.Node.Index,
            targetPort: (short) definition.Target.Index);
    }
}