namespace Coorth.Framework; 

public interface ICondition {
    bool Check(object host, IGraphContext context);
}

public interface ICondition<TContext> : ICondition where TContext : INodeContext {
    bool Check(object host, in TContext context);
}



//
// public class NodeState : NodeBehave {
//     
// }

public interface IBehaviour {
    bool Enter(object context);
    bool Execute();
    void Exit();
}

public interface IBehaviour<TContext> {
    bool Enter(ref TContext context);
    bool Execute(ref TContext context);
    void Exit(ref TContext context);
}