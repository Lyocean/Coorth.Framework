using System;

namespace Coorth.ECS {
    public interface IEvent {

    }

    #region Container

    public struct EventBeginInit : IEvent {

    }

    public struct EventEndInit : IEvent {

    }

    public struct EventStepUpdate : IEvent {
        public readonly TimeSpan Span;
    }

    public struct EventTickUpdate : IEvent {
        public readonly TimeSpan Span;
        public float DeltaTime => (float)Span.TotalSeconds;
    }

    public struct EventLateUpdate : IEvent {
        public readonly TimeSpan Span;
    }

    public struct EventDestroy : IEvent {

    }

    #endregion

    #region Entity

    public struct EventEntityAdd : IEvent {
        public readonly EcsContainer Container;
        public readonly EntityId Id;
        public Entity Entity => Container.GetEntity(Id);
        public EntityContext Context => Container.GetContext(Id);

        internal EventEntityAdd(EcsContainer container, EntityId id) {
            this.Container = container;
            this.Id = id;
        }
    }

    public struct EventEntityRemove : IEvent {
        public readonly EcsContainer Container;
        public readonly EntityId Id;
        public Entity Entity => Container.GetEntity(Id);
        public EntityContext Context => Container.GetContext(Id);

        internal EventEntityRemove(EcsContainer container, EntityId id) {
            this.Container = container;
            this.Id = id;
        }
    }

    #endregion

    #region Component

    public struct EventComponentAdd : IEvent {
        public readonly EcsContainer Container;
        public readonly EntityId Id;
        private readonly IComponentGroup group;
        private readonly int index;
        public Entity Entity => Container.GetEntity(Id);
        public EntityContext Context => Container.GetContext(Id);
        public Type Type => group.Type;
        public ref T Ref<T>() where T : struct, IComponent => ref ((ValComponentGroup<T>)group).Ref(index);
        public T Get<T>() where T : struct, IComponent => ((IComponentGroup<T>)group)[index];

        internal EventComponentAdd(EcsContainer container, EntityId id, IComponentGroup group, int index) {
            this.Container = container;
            this.Id = id;
            this.group = group;
            this.index = index;
        }
    }

    public struct EventComponentAdd<T> : IEvent where T : IComponent {
        public readonly EcsContainer Container;
        public readonly EntityId Id;
        private readonly IComponentGroup group;
        private readonly int index;

        public Entity Entity => Container.GetEntity(Id);
        public EntityContext Context => Container.GetContext(Id);
        public Type Type => group.Type;
        public T Component => ((IComponentGroup<T>)group)[index];
        public ref T2 Ref<T2>() where T2 : struct, IComponent => ref ((ValComponentGroup<T2>)group).Ref(index);
        public T Get() => ((IComponentGroup<T>)group)[index];

        internal EventComponentAdd(EcsContainer container, EntityId id, IComponentGroup group, int index) {
            this.Container = container;
            this.Id = id;
            this.group = group;
            this.index = index;
        }
    }

    public struct EventComponentModify : IEvent {
        public readonly EcsContainer Container;
        public readonly EntityId Id;
        private readonly IComponentGroup group;
        private readonly int index;

        public Entity Entity => Container.GetEntity(Id);
        public EntityContext Context => Container.GetContext(Id);
        public Type Type => group.Type;
        public ref T Ref<T>() where T : struct, IComponent => ref ((ValComponentGroup<T>)group).Ref(index);
        public T Get<T>() where T : struct, IComponent => ((IComponentGroup<T>)group)[index];

        internal EventComponentModify(EcsContainer container, EntityId id, IComponentGroup group, int index) {
            this.Container = container;
            this.Id = id;
            this.group = group;
            this.index = index;
        }
    }

    public struct EventComponentModify<T> : IEvent where T : IComponent {
        public readonly EcsContainer Container;
        public readonly EntityId Id;
        private readonly IComponentGroup group;
        private readonly int index;

        public Entity Entity => Container.GetEntity(Id);
        public EntityContext Context => Container.GetContext(Id);
        public Type Type => group.Type;
        public T Component => ((IComponentGroup<T>)group)[index];
        public ref T2 Ref<T2>() where T2 : struct, IComponent => ref ((ValComponentGroup<T2>)group).Ref(index);
        public T Get() => ((IComponentGroup<T>)group)[index];

        internal EventComponentModify(EcsContainer container, EntityId id, IComponentGroup group, int index) {
            this.Container = container;
            this.Id = id;
            this.group = group;
            this.index = index;
        }
    }

    public struct EventComponentRemove : IEvent {
        public readonly EcsContainer Container;
        public readonly EntityId Id;
        private readonly IComponentGroup group;
        private readonly int index;
        public Type Type => group.Type;
        public Entity Entity => Container.GetEntity(Id);
        public EntityContext Context => Container.GetContext(Id);
        public ref T Ref<T>() where T : struct, IComponent => ref ((ValComponentGroup<T>)group).Ref(index);
        public T Get<T>() where T : struct, IComponent => ((IComponentGroup<T>)group)[index];

        internal EventComponentRemove(EcsContainer container, EntityId id, IComponentGroup group, int index) {
            this.Container = container;
            this.Id = id;
            this.group = group;
            this.index = index;
        }
    }

    public struct EventComponentRemove<T> : IEvent where T : IComponent {
        public readonly EcsContainer Container;
        public readonly EntityId Id;
        private readonly IComponentGroup group;
        private readonly int index;

        public Entity Entity => Container.GetEntity(Id);
        public EntityContext Context => Container.GetContext(Id);
        public Type Type => group.Type;
        public T Component => ((IComponentGroup<T>)group)[index];
        public ref T2 Ref<T2>() where T2 : struct, IComponent => ref ((ValComponentGroup<T2>)group).Ref(index);
        public T Get() => ((IComponentGroup<T>)group)[index];

        internal EventComponentRemove(EcsContainer container, EntityId id, IComponentGroup group, int index) {
            this.Container = container;
            this.Id = id;
            this.group = group;
            this.index = index;
        }
    }

    #endregion

    #region System

    public struct EventSystemAdd : IEvent {
        public readonly EcsContainer Container;
        public readonly Type Type;
        public readonly IEcsSystem System;

        public EventSystemAdd(EcsContainer container, Type type, IEcsSystem system) {
            this.Container = container;
            this.Type = type;
            this.System = system;
        }
    }

    public struct EventSystemAdd<T> : IEvent {
        public readonly EcsContainer Container;
        public readonly Type Type;
        public readonly IEcsSystem System;

        public EventSystemAdd(EcsContainer container, Type type, IEcsSystem system) {
            this.Container = container;
            this.Type = type;
            this.System = system;
        }
    }

    public struct EventSystemRemove : IEvent {
        public readonly EcsContainer Container;
        public readonly Type Type;
        public readonly IEcsSystem System;

        public EventSystemRemove(EcsContainer container, Type type, IEcsSystem system) {
            this.Container = container;
            this.Type = type;
            this.System = system;
        }
    }

    public struct EventSystemRemove<T> : IEvent {
        public readonly EcsContainer Container;
        public readonly Type Type;
        public readonly IEcsSystem System;

        public EventSystemRemove(EcsContainer container, Type type, IEcsSystem system) {
            this.Container = container;
            this.Type = type;
            this.System = system;
        }
    }

    #endregion
}
