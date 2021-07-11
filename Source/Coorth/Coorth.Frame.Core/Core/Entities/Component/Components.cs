using System.Collections;
using System.Collections.Generic;

namespace Coorth {
    public readonly struct ComponentCollection<T> : IEnumerable<T> where T : IComponent {
        
        private readonly ComponentGroup<T> group;

        internal ComponentCollection(ComponentGroup<T> group) {
            this.group = group;
        }
        
        public struct Enumerator : IEnumerator<T>, IEnumerator {
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
}