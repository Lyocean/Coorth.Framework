using System;
using System.Runtime.Serialization;

namespace Coorth.Tasks.Ticking; 

[Serializable, DataContract]
public sealed class TickSetting {

    [DataMember(Order = 1)]
    public int MaxStepPerFrame { get; set; } = int.MaxValue;

    [DataMember(Order = 2)]
    public float StepFrameRate { get; set; } = 60f;

    [DataMember(Order = 3)]
    public float TickFrameRate { get; set; } = 60f;

    [DataMember(Order = 4)]
    public float TimeScale { get; set; } = 1f;

    public TimeSpan StepDeltaTime => TimeSpan.FromSeconds(1f / StepFrameRate);

    public TimeSpan TickDeltaTime => TimeSpan.FromSeconds(1f / TickFrameRate);
}