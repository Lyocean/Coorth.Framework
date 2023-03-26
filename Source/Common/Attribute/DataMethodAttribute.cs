using System;

namespace Coorth;

[AttributeUsage(AttributeTargets.Method)]
public class DataMethodAttribute : Attribute {

    public int Order { get; }
    
    public DataMethodAttribute(int order) {
        Order = order;
    }
}