using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Coorth.ECS {

    internal sealed class Components {

        private ChunkList<IComponentGroup> groups;

        public Components(EcsContainer container) {
            groups = new ChunkList<IComponentGroup>(container.config.ComponentsCapacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IComponentGroup GetGroup(int typeId) {
            ref var group = ref groups.Alloc(typeId);
            return group;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IComponentGroup<T> GetGroup<T>(int typeId, EcsContainer container) where T : IComponent {
            ref var group = ref groups.Alloc(typeId);
            if (group == null) {
                if (typeof(T).IsValueType) {
                    group = new ValComponentGroup<T>(container);
                    return (ValComponentGroup<T>)group;

                } else {
                    group = new RefComponentGroup<T>(container);
                    return (RefComponentGroup<T>)group;
                }
            } else {
                return (IComponentGroup<T>)group;
            }
        }

        private static int TypeCount;

        public static ConcurrentDictionary<Type, int> TypeIds = new ConcurrentDictionary<Type, int>();

        public static class Types<T> {
            public static readonly int Id;
            public static readonly Type Type;
            public static Func<EcsContainer, T> Factory;
            public static Func<EcsContainer, T, T> Recycle;

            static Types() {
                Id = Interlocked.Increment(ref TypeCount);
                Type = typeof(T);
                Factory = null;
                Recycle = null;
                TypeIds[typeof(T)] = Id;
            }
        }
    }

    internal interface IComponentGroup {
        int Count { get; }
        Type Type { get; }
        int TypeId { get; }
        bool Remove(int index);
        void OnRemove(ref EntityData data, int index);
    }

    internal interface IComponentGroup<T> : IComponentGroup, IGroup<T> where T : IComponent {
        ref T Ref(int index);
        T this[int index] { get; set; }
        int Add(int index);

    }

    internal abstract class ComponentGroup<T> : IComponentGroup<T> where T : IComponent {

        protected EcsContainer container;

        internal T[] items;
        internal int[] actives;

        protected Stack<int> ids;

        protected int count = 0;
        public int Count => count;

        public Type Type => typeof(T);

        public int TypeId => Components.Types<T>.Id;

        public ComponentGroup(EcsContainer container) {
            this.container = container;
            this.items = new T[container.config.ComponentCapacity];
            this.actives = new int[container.config.ComponentCapacity];
            this.ids = new Stack<int>(container.config.ComponentCapacity);
        }

        public virtual T this[int index] { get => items[index]; set => items[index] = value; }

        public ref T Ref(int index) => ref items[index];

        protected int Alloc(int entityIndex) {
            if (ids.Count > 0) {
                var index = ids.Pop();
                actives[index] = entityIndex;
                if (index >= count) {
                    count = index + 1;
                }
                return index;
            } else {
                if (items.Length <= count) {
                    var size = items.Length << 1;
                    Array.Resize(ref items, size);
                    Array.Resize(ref actives, size);
                }
                var index = count;
                actives[count] = entityIndex;
                count++;
                return index;
            }
        }

        protected void Recycle(int index) {
            ids.Push(index);
            actives[index] = 0;
            if (index == count - 1) {
                while (count > 0 && actives[--count] == 0) {
                }
            }
        }

        public abstract int Add(int index);

        public abstract int Add(int index, T component);

        public abstract bool Remove(int index);

        public IEnumerator<T> GetEnumerator() {
            for(var i=0;i < count; i++) {
                if (actives[i] != 0) {
                    yield return items[i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void OnRemove(ref EntityData data, int index) {
            container.OnComponentRemove<T>(ref data, ref items[index], this, index);
        }
    }

    internal class ValComponentGroup<T> : ComponentGroup<T> where T : IComponent {

        public ValComponentGroup(EcsContainer container) : base(container) { }

        public override int Add(int entityIndex) {
            var index = Alloc(entityIndex);
            var factory = Components.Types<T>.Factory;
            if (factory != null) {
                items[count] = factory(container);
            }
            return index;
        }

        public override int Add(int entityIndex, T component) {
            var index = Alloc(entityIndex);
            items[count] = component;
            return index;
        }


        public override bool Remove(int index) {
            var recycle = Components.Types<T>.Recycle;
            Recycle(index);
            if (recycle != null) {
                items[index] = recycle(container, items[index]);
            } else {
                items[index] = default;
            }
            return false;
        }
    }

    internal class RefComponentGroup<T> : ComponentGroup<T> where T : IComponent {

        private Stack<T> pool = new Stack<T>();

        public RefComponentGroup(EcsContainer container) : base(container) { }

        public override int Add(int entityIndex) {
            var index = Alloc(entityIndex);
            if (pool.Count > 0) {
                items[index] = pool.Pop();
            } else {
                var factory = Components.Types<T>.Factory;
                var value = factory != null ? factory(container) : Activator.CreateInstance<T>();
                items[index] = value;
            }
            return index;
        }

        public override int Add(int entityIndex, T component) {
            var index = Alloc(entityIndex);
            items[count] = component;
            return index;
        }
        public override bool Remove(int index) {
            Recycle(index);
            var recycle = Components.Types<T>.Recycle;
            var component = items[index];
            items[index] = default;
            if (recycle != null) {
                component = recycle(container, component);
            }
            pool.Push(component);
            return (component != null);
        }
    }

    public delegate void ComponentAction<T>(ref T component) where T : IComponent;

    public delegate void ComponentAction<T1, T2>(ref T1 component1, ref T2 component2) where T1 : IComponent where T2 : IComponent;

    public delegate void ComponentAction<T1, T2, T3>(ref T1 component1, ref T2 component2, ref T3 component3) where T1 : IComponent where T2 : IComponent where T3 : IComponent;


}
