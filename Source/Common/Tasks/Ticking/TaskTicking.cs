using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Framework;

namespace Coorth.Tasks.Ticking; 

public class TaskTicking : Disposable {
    
    private TimeTicker ticker;

    private readonly Dispatcher dispatcher;
    
    private readonly Disposable host;

    private bool IsRunning = false;

    private TaskCompletionSource<bool>? completion;

    public event Action? OnTicking;
    
    public TaskTicking(Disposable host, Dispatcher dispatcher, TickSetting setting) {
        this.host = host;
        this.dispatcher = dispatcher;
        this.ticker = new TimeTicker(setting);
    }

    public Thread RunInThread(TaskCompletionSource<bool> tcs) {
        completion = tcs;
        var thread = new Thread(Run);
        thread.Start();
        return thread;
    }
    
    public void Run() {
        IsRunning = true;
        var lastTime = new DateTime(Stopwatch.GetTimestamp());
        ref var frameStepTime = ref ticker.RemainingStepTime;
        while (!IsDisposed && IsRunning && !host.IsDisposed) {
            var currentTime = new DateTime(Stopwatch.GetTimestamp());
            var deltaTickTime = currentTime - lastTime;
            TickLoop(ref frameStepTime, in deltaTickTime);
            OnTicking?.Invoke();
            var remainingTime = ticker.TickDeltaTime - deltaTickTime;
            ticker.WaitTime(remainingTime);
            lastTime = currentTime;
        }
        completion?.SetResult(true);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void TickLoop(ref TimeSpan frameStepTime, in TimeSpan deltaTickTime) {
        //Step Update
        frameStepTime += ticker.StepDeltaTime;
        for (var i = 0; i < ticker.MaxStepPerFrame && frameStepTime >= TimeSpan.Zero; i++, frameStepTime -= ticker.StepDeltaTime) {
            dispatcher.Dispatch(new EventStepUpdate(ticker.StepTotalTime, ticker.StepDeltaTime, ticker.TotalStepFrameCount));
            ticker.StepUpdate();
        }

        //Tick Update
        dispatcher.Dispatch(new EventTickBefore(ticker.TickTotalTime, deltaTickTime, ticker.TotalTickFrameCount));
        dispatcher.Dispatch(new EventTickUpdate(ticker.TickTotalTime, deltaTickTime, ticker.TotalTickFrameCount));

        //Late Update
        dispatcher.Dispatch(new EventLateUpdate(ticker.TickTotalTime, deltaTickTime, ticker.TotalTickFrameCount));

        ticker.TickUpdate(deltaTickTime);
    }
}


public struct TimeTicker {
    //Step

    public int MaxStepPerFrame => Setting.MaxStepPerFrame;

    public float StepFrameRate => Setting.StepFrameRate;

    public TimeSpan StepDeltaTime => Setting.StepDeltaTime;

    public readonly TickSetting Setting;

    public TimeSpan StepTotalTime;

    public long TotalStepFrameCount;

    public TimeSpan RemainingStepTime;

    //Tick

    public float TickFrameRate => Setting.TickFrameRate;

    public TimeSpan TickDeltaTime => Setting.TickDeltaTime;

    public TimeSpan TickTotalTime;

    public long TotalTickFrameCount;

    public float TimeScale => Setting.TimeScale;


    public TimeTicker(TickSetting setting) {
        this.Setting = setting;
        this.StepTotalTime = TimeSpan.Zero;
        this.TotalStepFrameCount = 0;
        this.RemainingStepTime = TimeSpan.Zero;
        this.TickTotalTime = TimeSpan.Zero;
        this.TotalTickFrameCount = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void StepUpdate() {
        TotalStepFrameCount++;
        StepTotalTime += StepDeltaTime;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void TickUpdate(in TimeSpan deltaTickTime) {
        TotalTickFrameCount++;
        TickTotalTime += deltaTickTime;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WaitTime(in TimeSpan remainingTime) {
        if (remainingTime > TimeSpan.Zero) {
            Thread.Sleep(remainingTime);
        }
        else {
            Thread.Sleep(0);
        }
    }
}