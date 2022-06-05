using System;
using System.Threading.Tasks;
using Coorth.Framework;

namespace Coorth.Worlds; 

public class ActorComponent : Component, IActor, IActorLifetime {

    private RouterComponent Router => Sandbox.Singleton<RouterComponent>();
    
    private ActorLocalNode? node;
    public ActorLocalNode Node => node ?? throw new NullReferenceException();
    
    public ActorId ActorId => Node.Id;

    void IActorLifetime.Setup(ActorLocalNode value) => node = value;

    void IActorLifetime.Clear() => node = null;

    public ValueTask ReceiveAsync(ActorContext context, IMessage m) {
        return Router.DispatchAsync(this, context, m);
    }
}