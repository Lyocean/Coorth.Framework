using System.Threading.Tasks;

namespace Coorth.Framework;

public interface IActor {
    ValueTask ReceiveAsync(ActorContext context, IMessage m);
}

internal interface IActorLifetime {
    void Setup(ActorLocalNode node);
    void Clear();
}

public abstract class Actor : IActor, IActorLifetime {
    
#nullable disable
    protected ActorLocalNode Node { get; private set; }
#nullable enable

    public ActorId ActorId => Node.Id;
    
    void IActorLifetime.Setup(ActorLocalNode node) {
        Node = node;
    }

    void IActorLifetime.Clear() {
        Node = null;
    }
    
    public abstract ValueTask ReceiveAsync(ActorContext context, IMessage m);

}