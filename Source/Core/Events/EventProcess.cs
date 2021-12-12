using System;
using System.Threading.Tasks;

namespace Coorth {
    public interface IEventProcess {
        EventId ProcessId { get; }
    }

    public interface IEventProcess<T> : IEventProcess {
        void Dispatch(in T e);
    }
}