using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Coorth.Framework;
using Coorth.Platforms;

namespace Coorth.Tasks.Ticking;

public interface ITickingContext {
    
}

public class TickingTask : ITickingContext {

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

    private TimeSpan startTime;
    
    public event Action? OnTicking;
    
    public event Action? OnComplete;

    private ITaskManager TaskManager { get; }

    public TickingTask(ITaskManager taskManager, Dispatcher dispatcher, TickSetting setting, CancellationToken cancellationToken) {
        this.TaskManager = taskManager;
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
    private TimeSpan TickLoop(ref TimeSpan lastTime) {
        var currentTime = TimeSpan.FromTicks(Stopwatch.GetTimestamp());
        
        var deltaTickTime = currentTime - lastTime;
        lastTime = currentTime;

        var tickingContext = this;
        
        //Step Update
        RemainingStepTime += StepDeltaTime;
        for (var i = 0; i < MaxStepPerFrame && RemainingStepTime >= TimeSpan.Zero; i++, RemainingStepTime -= StepDeltaTime) {
            dispatcher.Dispatch(new EventStepUpdate(tickingContext, StepTotalTime, StepDeltaTime, TotalStepFrameCount));
            TotalStepFrameCount++;
            StepTotalTime += StepDeltaTime;
        }
        
        //Tick Update
        dispatcher.Dispatch(new EventTickBefore(tickingContext, TickTotalTime, deltaTickTime, TotalTickFrameCount));
        dispatcher.Dispatch(new EventTickUpdate(tickingContext, TickTotalTime, deltaTickTime, TotalTickFrameCount));

        //Late Update
        dispatcher.Dispatch(new EventLateUpdate(tickingContext, TickTotalTime, deltaTickTime, TotalTickFrameCount));
        
        OnTicking?.Invoke();

        TotalTickFrameCount++;
        TickTotalTime += deltaTickTime;
        
        Console.WriteLine($"Delta:{deltaTickTime.TotalMilliseconds}");

        var remainingTime = TickDeltaTime - (TimeSpan.FromTicks(Stopwatch.GetTimestamp()) - currentTime);
        return remainingTime;
    }
    
    public Thread RunInThread(CancellationToken cancellation) {
        cancellationToken = cancellation;
        var thread = new Thread(RunLoop);
        thread.Start();
        return thread;
    }
    
    public void RunLoop() {
        startTime = TimeSpan.FromTicks(Stopwatch.GetTimestamp());
        var lastTime = startTime;
        while (!cancellationToken.IsCancellationRequested) {
            var remainingTime = TickLoop(ref lastTime);
            if (remainingTime <= TimeSpan.Zero) {
                continue;
            }
            TaskManager.Sleep(remainingTime, SleepOptions.Precision);
        }
        OnComplete?.Invoke();
    }
}