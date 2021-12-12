using System;

namespace Coorth {
    public interface ISandboxEvent : IEvent { }

    public readonly struct EventSandboxBeginInit : ISandboxEvent {
        
    }

    public readonly struct EventSandboxEndInit : ISandboxEvent {

    }

    public struct EventSandboxDestroy : ISandboxEvent {

    }
}