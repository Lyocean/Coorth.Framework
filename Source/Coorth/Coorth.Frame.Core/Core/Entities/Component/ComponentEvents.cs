using System;

namespace Coorth {
    public readonly struct EventComponentAdd : ISandboxEvent {
        
        public Sandbox Sandbox => group.Sandbox;
        
        public readonly EntityId Id;
        
        private readonly IComponentGroup group;
        
        private readonly int index;

        public Entity Entity => Sandbox.GetEntity(Id);

        public Type Type => group.Type;
        
        public IComponent Component => group.Get(index);

        public ref T Ref<T>() where T : struct, IComponent => ref ((ComponentGroup<T>)group).Ref(index);
        
        public T Get<T>() where T : struct, IComponent => ((ComponentGroup<T>)group)[index];

        internal EventComponentAdd(EntityId id, IComponentGroup group, int index) {
            this.Id = id;
            this.group = group;
            this.index = index;
        }
    }

    public readonly struct EventComponentAdd<T> : ISandboxEvent where T : IComponent {
        
        public Sandbox Sandbox => group.Sandbox;

        public readonly EntityId Id;
        
        private readonly IComponentGroup group;
        
        private readonly int index;

        public Entity Entity => Sandbox.GetEntity(Id);
        
        public Type Type => group.Type;
        
        public T Component => ((ComponentGroup<T>)group)[index];

        public ref T2 Ref<T2>() where T2 : struct, IComponent => ref ((ComponentGroup<T2>)group).Ref(index);
        
        public T Get() => ((ComponentGroup<T>)group)[index];

        internal EventComponentAdd(EntityId id, IComponentGroup group, int index) {
            this.Id = id;
            this.group = group;
            this.index = index;
        }
    }

    public readonly struct EventComponentModify : ISandboxEvent {
        
        public Sandbox Sandbox => group.Sandbox;
        
        public readonly EntityId Id;
        
        private readonly IComponentGroup group;
        
        private readonly int index;

        public Entity Entity => Sandbox.GetEntity(Id);
        
        public Type Type => group.Type;
        
        public IComponent Component => group.Get(index);

        public ref T Ref<T>() where T : struct, IComponent => ref ((ComponentGroup<T>)group).Ref(index);
        
        public T Get<T>() where T : struct, IComponent => ((ComponentGroup<T>)group)[index];

        internal EventComponentModify(EntityId id, IComponentGroup group, int index) {
            this.Id = id;
            this.group = group;
            this.index = index;
        }
    }

    public readonly struct EventComponentModify<T> : ISandboxEvent where T : IComponent {
        
        public Sandbox Sandbox => group.Sandbox;
        
        public readonly EntityId Id;
        
        private readonly IComponentGroup group;
        
        private readonly int index;

        public Entity Entity => Sandbox.GetEntity(Id);
        
        public Type Type => group.Type;
        
        public T Component => ((ComponentGroup<T>)group)[index];

        public ref T2 Ref<T2>() where T2 : struct, IComponent => ref ((ComponentGroup<T2>)group).Ref(index);
        
        public T Get() => ((ComponentGroup<T>)group)[index];

        internal EventComponentModify(EntityId id, IComponentGroup group, int index) {
            this.Id = id;
            this.group = group;
            this.index = index;
        }
    }

    public readonly struct EventComponentRemove : ISandboxEvent {
        
        public Sandbox Sandbox => group.Sandbox;
        
        public readonly EntityId Id;
        
        private readonly IComponentGroup group;
        
        private readonly int index;

        public Type Type => group.Type;
        
        public Entity Entity => Sandbox.GetEntity(Id);
        
        public IComponent Component => group.Get(index);

        public ref T Ref<T>() where T : struct, IComponent => ref ((ComponentGroup<T>)group).Ref(index);
        
        public T Get<T>() where T : struct, IComponent => ((ComponentGroup<T>)group)[index];

        internal EventComponentRemove(EntityId id, IComponentGroup group, int index) {
            this.Id = id;
            this.group = group;
            this.index = index;
        }
    }

    public readonly struct EventComponentRemove<T> : ISandboxEvent where T : IComponent {
        
        public Sandbox Sandbox => group.Sandbox;
        
        public readonly EntityId Id;
        
        private readonly IComponentGroup group;
        
        private readonly int index;

        public Entity Entity => Sandbox.GetEntity(Id);
        
        public Type Type => group.Type;
        
        public T Component => ((ComponentGroup<T>)group)[index];

        public ref T2 Ref<T2>() where T2 : struct, IComponent => ref ((ComponentGroup<T2>)group).Ref(index);
        
        public T Get() => ((ComponentGroup<T>)group)[index];

        internal EventComponentRemove(EntityId id, IComponentGroup group, int index) {
            this.Id = id;
            this.group = group;
            this.index = index;
        }
    }

}