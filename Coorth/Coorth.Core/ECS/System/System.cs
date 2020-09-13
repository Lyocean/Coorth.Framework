using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Coorth.ECS {

    #region System

    public interface IEcsSystem {
        EcsContainer Container { get; set; }
    }

    public interface ISystemAdd {
        void OnAdd();
    }

    public interface ISystemRemove {
        void OnRemove();
    }

    public class EcsSystem : IEcsSystem {
        public EcsContainer Container { get; set; }
    }

    #endregion

    #region EventSystem

    public interface IEventSystem {

    }

    public interface IEventSystem<T> : IEventSystem where T : IEvent {
        void Execute(T evt);
    }

    public interface IAsyncEventSystem<T> : IEventSystem where T : IEvent {
        Task Execute(T evt);
    }

    public class ActionSystem<T> : EcsSystem, IEventSystem<T> where T : IEvent {

        protected readonly Action<T> action;

        public ActionSystem(Action<T> action) {
            this.action = action;
        }

        public void Execute(T evt) {
            this.action(evt);
        }
    }

    #endregion

    #region ReactiveSystem

    public abstract class ReactiveSystem : EcsSystem, ISystemAdd, ISystemRemove {
        [Flags]
        protected enum MatchTypes {
            OnAdd = 1,
            OnRemove = 1 << 2,
        }

        protected EntityGroup group;

        protected IMatcher matcher;

        protected bool isActive = false;

        protected abstract IMatcher GetMatcher();


        protected virtual EntityGroup GetGroup() {
            matcher = GetMatcher();
            return Container.GetGroup(matcher);
        }

        public virtual void OnAdd() {
            group = GetGroup();
            SetActive(true);
        }

        public virtual void OnRemove() {
            SetActive(false);
            Container.RemoveGroup(group);
        }

        public void SetActive(bool active) {
            if (isActive = active) {
                return;
            }
            isActive = active;
            if (isActive) {
                group.OnAdd += OnEntityAdd;
                group.OnRemove += OnEntityRemove;
            } else {
                group.OnAdd -= OnEntityAdd;
                group.OnRemove -= OnEntityRemove;
            }
        }

        protected abstract void OnEntityAdd(EntityId id);

        protected abstract void OnEntityRemove(EntityId id);
    }

    public abstract class ReactiveSystem<T> : ReactiveSystem, ISystemAdd, ISystemRemove, IEventSystem<T> where T : IEvent {

        private List<EntityId> entities = new List<EntityId>(DEFAULT_CAPACITY);


        private const int DEFAULT_CAPACITY = 16;

        protected abstract MatchTypes GetMatchType();

        protected override void OnEntityAdd(EntityId id) {
            if (GetMatchType().HasFlag(MatchTypes.OnAdd)) {
                entities.Add(id);
            }
        }

        protected override void OnEntityRemove(EntityId id) {
            if (GetMatchType().HasFlag(MatchTypes.OnRemove)) {
                entities.Add(id);
            }
        }

        public void Execute(T evt) {
            if (entities.Count > 0) {
                OnExecute();
                entities.Clear();
            }
        }

        protected abstract void OnExecute();
    }

    #endregion

    #region ComponentSystem

    public abstract class ComponentSystem<T> : EcsSystem,
                                        IEventSystem<EventComponentAdd<T>>,
                                        IEventSystem<EventComponentModify<T>>,
                                        IEventSystem<EventComponentRemove<T>> where T : IComponent {
        public virtual void Execute(EventComponentAdd<T> evt) {
        }

        public virtual void Execute(EventComponentModify<T> evt) {
        }

        public virtual void Execute(EventComponentRemove<T> evt) {
        }
    }

    #endregion
}
