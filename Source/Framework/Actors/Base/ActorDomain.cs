using System.Threading.Tasks;

namespace Coorth.Framework;

public abstract class ActorDomain : ActorNode, IActor {
    
    public override ActorsRuntime Runtime { get; }

    public override ActorDomain Domain => this;
    
    protected ActorDomain(string? name, ActorsRuntime runtime, ActorNode? parent, IActor actor) : base(ActorId.New(), name, parent, actor, actor as IActorProcessor) {
        Runtime = runtime;
    }

    protected sealed override ValueTask Receive(in MessageContext context, in IMessage message) {
        return ReceiveAsync(context, message);
    }

    public abstract ValueTask ReceiveAsync(MessageContext context, IMessage m);
}