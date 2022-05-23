using System;

namespace Coorth.Framework;

[Event]
public readonly record struct EventModuleAdd(Type Key, Module Module) : IEvent {
    
    public readonly Type Key = Key;
    
    public readonly Module Module = Module;
    
}
    
[Event]
public readonly record struct EventModuleAdd<TModule>(Type Key, TModule Module) : IEvent where TModule : Module {
    
    public readonly Type Key = Key;
    
    public readonly TModule Module = Module;
    
}

