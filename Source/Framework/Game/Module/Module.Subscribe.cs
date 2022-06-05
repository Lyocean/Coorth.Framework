
using System;

namespace Coorth.Framework;

public partial class Module {
    protected void Subscribe<TEvent>(Action<TEvent> action) where TEvent : notnull {
        var reaction = Dispatcher.Subscribe(action);
        Collector.Add(reaction);
    }
}