using System;
using System.Threading.Tasks;

namespace Coorth.Framework;

public sealed class ActorLocalDomain : ActorDomain {

    #region Node

    public ActorLocalDomain(string? name, ActorsRuntime runtime, ActorNode? parent) : base(name, runtime, parent) {
    }
    
    public override ValueTask ReceiveAsync(MessageContext context, IMessage m) {

        return new ValueTask();
    }

    private ActorLocalNode CreateChild(ActorOptions options, IActor actor) {
        var node = new ActorLocalNode(options, actor, this, this);
        (actor as IActorLifetime)?.Setup(node);
        return node;
    }

    private bool DestroyChild(ActorId id) {
        if (!Children.TryGetValue(id, out var node)) {
            return false;
        }
        (node.Actor as IActorLifetime)?.Clear();
        node.Dispose();
        return true;
    }

    #endregion

    #region Actor

    public ActorRef CreateActor(Type key, IActor actor, ActorOptions options) {
        var node = CreateChild(options, actor);
        Runtime.Dispatcher.Dispatch(new EventActorCreate(node.Ref, key, actor));
        return node.Ref;
    }
    
    public ActorRef CreateActor<TActor>(TActor actor, ActorOptions options) where TActor : class, IActor {
        return CreateActor(typeof(TActor), actor, options);
    }

    public ActorRef CreateActor<TActor>(TActor actor, string? name = null) where TActor : class, IActor {
        var options = new ActorOptions(name, int.MaxValue, 0);
        return CreateActor(typeof(TActor), actor, options);
    }

    public ActorRef CreateActor<TActor>(string? name = null) where TActor : class, IActor, new() {
        var actor = Runtime.Services.Create<TActor>();
        var options = new ActorOptions(name, int.MaxValue, 0);
        return CreateActor(typeof(TActor), actor, options);
    }

    public bool RemoveActor(ActorId id) {
        return DestroyChild(id);
    }
    
    public bool RemoveActor(ActorRef actorRef) {
        return DestroyChild(actorRef.Id);
    }
    
    #endregion
}