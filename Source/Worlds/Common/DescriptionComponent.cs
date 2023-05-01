using System;
using Coorth.Collections;


namespace Coorth.Framework; 

[Serializable, DataDefine]
[Component, Guid("D118FB05-0CEB-48EE-A45A-E8EFD7ABECC4")]
public partial struct DescriptionComponent : IComponent {

    [DataMember(1)]
    public string? Name { get; set; }

    [DataMember(2)]
    private BitMask64 mask;
    public BitMask64 Mask => mask;

    public void SetFlag(int index, bool value) {
        if (index < 0 || BitMask64.CAPACITY <= index) {
            throw new IndexOutOfRangeException();
        }
        mask[index] = value;
    }

    public readonly bool GetFlag(int index) {
        if (index < 0 || BitMask64.CAPACITY <= index) {
            throw new IndexOutOfRangeException();
        }
        return mask[index];
    }
    
    public bool this[int index] { get => GetFlag(index); set => SetFlag(index, value); }
}

public static class DescriptionExtension {
    public static void SetName(this in Entity entity, string? name) {
        entity.Offer<DescriptionComponent>().Name = name;
    }
    
    public static string? GetName(this in Entity entity) {
        return entity.Offer<DescriptionComponent>().Name;
    }
    
    public static void SetFlag(this in Entity entity, int index, bool value) {
        entity.Offer<DescriptionComponent>().SetFlag(index, value);
    }
    
    public static bool GetFlag(this in Entity entity, int index) {
        return entity.Offer<DescriptionComponent>().GetFlag(index);
    }
}