using System;

namespace Coorth.Framework;

[Event]
public readonly record struct EventDrawBefore(TimeSpan DeltaTime, TimeSpan TotalTime, long FrameCount) : IEvent {
    
    public readonly TimeSpan DeltaTime = DeltaTime;
    
    public readonly TimeSpan TotalTime = TotalTime;
    
    public readonly long FrameCount = FrameCount;
        
    public float DeltaSeconds => (float)DeltaTime.TotalSeconds;

    public float TotalSeconds => (float)TotalTime.TotalSeconds;
}