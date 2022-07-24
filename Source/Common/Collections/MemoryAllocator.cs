using System;

namespace Coorth.Collections;

public interface IMemoryAllocator {
    Memory<T> Alloc<T>(int size);
    void Return<T>(Memory<T> memory);
}
