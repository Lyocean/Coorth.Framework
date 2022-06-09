using System;
using Coorth.Framework;

namespace Coorth.Platforms;

public enum SleepOptions {
    Precision,
    Performance,
}

public interface IPlatformManager : IManager {
    void Sleep(TimeSpan time, SleepOptions options);
}

public abstract class PlatformManager : Manager, IPlatformManager {
    public abstract void Sleep(TimeSpan time, SleepOptions options);
}