using System;
using Coorth.Maths;
using Coorth.Tasks.Ticking;

namespace Coorth.Framework;

#region App

[Event]
public record ApplicationInitBeginEvent;

[Event]
public record ApplicationInitAfterEvent;

[Event]
public record ApplicationLoadEvent;

[Event]
public record ApplicationStartEvent(IApplication App) {
    public readonly IApplication App = App;
}

[Event]
public record ApplicationStatusChangeEvent(bool IsPause) {
    public readonly bool IsPause = IsPause;
}

[Event]
public record ApplicationCloseEvent;

[Event]
public record ApplicationQuitEvent;


#endregion

#region Step

[Event]
public readonly record struct StepBeforeEvent(ITickingContext TickingContext, TimeSpan TotalTime, TimeSpan DeltaTime, long FrameCount) : ITickEvent {
    public readonly ITickingContext TickingContext = TickingContext;
    public readonly TimeSpan TotalTime = TotalTime;
    public readonly TimeSpan DeltaTime = DeltaTime;
    public readonly long FrameCount = FrameCount;
    public DateTime CurrentTime => DateTime.MinValue + TotalTime;
    public float DeltaSecond => (float)DeltaTime.TotalSeconds;
    public float TotalSecond => (float)TotalTime.TotalSeconds;
    public TimeSpan GetDeltaTime() => DeltaTime;
    public TimeSpan GetTotalTime() => TotalTime;
}

[Event]
public readonly record struct StepUpdateEvent(ITickingContext TickingContext, TimeSpan TotalTime, TimeSpan DeltaTime, long FrameCount) : ITickEvent {
    public readonly ITickingContext TickingContext = TickingContext;
    public readonly TimeSpan TotalTime = TotalTime;
    public readonly TimeSpan DeltaTime = DeltaTime;
    public readonly long FrameCount = FrameCount;
    public float DeltaSecond => (float)DeltaTime.TotalSeconds;
    public float TotalSecond => (float)TotalTime.TotalSeconds;
    public TimeSpan GetDeltaTime() => DeltaTime;
    public TimeSpan GetTotalTime() => TotalTime;
}


#endregion

#region Tick


[Event]
public readonly record struct TickBeforeEvent(ITickingContext TickingContext, TimeSpan TotalTime, TimeSpan DeltaTime, long FrameCount) : ITickEvent {
    public readonly ITickingContext TickingContext = TickingContext;
    public readonly TimeSpan TotalTime = TotalTime;
    public readonly TimeSpan DeltaTime = DeltaTime;
    public readonly long FrameCount = FrameCount;
    public DateTime CurrentTime => DateTime.MinValue + TotalTime;
    public float DeltaSecond => (float)DeltaTime.TotalSeconds;
    public float TotalSecond => (float)TotalTime.TotalSeconds;
    public TimeSpan GetDeltaTime() => DeltaTime;
    public TimeSpan GetTotalTime() => TotalTime;
}

[Event]
public readonly record struct TickUpdateEvent(ITickingContext TickingContext, TimeSpan TotalTime, TimeSpan DeltaTime, long FrameCount) : ITickEvent {
    public readonly ITickingContext TickingContext = TickingContext;
    public readonly TimeSpan TotalTime = TotalTime;
    public readonly TimeSpan DeltaTime = DeltaTime;
    public readonly long FrameCount = FrameCount;
    public DateTime CurrentTime => DateTime.MinValue + TotalTime;
    public float DeltaSecond => (float)DeltaTime.TotalSeconds;
    public float TotalSecond => (float)TotalTime.TotalSeconds;
    public TimeSpan GetDeltaTime() => DeltaTime;
    public TimeSpan GetTotalTime() => TotalTime;
}

[Event]
public readonly record struct LateUpdateEvent(ITickingContext TickingContext, TimeSpan TotalTime, TimeSpan DeltaTime, long FrameCount) : ITickEvent {
    public readonly ITickingContext TickingContext = TickingContext;
    public readonly TimeSpan TotalTime = TotalTime;
    public readonly TimeSpan DeltaTime = DeltaTime;
    public TimeSpan UnscaleDeltaTime => DeltaTime;

    public readonly long FrameCount = FrameCount;
    public float DeltaSecond => (float)DeltaTime.TotalSeconds;
    public float TotalSecond => (float)TotalTime.TotalSeconds;
    public TimeSpan GetDeltaTime() => DeltaTime;
    public TimeSpan GetTotalTime() => TotalTime;
}

[Event]
public readonly record struct EventEndOfFrame(ITickingContext TickingContext, TimeSpan TotalTime, TimeSpan DeltaTime, long FrameCount) {
    public readonly ITickingContext TickingContext = TickingContext;
    public readonly TimeSpan TotalTime = TotalTime;
    public readonly TimeSpan DeltaTime = DeltaTime;
    public readonly long FrameCount = FrameCount;
    public float DeltaSecond => (float)DeltaTime.TotalSeconds;
    public float TotalSecond => (float)TotalTime.TotalSeconds;
}

#endregion

#region Draw

[Event]
public readonly struct DrawSetupEvent {
        
}

[Event]
public readonly record struct DrawResizeEvent(Size2 Size) : IEvent {

    public readonly Size2 Size = Size;

}

[Event]
public readonly record struct DrawBeforeEvent(ITickingContext TickingContext, TimeSpan TotalTime, TimeSpan DeltaTime, long FrameCount) : IEvent {
    public readonly ITickingContext TickingContext = TickingContext;
    public readonly TimeSpan TotalTime = TotalTime;
    public readonly TimeSpan DeltaTime = DeltaTime;
    public readonly long FrameCount = FrameCount;
    public float DeltaSeconds => (float)DeltaTime.TotalSeconds;
    public float TotalSeconds => (float)TotalTime.TotalSeconds;
}

[Event]
public readonly record struct DrawUpdateEvent(ITickingContext TickingContext, TimeSpan TotalTime, TimeSpan DeltaTime, long FrameCount) : IEvent {
    public readonly ITickingContext TickingContext = TickingContext;
    public readonly TimeSpan DeltaTime = DeltaTime;
    public readonly TimeSpan TotalTime = TotalTime;
    public readonly long FrameCount = FrameCount;
    public float DeltaSeconds => (float)DeltaTime.TotalSeconds;
    public float TotalSeconds => (float)TotalTime.TotalSeconds;
}

[Event]
public readonly record struct DrawFinishEvent(ITickingContext TickingContext, TimeSpan TotalTime, TimeSpan DeltaTime, long FrameCount) : IEvent {
    public readonly ITickingContext TickingContext = TickingContext;
    public readonly TimeSpan TotalTime = TotalTime;
    public readonly TimeSpan DeltaTime = DeltaTime;
    public readonly long FrameCount = FrameCount;
    public float DeltaSeconds => (float)DeltaTime.TotalSeconds;
    public float TotalSeconds => (float)TotalTime.TotalSeconds;
}

#endregion

#region Module

[Event]
public readonly record struct ModuleAddEvent(Type Key, ModuleBase Module) : IEvent {
    public readonly Type Key = Key;
    public readonly ModuleBase Module = Module;
}

[Event]
public readonly record struct ModuleRemoveEvent(Type Key, ModuleBase Module) : IEvent {
    public readonly Type Key = Key;
    public readonly ModuleBase Module = Module;
}

#endregion
