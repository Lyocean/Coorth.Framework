using System;
using System.Threading.Tasks;

namespace Coorth {
    public interface IEventProcess {
        EventId ProcessId { get; }
    }

    public interface IEventProcess<T> : IEventProcess where T : IEvent {
        void Execute(in T e);
    }
}