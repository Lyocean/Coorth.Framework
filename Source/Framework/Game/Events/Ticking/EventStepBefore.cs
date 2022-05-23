using System;

namespace Coorth.Framework;

[Event]
public readonly record struct EventStepBefore(TimeSpan TotalTime, TimeSpan DeltaTime, long FrameCount) : ITickEvent {
        
    public readonly TimeSpan TotalTime = TotalTime;

    public readonly TimeSpan DeltaTime = DeltaTime;
        
    public readonly long FrameCount = FrameCount;
        
    public DateTime CurrentTime => DateTime.MinValue + TotalTime;
        
    public float DeltaSecond => (float)DeltaTime.TotalSeconds;
        
    public float TotalSecond => (float)TotalTime.TotalSeconds;

    public TimeSpan GetDeltaTime() => DeltaTime;
    
    public TimeSpan GetTotalTime() => TotalTime;
}