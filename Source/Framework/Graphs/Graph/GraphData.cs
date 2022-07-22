namespace Coorth.Framework;

public readonly record struct GraphData(NodeData[] Nodes, PortData[] Ports, EdgeData[] Edges) {
    public readonly NodeData[] Nodes = Nodes;
    public readonly PortData[] Ports = Ports;
    public readonly EdgeData[] Edges = Edges;
}
