using System;

namespace Coorth.Framework;

public interface ITickEvent : IEvent {
    TimeSpan GetTotalTime();
    TimeSpan GetDeltaTime();
}