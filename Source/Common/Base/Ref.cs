using System;
using System.Runtime.InteropServices;

namespace Coorth.Framework;

public readonly ref struct Ref<T> {
    
    private readonly Span<T> span;

    public ref T Value => ref MemoryMarshal.GetReference(span);
    
    public Ref(ref T value) {
        span = MemoryMarshal.CreateSpan(ref value, 1);
    }
}
