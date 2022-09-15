using System;

namespace Coorth; 

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class StoreIgnoreAttribute : Attribute {
}