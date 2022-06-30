using System;

namespace Coorth.Framework;

[Event]
public record EventModuleRemove(Type Key, Module Module) : IEvent {
    
    public readonly Type Key = Key;
    
    public readonly Module Module = Module;
}

[Event]
public record EventModuleRemove<TModule>(Type Key, TModule Module) : IEvent where TModule : Module {
    
    public readonly Type Key = Key;
    
    public readonly TModule Module = Module;
}