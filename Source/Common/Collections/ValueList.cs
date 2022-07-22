using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Coorth.Collections;

// #nullable disable

public struct ValueList<T> : IList<T> {
    
    private T?[] values;

    public int Count { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; private set; }

    public bool IsReadOnly => false;

    public int Capacity { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => values.Length; }

    public bool IsNull { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => values == null; }
        
    public static readonly ValueList<T> Empty = new() { values = Array.Empty<T>(), Count = 0 };

    public Span<T> Span => values.AsSpan();
    
    public ValueList(int capacity) {
        if (capacity <= 0) {
            throw new ArgumentException("Capacity must larger than 0");
        }
        values = new T[capacity];
        Count = 0;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Add() {
        if (Count == Capacity) {
            var size = Capacity == 0 ? 4 : Capacity << 1;
            Array.Resize(ref values, size);
        }
        var index = Count;
        Count++;
        return ref values[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(T value) {
        ref var item = ref Add();
        item = value;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Get(int index) => values[index];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Ref(int index) => ref values[index];

    public ref T this[int index] { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ref values[index]; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(int index, T value) { ref var item = ref Ref(index); item = value; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(T value) => values.Contains(value);
    
    public void SwapRemove(int index) {
        var tail = Count - 1;
        if (index != tail) {
            values[index] = values[tail];
        }
        values[tail] = default;
        Count--;
    }

    public void CopyTo(T[] array, int arrayIndex) {
        var src = values.AsSpan(0, Count);
        var dst = array.AsSpan(arrayIndex, Count);
        src.CopyTo(dst);
    }

    public bool Remove(T item) {
        var index = Array.IndexOf(values, item, 0, values.Length);
        if (index >= 0) {
            RemoveAt(index);
            Count--;
            return true;
        }
        return false;
        
    }
    
    public void Insert(int index, T item) {
        if (index < 0 || index > Count) {
            throw new IndexOutOfRangeException();
        }
        if (Count == Capacity) {
            var size = Capacity == 0 ? 4 : Capacity << 1;
            Array.Resize(ref values, size);
        }
        for (var i = Count; i > index; i--) {
            values[i] = values[i - 1];
        }
        Count++;
        values[index] = item;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveAt(int index) {
        Count--;
        if (index < Count) {
            Array.Copy(values, index + 1, values, index, Count - index);
        }
        values[Count] = default;
    }

    T IList<T>.this[int index] { get => values[index]; set => values[index] = value; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveLast() {
        Count--;
        values[Count] = default;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int IndexOf(T item) => Array.IndexOf(values, item, 0, Count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear() {
        Array.Clear(values, 0, Count);
        Count = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Alloc(int index) {
        if (index < Count) {
            return ref values[index];
        }
        var size = values.Length;
        if (index < size) {
            Count = index + 1;
            return ref values[index];
        }
        size = size == 0 ? 4 : size * 2;
        do {
            size *= 2;
        } while (index >= size);
        Array.Resize(ref values, size);
        Count = index + 1;
        return ref values[index];
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Alloc(int index, int capacity) {
        if (index >= capacity) {
            throw new ArgumentException("index must less than capacity.");
        }
        var size = Capacity;
        if (index >= size) {
            size = size == 0 ? 4 : Math.Min(size * 2, capacity);
            do {
                size = Math.Min(size * 2, capacity);
            } while (index >= size);
            Array.Resize(ref values, size);
        }
        Count++;
        return ref values[index];
    }

    public struct Enumerator : IEnumerator<T> {

        private readonly T[] values;

        private readonly int length;

        private int index;

        private T current;

        public T Current => current;

        object IEnumerator.Current => current;

        public Enumerator(T[] values, int length) {
            this.values = values;
            this.length = length;
            this.index = 0;
            this.current = default;
        }
            
        public bool MoveNext() {
            if (index < length) {
                current = values[index];
                index++;
                return true;
            }
            current = default;
            index = length + 1;
            return false;
        }

        public void Reset() {
            current = default;
            index = 0;
        }

        public void Dispose() { }
    }

    public Enumerator GetEnumerator() => new(values, Count);

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}