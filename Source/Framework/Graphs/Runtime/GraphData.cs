namespace Coorth.Framework; 

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