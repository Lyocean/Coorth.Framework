using System.Threading;

namespace Coorth.Framework;

public readonly record struct ActorMail(IMessage Message, ActorRef Sender, CancellationToken Cancellation) {
    
    public readonly IMessage Message = Message;
    
    public readonly ActorRef Sender = Sender;
    
    public readonly CancellationToken Cancellation = Cancellation;
    
}