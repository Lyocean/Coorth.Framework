using System;

namespace Coorth {
    public interface IEvent {
    }

    public interface ITimeEvent : IEvent {
        TimeSpan GetTotalTime();
    }

    public interface ITickEvent : IEvent {
        TimeSpan GetDeltaTime();
    }

    public interface IAppEvent : IEvent {
        
    }
}