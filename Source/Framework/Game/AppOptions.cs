using Coorth.Logs;
using Coorth.Tasks;

namespace Coorth.Framework; 

public record AppOptions(int Id, string Name, ILogger Logger, Dispatcher? Dispatcher, ServiceLocator? Services, ScheduleContext? Schedule) {
    
    public readonly int Id = Id;
    
    public readonly string Name = Name;
    
    public readonly ILogger Logger = Logger;
    
    public readonly Dispatcher? Dispatcher = Dispatcher;
    
    public readonly ServiceLocator? Services = Services;
    
    public readonly ScheduleContext? Schedule = Schedule;
}