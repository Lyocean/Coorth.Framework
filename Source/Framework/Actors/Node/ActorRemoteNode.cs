using System.Threading.Tasks;

namespace Coorth.Framework;

public sealed class ActorRemoteNode : ActorNode {

    private readonly IActorProxy? proxy;
    public override IActor Actor => proxy ?? (IActor)domain;

    private readonly ActorRemoteDomain domain;
    public override ActorDomain Domain => domain;
    
    public override ActorsRuntime Runtime { get; }

    public ActorRemoteNode(string name, IActorProxy? actorProxy, ActorRemoteDomain actorDomain, ActorsRuntime runtime, ActorNode? parent) : base(ActorId.New(), name, parent) {
        domain = actorDomain;
        proxy = actorProxy;
        Runtime = runtime;
    }

    protected override ValueTask Receive(in ActorContext context, in IMessage message) {
        return domain.ReceiveAsync(context, message);
    }
}