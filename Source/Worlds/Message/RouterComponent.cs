using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Coorth.Framework;

namespace Coorth.Framework; 

[Component, Guid("1DEEB46B-9189-466E-B8C4-2FAB0149EE2F")]
public class RouterComponent : Component {

    private readonly Router<(ActorComponent, MessageContext)> router = new();

    public ValueTask DispatchAsync(ActorComponent actor, MessageContext context, IMessage m) {
        return router.DispatchAsync((actor, context), m);
    }
    
    public Reaction<(ActorComponent, MessageContext), T> OnReceive<T>(Action<ActorComponent, MessageContext, T> action) where T: IMessage {
        return router.Subscribe<T>((tuple, message) => action(tuple.Item1, tuple.Item2, message));
    }
    
    public Reaction<(ActorComponent, MessageContext), T> OnReceive<T>(Action<MessageContext, T> action) where T: IMessage {
        return router.Subscribe<T>((tuple, message) => action(tuple.Item2, message));
    }
    
    public Reaction<(ActorComponent, MessageContext), T> OnReceive<T>(Func<ActorComponent, MessageContext, T, ValueTask> action) where T: IRequest {
        return router.Subscribe<T>((tuple, message) => action(tuple.Item1, tuple.Item2, message));
    }
    
    public Reaction<(ActorComponent, MessageContext), T> OnReceive<T>(Func<MessageContext, T, ValueTask> action) where T: IRequest {
        return router.Subscribe<T>((tuple, message) => action(tuple.Item2, message));
    }
}