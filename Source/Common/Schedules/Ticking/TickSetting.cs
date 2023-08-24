using System;


namespace Coorth.Tasks.Ticking; 

[Serializable, DataDefine]
public sealed partial class TickSetting {

    [DataMember(1)]
    public int MaxStepPerFrame { get; set; } = int.MaxValue;

    [DataMember(2)]
    public float StepFrameRate { get; set; } = 60f;

    [DataMember(3)]
    public float TickFrameRate { get; set; } = 60f;

    [DataMember(4)]
    public float TimeScale { get; set; } = 1f;

    public TimeSpan StepDeltaTime => TimeSpan.FromSeconds(1f / StepFrameRate);

    public TimeSpan TickDeltaTime => TimeSpan.FromSeconds(1f / TickFrameRate);
}