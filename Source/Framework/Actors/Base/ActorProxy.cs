using System.Threading.Tasks;

namespace Coorth.Framework; 

public interface IActorProxy : IActor {

}
    
public abstract class ActorProxy : IActor {
        
    public readonly ActorRef Ref;
    
    public ActorProxy(ActorRef value) {
        Ref = value;
    }
    
    public ValueTask ReceiveAsync(ActorContext context, IMessage m) {
        Ref.Send(m);
        return new ValueTask();
    }
}
