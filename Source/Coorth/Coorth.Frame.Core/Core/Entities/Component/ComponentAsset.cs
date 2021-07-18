using System;

namespace Coorth {
    public abstract class ComponentAsset {
        public Type Type;
    }

    public class ComponentAsset<T> : ComponentAsset {
        
        public readonly T Component;

        public ComponentAsset(T component) {
            this.Component = component;
        }
    }
    
    public class ComponentsAsset<T> : ComponentAsset {
        public readonly T[] Components;
    }
}