
namespace Coorth.Framework; 

public interface IActorProxy : IActor {
    void Setup(ActorNode node, ActorId id);
    void Clear();
}

public abstract class ActorProxy : IActorProxy {
    
    protected ActorRef Ref = ActorRef.Null;

    protected ActorNode Node => Ref.Node;

    public void Setup(ActorNode node, ActorId id) {
        Ref = new ActorRef(node, id);
    }

    public void Clear() {
        Ref = ActorRef.Null;
    }
}
