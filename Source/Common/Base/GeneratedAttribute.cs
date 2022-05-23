using System;

namespace Coorth; 

[AttributeUsage(AttributeTargets.Class| AttributeTargets.Struct)]
public class GeneratedAttribute : Attribute {
}