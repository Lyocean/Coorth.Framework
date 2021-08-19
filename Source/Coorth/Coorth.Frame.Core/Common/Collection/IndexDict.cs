﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Coorth {
    public struct IndexDict<T> : IEnumerable<KeyValuePair<int, T>> {
        
        private Entry[] values;
        
        private int count;

        public int Count => count;

        public int Capacity => values.Length;

        private int bit;

        private int mask;

        public IndexDict(int capacity = 4) {
            this.count = 0;
            this.bit = 1;
            while ((1 << bit) < capacity) {
                bit++;
            }
            this.mask = (1 << bit) - 1;
            this.values = new Entry[1 << bit];
        }

        public T this[int index] {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Get(index);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => Set(index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get(int key) {
            return Ref(key);
        }

        public ref T Ref(int key) {
            //var position = key  % values.Length;
            var position = key & mask;

            do {
                ref var entry = ref values[position];
                if (entry.Key == key + 1) {
                    return ref entry.Value;
                }
                position = entry.Next - 1;
            } while (position >= 0);
            throw new KeyNotFoundException(key.ToString());
        }

        public void Add(int key, T value) {
            if (count >= values.Length) {
                Resize();
            }
            //var position = key % values.Length;
            var position = key & mask;
            ref var entry = ref values[position];
            if (entry.Key == 0) {
                entry.Key = key + 1;
                entry.Value = value;
                count++;
                return;
            }
            if (entry.Key == key + 1) {
                throw new ArgumentException($"Value with key: {key} has existed.");
            }
            while (values[position].Next!=0) {
                position = values[position].Next-1;
            }
            var start = position;
            do {
                position = (position + 1) & mask;
                entry = ref values[position];
                if (entry.Key == 0) {
                    entry.Key = key + 1;
                    entry.Value = value;
                    count++;
                    values[start].Next = position + 1;
                    return;
                }
            } while (position != start);
            throw new ArgumentException();
        }

        private void Set(int key, T value) {
          
            //var position = key % values.Length;
            var position = key & mask;
            ref var entry = ref values[position];
            if (entry.Key == 0) {
                entry.Key = key + 1;
                entry.Value = value;
                count++;
                return;
            }
            if (entry.Key == key + 1) {
                entry.Value = value;
                return;
            }
            while (entry.Next != 0) {
                position = entry.Next - 1;
                entry = ref values[position];
                if (entry.Key == key + 1) {
                    entry.Value = value;
                    return;
                }
            }
            if (count < values.Length) {
                var start = position;
                do {
                    position = (position + 1) & mask;
                    entry = ref values[position];
                    if (entry.Key == 0) {
                        entry.Key = key + 1;
                        entry.Value = value;
                        count++;
                        values[start].Next = position + 1;
                        return;
                    }
                } while (position != start);
            }
            else {
                Add(key, value); 
                return;
            }
            throw new ArgumentException();
        }

        public bool ContainsKey(int key) {
            //var position = key % values.Length;
            var position = key & mask;

            do {
                ref var entry = ref values[position];
                if (entry.Key == key+1) {
                    return true;
                }
                position = entry.Next - 1;
            } while (position >= 0);
            return false;
        }

        public bool TryGetValue(int key, out T value) {
            //var position = key % values.Length;
            var position = key & mask;

            do {
                ref var entry = ref values[position];
                if (entry.Key == key + 1) {
                    value = entry.Value;
                    return true;
                }
                position = entry.Next - 1;
            } while (position >= 0);

            value = default(T);
            return false;
        }

        public bool Remove(int key) {
            //var position = key % values.Length;
            var position = key & mask;

            do {
                ref var entry = ref values[position];
                if (entry.Key == key+1) {
                    if (entry.Next != 0) {
                        position = entry.Next - 1;
                        entry = values[position];
                        values[position] = default;
                    }
                    else {
                        entry = default;
                    }
                    count--;
                    return true;
                }
                position = entry.Next - 1;
            } while (position >= 0);
            return false;
        }

        private void Resize() {
            var length = values.Length;
            var origin = values;
            values = new Entry[length << 1];
            bit++;
            this.mask = (1 << bit) - 1;
            count = 0;
            for (var i = 0; i < length; i++) {
                ref var entry = ref origin[i];
                if (entry.Key != 0) {
                    Add(entry.Key - 1, entry.Value);
                }
            }
        }

        public void Clear() {
            Array.Clear(values, 0, values.Length);
            count = 0;
        }


 

        private struct Entry {
            
            public int Key;
            
            public int Next;
            
            public T Value;
        }



        public IEnumerator<KeyValuePair<int, T>> GetEnumerator() {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public struct Enumerator : IEnumerator<KeyValuePair<int, T>> {

            private IndexDict<T> dict;

            private int index;

            public Enumerator(IndexDict<T> d) {
                this.dict = d;
                this.index = 0;
                this.current = default;
            }

            private KeyValuePair<int, T> current;
            public KeyValuePair<int, T> Current => current;

            object IEnumerator.Current => current;

            public void Dispose() {
                index = 0;
                dict = default;
                current = default;
            }

            public bool MoveNext() {
                while(index < dict.Capacity) {
                    ref var entry = ref dict.values[index];
                    if(entry.Key == 0) {
                        index++;
                        continue;
                    }
                    current = new KeyValuePair<int, T>(entry.Key - 1, entry.Value);
                    index++;
                    return true;
                }
                return false;
            }

            public void Reset() {
                index = 0;
                current = default;
            }
        }
    }
}