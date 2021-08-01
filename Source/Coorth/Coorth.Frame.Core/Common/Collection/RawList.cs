using System;
using System.Runtime.CompilerServices;

namespace Coorth {
    public struct RawList<T> {
    
        public T[] Values;

        public int Count;

        public int Capacity {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Values.Length;
        }

        public bool IsNull {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Values == null;
        }
        
        public static readonly RawList<T> Empty = new RawList<T>(0);
        
        public RawList(int capacity) {
            Values = new T[capacity];
            Count = 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Add() {
            if (Count == Capacity) {
                var size = Capacity == 0 ? 4 : Capacity << 1;
                Array.Resize(ref Values, size);
            }
            var index = Count;
            Count++;
            return ref Values[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T value) {
            ref var item = ref Add();
            item = value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get(int index) {
            return Values[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Ref(int index) {
            return ref Values[index];
        }
        
        public ref T this[int index] {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref Values[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(int index, T value) {
            ref var item = ref Ref(index);
            item = value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SwapRemove(int index) {
            var tail = Count - 1;
            if (index != tail) {
                Values[index] = Values[tail];
            }
            Values[tail] = default;
            Count--;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveAt(int index) {
            Count--;
            if (index < Count) {
                Array.Copy(Values, index + 1, Values, index, Count - index);
            }
            Values[Count] = default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveLast() {
            Count--;
            Values[Count] = default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IndexOf(T item) {
            return Array.IndexOf<T>(this.Values, item, 0, this.Count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear() {
            Array.Clear(Values, 0, Count);
            Count = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Alloc(int index) {
            int size = Capacity;
            if (index >= size) {
                size = size == 0 ? 4 : size * 2;
                do {
                    size = size * 2;
                } while (index >= size);
                Array.Resize(ref Values, size);
            }
            Count++;
            return ref Values[index];
        }
    }
    
}