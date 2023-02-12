namespace Coorth.Framework; 

public interface INodeBase {
    
    void Setup(INodeContext context);
    
    void Reset(INodeContext context);
    
    void Enter(INodeContext context);
    
    NodeStatus Update(INodeContext context, in NodeFrame frame);
    
    void Exit(INodeContext context);
}

public abstract class NodeBase : INodeBase, INodeState {
    
    public abstract void Setup(INodeContext context);
    
    public abstract void Reset(INodeContext context);
    
    public abstract void Enter(INodeContext context);
    
    public abstract NodeStatus Update(INodeContext context, in NodeFrame frame);
    
    public abstract void Exit(INodeContext context);

    void INodeBehave.OnReset(INodeBase node, INodeContext context) => Reset(context);

    void INodeBehave.OnSetup(INodeBase node, INodeContext context) => Setup(context);

    void INodeState.OnEnter(INodeBase node, INodeContext context) => Enter(context);

    NodeStatus INodeState.OnUpdate(INodeBase node, INodeContext context, in NodeFrame frame) => Update(context, in frame);

    void INodeState.OnExit(INodeBase node, INodeContext context) => Exit(context);
}