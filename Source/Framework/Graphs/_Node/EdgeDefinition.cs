namespace Coorth.Framework;

public class EdgeDefinition {

    public PortDefinition Source { get; internal set; } = PortDefinition.Empty;

    public PortDefinition Target { get; internal set; } = PortDefinition.Empty;
    
    public EdgeData Compile() {
        return new EdgeData(
            SourceNode: Source.Node.Index, 
            SourcePort: Source.Index,
            TargetNode: Target.Node.Index,
            TargetPort: Target.Index);
    }
}

public class EdgeDefinition<T> : EdgeDefinition {

}

