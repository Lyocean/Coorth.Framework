namespace Coorth.Framework; 

public interface IActorProxy : IActor {

}
    
public abstract class ActorProxy : IActor {
        
    public readonly ActorRef Ref;
    
    public ActorProxy(ActorRef value) {
        Ref = value;
    }
}
