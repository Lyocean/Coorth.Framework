namespace Coorth.Framework; 

public partial class GraphDefinition2 {

    protected GraphData data;
        
    public GraphData Compile(GraphDefinition graphDefinition) {
        Optimize();
        Generate(graphDefinition);
        return this.data;
    }

    protected virtual void Optimize() {
            
    }
        
    protected virtual void Generate(GraphDefinition graphDefinition) {
        //Generate Node Data
        var nodeDataList = new NodeData[graphDefinition.Nodes.Count];
        int nodeCount = 0;
        int portCount = 0;
        foreach (var pair in graphDefinition.Nodes) {
            NodeDefinition nodeDefinition = pair.Value;
            nodeDefinition.Index = nodeCount;
            nodeDataList[nodeCount] = nodeDefinition.Compile(ref portCount);
            nodeCount++;
        }
        //Generate Port Data
        var portDataList = new PortData[portCount];
        var portIndex = 0;
        var edgeCount = 0;
        for (var i = 0; i < nodeDataList.Length; i++) {
            var nodeDefinition = nodeDataList[i].Definition;
            nodeDefinition.Index = i;
            for (var j = 0; j < nodeDefinition.InPorts.Count; j++) {
                portDataList[portIndex + j] = PortData.Compile(nodeDefinition.InPorts[j], ref edgeCount);
            }
            portIndex += nodeDefinition.InPorts.Count;
            for (var j = 0; j < nodeDefinition.OutPorts.Count; j++) {
                portDataList[portIndex + j] =PortData.Compile( nodeDefinition.OutPorts[j], ref edgeCount);
            } 
            portIndex += nodeDefinition.OutPorts.Count;
        }
        //Generate Edge Data
        var edgeDataList = new EdgeData[edgeCount];
        var edgeIndex = 0;
        for (var i = 0; i < nodeDataList.Length; i++) {
            var nodeDefinition = nodeDataList[i].Definition;
            for (var j = 0; j < nodeDefinition.InPorts.Count; j++) {
                var portDefinition = nodeDefinition.InPorts[j];
                for (var k = 0; k < portDefinition.Edges.Count; k++) {
                    edgeDataList[edgeIndex] = EdgeData.Compile(portDefinition.Edges[k]);
                    edgeIndex++;
                }
            }
            for (var j = 0; j < nodeDefinition.OutPorts.Count; j++) {
                var portDefinition = nodeDefinition.OutPorts[j];
                for (var k = 0; k < portDefinition.Edges.Count; k++) {
                    edgeDataList[edgeIndex] = EdgeData.Compile(portDefinition.Edges[k]);
                    edgeIndex++;
                }
            } 
        }

        // edgeCount = 0;
        // foreach (var edgeDefinition in edges) {
        //     edgeDataList[edgeCount] = new EdgeData(
        //         sourceNode: (short) edgeDefinition.Source.Node.Index,
        //         sourcePort: (short) edgeDefinition.Source.Index,
        //         targetNode: (short) edgeDefinition.Target.Node.Index,
        //         targetPort: (short) edgeDefinition.Target.Index);
        //     edgeCount++;
        // }
        data = new GraphData(nodeDataList, portDataList, edgeDataList);
    } 
        
}