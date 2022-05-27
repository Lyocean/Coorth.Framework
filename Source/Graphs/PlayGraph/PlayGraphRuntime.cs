using System;
using System.Diagnostics;

namespace Coorth.Graphs; 

public class PlayGraphRuntime<TContext> {

    public readonly PassNodeRuntime<TContext>[] PassNodes;

    public readonly DataNodeRuntime<TContext>[] DataNodes;

    public PlayGraphRuntime(PassNodeRuntime<TContext>[] passNodes, DataNodeRuntime<TContext>[] dataNodes) {
        PassNodes = passNodes;
        DataNodes = dataNodes;
    }

    public virtual void Compile() {
        
    }

    [Conditional("DEBUG")]
    private void ValidateOutputs(int id, int[] indexes) {
        foreach (var index in indexes) {
            var dataNode = ValidateAndGetData(index);
            if (dataNode.WriterIndex != id) {
                throw new ArgumentOutOfRangeException();
            }
        }
    }

    private ref DataNodeRuntime<TContext> ValidateAndGetData(int index) {
        ref var dataNode = ref DataNodes[index];
        return ref dataNode;
    }

    public void Execute(TContext context) {
        for (int i = 0, count = PassNodes.Length; i < count; i++) {
            ref var passNode = ref PassNodes[i];
            if (passNode.RefCount > 0) {
                ExecuteNode(ref passNode, context);
            }
        }

    }
    
    protected virtual void ExecuteNode(ref PassNodeRuntime<TContext> node, TContext context) {
        foreach (var index in node.Initial) {
            ref var dataNode = ref DataNodes[index];
            dataNode.Definition.InitialBefore(context);
        }
        foreach (var index in node.Release) {
            ref var dataNode = ref DataNodes[index];
            dataNode.Definition.ReleaseBefore(context);
        }
            
        node.Definition.Execute(context);
            
        foreach (var index in node.Initial) {
            ref var dataNode = ref DataNodes[index];
            dataNode.Definition.InitialAfter(context);
        }
        
        foreach (var index in node.Release) {
            ref var dataNode = ref DataNodes[index];
            dataNode.Definition.ReleaseAfter(context);
        }
    }

    
}