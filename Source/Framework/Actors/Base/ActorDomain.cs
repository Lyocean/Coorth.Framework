using System.Threading.Tasks;

namespace Coorth.Framework;

public abstract class ActorDomain : ActorNode, IActor {
    
    public override IActor Actor => this;

    public override ActorsRuntime Runtime { get; }

    public override ActorDomain Domain => this;
    
    protected ActorDomain(string? name, ActorsRuntime runtime, ActorNode? parent) : base(ActorId.New(), name, parent) {
        Runtime = runtime;
    }

    protected sealed override ValueTask Receive(in ActorContext context, in IMessage message) {
        return ReceiveAsync(context, message);
    }

    public abstract ValueTask ReceiveAsync(ActorContext context, IMessage m);
}