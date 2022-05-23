using System;
using System.Buffers;
using System.Collections.Generic;

namespace Coorth.Collections;

public ref struct TempList<T>  {
    
    private int count;

    private T[] array;

    private readonly Span<T> span;

    public int Count => count;

    public int Capacity => span.Length;

    public bool IsNull => array == null;

    private TempList(int capacity, int c) {
        array = ArrayPool<T>.Shared.Rent(capacity);
        span = array.AsSpan();
        count = c;
    }

    public TempList(int capacity) : this(capacity, 0) { }

    public TempList(ReadOnlySpan<T> values) : this(values.Length, values.Length) {
        values.CopyTo(span);
    }

    public TempList(IReadOnlyList<T> list) : this(list.Count, list.Count) {
        for (var i = 0; i < list.Count; i++) {
            span[i] = list[i];
        }
    }
        
    public void Add(T value) {
        if (Capacity <= count) {
            Resize(count  + 1);
        }
        span[count] = value;
        count++;
    }

    public void Add(T[] values) {
        var totalLength = count + values.Length;
        if (Capacity < totalLength) {
            Resize(totalLength);
        }
        values.CopyTo(span.Slice(count, values.Length));
        count = totalLength;
    }
        
    public void Add(ReadOnlySpan<T> values) {
        var totalLength = count + values.Length;
        if (Capacity < totalLength) {
            Resize(totalLength);
        }
        values.CopyTo(span.Slice(count, values.Length));
        count = totalLength;
    }

    public T this[int index] {
        get => span[index];
        set => span[index] = value;
    }

    private void Resize(int size) {
        
        var newSize = array.Length * 2;
        while (newSize < size) {
            newSize *= 2;
        }

        T[] newArray = ArrayPool<T>.Shared.Rent(newSize);
        var length = array.Length;
        Array.Copy(array, newArray, length);
        ArrayPool<T>.Shared.Return(array, true);
        array = newArray;
    }
        
    public ReadOnlySpan<T> AsReadOnlySpan() {
        return new ReadOnlySpan<T>(array, 0, count);
    }
        
    public Span<T> AsSpan() {
        return new Span<T>(array, 0, count);
    }
        
    public void Clear() {
        Array.Clear(array, 0, array.Length);
    }

    public void Dispose() {
        ArrayPool<T>.Shared.Return(array, true);
    }
    
}
