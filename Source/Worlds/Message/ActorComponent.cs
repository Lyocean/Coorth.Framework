using System;
using System.Threading.Tasks;

namespace Coorth.Framework; 

public class ActorComponent : Component, IActor, IActorLifetime {

    private RouterComponent Router => World.Singleton<RouterComponent>();
    
    private ActorLocalNode? node;
    public ActorLocalNode Node => node ?? throw new NullReferenceException();
    
    public ActorId ActorId => Node.Id;

    void IActorLifetime.Setup(ActorLocalNode value) => node = value;

    void IActorLifetime.Clear() => node = null;

    public ValueTask ReceiveAsync(MessageContext context, IMessage m) {
        return Router.DispatchAsync(this, context, m);
    }
}