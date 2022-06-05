using System.Threading.Tasks;

namespace Coorth.Framework;

public sealed class ActorRoot : ActorDomain {
    
    public ActorRoot(ActorsRuntime runtime) : base(string.Empty, runtime, null) {
    }

    public override ValueTask ReceiveAsync(ActorContext context, IMessage m) {
        return Runtime.ReceiveAsync(context, m);
    }
}