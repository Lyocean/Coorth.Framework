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

    public event Action<TimeSpan>? OnTicking;

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
    private TimeSpan TickLoop(ref TimeSpan last_time, TaskSyncContext schedule, Dispatcher dispatcher) {
        var current_time    = GetCurrentTime();
        var delta_tick_time = current_time - last_time;
        var ticking_task    = this;
        last_time           = current_time;

        //Step Update
        RemainingStepTime += StepDeltaTime;
        for (var i = 0; i < MaxStepPerFrame && RemainingStepTime >= TimeSpan.Zero; i++, RemainingStepTime -= StepDeltaTime) {
            dispatcher.Dispatch(new StepBeforeEvent(ticking_task, StepTotalTime, StepDeltaTime, TotalStepFrameCount));
            dispatcher.Dispatch(new StepUpdateEvent(ticking_task, StepTotalTime, StepDeltaTime, TotalStepFrameCount));
            TotalStepFrameCount++;
            StepTotalTime += StepDeltaTime;
        }
        
        schedule.Execute();
        
        //Tick update
        dispatcher.Dispatch(new TickBeforeEvent(ticking_task, TickTotalTime, delta_tick_time, TotalTickFrameCount));
        dispatcher.Dispatch(new TickUpdateEvent(ticking_task, TickTotalTime, delta_tick_time, TotalTickFrameCount));

        //Late update
        dispatcher.Dispatch(new LateUpdateEvent(ticking_task, TickTotalTime, delta_tick_time, TotalTickFrameCount));

        //Draw update
        dispatcher.Dispatch(new DrawBeforeEvent(ticking_task, TickTotalTime, delta_tick_time, TotalTickFrameCount));
        dispatcher.Dispatch(new DrawUpdateEvent(ticking_task, TickTotalTime, delta_tick_time, TotalTickFrameCount));
        dispatcher.Dispatch(new DrawFinishEvent(ticking_task, TickTotalTime, delta_tick_time, TotalTickFrameCount));

        OnTicking?.Invoke(delta_tick_time);
        schedule.Execute();
        
        TotalTickFrameCount++;
        TickTotalTime += delta_tick_time;
        
        var remaining_time = TickDeltaTime - (GetCurrentTime() - current_time);
        return remaining_time;
    }
    
    public void RunLoop(TaskSyncContext sync_context, Dispatcher dispatcher) {
        startTime = GetCurrentTime();
        var last_time = startTime;
        var platform = PlatformManager.Platform;
        using var _ = platform.TimePeriodScope(TimeSpan.FromMilliseconds(1));
        var cancellation = sync_context.Cancellation;
        while (!cancellation.IsCancellationRequested) {
            var remaining_time = TickLoop(ref last_time, sync_context, dispatcher);
            if (remaining_time <= TimeSpan.Zero) {
                continue;
            }
            platform.Sleep(remaining_time, SleepOptions.Precision);
        }
        OnComplete?.Invoke();
    }

}