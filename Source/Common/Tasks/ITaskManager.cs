using System;
using Coorth.Framework;
using Coorth.Platforms;

namespace Coorth.Tasks;

[Manager]
public interface ITaskManager : IManager {
    void Sleep(TimeSpan time, SleepOptions option);
}

public class TaskManager : Manager, ITaskManager {

    private IPlatformManager PlatformManager { get; }

    public TaskManager(IPlatformManager platformManager) {
        PlatformManager = platformManager;
    }

    public void Sleep(TimeSpan time, SleepOptions option) => PlatformManager.Sleep(time, option);
}