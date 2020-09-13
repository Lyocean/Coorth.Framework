using System;

namespace Coorth.ECS {
    internal struct ChunkList<T> {
        public T[] Items;
        public int Count;

        public ChunkList(int capacity) {
            Items = new T[capacity];
            Count = 0;
        }

        public ref T this[int index] {
            get {
                return ref Items[index];
            }
        }

        public ref T Alloc(int index) {
            if(index >= Items.Length) {
                Array.Resize(ref Items, index << 2);
            }
            return ref Items[index];
        }
    }
}
