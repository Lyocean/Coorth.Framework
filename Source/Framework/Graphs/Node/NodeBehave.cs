namespace Coorth.Framework;

public interface INodeBehave {
    void OnSetup(INodeBase node, INodeContext context);
    
    void OnReset(INodeBase node, INodeContext context);
}

public abstract class NodeBehave : INodeBehave {
    
    public virtual void OnSetup(INodeBase node, INodeContext context) { }
    
    public virtual void OnReset(INodeBase node, INodeContext context) { }
}

public interface INodeState : INodeBehave {
    void OnEnter(INodeBase node, INodeContext context);

    NodeStatus OnUpdate(INodeBase node, INodeContext context, in NodeFrame frame);

    void OnExit(INodeBase node, INodeContext context);
}

public abstract class NodeState : NodeBehave, INodeState {
    public abstract void OnEnter(INodeBase node, INodeContext context);
    
    public abstract NodeStatus OnUpdate(INodeBase node, INodeContext context, in NodeFrame frame);

    public abstract void OnExit(INodeBase node, INodeContext context);
}


public interface INodeAction : INodeBehave {
    bool Execute(INodeBase node, INodeContext context);
}

public abstract class NodeAction : NodeBehave, INodeAction {
    public abstract bool Execute(INodeBase node, INodeContext context);
}

public interface INodeCondition {
    bool Check(INodeBase node, INodeContext context);
}

