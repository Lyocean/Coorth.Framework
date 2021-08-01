using System;
using System.Collections;
using System.Collections.Generic;

namespace Coorth {
    public readonly struct ComponentCollection<T> : IEnumerable<T> where T : IComponent {
        
        private readonly ComponentGroup<T> group;

        internal ComponentCollection(Sandbox sandbox) {
            this.group = sandbox.GetComponentGroup<T>();
        }

        internal ComponentCollection(ComponentGroup<T> group) {
            this.group = group;
        }

        public void ForEach(Action<T> action) {
            for(var i = 0; i< group.Count; i++) {
                action(group.components[i]);
            }
        }

        public void ForEach(Action<Entity, T> action) {
            var sandbox = group.Sandbox;
            for(var i = 0; i< group.Count; i++) {
                ref var context = ref sandbox.GetContext(group.mapping[i]);
                action(context.GetEntity(sandbox), group.components[i]);
            }
        }

        public void ForEach<TState>(TState state, Action<TState, T> action) {
            for(var i = 0; i< group.Count; i++) {
                action(state, group.components[i]);
            }
        }

        public void ForEach<TState>(TState state, Action<TState, Entity, T> action) {
            var sandbox = group.Sandbox;
            for(var i = 0; i< group.Count; i++) {
                var index = group.mapping[i];
                ref var context = ref sandbox.GetContext(index);
                action(state, context.GetEntity(sandbox), group.components[i]);
            }
        }

        public struct Enumerator : IEnumerator<T> {
            private readonly ComponentGroup<T> group;

            private int index;

            private T current;

            internal Enumerator(ComponentGroup<T> value) {
                this.group = value;
                index = 0;
                current = default;
            }

            public bool MoveNext() {
                ComponentGroup<T> localGroup = group;
                if (index < localGroup.Count) {
                    current = localGroup.components[index];
                    index++;
                    return true;
                }

                return false;
            }

            public void Reset() {
                index = 0;
                current = default;
            }

            public T Current => current;

            object IEnumerator.Current => Current;

            public void Dispose() {
            }
        }

        public Enumerator GetEnumerator() => new Enumerator(this.group);

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new Enumerator(this.group);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }


