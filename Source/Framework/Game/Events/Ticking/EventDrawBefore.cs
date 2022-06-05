using System;

namespace Coorth.Framework;

[Event]
public readonly record struct EventDrawBefore(TimeSpan TotalTime, TimeSpan DeltaTime, long FrameCount) : IEvent {
    
    public readonly TimeSpan TotalTime = TotalTime;

    public readonly TimeSpan DeltaTime = DeltaTime;
    
    public readonly long FrameCount = FrameCount;
        
    public float DeltaSeconds => (float)DeltaTime.TotalSeconds;

    public float TotalSeconds => (float)TotalTime.TotalSeconds;
}