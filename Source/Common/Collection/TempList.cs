using System;
using System.Buffers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coorth {
    public ref struct TempList<T>  {
        
        private int count;
        
        private T[] array;

        public int Count => count;

        public int Capacity => array.Length;

        public bool IsNull => array == null;

        public TempList(int capacity) {
            this.array = ArrayPool<T>.Shared.Rent(capacity);
            this.count = 0;
        }

        public TempList(ReadOnlySpan<T> values) {
            this.array = ArrayPool<T>.Shared.Rent(values.Length);
            this.count = 0;
            values.CopyTo(array.AsSpan());
        }
        
        public TempList(List<T> list) {
            this.array = ArrayPool<T>.Shared.Rent(list.Capacity);
            this.count = list.Count;
            list.CopyTo(0, this.array, 0, list.Count);
        }
        
        public void Add(T value) {
            if (array.Length <= count) {
                Resize(count  + 1);
            }
            array[count++] = value;
        }

        public void Add(T[] values) {
            var totalLength = count + values.Length;
            if (array.Length < totalLength) {
                Resize(totalLength);
            }
            Array.Copy(values, 0, array, count, values.Length);
        }
        
        public void Add(ReadOnlySpan<T> values) {
            var totalLength = count + values.Length;
            if (array.Length < totalLength) {
                Resize(totalLength);
            }
            values.CopyTo(array.AsSpan(count, values.Length));
        }

        public T this[int index] {
            get => array[index];
            set => array[index] = value;
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
            return new Span<T>(array);
        }
        
        public void Clear() {
            if(array != null) {
                ArrayPool<T>.Shared.Return(array, true);
            }
            array = null;
        }

        public void Dispose() => Clear();
    }
}