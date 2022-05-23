using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth.Framework;

public readonly record struct ActorRef(ActorNode Node, ActorId Id) {
    
    public readonly ActorNode Node = Node;
    
    public readonly ActorId Id = Id;

    public static ActorRef Null => default;

    public bool IsNull => Node == null;

    public ActorsRuntime Runtime => Node.Runtime;

    public TActor GetActor<TActor>() => Node.GetActor<TActor>();
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Send(IMessage message) => Node.Send(message);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Send(IMessage message, ActorRef sender) => Node.Send(message, sender);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask<IResponse> Request(IRequest message) => Node.Request(message);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask<IResponse> Request(IRequest message, ActorRef sender) => Node.Request(message, sender);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask<IResponse> Request(IRequest message, CancellationToken cancellation) => Node.Request(message, Null, cancellation);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask<IResponse> Request(IRequest message, ActorRef sender, CancellationToken cancellation) => Node.Request(message, sender, cancellation);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask<IResponse> Request(IRequest message, TimeSpan span) => Node.Request(message, Null, span);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask<IResponse> Request(IRequest message, ActorRef sender, TimeSpan span) => Node.Request(message, sender, span);
    
}