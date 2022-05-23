using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Coorth; 

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

    public void Setup(TickSetting setting) {
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