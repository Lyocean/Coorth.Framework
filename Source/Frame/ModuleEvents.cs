using System;


namespace Coorth {
    public readonly struct EventModuleAdd : IEvent {
        public readonly Module Parent;
        public readonly Type Key;
        public readonly Module Module;

        public EventModuleAdd(Module parent, Type key, Module module) {
            this.Parent = parent;
            this.Key = key;
            this.Module = module;
        }
    }
    
    public readonly struct EventModuleAdd<TModule> : IEvent where TModule : Module {
        public readonly Module Parent;
        public readonly Type Key;
        public readonly TModule Module;
        
        public EventModuleAdd(Module parent, Type key, TModule module) {
            this.Parent = parent;
            this.Key = key;
            this.Module = module;
        }
    }
    
    public readonly struct EventModuleRemove : IEvent {
        public readonly Module Parent;
        public readonly Type Key;
        public readonly Module Module;
        
        public EventModuleRemove(Module parent, Type key, Module module) {
            this.Parent = parent;
            this.Key = key;
            this.Module = module;
        }
    }
    
    public readonly struct EventModuleRemove<TModule> : IEvent where TModule : Module {
        public readonly Module Parent;
        public readonly Type Key;
        public readonly TModule Module;
        
        public EventModuleRemove(Module parent, Type key, TModule module) {
            this.Parent = parent;
            this.Key = key;
            this.Module = module;
        }
    }
}