using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Coorth.Framework;

namespace Coorth.Worlds; 

[Component, Guid("1DEEB46B-9189-466E-B8C4-2FAB0149EE2F")]
public class RouterComponent : ActorComponent {

    private readonly Dispatcher<ActorContext> dispatcher = new();

    public void Dispatch(ActorComponent actor, ActorContext context, IMessage m) {
        
    }
    
    public override ValueTask ReceiveAsync(ActorContext context, IMessage m) {
        return dispatcher.DispatchAsync(context, m);
    }
    
    public Reaction<ActorContext, T> OnReceive<T>(Action<ActorContext, T> action) where T: IMessage {
        return dispatcher.Subscribe(action);
    }
    
    public Reaction<ActorContext, T> OnReceive<T>(Func<ActorContext, T, ValueTask> action) where T: IRequest {
        return dispatcher.Subscribe(action);
    }
    
    // public async ValueTask ReceiveAsync(ActorContext context, IMessage m) {
    //     await dispatcher.DispatchAsync(context, m);
    //     await OnReceive(in context, in m);
    // }
    //
    // protected virtual ValueTask OnReceive(in ActorContext context, in IMessage m) {
    //     return new ValueTask();
    // }
}