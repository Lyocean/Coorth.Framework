using System;
using System.Threading;

namespace Coorth.Platforms;

public abstract class PlatformBase {

    public abstract PlatformTypes Type { get; }

    public TimePeriodScope TimePeriodScope(TimeSpan time) => new(this, time);
    
    public virtual TimeSpan TimePeriodBegin(TimeSpan time) => time;
    
    public virtual TimeSpan TimePeriodEnd(TimeSpan time) => time;

    public virtual void Sleep(TimeSpan time, SleepOptions options) {
        if (time <= TimeSpan.Zero) {
            return;
        }
        Thread.Sleep(time);
    }

}

public enum SleepOptions {
    Precision,
    Performance,
}

public readonly ref struct TimePeriodScope {

    private readonly PlatformBase platform;

    private readonly TimeSpan period;
    
    public TimePeriodScope(PlatformBase value, TimeSpan time) {
        platform = value;
        period = platform.TimePeriodBegin(time);
    }
    
    public void Dispose() {
        platform.TimePeriodEnd(period);
    }
}
