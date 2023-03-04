using System;
using System.Runtime.CompilerServices;

namespace Coorth.Framework; 

internal struct ComponentMask : IEquatable<ComponentMask> {
    
    private uint[] array;
    
    private int Capacity => array.Length * 32;
        
    public ComponentMask(int length) {
        var capacity = (uint) (length - 1 + 32) >> 5;
        array = new uint[capacity];
    }

    public ComponentMask(ComponentMask other, int length) {
        var capacity = (uint) (length - 1 + 32) >> 5;
        array = new uint[capacity];
        var min = Math.Min(array.Length, other.array.Length);
        Array.Copy(other.array, array, min);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Get(int index) {
        if ((uint) index >= Capacity) {
            return false;
        }
        return (array[index >> 5] & 1u << index) > 0U;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(int index, bool value) {
        if (index >= Capacity) {
            var capacity = (int) ((uint) (index + 32) >> 5);
            Array.Resize(ref array, capacity);
        }
        var num = 1u << index;
        ref var local = ref array[index >> 5];
        if (value) {
            local |= num;
        }
        else {
            local &= ~num;
        }
    }

    public bool Equals(ComponentMask other) {
        if (ReferenceEquals(array, other.array)) {
            return true;
        }
        var length1 = array.Length;
        var length2 = other.array.Length;
        
        if (length1 == length2) {
            return array.AsSpan().SequenceEqual(other.array.AsSpan());
        }
        if (length1 < length2) {
            if (!array.AsSpan().SequenceEqual(other.array.AsSpan(0, length1))) {
                return false;
            }
            for (var i = length1; i < length2; i++) {
                if (other.array[i] != 0) {
                    return false;
                }
            }
        }
        if (!other.array.AsSpan().SequenceEqual(array.AsSpan(0, length2))) {
            return false;
        }
        for (var i = length2; i < length1; i++) {
            if (array[i] != 0) {
                return false;
            }
        }
        return true;
    }

    public override bool Equals(object? obj) => obj is ComponentMask other && Equals(other);

    public override int GetHashCode() {
        return array.GetHashCode();
    }
}