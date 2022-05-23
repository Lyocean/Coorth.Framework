using System;

namespace Coorth.Framework;

[Event]
public readonly record struct EventDrawUpdate(TimeSpan Delta, TimeSpan Total, long FrameCount) : IEvent {
    
    public readonly TimeSpan Delta = Delta;
    
    public readonly TimeSpan Total = Total;
    
    public readonly long FrameCount = FrameCount;
        
    public float DeltaSeconds => (float)Delta.TotalSeconds;

    public float TotalSeconds => (float)Total.TotalSeconds;
}