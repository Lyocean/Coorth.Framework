using System;
using Coorth.Framework;

namespace Coorth.Platforms;

public enum SleepOptions {
    Precision,
    Performance,
}

public readonly struct TimePeriodScope : IDisposable {

    private readonly PlatformManager manager;

    private readonly TimeSpan period;
    
    public TimePeriodScope(PlatformManager value, TimeSpan time) {
        manager = value;
        period = manager.TimePeriodBegin(time);
    }
    
    public void Dispose() {
        manager.TimePeriodEnd(period);
    }
}

public interface IPlatformManager : IManager {
    TimePeriodScope TimePeriodScope(TimeSpan time);
    TimeSpan TimePeriodBegin(TimeSpan time);
    TimeSpan TimePeriodEnd(TimeSpan time);
    void Sleep(TimeSpan time, SleepOptions options);
}

public abstract class PlatformManager : Manager, IPlatformManager {
    public TimePeriodScope TimePeriodScope(TimeSpan time) => new(this, time);
    public abstract TimeSpan TimePeriodBegin(TimeSpan time);
    public abstract TimeSpan TimePeriodEnd(TimeSpan time);
    public abstract void Sleep(TimeSpan time, SleepOptions options);
}