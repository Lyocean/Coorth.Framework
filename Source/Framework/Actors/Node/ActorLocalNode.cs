using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Coorth.Framework;

public static class ActorStatus {
    public const int IDLE_STATE = 0;
    public const int BUSY_STATE = 1;
}

public sealed class ActorLocalNode : ActorNode {
    
    private readonly ActorLocalDomain domain;
    public override ActorDomain Domain => domain;
    
    public override ActorsRuntime Runtime { get; }

    private volatile int status = ActorStatus.IDLE_STATE;

    public bool IsActive => status > 0;

    public readonly Channel<ActorMail> Mailbox;

    public readonly int Throughput;
    
    public ActorLocalNode(ActorId id, ActorOptions options, IActor actor, ActorLocalDomain localDomain, ActorNode? parent) : base(id, options.Name, parent, actor, actor as IActorProcessor) {
        domain = localDomain;
        Runtime = localDomain.Runtime;
        Throughput = options.Throughput;
        Mailbox = options.MailboxSize <= 0 
            ? Channel.CreateUnbounded<ActorMail>() 
            : Channel.CreateBounded<ActorMail>(new BoundedChannelOptions(options.MailboxSize), mail => Runtime.OnMailOverflow(this, mail));
    }
    
    protected override ValueTask Receive(in MessageContext context, in IMessage message) {
        if (Interlocked.CompareExchange(ref status, ActorStatus.BUSY_STATE, ActorStatus.IDLE_STATE) == ActorStatus.IDLE_STATE) {
            Schedule(context, message);
            return new ValueTask();
        }
        var mail = new ActorMail(message, context.Sender, context.Cancellation);
        return Mailbox.Writer.WriteAsync(mail);
    }
    
    private async void Schedule(MessageContext context, IMessage message) {
        if (Processor != null) {
            await Processor.ReceiveAsync(context, message);
        }
        if (Mailbox.Reader.Count == 0) {
            Interlocked.CompareExchange(ref status, ActorStatus.IDLE_STATE, ActorStatus.BUSY_STATE);
            return;
        }
        for (var i = 0; i < Throughput; i++) {
            if (!Mailbox.Reader.TryRead(out var mail)) {
                Interlocked.CompareExchange(ref status, ActorStatus.IDLE_STATE, ActorStatus.BUSY_STATE);
                return;
            }
            using var ctx = new MessageContext(this, mail.Sender, CancellationToken.None);
            if (Processor != null) {
                await Processor.ReceiveAsync(ctx, message);
            }
        }
        Runtime.ThroughputOverflow(this);
    }
    
    public ActorLocalNode CreateChild(ActorId id, ActorOptions options, IActor actor) {
        var node = new ActorLocalNode(id, options, actor, domain, this);
        (actor as IActorLifetime)?.Setup(node);
        return node;
    }
}

