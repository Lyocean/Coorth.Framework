using System;

namespace Coorth.Framework;

[Event]
public readonly record struct EventModuleRemove(Type Key, Module Module) : IEvent {
    
    public readonly Type Key = Key;
    
    public readonly Module Module = Module;
}

[Event]
public readonly record struct EventModuleRemove<TModule>(Type Key, TModule Module) : IEvent where TModule : Module {
    
    public readonly Type Key = Key;
    
    public readonly TModule Module = Module;
}