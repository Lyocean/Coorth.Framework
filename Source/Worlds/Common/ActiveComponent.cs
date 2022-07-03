using System;
using System.Runtime.InteropServices;
using Coorth.Collections;
using Coorth.Framework;

namespace Coorth.Worlds; 

[Serializable, DataContract]
[Component, Guid("D118FB05-0CEB-48EE-A45A-E8EFD7ABECC4")]
public struct ActiveComponent : IComponent {
    [DataMember(Order = 1)]
    private BitMask64 mask;
    public BitMask64 Mask => mask;

    public void Set(int index, bool value) {
        if (index < 0 || BitMask64.CAPACITY <= index) {
            throw new IndexOutOfRangeException();
        }
        mask[index] = value;
    }

    public readonly bool Get(int index) {
        if (index < 0 || BitMask64.CAPACITY <= index) {
            throw new IndexOutOfRangeException();
        }
        return mask[index];
    }
        
    public bool this[int index] { get => Get(index); set => Set(index, value); }
}