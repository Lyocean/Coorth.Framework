using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth.Framework;

public readonly struct MessageContext : IDisposable {
    
    public readonly ActorNode Node;
    
    public readonly ActorRef Sender;
    
    public readonly CancellationToken Cancellation;
    
    public readonly TaskCompletionSource<IResponse>? Completion;

    public ActorRef Self => Node.Ref;

    public MessageContext(ActorNode node, ActorRef sender, CancellationToken cancellation) {
        Node = node;
        Sender = sender;
        Cancellation = cancellation;
        Completion = null;
    }
    
    public MessageContext(ActorNode node, ActorRef sender, TaskCompletionSource<IResponse> completion) {
        Node = node;
        Sender = sender;
        Cancellation = CancellationToken.None;
        Completion = completion;
    }
    
    public void Dispose() {
        Completion?.TrySetCanceled();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Send(IMessage message, ActorRef target) {
        target.Node.Send(message, target);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask<IResponse> Request(IRequest request, ActorRef target) {
        return target.Node.Request(request, target, CancellationToken.None);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reply(IResponse response) {
        if (Completion != null) {
            if (Completion.TrySetResult(response)) {
                return;
            }
        }
        Send(response, Sender);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask ReplyAsync(IResponse response) {
        Reply(response);
        return new ValueTask();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Forward(IMessage message, ActorRef target) {
        target.Node.Send(message, Sender);
    }
}
