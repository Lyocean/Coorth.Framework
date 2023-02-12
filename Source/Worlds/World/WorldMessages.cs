namespace Coorth.Framework; 

public record WorldCreateRequest(string Name) : IRequest {
    public readonly string Name = Name;
}

public record WorldCreateResponse(ActorId Id) : IResponse {
    public readonly ActorId Id = Id;
}

public record WorldDestroyRequest(ActorId Id) : IRequest {
    public readonly ActorId Id = Id;
}

public record WorldDestroyResponse(bool Result) : IResponse {
    public readonly bool Result = Result;
}
