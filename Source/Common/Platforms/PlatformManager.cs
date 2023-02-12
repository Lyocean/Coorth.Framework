using System;
using System.Threading;
using Coorth.Framework;

namespace Coorth.Platforms;

public interface IPlatformManager : IManager {

    PlatformTypes GetPlatformType();
    
    TimePeriodScope TimePeriodScope(TimeSpan time);
    
    TimeSpan TimePeriodBegin(TimeSpan time);
    
    TimeSpan TimePeriodEnd(TimeSpan time);
    
    void Sleep(TimeSpan time, SleepOptions options);
}

public class PlatformManager : Manager, IPlatformManager {

    public virtual PlatformTypes GetPlatformType() => PlatformTypes.Other;

    public TimePeriodScope TimePeriodScope(TimeSpan time) => new(this, time);

    public virtual TimeSpan TimePeriodBegin(TimeSpan time) => time;

    public virtual TimeSpan TimePeriodEnd(TimeSpan time) => time;

    public virtual void Sleep(TimeSpan time, SleepOptions options) => Thread.Sleep(time);
}


public enum SleepOptions {
    Precision,
    Performance,
}

public readonly ref struct TimePeriodScope {

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
