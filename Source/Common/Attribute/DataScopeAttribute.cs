using System;

namespace Coorth; 

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
public sealed class DataScopeAttribute : Attribute {

    public string? Label { get; }

    public bool CanFold { get; }

    public DataScopeAttribute(string label, bool canFold = true) {
        Label = label;
        CanFold = canFold;
    }
}