    public readonly struct ComponentCollection<T1, T2> : IEnumerable<(T1, T2)> where T1 : IComponent where T2 : IComponent {
        private readonly ComponentGroup<T1> group1;
        private readonly ComponentGroup<T2> group2;

        internal ComponentCollection(Sandbox sandbox) {
            this.group1 = sandbox.GetComponentGroup<T1>();
            this.group2 = sandbox.GetComponentGroup<T2>();
        }

        internal ComponentCollection(ComponentGroup<T1> group1, ComponentGroup<T2> group2) {
            this.group1 = group1;
            this.group2 = group2;
        }

        public void ForEach(Action<T1, T2> action) {
            var sandbox = group1.Sandbox;
            for(var i = 0; i< group1.Count; i++) {
                var component1 = group1.components[i];
                ref var context = ref sandbox.GetContext(group1.mapping[i]);
                var compIndex2 = context.Components[group2.Id];
                var component2 = group2.components[compIndex2];
                action(component1, component2);
            }
        }

        public void ForEach(Action<Entity, T1, T2> action) {
            var sandbox = group1.Sandbox;
            for(var i = 0; i< group1.Count; i++) {
                var component1 = group1.components[i];
                ref var context = ref sandbox.GetContext(group1.mapping[i]);
                var compIndex2 = context.Components[group2.Id];
                var component2 = group2.components[compIndex2];
                action(context.GetEntity(sandbox), component1, component2);
            }
        }

        public void ForEach<TState>(TState state, Action<TState, T1, T2> action) {
            var sandbox = group1.Sandbox;
            for(var i = 0; i< group1.Count; i++) {
                var component1 = group1.components[i];
                ref var context = ref sandbox.GetContext(group1.mapping[i]);
                var compIndex2 = context.Components[group2.Id];
                var component2 = group2.components[compIndex2];
                action(state, component1, component2);
            }
        }

        public void ForEach<TState>(TState state, Action<TState, Entity, T1, T2> action) {
            var sandbox = group1.Sandbox;
            for(var i = 0; i< group1.Count; i++) {
                var component1 = group1.components[i];
                ref var context = ref sandbox.GetContext(group1.mapping[i]);
                var compIndex2 = context.Components[group2.Id];
                var component2 = group2.components[compIndex2];
                action(state, context.GetEntity(sandbox), component1, component2);
            }
        }

        public Enumerator GetEnumerator() => new Enumerator(group1, group2);

        IEnumerator<(T1, T2)> IEnumerable<(T1, T2)>.GetEnumerator() => new Enumerator(group1, group2);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public struct Enumerator : IEnumerator<(T1, T2)> {
            private readonly ComponentGroup<T1> group1;
            private readonly ComponentGroup<T2> group2;
            private int index;

            private (T1, T2) current;

            internal Enumerator(ComponentGroup<T1> group1, ComponentGroup<T2> group2) {
                this.group1 = group1;
                this.group2 = group2;
                index = 0;
                current = default;
            }

            public bool MoveNext() {
                ComponentGroup<T1> localGroup1 = this.group1;
                ComponentGroup<T2> localGroup2 = this.group2;
                if (index < localGroup1.Count) {
                    var component1 = localGroup1.components[index];
                    ref var context = ref localGroup1.Sandbox.GetContext(localGroup1.mapping[index]);
                    var compIndex2 = context.Components[localGroup2.Id];
                    var component2 = localGroup2.components[compIndex2];
                    current = (component1, component2);
                    index++;
                    return true;
                }

                return false;
            }

            public void Reset() {
                index = 0;
                current = default;
            }

            public (T1, T2) Current => current;

            object IEnumerator.Current => Current;

            public void Dispose() {
            }
        }
    }

    public readonly struct ComponentCollection<T1, T2, T3> : IEnumerable<(T1, T2, T3)> where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        private readonly ComponentGroup<T1> group1;
        private readonly ComponentGroup<T2> group2;
        private readonly ComponentGroup<T3> group3;

        internal ComponentCollection(Sandbox sandbox) {
            this.group1 = sandbox.GetComponentGroup<T1>();
            this.group2 = sandbox.GetComponentGroup<T2>();
            this.group3 = sandbox.GetComponentGroup<T3>();
        }

        internal ComponentCollection(ComponentGroup<T1> group1, ComponentGroup<T2> group2, ComponentGroup<T3> group3) {
            this.group1 = group1;
            this.group2 = group2;
            this.group3 = group3;
        }

        public void ForEach(Action<T1, T2, T3> action) {
            var sandbox = group1.Sandbox;
            for(var i = 0; i< group1.Count; i++) {
                var component1 = group1.components[i];
                ref var context = ref sandbox.GetContext(group1.mapping[i]);
                var compIndex2 = context.Components[group2.Id];
                var component2 = group2.components[compIndex2];
                var compIndex3 = context.Components[group3.Id];
                var component3 = group3.components[compIndex3];
                action(component1, component2, component3);
            }
        }

        public void ForEach(Action<Entity, T1, T2, T3> action) {
            var sandbox = group1.Sandbox;
            for(var i = 0; i< group1.Count; i++) {
                var component1 = group1.components[i];
                ref var context = ref sandbox.GetContext(group1.mapping[i]);
                var compIndex2 = context.Components[group2.Id];
                var component2 = group2.components[compIndex2];
                var compIndex3 = context.Components[group3.Id];
                var component3 = group3.components[compIndex3];
                action(context.GetEntity(sandbox), component1, component2, component3);
            }
        }

