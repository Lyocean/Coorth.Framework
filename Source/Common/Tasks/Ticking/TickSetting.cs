using System;


namespace Coorth.Tasks.Ticking; 

[Serializable, StoreContract]
public sealed class TickSetting {

    [StoreMember(1)]
    public int MaxStepPerFrame { get; set; } = int.MaxValue;

    [StoreMember(2)]
    public float StepFrameRate { get; set; } = 60f;

    [StoreMember(3)]
    public float TickFrameRate { get; set; } = 60f;

    [StoreMember(4)]
    public float TimeScale { get; set; } = 1f;

    public TimeSpan StepDeltaTime => TimeSpan.FromSeconds(1f / StepFrameRate);

    public TimeSpan TickDeltaTime => TimeSpan.FromSeconds(1f / TickFrameRate);
}