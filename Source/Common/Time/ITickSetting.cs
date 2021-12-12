namespace Coorth {
    public interface ITickSetting {
        int MaxStepPerFrame { get; }
        float StepFrameRate { get; }
        float TickFrameRate { get; }
        float TimeScale { get; }
    }
}