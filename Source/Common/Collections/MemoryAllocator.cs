using System;
using System.Buffers;
using System.Runtime.InteropServices;

namespace Coorth.Collections;

public interface IMemoryAllocator {
    Memory<T> Alloc<T>(int size);
    void Return<T>(Memory<T> memory);
}

public class MemoryAllocator : IMemoryAllocator {
    public Memory<T> Alloc<T>(int size) {
        var array = ArrayPool<T>.Shared.Rent(size);
        return array.AsMemory();
    }

    public void Return<T>(Memory<T> memory) {
        if(!MemoryMarshal.TryGetArray<T>(memory, out var segment)) {
            return;
        }
        if (segment.Array == null) {
            return;
        }
        ArrayPool<T>.Shared.Return(segment.Array);
    }
}
