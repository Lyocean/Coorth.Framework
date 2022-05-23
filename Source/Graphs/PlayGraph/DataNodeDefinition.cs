using Coorth.Framework;

namespace Coorth.Graphs; 

public class DataNodeDefinition<TContext> : NodeDefinition {
    
    public DataNodeDefinition(int index, GraphDefinition graph) : base(index, graph) {
    }
    
    public virtual void InitialBefore(TContext context){}
    public virtual void ReleaseBefore(TContext context){}
    public virtual void InitialAfter(TContext context){}
    public virtual void ReleaseAfter(TContext context){}
}