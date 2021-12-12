using System;

namespace Coorth {
    public abstract class SystemBinding {
        public abstract Type SystemType { get; }
        internal abstract SystemBase AddSystem(SystemBase parent);
        internal abstract bool RemoveSystem(SystemBase parent);
    }
    
    public class SystemBinding<T> : SystemBinding where T : SystemBase {
        
        public override Type SystemType => typeof(T);
        
        internal override SystemBase AddSystem(SystemBase parent) {
            var system = Activator.CreateInstance<T>();
            parent.AddSystem<T>(system);
            return system;
        }

        internal override bool RemoveSystem(SystemBase parent) {
            return parent.RemoveSystem<T>();
        }

    }
}