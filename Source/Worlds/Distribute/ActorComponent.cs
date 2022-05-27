using System.Threading.Tasks;
using Coorth.Framework;

namespace Coorth.Worlds; 

public abstract class ActorComponent : Component, IActor, IActorLifetime {

#nullable disable
    public ActorLocalNode Node { get; private set; }
#nullable enable
    
    public ActorId ActorId => Node.Id;

    void IActorLifetime.Setup(ActorLocalNode node) => Node = node;

    void IActorLifetime.Clear() {}
    
    public abstract ValueTask ReceiveAsync(ActorContext context, IMessage m);
}