using System;

namespace Coorth; 

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class DataMemberAttribute : Attribute {
    
    public string? Name { get; }

    public int Order { get; }

    public bool IsRequired { get; set; }
    
    public DataMemberAttribute() { }
    
    public DataMemberAttribute(int order) {
        Order = order;
    }
    
    public DataMemberAttribute(int order, string name) {
        Name = name;
        Order = order;
    }
}

