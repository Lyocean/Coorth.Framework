using System;
using Coorth.Framework;
using Coorth.Platforms;

namespace Coorth.Tasks;

[Manager]
public interface IScheduleManager : IManager {
}

public class ScheduleManager : Manager, IScheduleManager {

    public ScheduleManager(IServiceCollection services) {
    }
}