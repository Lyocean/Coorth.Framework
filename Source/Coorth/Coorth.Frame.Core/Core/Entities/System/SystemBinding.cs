using System;

namespace Coorth {
    public abstract class SystemBinding {
        public abstract Type SystemType { get; }
        internal abstract SystemBase AddSystem(Sandbox sandbox, SystemBase parent);
        internal abstract bool RemoveSystem(Sandbox sandbox);
    }
    
    public class SystemBinding<T> : SystemBinding where T : SystemBase {
        
        public override Type SystemType => typeof(T);
        
        internal override SystemBase AddSystem(Sandbox sandbox, SystemBase parent) {
            var system = Activator.CreateInstance<T>();
            sandbox.OnSystemAdd<T>(SystemType, system, parent, this);
            return system;
        }

        internal override bool RemoveSystem(Sandbox sandbox) {
            return sandbox.OnSystemRemove<T>(SystemType);
        }

    }
}