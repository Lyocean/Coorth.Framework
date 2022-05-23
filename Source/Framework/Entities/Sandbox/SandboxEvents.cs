using System;

namespace Coorth.Framework; 

public interface ISandboxEvent : IEvent { }

public readonly struct EventSandboxBeginInit : ISandboxEvent {
        
}

public readonly struct EventSandboxEndInit : ISandboxEvent {

}

public struct EventSandboxDestroy : ISandboxEvent {

}