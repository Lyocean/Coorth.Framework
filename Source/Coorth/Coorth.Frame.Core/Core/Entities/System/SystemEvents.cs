using System;

namespace Coorth {

    public readonly struct EventSystemAdd : ISandboxEvent {
        public readonly Sandbox Sandbox;
        public readonly Type Type;
        public readonly SystemBase System;

        public EventSystemAdd(Sandbox sandbox, Type type, SystemBase system) {
            this.Sandbox = sandbox;
            this.Type = type;
            this.System = system;
        }
    }

    public readonly struct EventSystemAdd<T> : ISandboxEvent {
        public readonly Sandbox Sandbox;
        public readonly Type Type;
        public readonly SystemBase System;

        public EventSystemAdd(Sandbox sandbox, Type type, SystemBase system) {
            this.Sandbox = sandbox;
            this.Type = type;
            this.System = system;
        }
    }

    public readonly struct EventSystemRemove : ISandboxEvent {
        public readonly Sandbox Sandbox;
        public readonly Type Type;
        public readonly SystemBase System;

        public EventSystemRemove(Sandbox sandbox, Type type, SystemBase system) {
            this.Sandbox = sandbox;
            this.Type = type;
            this.System = system;
        }
    }

    public readonly struct EventSystemRemove<T> : ISandboxEvent {
        public readonly Sandbox Sandbox;
        public readonly Type Type;
        public readonly SystemBase System;

        public EventSystemRemove(Sandbox sandbox, Type type, SystemBase system) {
            this.Sandbox = sandbox;
            this.Type = type;
            this.System = system;
        }
    }
}