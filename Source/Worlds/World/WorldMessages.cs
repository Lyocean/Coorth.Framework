using System;
using Coorth.Framework;

namespace Coorth.Worlds; 

public record SandboxCreateRequest(string Name) : IRequest {
    public readonly string Name = Name;
}

public record SandboxCreateResponse(ActorId Id) : IResponse {
    public readonly ActorId Id = Id;
}

public record SandboxDestroyRequest(ActorId Id) : IRequest {
    public readonly ActorId Id = Id;
}

public record SandboxDestroyResponse(bool Result) : IResponse {
    public readonly bool Result = Result;

}