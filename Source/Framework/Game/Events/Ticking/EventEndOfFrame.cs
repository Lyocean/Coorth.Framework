using System;

namespace Coorth.Framework;

[Event]
public readonly record struct EventEndOfFrame(TimeSpan TotalTime, TimeSpan DeltaTime, long FrameCount) {
        
    public readonly TimeSpan TotalTime = TotalTime;

    public readonly TimeSpan DeltaTime = DeltaTime;
        
    public readonly long FrameCount = FrameCount;

    public float DeltaSecond => (float)DeltaTime.TotalSeconds;
        
    public float TotalSecond => (float)TotalTime.TotalSeconds;
}