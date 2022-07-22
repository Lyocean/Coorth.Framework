namespace Coorth.Framework;

public readonly record struct EdgeData(int SourceNode, int SourcePort, int TargetNode, int TargetPort) {
    public readonly int SourceNode = SourceNode;
    public readonly int SourcePort = SourcePort;
    public readonly int TargetNode = TargetNode;
    public readonly int TargetPort = TargetPort;
}