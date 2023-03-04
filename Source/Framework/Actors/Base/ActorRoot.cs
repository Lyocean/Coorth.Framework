using System.Threading.Tasks;

namespace Coorth.Framework;

public sealed class ActorRoot : ActorDomain {
    
    public ActorRoot(ActorsRuntime runtime) : base(string.Empty, runtime, null, runtime) {
    }

    public override ValueTask ReceiveAsync(MessageContext context, IMessage m) {
        return Runtime.ReceiveAsync(context, m);
    }
}

public sealed class ActorDeath : ActorBase {
    
    public override ValueTask ReceiveAsync(MessageContext context, IMessage m) {
        Node.Runtime.OnDeath(context, m);
        return new ValueTask();
    }
}