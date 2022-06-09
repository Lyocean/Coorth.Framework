using System;
using Coorth.Tasks.Ticking;

namespace Coorth.Framework;

[Event]
public readonly record struct EventEndOfFrame(ITickingContext TickingContext, TimeSpan TotalTime, TimeSpan DeltaTime, long FrameCount) {
        
    public readonly ITickingContext TickingContext = TickingContext;

    public readonly TimeSpan TotalTime = TotalTime;

    public readonly TimeSpan DeltaTime = DeltaTime;
        
    public readonly long FrameCount = FrameCount;

    public float DeltaSecond => (float)DeltaTime.TotalSeconds;
        
    public float TotalSecond => (float)TotalTime.TotalSeconds;
}