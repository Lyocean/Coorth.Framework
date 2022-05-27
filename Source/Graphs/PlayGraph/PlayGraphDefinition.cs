using System.Collections.Generic;
using Coorth.Framework;

namespace Coorth.Graphs;

//TODO: PlayGraph
public class PlayGraphDefinition<TContext> : GraphDefinition {

    public HashSet<DataNodeDefinition<TContext>> DataNodes = new();
    public HashSet<PassNodeDefinition<TContext>> PassNodes = new();

    public void Compile(ref PlayGraphRuntime<TContext> graphRuntime) {
        
        
        //Calculate pass and data ref count
        // for (int i = 0, count = PassNodes.Count; i < count; i++) {
        //     ref var passNode = ref PassNodes.Values[i];
        //     passNode.RefCount = passNode.Outputs.Count;
        //     foreach (var index in passNode.Inputs.Values) {
        //         ref var dataNode = ref ValidateAndGetData(index);
        //         dataNode.ReaderCount++;
        //     }
        //
        //     ValidateOutputs(i, passNode.Outputs.Values);
        // }

    }
}