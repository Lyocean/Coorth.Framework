using System;
using System.Threading.Tasks;

namespace Coorth.Framework;

public class ActorDispatch : Disposable, IActor {
    
    private readonly Router<MessageContext> router = new ();
    
    public Reaction<MessageContext, T> OnReceive<T>(Action<MessageContext, T> action) where T: IMessage {
        return router.Subscribe(action);
    }
    
    public Reaction<MessageContext, T> OnReceive<T>(Func<MessageContext, T, ValueTask> action) where T: IRequest {
        return router.Subscribe(action);
    }
    
    public async ValueTask ReceiveAsync(MessageContext context, IMessage m) {
        await router.DispatchAsync(context, m);
        await OnReceive(in context, in m);
    }

    protected virtual ValueTask OnReceive(in MessageContext context, in IMessage m) {
        return new ValueTask();
    }
}