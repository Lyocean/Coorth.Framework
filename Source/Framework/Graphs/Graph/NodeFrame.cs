using System;

namespace Coorth.Framework;

public interface IFrameContext {

}

public readonly record struct NodeFrame(IFrameContext context, TimeSpan DeltaTime) {
    
    private readonly IFrameContext context = context;

    public readonly TimeSpan DeltaTime = DeltaTime;
}