using System;

namespace Coorth; 

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class DataRangeAttribute : Attribute {

    public double Min { get; }

    public double Max { get; }
        
    public DataRangeAttribute(double min, double max) {
        Min = min;
        Max = max;
    }
}