        public void ForEach<TState>(TState state, Action<TState, T1, T2, T3> action) {
            var sandbox = group1.Sandbox;
            for(var i = 0; i< group1.Count; i++) {
                var component1 = group1.components[i];
                ref var context = ref sandbox.GetContext(group1.mapping[i]);
                var compIndex2 = context.Components[group2.Id];
                var component2 = group2.components[compIndex2];
                var compIndex3 = context.Components[group3.Id];
                var component3 = group3.components[compIndex3];
                action(state, component1, component2, component3);
            }
        }

        public void ForEach<TState>(TState state, Action<TState, Entity, T1, T2, T3> action) {
            var sandbox = group1.Sandbox;
            for(var i = 0; i< group1.Count; i++) {
                var component1 = group1.components[i];
                ref var context = ref sandbox.GetContext(group1.mapping[i]);
                var compIndex2 = context.Components[group2.Id];
                var component2 = group2.components[compIndex2];
                var compIndex3 = context.Components[group3.Id];
                var component3 = group3.components[compIndex3];
                action(state, context.GetEntity(sandbox), component1, component2, component3);
            }
        }

        public Enumerator GetEnumerator() => new Enumerator(group1, group2, group3);

        IEnumerator<(T1, T2, T3)> IEnumerable<(T1, T2, T3)>.GetEnumerator() => new Enumerator(group1, group2, group3);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public struct Enumerator : IEnumerator<(T1, T2, T3)> {
            private readonly ComponentGroup<T1> group1;
            private readonly ComponentGroup<T2> group2;
            private readonly ComponentGroup<T3> group3;
            private int index;

            private (T1, T2, T3) current;

            internal Enumerator(ComponentGroup<T1> group1, ComponentGroup<T2> group2, ComponentGroup<T3> group3) {
                this.group1 = group1;
                this.group2 = group2;
                this.group3 = group3;
                index = 0;
                current = default;
            }

            public bool MoveNext() {
                ComponentGroup<T1> localGroup1 = this.group1;
                ComponentGroup<T2> localGroup2 = this.group2;
                ComponentGroup<T3> localGroup3 = this.group3;
                if (index < localGroup1.Count) {
                    var component1 = localGroup1.components[index];
                    ref var context = ref localGroup1.Sandbox.GetContext(localGroup1.mapping[index]);
                    var compIndex2 = context.Components[localGroup2.Id];
                    var component2 = localGroup2.components[compIndex2];
                    var compIndex3 = context.Components[localGroup3.Id];
                    var component3 = localGroup3.components[compIndex3];
                    current = (component1, component2, component3);
                    index++;
                    return true;
                }

                return false;
            }

            public void Reset() {
                index = 0;
                current = default;
            }

            public (T1, T2, T3) Current => current;

            object IEnumerator.Current => Current;

            public void Dispose() {
            }
        }
    }

    public readonly struct ComponentCollection<T1, T2, T3, T4> : IEnumerable<(T1, T2, T3, T4)> where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        private readonly ComponentGroup<T1> group1;
        private readonly ComponentGroup<T2> group2;
        private readonly ComponentGroup<T3> group3;
        private readonly ComponentGroup<T4> group4;

        internal ComponentCollection(Sandbox sandbox) {
            this.group1 = sandbox.GetComponentGroup<T1>();
            this.group2 = sandbox.GetComponentGroup<T2>();
            this.group3 = sandbox.GetComponentGroup<T3>();
            this.group4 = sandbox.GetComponentGroup<T4>();
        }

        internal ComponentCollection(ComponentGroup<T1> group1, ComponentGroup<T2> group2, ComponentGroup<T3> group3, ComponentGroup<T4> group4) {
            this.group1 = group1;
            this.group2 = group2;
            this.group3 = group3;
            this.group4 = group4;
        }

        public void ForEach(Action<T1, T2, T3, T4> action) {
            var sandbox = group1.Sandbox;
            for(var i = 0; i< group1.Count; i++) {
                var component1 = group1.components[i];
                ref var context = ref sandbox.GetContext(group1.mapping[i]);
                var compIndex2 = context.Components[group2.Id];
                var component2 = group2.components[compIndex2];
                var compIndex3 = context.Components[group3.Id];
                var component3 = group3.components[compIndex3];
                var compIndex4 = context.Components[group4.Id];
                var component4 = group4.components[compIndex4];
                action(component1, component2, component3, component4);
            }
        }

        public void ForEach(Action<Entity, T1, T2, T3, T4> action) {
            var sandbox = group1.Sandbox;
            for(var i = 0; i< group1.Count; i++) {
                var component1 = group1.components[i];
                ref var context = ref sandbox.GetContext(group1.mapping[i]);
                var compIndex2 = context.Components[group2.Id];
                var component2 = group2.components[compIndex2];
                var compIndex3 = context.Components[group3.Id];
                var component3 = group3.components[compIndex3];
                var compIndex4 = context.Components[group4.Id];
                var component4 = group4.components[compIndex4];
                action(context.GetEntity(sandbox), component1, component2, component3, component4);
            }
        }

        public void ForEach<TState>(TState state, Action<TState, T1, T2, T3, T4> action) {
            var sandbox = group1.Sandbox;
            for(var i = 0; i< group1.Count; i++) {
                var component1 = group1.components[i];
                ref var context = ref sandbox.GetContext(group1.mapping[i]);
                var compIndex2 = context.Components[group2.Id];
                var component2 = group2.components[compIndex2];
                var compIndex3 = context.Components[group3.Id];
                var component3 = group3.components[compIndex3];
                var compIndex4 = context.Components[group4.Id];
                var component4 = group4.components[compIndex4];
                action(state, component1, component2, component3, component4);
            }
        }

        public void ForEach<TState>(TState state, Action<TState, Entity, T1, T2, T3, T4> action) {
            var sandbox = group1.Sandbox;
            for(var i = 0; i< group1.Count; i++) {
                var component1 = group1.components[i];
                ref var context = ref sandbox.GetContext(group1.mapping[i]);
                var compIndex2 = context.Components[group2.Id];
                var component2 = group2.components[compIndex2];
                var compIndex3 = context.Components[group3.Id];
                var component3 = group3.components[compIndex3];
                var compIndex4 = context.Components[group4.Id];
                var component4 = group4.components[compIndex4];
                action(state, context.GetEntity(sandbox), component1, component2, component3, component4);
            }
        }

        public Enumerator GetEnumerator() => new Enumerator(group1, group2, group3, group4);

        IEnumerator<(T1, T2, T3, T4)> IEnumerable<(T1, T2, T3, T4)>.GetEnumerator() => new Enumerator(group1, group2, group3, group4);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public struct Enumerator : IEnumerator<(T1, T2, T3, T4)> {
            private readonly ComponentGroup<T1> group1;
            private readonly ComponentGroup<T2> group2;
            private readonly ComponentGroup<T3> group3;
            private readonly ComponentGroup<T4> group4;
            private int index;

            private (T1, T2, T3, T4) current;

            internal Enumerator(ComponentGroup<T1> group1, ComponentGroup<T2> group2, ComponentGroup<T3> group3, ComponentGroup<T4> group4) {
                this.group1 = group1;
                this.group2 = group2;
                this.group3 = group3;
                this.group4 = group4;
                index = 0;
                current = default;
            }

            public bool MoveNext() {
                ComponentGroup<T1> localGroup1 = this.group1;
                ComponentGroup<T2> localGroup2 = this.group2;
                ComponentGroup<T3> localGroup3 = this.group3;
                ComponentGroup<T4> localGroup4 = this.group4;
                if (index < localGroup1.Count) {
                    var component1 = localGroup1.components[index];
                    ref var context = ref localGroup1.Sandbox.GetContext(localGroup1.mapping[index]);
                    var compIndex2 = context.Components[localGroup2.Id];
                    var component2 = localGroup2.components[compIndex2];
                    var compIndex3 = context.Components[localGroup3.Id];
                    var component3 = localGroup3.components[compIndex3];
                    var compIndex4 = context.Components[localGroup4.Id];
                    var component4 = localGroup4.components[compIndex4];
                    current = (component1, component2, component3, component4);
                    index++;
                    return true;
                }

                return false;
            }

            public void Reset() {
                index = 0;
                current = default;
            }

            public (T1, T2, T3, T4) Current => current;

            object IEnumerator.Current => Current;

            public void Dispose() {
            }
        }
    }

    public readonly struct ComponentCollection<T1, T2, T3, T4, T5> : IEnumerable<(T1, T2, T3, T4, T5)> where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        private readonly ComponentGroup<T1> group1;
        private readonly ComponentGroup<T2> group2;
        private readonly ComponentGroup<T3> group3;
        private readonly ComponentGroup<T4> group4;
        private readonly ComponentGroup<T5> group5;

        internal ComponentCollection(Sandbox sandbox) {
            this.group1 = sandbox.GetComponentGroup<T1>();
            this.group2 = sandbox.GetComponentGroup<T2>();
            this.group3 = sandbox.GetComponentGroup<T3>();
            this.group4 = sandbox.GetComponentGroup<T4>();
            this.group5 = sandbox.GetComponentGroup<T5>();
        }

        internal ComponentCollection(ComponentGroup<T1> group1, ComponentGroup<T2> group2, ComponentGroup<T3> group3, ComponentGroup<T4> group4, ComponentGroup<T5> group5) {
            this.group1 = group1;
            this.group2 = group2;
            this.group3 = group3;
            this.group4 = group4;
            this.group5 = group5;
        }

        public void ForEach(Action<T1, T2, T3, T4, T5> action) {
            var sandbox = group1.Sandbox;
            for(var i = 0; i< group1.Count; i++) {
                var component1 = group1.components[i];
                ref var context = ref sandbox.GetContext(group1.mapping[i]);
                var compIndex2 = context.Components[group2.Id];
                var component2 = group2.components[compIndex2];
                var compIndex3 = context.Components[group3.Id];
                var component3 = group3.components[compIndex3];
                var compIndex4 = context.Components[group4.Id];
                var component4 = group4.components[compIndex4];
                var compIndex5 = context.Components[group5.Id];
                var component5 = group5.components[compIndex5];
                action(component1, component2, component3, component4, component5);
            }
        }

        public void ForEach(Action<Entity, T1, T2, T3, T4, T5> action) {
            var sandbox = group1.Sandbox;
            for(var i = 0; i< group1.Count; i++) {
                var component1 = group1.components[i];
                ref var context = ref sandbox.GetContext(group1.mapping[i]);
                var compIndex2 = context.Components[group2.Id];
                var component2 = group2.components[compIndex2];
                var compIndex3 = context.Components[group3.Id];
                var component3 = group3.components[compIndex3];
                var compIndex4 = context.Components[group4.Id];
                var component4 = group4.components[compIndex4];
                var compIndex5 = context.Components[group5.Id];
                var component5 = group5.components[compIndex5];
                action(context.GetEntity(sandbox), component1, component2, component3, component4, component5);
            }
        }

        public void ForEach<TState>(TState state, Action<TState, T1, T2, T3, T4, T5> action) {
            var sandbox = group1.Sandbox;
            for(var i = 0; i< group1.Count; i++) {
                var component1 = group1.components[i];
                ref var context = ref sandbox.GetContext(group1.mapping[i]);
                var compIndex2 = context.Components[group2.Id];
                var component2 = group2.components[compIndex2];
                var compIndex3 = context.Components[group3.Id];
                var component3 = group3.components[compIndex3];
                var compIndex4 = context.Components[group4.Id];
                var component4 = group4.components[compIndex4];
                var compIndex5 = context.Components[group5.Id];
                var component5 = group5.components[compIndex5];
                action(state, component1, component2, component3, component4, component5);
            }
        }

        public void ForEach<TState>(TState state, Action<TState, Entity, T1, T2, T3, T4, T5> action) {
            var sandbox = group1.Sandbox;
            for(var i = 0; i< group1.Count; i++) {
                var component1 = group1.components[i];
                ref var context = ref sandbox.GetContext(group1.mapping[i]);
                var compIndex2 = context.Components[group2.Id];
                var component2 = group2.components[compIndex2];
                var compIndex3 = context.Components[group3.Id];
                var component3 = group3.components[compIndex3];
                var compIndex4 = context.Components[group4.Id];
                var component4 = group4.components[compIndex4];
                var compIndex5 = context.Components[group5.Id];
                var component5 = group5.components[compIndex5];
                action(state, context.GetEntity(sandbox), component1, component2, component3, component4, component5);
            }
        }

        public Enumerator GetEnumerator() => new Enumerator(group1, group2, group3, group4, group5);

        IEnumerator<(T1, T2, T3, T4, T5)> IEnumerable<(T1, T2, T3, T4, T5)>.GetEnumerator() => new Enumerator(group1, group2, group3, group4, group5);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public struct Enumerator : IEnumerator<(T1, T2, T3, T4, T5)> {
            private readonly ComponentGroup<T1> group1;
            private readonly ComponentGroup<T2> group2;
            private readonly ComponentGroup<T3> group3;
            private readonly ComponentGroup<T4> group4;
            private readonly ComponentGroup<T5> group5;
            private int index;

            private (T1, T2, T3, T4, T5) current;

            internal Enumerator(ComponentGroup<T1> group1, ComponentGroup<T2> group2, ComponentGroup<T3> group3, ComponentGroup<T4> group4, ComponentGroup<T5> group5) {
                this.group1 = group1;
                this.group2 = group2;
                this.group3 = group3;
                this.group4 = group4;
                this.group5 = group5;
                index = 0;
                current = default;
            }

            public bool MoveNext() {
                ComponentGroup<T1> localGroup1 = this.group1;
                ComponentGroup<T2> localGroup2 = this.group2;
                ComponentGroup<T3> localGroup3 = this.group3;
                ComponentGroup<T4> localGroup4 = this.group4;
                ComponentGroup<T5> localGroup5 = this.group5;
                if (index < localGroup1.Count) {
                    var component1 = localGroup1.components[index];
                    ref var context = ref localGroup1.Sandbox.GetContext(localGroup1.mapping[index]);
                    var compIndex2 = context.Components[localGroup2.Id];
                    var component2 = localGroup2.components[compIndex2];
                    var compIndex3 = context.Components[localGroup3.Id];
                    var component3 = localGroup3.components[compIndex3];
                    var compIndex4 = context.Components[localGroup4.Id];
                    var component4 = localGroup4.components[compIndex4];
                    var compIndex5 = context.Components[localGroup5.Id];
                    var component5 = localGroup5.components[compIndex5];
                    current = (component1, component2, component3, component4, component5);
                    index++;
                    return true;
                }

                return false;
            }

            public void Reset() {
                index = 0;
                current = default;
            }

            public (T1, T2, T3, T4, T5) Current => current;

            object IEnumerator.Current => Current;

            public void Dispose() {
            }
        }
    }

    public static class ComponentCollectionExtension {

        public static ComponentCollection<T1, T2> GetComponents<T1, T2>(this Sandbox sandbox)  where T1 : IComponent where T2 : IComponent {
            return new ComponentCollection<T1, T2>(sandbox);
        }   

        public static ComponentCollection<T1, T2, T3> GetComponents<T1, T2, T3>(this Sandbox sandbox)  where T1 : IComponent where T2 : IComponent where T3 : IComponent {
            return new ComponentCollection<T1, T2, T3>(sandbox);
        }   

        public static ComponentCollection<T1, T2, T3, T4> GetComponents<T1, T2, T3, T4>(this Sandbox sandbox)  where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
            return new ComponentCollection<T1, T2, T3, T4>(sandbox);
        }   

        public static ComponentCollection<T1, T2, T3, T4, T5> GetComponents<T1, T2, T3, T4, T5>(this Sandbox sandbox)  where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
            return new ComponentCollection<T1, T2, T3, T4, T5>(sandbox);
        }   
    }
}