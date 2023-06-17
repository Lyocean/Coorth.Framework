using System;

namespace Coorth; 

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
public sealed class DataScopeAttribute : Attribute {

    public string? Label { get; }

    public bool CanFold { get; }

    public DataScopeAttribute(string label, bool can_fold = true) {
        Label = label;
        CanFold = can_fold;
    }
}