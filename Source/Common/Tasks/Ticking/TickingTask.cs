using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Coorth.Framework;
using Coorth.Platforms;

namespace Coorth.Tasks.Ticking;

public interface ITickingContext {
    
}

public class TickingTask : ITickingContext {
    
    private TickSetting Setting { get; }
    
    private IPlatformManager PlatformManager { get; }

    public int MaxStepPerFrame => Setting.MaxStepPerFrame;

    public float StepFrameRate => Setting.StepFrameRate;

    public TimeSpan StepDeltaTime => Setting.StepDeltaTime;


    public TimeSpan StepTotalTime;

    public long TotalStepFrameCount;

    public TimeSpan RemainingStepTime;

    //Tick

    public float TickFrameRate => Setting.TickFrameRate;

    public TimeSpan TickDeltaTime => Setting.TickDeltaTime;

    public TimeSpan TickTotalTime;

    public long TotalTickFrameCount;

    public float TimeScale => Setting.TimeScale;

    private TimeSpan startTime;

    public event Action? OnTicking;

    public event Action? OnComplete;
    
    public TickingTask(IPlatformManager platformManager, TickSetting setting) {
        PlatformManager = platformManager;
        Setting = setting;
        StepTotalTime = TimeSpan.Zero;
        TotalStepFrameCount = 0;
        RemainingStepTime = TimeSpan.Zero;
        TickTotalTime = TimeSpan.Zero;
        TotalTickFrameCount = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static TimeSpan GetCurrentTime() {
        var tickFrequency = (double) TimeSpan.TicksPerSecond / Stopwatch.Frequency;
        return TimeSpan.FromTicks(unchecked((long)(Stopwatch.GetTimestamp() * tickFrequency)));
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TimeSpan TickLoop(ref TimeSpan lastTime, TaskSyncContext schedule, Dispatcher dispatcher) {
        var currentTime = GetCurrentTime();
        
        var deltaTickTime = currentTime - lastTime;
        
        // Console.WriteLine($"Delta: {deltaTickTime.TotalMilliseconds}");
        
        lastTime = currentTime;

        var tickingContext = this;

        //Step Update
        RemainingStepTime += StepDeltaTime;
        for (var i = 0; i < MaxStepPerFrame && RemainingStepTime >= TimeSpan.Zero; i++, RemainingStepTime -= StepDeltaTime) {
            dispatcher.Dispatch(new EventStepUpdate(tickingContext, StepTotalTime, StepDeltaTime, TotalStepFrameCount));
            TotalStepFrameCount++;
            StepTotalTime += StepDeltaTime;
        }
        
        schedule.Execute();
        
        //Tick Update
        dispatcher.Dispatch(new EventTickBefore(tickingContext, TickTotalTime, deltaTickTime, TotalTickFrameCount));
        dispatcher.Dispatch(new EventTickUpdate(tickingContext, TickTotalTime, deltaTickTime, TotalTickFrameCount));

        //Late Update
        dispatcher.Dispatch(new EventLateUpdate(tickingContext, TickTotalTime, deltaTickTime, TotalTickFrameCount));

        OnTicking?.Invoke();
        schedule.Execute();
        
        TotalTickFrameCount++;
        TickTotalTime += deltaTickTime;
        
        // Console.WriteLine($"DeltaTime:{deltaTickTime.TotalMilliseconds} ms");

        var remainingTime = TickDeltaTime - (GetCurrentTime() - currentTime);
        return remainingTime;
    }
    
    public void RunLoop(TaskSyncContext syncContext, Dispatcher dispatcher) {
        startTime = GetCurrentTime();
        var lastTime = startTime;
        using var _ = PlatformManager.TimePeriodScope(TimeSpan.FromMilliseconds(1));
        var cancellation = syncContext.Cancellation;
        while (!cancellation.IsCancellationRequested) {
            var remainingTime = TickLoop(ref lastTime, syncContext, dispatcher);
            if (remainingTime <= TimeSpan.Zero) {
                continue;
            }
            PlatformManager.Sleep(remainingTime, SleepOptions.Precision);
        }
        OnComplete?.Invoke();
    }

}