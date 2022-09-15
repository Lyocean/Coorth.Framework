using System;

namespace Coorth; 

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class StoreMemberAttribute : Attribute {
    
    public string? Name { get; }

    public int Order { get; }

    public bool IsRequired { get; set; }
    
    public StoreMemberAttribute() { }
    
    public StoreMemberAttribute(int order) {
        Order = order;
    }
    
    public StoreMemberAttribute(int order, string name) {
        Name = name;
        Order = order;
    }
}