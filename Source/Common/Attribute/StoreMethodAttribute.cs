using System;

namespace Coorth;

[AttributeUsage(AttributeTargets.Method)]
public class StoreMethodAttribute : Attribute {

    public int Order { get; }
    
    public StoreMethodAttribute(int order) {
        Order = order;
    }
}