using System.Threading.Tasks;
using Coorth.Framework;

namespace Coorth.Worlds;

//TODO:Remote control world
public interface IWorldProxy {
    ValueTask<SandboxCreateResponse> SandboxCreate(SandboxCreateRequest request);
    ValueTask<SandboxDestroyResponse> SandboxDestroy(SandboxDestroyRequest request);
}

public class WorldProxy : ActorProxy, IWorldProxy {
    
    public WorldProxy(ActorRef value) : base(value) {
    }
    
    public async ValueTask<SandboxCreateResponse> SandboxCreate(SandboxCreateRequest request) {
        var response = (SandboxCreateResponse)await Ref.Request(request);
        return response;
    }

    public async ValueTask<SandboxDestroyResponse> SandboxDestroy(SandboxDestroyRequest request) {
        var response = (SandboxDestroyResponse)await Ref.Request(request);
        return response;
    }

}