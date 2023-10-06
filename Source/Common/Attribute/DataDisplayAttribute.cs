using System;

namespace Coorth; 

[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property)]
public class DataDisplayAttribute : Attribute {

    public string? Label { get; set; }

    public bool Editable { get; set; } = true;
        
    public bool Visible { get; set; } = true;
    
    public string? Tooltips { get; set; }
}