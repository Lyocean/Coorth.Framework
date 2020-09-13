using System;
using System.Collections;
using System.Collections.Generic;

namespace Coorth.ECS {
    public interface IGroup {

    }

    public interface IGroup<T> : IGroup, IEnumerable<T> {

    }

    public class EntityGroup : IGroup<EntityId> {

        protected readonly EcsContainer Container;

        private HashSet<EntityId> entities = new HashSet<EntityId>();

        public event Action<EntityId> OnAdd;

        public event Action<EntityId> OnRemove;

        public int Count => entities.Count;

        public bool Contains(Entity entity) => entities.Contains(entity.Id);

        public bool Contains(EntityId id) => entities.Contains(id);


        public readonly IMatcher matcher;

        public EntityGroup(EcsContainer container, IMatcher matcher) {
            this.Container = container;
            this.matcher = matcher;
        }

        internal void Match(ref EntityData data) {
            if (matcher.Match(Container, data.Id)) {
                if (entities.Add(data.Id)) {
                    OnAdd?.Invoke(data.Id);
                }
            }
        }

        internal void Match(ref EntityData data, int typeId, bool isAdd) {
            if (isAdd) {
                if (entities.Contains(data.Id)) {
                    if (matcher.IsExclude(typeId)) {
                        entities.Remove(data.Id);
                        OnRemove?.Invoke(data.Id);
                    }
                } else {
                    if (matcher.Match(Container, data.Id)) {
                        entities.Add(data.Id);
                        OnAdd?.Invoke(data.Id);
                    }
                }
            } else {
                if (entities.Contains(data.Id)) {
                    if (matcher.IsInclude(typeId)) {
                        entities.Remove(data.Id);
                        OnRemove?.Invoke(data.Id);
                    }
                } else {
                    if (matcher.Match(Container, data.Id)) {
                        entities.Add(data.Id);
                        OnAdd?.Invoke(data.Id);
                    }
                }
            }
        }

        public IEnumerator<EntityId> GetEnumerator() {
            return entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public IEnumerable<Entity> GetEntities() {
            foreach(var id in entities) {
                yield return Container.GetEntity(id);
            }
        }

        public void Clear() {
            entities.Clear();
        }
    }
}
