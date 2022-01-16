namespace Coorth {
    public partial class GraphDefinition {

        protected GraphData data;
        
        public GraphData Compile() {
            Optimize();
            Generate();
            return this.data;
        }

        protected virtual void Optimize() {
            
        }
        
        protected virtual void Generate() {
            //Generate Node Data
            var nodeDataList = new NodeData[nodes.Count];
            int nodeCount = 0;
            int portCount = 0;
            foreach (var pair in nodes) {
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
                    portDataList[portIndex + j] = nodeDefinition.InPorts[j].Compile(ref edgeCount);
                }
                portIndex += nodeDefinition.InPorts.Count;
                for (var j = 0; j < nodeDefinition.OutPorts.Count; j++) {
                    portDataList[portIndex + j] = nodeDefinition.OutPorts[j].Compile(ref edgeCount);
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
                        edgeDataList[edgeIndex] = portDefinition.Edges[k].Compile();
                        edgeIndex++;
                    }
                }
                for (var j = 0; j < nodeDefinition.OutPorts.Count; j++) {
                    var portDefinition = nodeDefinition.OutPorts[j];
                    for (var k = 0; k < portDefinition.Edges.Count; k++) {
                        edgeDataList[edgeIndex] = portDefinition.Edges[k].Compile();
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
            this.data = new GraphData(nodeDataList, portDataList, edgeDataList);
        } 
        
    }
}