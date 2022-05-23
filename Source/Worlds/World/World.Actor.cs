using System.Threading.Tasks;
using Coorth.Framework;
using Coorth.Logs;

namespace Coorth.Worlds; 

public partial class World : IWorldProxy {

    public ValueTask<SandboxCreateResponse> SandboxCreate(SandboxCreateRequest request) {
        return new ValueTask<SandboxCreateResponse>(OnReceive(request));
    }

    public ValueTask<SandboxDestroyResponse> SandboxDestroy(SandboxDestroyRequest request) {
        return new ValueTask<SandboxDestroyResponse>(OnReceive(request));
    }

    private SandboxCreateResponse OnReceive(SandboxCreateRequest request) {
        var sandbox = CreateSandbox(request.Name);
        var director = sandbox.Singleton<DirectorComponent>();
        return new SandboxCreateResponse(director.ActorId);
    }

    private SandboxDestroyResponse OnReceive(SandboxDestroyRequest request) {
        var sandbox = sandboxes.Find(sandbox => GetId(sandbox) == request.Id);
        if (sandbox != null) {
            sandbox.Dispose();
            return new SandboxDestroyResponse(true);
        }
        return new SandboxDestroyResponse(false);

        static ActorId GetId(Sandbox s) { return s.Singleton<DirectorComponent>().ActorId; }
    }
    
    public override ValueTask ReceiveAsync(ActorContext context, IMessage m) {
        switch (m) {
            case SandboxCreateRequest request: {
                return context.ReplyAsync(OnReceive(request));
            }
            case SandboxDestroyRequest request: {
                return context.ReplyAsync(OnReceive(request));
            }
            default:
                LogUtil.Error($"World receive unknown message:{m}, sender:{context.Sender}");
                break;
        }

        return new ValueTask();
    }


}