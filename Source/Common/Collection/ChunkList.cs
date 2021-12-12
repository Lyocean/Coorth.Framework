using System;
using System.Runtime.CompilerServices;

namespace Coorth {
    public struct ChunkList<T> {
        
        private RawList<RawList<T>> chunks;
        
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
            if (indexCapacity <= 0 || chunkCapacity <= 0) {
                throw new ArgumentException("Capacity must larger than 0");
            }
            indexCapacity = (int) ((uint) (indexCapacity - 1 + 4) >> 2) << 2;
            chunkCapacity = (int) ((uint) (chunkCapacity - 1 + 4) >> 2) << 2;

            this.chunkCapacity = chunkCapacity;
            this.chunks = new RawList<RawList<T>>(indexCapacity);
            this.chunks.Add(new RawList<T>(this.chunkCapacity));
            this.Count = 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Add() {
            var index = Count;
            int chunkIndex = index / chunkCapacity;
            if (chunkIndex >= ChunkCount) {
                chunks.Add(new RawList<T>(chunkCapacity));
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
            ref var chunk = ref chunks.Values[chunkIndex];
            if (chunk.IsNull) {
                chunk = new RawList<T>(chunkCapacity);
            }
            return ref chunk.Values[itemIndex];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get(int index) {
            int chunkIndex = index / chunkCapacity;
            //int itemIndex = index % chunkCapacity;
            ref var chunk = ref chunks.Values[chunkIndex];
            if (chunk.IsNull) {
                chunk = new RawList<T>(chunkCapacity);
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
            ref var chunk = ref chunks.Ref(chunks.Count - 1);
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
            ref var chunk = ref chunks.Values[chunkIndex];
            return chunk.Values != null ? chunk.Values.AsSpan() : Span<T>.Empty;
        }
    }
}
