using System;

namespace Coorth.Framework;

[Event]
public readonly record struct EventDrawUpdate(TimeSpan TotalTime, TimeSpan DeltaTime, long FrameCount) : IEvent {
    
    public readonly TimeSpan DeltaTime = DeltaTime;
    
    public readonly TimeSpan TotalTime = TotalTime;
    
    public readonly long FrameCount = FrameCount;
        
    public float DeltaSeconds => (float)DeltaTime.TotalSeconds;

    public float TotalSeconds => (float)TotalTime.TotalSeconds;
}