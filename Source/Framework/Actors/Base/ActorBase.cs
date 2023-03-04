using System;
using System.Threading.Tasks;

namespace Coorth.Framework;

public interface IActor {
}

public interface IActorLifetime {
    void Setup(ActorLocalNode node);
    void Clear();
}

public interface IActorProcessor {
    ValueTask ReceiveAsync(MessageContext context, IMessage m);
}

public abstract class ActorBase : IActor, IActorLifetime, IActorProcessor {
    private ActorLocalNode? node;
    protected ActorLocalNode Node => node ?? throw new NullReferenceException();

    public ActorId ActorId => node?.Id ?? ActorId.Null;

    void IActorLifetime.Setup(ActorLocalNode value) {
        node = value;
    }

    void IActorLifetime.Clear() {
        node = null;
    }

    public abstract ValueTask ReceiveAsync(MessageContext context, IMessage m);
}