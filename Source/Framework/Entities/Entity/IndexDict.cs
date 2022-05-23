using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Coorth.Framework; 

public struct IndexDict<T> : IEnumerable<KeyValuePair<int, T>> where T: unmanaged {
    public struct Entry {
        public int Key;
        public int Next;
        public T Value;
    }
    
    private Memory<Entry> data;
    private Span<Entry> Span { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => data.Span; }

    private int count;
    public int Count => count;

    private int capacity;
    public int Capacity => capacity;
        
    public IndexDict(int size = 4) {
        count = 0;
        capacity = 2;
        while (capacity < size) {
            capacity <<= 1;
        }
        data = new Memory<Entry>(new Entry[capacity]);
    }
        
    public IndexDict(ArraySegment<Entry> segment) {
        count = 0;
        capacity = segment.Count;
        data = segment;
    }

    public T this[int index] {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Get(index); 
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set => Set(index, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Get(int key) => Ref(key);

    public ref T Ref(int key) {
        //var position = key  % values.Length;
        var position = key & (capacity - 1);
        do {
            ref var entry = ref Span[position];
            if (entry.Key == key + 1) {
                return ref entry.Value;
            }
            position = entry.Next - 1;
        } while (position >= 0);
        throw new KeyNotFoundException(key.ToString());
    }

    public void Add(int key, T value) {
        if (count >= capacity) {
            Resize();
        }
        //var position = key % values.Length;
        var position = key & (capacity - 1);
        ref var entry = ref Span[position];
        if (entry.Key == 0) {
            entry.Key = key + 1;
            entry.Value = value;
            count++;
            return;
        }
        if (entry.Key == key + 1) {
            throw new ArgumentException($"Value with key: {key} has existed.");
        }
        while (Span[position].Next!=0) {
            position = Span[position].Next-1;
        }
        var start = position;
        do {
            position = (position + 1) & (capacity - 1);
            entry = ref Span[position];
            if (entry.Key == 0) {
                entry.Key = key + 1;
                entry.Value = value;
                count++;
                Span[start].Next = position + 1;
                return;
            }
        } while (position != start);
        throw new ArgumentException();
    }

    private void Set(int key, T value) {
        //var position = key % values.Length;
        var position = key & (capacity - 1);
        ref var entry = ref Span[position];
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
            entry = ref Span[position];
            if (entry.Key == key + 1) {
                entry.Value = value;
                return;
            }
        }
        if (count < capacity) {
            var start = position;
            do {
                position = (position + 1) & (capacity - 1);
                entry = ref Span[position];
                if (entry.Key == 0) {
                    entry.Key = key + 1;
                    entry.Value = value;
                    count++;
                    Span[start].Next = position + 1;
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
        var position = key & (capacity - 1);
        do {
            ref var entry = ref Span[position];
            if (entry.Key == key+1) {
                return true;
            }
            position = entry.Next - 1;
        } while (position >= 0);
        return false;
    }

    public bool TryGetValue(int key, out T value) {
        //var position = key % values.Length;
        var position = key & (capacity - 1);
        do {
            ref var entry = ref Span[position];
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
        var position = key & (capacity - 1);
        do {
            ref var entry = ref Span[position];
            if (entry.Key == key+1) {
                if (entry.Next != 0) {
                    position = entry.Next - 1;
                    entry = Span[position];
                    Span[position] = default;
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
        var length = capacity;
        var origin = Span;

        capacity <<= 1;
        data = new Memory<Entry>(new Entry[capacity]);
        count = 0;
        for (var i = 0; i < length; i++) {
            ref var entry = ref origin[i];
            if (entry.Key != 0) {
                Add(entry.Key - 1, entry.Value);
            }
        }
    }

    public void Clear() {
        Span.Clear();
        count = 0;
    }

    public Enumerator GetEnumerator() => new (this);

    IEnumerator<KeyValuePair<int, T>> IEnumerable<KeyValuePair<int, T>>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator : IEnumerator<KeyValuePair<int, T>> {

        private IndexDict<T> dict;

        private int index;

        public Enumerator(IndexDict<T> d) {
            dict = d;
            index = 0;
            current = default;
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
                ref var entry = ref dict.Span[index];
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