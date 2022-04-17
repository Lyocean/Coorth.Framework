using System;

namespace Coorth {
    public sealed class TickSetting {
        public int MaxStepPerFrame { get; set; } = int.MaxValue;
        
        public float StepFrameRate { get; set; } = 60f;
        
        public float TickFrameRate { get; set; } = 60f;
        
        public float TimeScale { get; set; } = 1f;
        
        public TimeSpan StepDeltaTime => TimeSpan.FromSeconds(1f / StepFrameRate);
        
        public TimeSpan TickDeltaTime => TimeSpan.FromSeconds(1f / TickFrameRate);
    }
}
