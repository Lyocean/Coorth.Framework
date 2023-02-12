using System;
using System.Buffers;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Coorth.Collections;

public ref struct TempList<T> {
    private int count;

    private T[]? array;

    private Span<T> span;

    public int Count => count;

    public int Capacity => span.Length;

    public TempList(int capacity) {
        array = ArrayPool<T>.Shared.Rent(capacity);
        span = array.AsSpan();
        count = 0;
    }

    public TempList(Span<T> value) {
        array = null;
        span = value;
        count = 0;
    }

    public TempList(List<T> list) {
        var size = list.Count;
        array = ArrayPool<T>.Shared.Rent(size);
        span = array.AsSpan();
        count = 0;
#if NET5_0_OR_GREATER
        var value = CollectionsMarshal.AsSpan(list);
        value.CopyTo(span);
#else
        for (var i = 0; i < list.Count; i++) {
            span[i] = list[i];
        }
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(T value) {
        if (Capacity <= count) {
            Resize(count + 1);
        }

        span[count] = value;
        count++;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(T[] values) {
        var totalLength = count + values.Length;
        if (Capacity < totalLength) {
            Resize(totalLength);
        }

        values.CopyTo(span.Slice(count, values.Length));
        count = totalLength;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(ReadOnlySpan<T> values) {
        var totalLength = count + values.Length;
        if (Capacity < totalLength) {
            Resize(totalLength);
        }

        values.CopyTo(span.Slice(count, values.Length));
        count = totalLength;
    }

    public T this[int index] {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => span[index];
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => span[index] = value;
    }


    private void Resize(int size) {
#if NET5_0_OR_GREATER
        var newSize = (int)BitOperations.RoundUpToPowerOf2((uint)(span.Length * 2));
#else
        var newSize = span.Length * 2;
        while (newSize < size) {
            newSize *= 2;
        }
#endif
        var newArray = ArrayPool<T>.Shared.Rent(newSize);
        var length = span.Length;
        span.CopyTo(newArray.AsSpan(0, length));
        span = newArray.AsSpan();
        if (array != null) {
            ArrayPool<T>.Shared.Return(array, true);
        }

        array = newArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<T> AsReadOnlySpan() {
        return new ReadOnlySpan<T>(array, 0, count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<T> AsSpan() {
        return new Span<T>(array, 0, count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear() {
        span.Clear();
    }

    public void Dispose() {
        if (array != null) {
            ArrayPool<T>.Shared.Return(array, true);
            this = default;
        }
    }
}