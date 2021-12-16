namespace Coorth {
    public partial class GraphDefinition {
        
        public GraphData Compile() {
            Optimize();
            return Generate();
        }

        protected virtual void Optimize() {
            
        }

        private GraphData Generate() {
            //Generate Node Data
            var nodeDataList = new NodeData[nodes.Count];
            short nodeCount = 0;
            short portCount = 0;
            foreach (var pair in nodes) {
                var nodeDefinition = pair.Value;
                nodeDefinition.Index = nodeCount;
                ref var nodeData = ref nodeDataList[nodeCount];
                short outputMin = (short) (portCount + nodeDefinition.InPorts.Count);
                nodeData = new NodeData(
                    definition: nodeDefinition,
                    inputMin: portCount,
                    inputMax: (short) (outputMin - 1),
                    outputMin: outputMin,
                    outputMax: (short) (outputMin + nodeDefinition.OutPorts.Count - 1));
                portCount += (short) (nodeDefinition.InPorts.Count + nodeDefinition.OutPorts.Count);
                nodeCount++;
            }
            //Generate Port Data
            var portDataList = new PortData[portCount];
            var portIndex = 0;
            var edgeIndex = 0;
            for (var i = 0; i < nodeDataList.Length; i++) {
                var nodeDefinition = nodeDataList[i].Definition;
                nodeDefinition.Index = i;
                for (var j = 0; j < nodeDefinition.InPorts.Count; j++) {
                    var portDefinition = nodeDefinition.InPorts[j];
                    portDataList[portIndex + j] = new PortData(
                        edgeMin: (short)edgeIndex, 
                        edgeMax:(short)(edgeIndex + portDefinition.Edges.Count-1));
                    edgeIndex += portDefinition.Edges.Count;
                }
                portIndex += nodeDefinition.InPorts.Count;
                for (var j = 0; j < nodeDefinition.OutPorts.Count; j++) {
                    var portDefinition = nodeDefinition.InPorts[j];
                    portDataList[portIndex + j] = new PortData(
                        edgeMin: (short)edgeIndex, 
                        edgeMax:(short)(edgeIndex + portDefinition.Edges.Count-1));
                    edgeIndex += portDefinition.Edges.Count;
                } 
                portIndex += nodeDefinition.OutPorts.Count;
            }
            //Generate Edge Data
            edgeIndex = 0;
            var edgeDataList = new EdgeData[edges.Count];
            foreach (var edgeDefinition in edges) {
                edgeDataList[edgeIndex] = new EdgeData(
                    sourceNode: (short) edgeDefinition.Source.Node.Index,
                    sourcePort: (short) edgeDefinition.Source.Index,
                    targetNode: (short) edgeDefinition.Target.Node.Index,
                    targetPort: (short) edgeDefinition.Target.Index);
            }
            return new GraphData(nodeDataList, portDataList, edgeDataList);
        } 
        
    }
}