using Coorth.Tasks;

namespace Coorth.Framework;

[Event]
public record EventGameStart(TaskSyncContext Schedule) {
    public readonly TaskSyncContext Schedule = Schedule;
}