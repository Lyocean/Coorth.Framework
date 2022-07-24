using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Coorth.Collections;

public struct ValueList<T> : IList<T> {

    private readonly IMemoryAllocator? allocator;
    private Memory<T> values;

    public int Count { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; private set; }

    public bool IsReadOnly => false;

    public int Capacity { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => values.Length; }

    public bool IsNull { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => values.IsEmpty; }
        
    public static readonly ValueList<T> Empty = new() { values = Array.Empty<T>(), Count = 0 };
    
    public Span<T> Span => values.Span[..Count];
    
    public ValueList(int capacity, IMemoryAllocator? alloc = null) {
        if (capacity <= 0) {
            throw new ArgumentException("Capacity must larger than 0");
        }
        allocator = alloc;
        values = alloc?.Alloc<T>(capacity) ?? new T[capacity];
        Count = 0;
    }
    
    public ValueList(Memory<T> memory, IMemoryAllocator? alloc = null) {
        allocator = alloc;
        values = memory;
        Count = 0;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Add() {
        if (Count >= Capacity) {
            Resize(Capacity < 4 ? 4 : Capacity << 1);
        }
        var index = Count;
        Count++;
        return ref values.Span[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(T value) {
        ref var item = ref Add();
        item = value;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Get(int index) => values.Span[index];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Ref(int index) => ref values.Span[index];

    public ref T this[int index] { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ref values.Span[index]; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(int index, T value) => values.Span[index] = value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(T value) => IndexOf(value) >= 0;
    
    public void SwapRemove(int index) {
        var tail = Count - 1;
        var span = values.Span;
        if (index != tail) {
            span[index] = span[tail];
        }
        #nullable disable
        span[tail] = default;
        #nullable restore
        Count--;
    }

    public void CopyTo(T[] array, int arrayIndex) {
        var src = values.Span[..Count];
        var dst = array.AsSpan(arrayIndex, Count);
        src.CopyTo(dst);
    }

    public bool Remove(T item) {
        var index = IndexOf(item);
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
        if (Count >= Capacity) {
            Resize(Capacity < 4 ? 4 : Capacity << 1);
        }
        var span = Span;
        for (var i = Count; i > index; i--) {
            span[i] = span[i - 1];
        }
        Count++;
        span[index] = item;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveAt(int index) {
        var span = values.Span;
        Count--;
        if (index < Count) {
            for (var i = index; i < Count; i++) {
                span[i] = span[i + 1];
            }
        }
#nullable disable
        span[Count] = default;
#nullable restore
    }

    T IList<T>.this[int index] { get => Span[index]; set => Span[index] = value; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveLast() {
        Count--;
#nullable disable
        values.Span[Count] = default;
#nullable restore

    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Pop() {
        Count--;
        var span = values.Span;
        var item = span[Count];
        
        span.Clear();
#nullable disable
        span[Count] = default;
#nullable restore
        return item;
    }

    public void Push(in T item) => Add(item);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int IndexOf(T item) {
        var comparer = EqualityComparer<T>.Default;
        var span = Span;
        for (var i = 0; i < span.Length; i++) {
            if (comparer.Equals(span[i], item)) {
                return i;
            }                  
        }
        return -1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear() {
        Span.Clear();
        Count = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Alloc(int index) {
        if (index < Count) {
            return ref values.Span[index];
        }
        var size = values.Length;
        if (index < size) {
            Count = index + 1;
            return ref values.Span[index];
        }
        size = size == 0 ? 4 : size * 2;
        do {
            size *= 2;
        } while (index >= size);
        Resize(size);
        Count = index + 1;
        return ref values.Span[index];
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
            Resize(size);
        }
        Count++;
        return ref values.Span[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Resize(int size) {
        var memory = allocator?.Alloc<T>(size) ?? new T[size];
        values.Span[..Count].CopyTo(memory.Span[..Count]);
        allocator?.Return(values);
        values = memory;
    }

    public bool Equals(ValueList<T> other) {
        return values.Equals(other.values) && Count == other.Count;
    }

    public override bool Equals(object? obj) {
        return obj is ValueList<T> other && Equals(other);
    }

    public override int GetHashCode() {
        return HashCode.Combine(values, Count);
    }
    
    public struct Enumerator : IEnumerator<T> {

        private readonly ReadOnlyMemory<T> values;

        private readonly int length;

        private int index;

        private T current;

        public T Current => current;

#nullable disable
        object IEnumerator.Current => current;

        public Enumerator(ReadOnlyMemory<T> values, int length) {
            this.values = values;
            this.length = length;
            this.index = 0;
            this.current = default;
        }
#nullable restore

        public bool MoveNext() {
            if (index < length) {
                current = values.Span[index];
                index++;
                return true;
            }
#nullable disable
            current = default;
#nullable restore
            index = length + 1;
            return false;
        }

        public void Reset() {
#nullable disable
            current = default;
#nullable restore
            index = 0;
        }

        public void Dispose() { }
    }

    public Enumerator GetEnumerator() => new(values, Count);

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


}