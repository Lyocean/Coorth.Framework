using Coorth.Framework;

namespace Coorth.Graphs; 

public class PassNodeDefinition<TContext> : NodeDefinition {
    public PassNodeDefinition(int index, GraphDefinition graph) : base(index, graph) {
    }
    
    public virtual void Execute(TContext context){}
}