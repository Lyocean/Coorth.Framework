using System;
using System.Threading.Tasks;

namespace Coorth.Framework;

public class ActorDispatch : Disposable, IActor {
    
    private readonly Dispatcher<ActorContext> dispatcher = new ();
    
    public Reaction<ActorContext, T> OnReceive<T>(Action<ActorContext, T> action) where T: IMessage {
        return dispatcher.Subscribe(action);
    }
    
    public Reaction<ActorContext, T> OnReceive<T>(Func<ActorContext, T, ValueTask> action) where T: IRequest {
        return dispatcher.Subscribe(action);
    }
    
    public async ValueTask ReceiveAsync(ActorContext context, IMessage m) {
        await dispatcher.DispatchAsync(context, m);
        await OnReceive(in context, in m);
    }

    protected virtual ValueTask OnReceive(in ActorContext context, in IMessage m) {
        return new ValueTask();
    }
}