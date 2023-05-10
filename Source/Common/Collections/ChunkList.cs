using System;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Coorth.Collections;

public struct ChunkList<T> {
    
    private ValueList<ValueList<T>> chunks;

    private readonly int chunkCapacity;

    public int Count;

    private int ChunkCount {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => chunks.Count;
    }

    private int Capacity {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ChunkCount * chunkCapacity;
    }

    public ChunkList(int indexCapacity, int chunkCapacity) {
        Debug.Assert(indexCapacity > 0 && chunkCapacity > 0);
#if NET5_0_OR_GREATER
        indexCapacity = ((indexCapacity - 1 + 4) >>> 2) << 2;
        chunkCapacity = ((chunkCapacity - 1 + 4) >>> 2) << 2;
#else
        indexCapacity = ((indexCapacity - 1 + 4) >>> 2) << 2;
        chunkCapacity = ((chunkCapacity - 1 + 4) >>> 2) << 2;
#endif

        this.chunkCapacity = chunkCapacity;
        this.chunks = new ValueList<ValueList<T>>(indexCapacity);
        this.chunks.Add(new ValueList<T>(this.chunkCapacity));
        this.Count = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Add() {
        var index = Count;
        int chunkIndex = index / chunkCapacity;
        if (chunkIndex >= ChunkCount) {
            chunks.Add(new ValueList<T>(chunkCapacity));
        }

        ref var chunk = ref chunks[chunkIndex];
        Count++;
        return ref chunk.Add();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(T value) {
        ref var item = ref Add();
        item = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Ref(int index) {
        int chunkIndex = index / chunkCapacity;
        int itemIndex = index % chunkCapacity;
        ref var chunk = ref chunks[chunkIndex];
        if (chunk.IsNull) {
            chunk = new ValueList<T>(chunkCapacity);
        }

        return ref chunk[itemIndex];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Get(int index) {
        int chunkIndex = index / chunkCapacity;
        //int itemIndex = index % chunkCapacity;
        ref var chunk = ref chunks[chunkIndex];
        if (chunk.IsNull) {
            chunk = new ValueList<T>(chunkCapacity);
        }

        return Ref(index);
    }

    public ref T this[int index] {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref Ref(index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(int index, T value) {
        ref var item = ref Ref(index);
        item = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SwapRemove(int index) {
        var tail = Count - 1;
        ref var tailItem = ref Ref(tail);
        if (index != tail) {
            Ref(index) = tailItem;
        }

        tailItem = default;
        Count--;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveLast() {
        var index = Count - 1;
        int chunkIndex = index / chunkCapacity;
        // int itemIndex = index % chunkCapacity;
        ref var chunk = ref chunks.Ref(chunkIndex);
        chunk.RemoveLast();
        Count--;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear() {
        for (var i = 0; i < chunks.Count; i++) {
            chunks[i].Clear();
        }

        Count = 0;
    }

    public Span<T> GetChunkSpan(int chunkIndex) {
        if (chunkIndex >= ChunkCount) {
            return Span<T>.Empty;
        }

        ref var chunk = ref chunks[chunkIndex];
        return chunk.Span;
    }
}