using System;


namespace Coorth {
    public readonly struct EventModuleAdd : IEvent {
        public readonly ModuleBase Parent;
        public readonly Type Key;
        public readonly ModuleBase Module;

        public EventModuleAdd(ModuleBase parent, Type key, ModuleBase module) {
            this.Parent = parent;
            this.Key = key;
            this.Module = module;
        }
    }
    
    public readonly struct EventModuleAdd<TModule> : IEvent where TModule : ModuleBase {
        public readonly ModuleBase Parent;
        public readonly Type Key;
        public readonly TModule Module;
        
        public EventModuleAdd(ModuleBase parent, Type key, TModule module) {
            this.Parent = parent;
            this.Key = key;
            this.Module = module;
        }
    }
    
    public readonly struct EventModuleRemove : IEvent {
        public readonly ModuleBase Parent;
        public readonly Type Key;
        public readonly ModuleBase Module;
        
        public EventModuleRemove(ModuleBase parent, Type key, ModuleBase module) {
            this.Parent = parent;
            this.Key = key;
            this.Module = module;
        }
    }
    
    public readonly struct EventModuleRemove<TModule> : IEvent where TModule : ModuleBase {
        public readonly ModuleBase Parent;
        public readonly Type Key;
        public readonly TModule Module;
        
        public EventModuleRemove(ModuleBase parent, Type key, TModule module) {
            this.Parent = parent;
            this.Key = key;
            this.Module = module;
        }
    }
}