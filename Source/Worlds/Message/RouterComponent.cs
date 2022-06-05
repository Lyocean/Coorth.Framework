using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Coorth.Framework;

namespace Coorth.Worlds; 

[Component, Guid("1DEEB46B-9189-466E-B8C4-2FAB0149EE2F")]
public class RouterComponent : Component {

    private readonly Dispatcher<(ActorComponent, ActorContext)> dispatcher = new();

    public ValueTask DispatchAsync(ActorComponent actor, ActorContext context, IMessage m) {
        return dispatcher.DispatchAsync((actor, context), m);
    }
    
    public Reaction<(ActorComponent, ActorContext), T> OnReceive<T>(Action<ActorComponent, ActorContext, T> action) where T: IMessage {
        return dispatcher.Subscribe<T>((tuple, message) => action(tuple.Item1, tuple.Item2, message));
    }
    
    public Reaction<(ActorComponent, ActorContext), T> OnReceive<T>(Action<ActorContext, T> action) where T: IMessage {
        return dispatcher.Subscribe<T>((tuple, message) => action(tuple.Item2, message));
    }
    
    public Reaction<(ActorComponent, ActorContext), T> OnReceive<T>(Func<ActorComponent, ActorContext, T, ValueTask> action) where T: IRequest {
        return dispatcher.Subscribe<T>((tuple, message) => action(tuple.Item1, tuple.Item2, message));
    }
    
    public Reaction<(ActorComponent, ActorContext), T> OnReceive<T>(Func<ActorContext, T, ValueTask> action) where T: IRequest {
        return dispatcher.Subscribe<T>((tuple, message) => action(tuple.Item2, message));
    }
}