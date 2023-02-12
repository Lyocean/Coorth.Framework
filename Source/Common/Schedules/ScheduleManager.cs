using System;
using Coorth.Framework;
using Coorth.Platforms;

namespace Coorth.Tasks;

[Manager]
public interface IScheduleManager : IManager {
    void Sleep(TimeSpan time, SleepOptions option);
}

public class ScheduleManager : Manager, IScheduleManager {

    private IPlatformManager PlatformManager { get; }

    public ScheduleManager(IServiceCollection services) {
        PlatformManager = services.GetService<IPlatformManager>();
    }

    public void Sleep(TimeSpan time, SleepOptions option) => PlatformManager.Sleep(time, option);
}