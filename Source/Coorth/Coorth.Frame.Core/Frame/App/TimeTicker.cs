using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Coorth {
    public struct TimeTicker {
        
        //Step

        public int MaxStepPerFrame;

        public float StepFrameRate;

        public TimeSpan StepDeltaTime => TimeSpan.FromSeconds(1.0/ StepFrameRate);

        public TimeSpan StepTotalTime;

        public long TotalStepFrameCount;

        //Tick

        public float TickFrameRate;

        public TimeSpan TickDeltaTime => TimeSpan.FromSeconds(1f / TickFrameRate);

        public TimeSpan TickTotalTime ;

        public long TotalTickFrameCount;

        public float TimeScale;
        

        public void Setup(ITickSetting setting) {
            //Step
            this.MaxStepPerFrame = setting.MaxStepPerFrame;
            this.StepFrameRate = setting.StepFrameRate;
            this.StepTotalTime = TimeSpan.Zero;
            this.TotalStepFrameCount = 0;
            //Tick
            this.TickFrameRate = setting.TickFrameRate;
            this.TickTotalTime = TimeSpan.Zero;
            this.TotalTickFrameCount = 0;
            this.TimeScale = setting.TimeScale;
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
            } else {
                Thread.Sleep(0);
            }
        }

    }
}
