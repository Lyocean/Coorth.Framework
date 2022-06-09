
using System;
using System.Threading.Tasks;

namespace Coorth.Framework;

public partial class Module {
    protected void Subscribe<TEvent>(Action<TEvent> action) where TEvent : notnull {
        var reaction = Dispatcher.Subscribe(action);
        Collector.Add(reaction);
    }
    
    protected void Subscribe<TEvent>(Func<TEvent, Task> action) where TEvent : notnull {
        var reaction = Dispatcher.Subscribe(action);
        Collector.Add(reaction);
    }
}