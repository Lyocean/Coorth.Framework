using Coorth.Tasks;

namespace Coorth.Framework;

[Event]
public record EventGameStart(ScheduleContext Schedule) {
    public readonly ScheduleContext Schedule = Schedule;
}