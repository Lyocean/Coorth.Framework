using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Coorth.Collections;

public ref struct ValueStringBuilder {
    
    private char[]? array;

    private Span<char> span;

    private int length;

    public ValueStringBuilder(Span<char> value) {
        array = null;
        span = value;
        length = 0;
    }

    public ValueStringBuilder(int capacity) {
        array = ArrayPool<char>.Shared.Rent(capacity);
        span = array.AsSpan();
        length = 0;
    }

    public int Length {
        get => length;
        set {
            Debug.Assert(0 <= value && value <= span.Length);
            length = value;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(char c) {
        if (length >= span.Length) {
            Grow(c);
        }

        span[length] = c;
        length++;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(string? s) {
        if (s == null) {
            return;
        }

        if (s.Length == 1 && length < span.Length) {
            span[length] = s[0];
            length++;
        }

        if (length > span.Length - s.Length) {
            Grow(s.Length);
        }
#if NET5_0_OR_GREATER
        s.CopyTo(span.Slice(length));
#else
        s.AsSpan().CopyTo(span.Slice(length));
#endif
        length += s.Length;
    }
    
#if NET5_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append<T>(T value, string? format = null, IFormatProvider? provider = null) where T : ISpanFormattable {
        if (value.TryFormat(span.Slice(length), out var charsWritten, format, provider)) {
            length += charsWritten;
        }
        else {
            Append(value.ToString(format, provider));
        }
    }
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append<T>(T value, string? format = null, IFormatProvider? provider = null) {
        Append(value?.ToString());
    }
#endif
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void Grow(int size) {
        Debug.Assert(0 < size);
        size = Math.Max(length + size, span.Length * 2);
        var new_array = ArrayPool<char>.Shared.Rent(size);
        span.Slice(0, length).CopyTo(new_array);
        if (array != null) {
            ArrayPool<char>.Shared.Return(array);
        }

        array = new_array;
        span = array.AsSpan();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose() {
        var value = array;
        this = default;
        if (value != null) {
            ArrayPool<char>.Shared.Return(value);
        }
    }

    public override string ToString() {
        return new string(span.Slice(0, length));
    }
}