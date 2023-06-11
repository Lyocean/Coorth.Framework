using System;
using System.Runtime.CompilerServices;


namespace Coorth.Framework; 

public struct ComponentMask : IEquatable<ComponentMask> {
    
    private uint[] data;
    
    private int Capacity => data.Length * 32;

    private const int MASK = (sizeof(uint) * 8) - 1;
    
    public ComponentMask(int length) {
        var capacity = (uint) (length - 1 + 32) >> 5;
        data = new uint[capacity];
    }

    public ComponentMask(ComponentMask other, int length) {
        var capacity = (uint) (length - 1 + 32) >> 5;
        data = new uint[capacity];
        var min = Math.Min(data.Length, other.data.Length);
        Array.Copy(other.data, data, min);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Get(int index) {
        var pos = index >> 5;
        if (pos >= data.Length) {
            return false;
        }
        return (data[pos] & (1u << (index & MASK))) > 0U;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(int index, bool value) {
        var pos = index >> 5;
        if (pos >= data.Length) {
            Array.Resize(ref data, pos + 1);
        }
        var num = (1u << (index & MASK));
        ref var local = ref data[index >> 5];
        if (value) {
            local |= num;
        }
        else {
            local &= ~num;
        }
    }

    public bool Equals(ComponentMask other) {
        if (ReferenceEquals(data, other.data)) {
            return true;
        }
        var length1 = data.Length;
        var length2 = other.data.Length;
        
        if (length1 == length2) {
            return data.AsSpan().SequenceEqual(other.data.AsSpan());
        }
        if (length1 < length2) {
            if (!data.AsSpan().SequenceEqual(other.data.AsSpan(0, length1))) {
                return false;
            }
            for (var i = length1; i < length2; i++) {
                if (other.data[i] != 0) {
                    return false;
                }
            }
        }
        if (!other.data.AsSpan().SequenceEqual(data.AsSpan(0, length2))) {
            return false;
        }
        for (var i = length2; i < length1; i++) {
            if (data[i] != 0) {
                return false;
            }
        }
        return true;
    }

    public override bool Equals(object? obj) => obj is ComponentMask other && Equals(other);

    public override int GetHashCode() => data.GetHashCode();
}