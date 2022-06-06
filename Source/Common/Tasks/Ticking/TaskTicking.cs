using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Coorth.Framework;

namespace Coorth.Tasks.Ticking; 

public class TaskTicking {

    private readonly Dispatcher dispatcher;
    
    private readonly TickSetting setting;

    private CancellationToken cancellationToken;

    public int MaxStepPerFrame => setting.MaxStepPerFrame;

    public float StepFrameRate => setting.StepFrameRate;

    public TimeSpan StepDeltaTime => setting.StepDeltaTime;


    public TimeSpan StepTotalTime;

    public long TotalStepFrameCount;

    public TimeSpan RemainingStepTime;

    //Tick

    public float TickFrameRate => setting.TickFrameRate;

    public TimeSpan TickDeltaTime => setting.TickDeltaTime;

    public TimeSpan TickTotalTime;

    public long TotalTickFrameCount;

    public float TimeScale => setting.TimeScale;

    private DateTime startTime;
    
    public event Action? OnTicking;
    
    public event Action? OnComplete;

    public TaskTicking(Dispatcher dispatcher, TickSetting setting, CancellationToken cancellationToken) {
        this.dispatcher = dispatcher;
        this.setting = setting;
        this.cancellationToken = cancellationToken;
        StepTotalTime = TimeSpan.Zero;
        TotalStepFrameCount = 0;
        RemainingStepTime = TimeSpan.Zero;
        TickTotalTime = TimeSpan.Zero;
        TotalTickFrameCount = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static DateTime GetCurrentTime() => new(Stopwatch.GetTimestamp());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TimeSpan TickLoop(ref DateTime lastTime) {
        var currentTime = GetCurrentTime();
        
        var deltaTickTime = currentTime - lastTime;
        lastTime = currentTime;

        //Step Update
        RemainingStepTime += StepDeltaTime;
        for (var i = 0; i < MaxStepPerFrame && RemainingStepTime >= TimeSpan.Zero; i++, RemainingStepTime -= StepDeltaTime) {
            dispatcher.Dispatch(new EventStepUpdate(StepTotalTime, StepDeltaTime, TotalStepFrameCount));
            TotalStepFrameCount++;
            StepTotalTime += StepDeltaTime;
        }
        
        //Tick Update
        dispatcher.Dispatch(new EventTickBefore(TickTotalTime, deltaTickTime, TotalTickFrameCount));
        dispatcher.Dispatch(new EventTickUpdate(TickTotalTime, deltaTickTime, TotalTickFrameCount));

        //Late Update
        dispatcher.Dispatch(new EventLateUpdate(TickTotalTime, deltaTickTime, TotalTickFrameCount));
        
        OnTicking?.Invoke();

        TotalTickFrameCount++;
        TickTotalTime += deltaTickTime;
        
        Console.WriteLine($"Delta:{deltaTickTime.TotalMilliseconds}");

        var remainingTime = TickDeltaTime - (GetCurrentTime() - currentTime);
        return remainingTime;
    }
    
    public Thread RunInThread(CancellationToken cancellation) {
        cancellationToken = cancellation;
        var thread = new Thread(RunLoop);
        thread.Start();
        return thread;
    }
    
    public void RunLoop() {
        startTime = GetCurrentTime();
        var lastTime = startTime;
        while (!cancellationToken.IsCancellationRequested) {
            var remainingTime = TickLoop(ref lastTime);
            WaitTime(remainingTime);
        }
        OnComplete?.Invoke();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void WaitTime(in TimeSpan remainingTime) {
        if (remainingTime <= TimeSpan.Zero) {
            return;
        }
        var nextTimeTicks = Stopwatch.GetTimestamp() + remainingTime.Ticks;
        while (Stopwatch.GetTimestamp() < nextTimeTicks) {
            Thread.Yield();
        }
    }
}