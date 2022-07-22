using System;
using System.Collections.Generic;

namespace Coorth.Framework; 

[AttributeUsage(AttributeTargets.Class| AttributeTargets.Struct)]
public class ComponentAttribute : Attribute {
        
    public bool Singleton { get; set; }

    public bool IsPinned { get; set; }

    public int Capacity = 0;
        
    public int Chunk = 0;

    public int ChunkCapacity => Singleton ? 1: Chunk;

    public int IndexCapacity => Singleton ? 1: Capacity;
        
}

[AttributeUsage(AttributeTargets.Class| AttributeTargets.Struct)]
public class DependencyAttribute : Attribute {
    
    public readonly Type[] Types;

    public DependencyAttribute(params Type[] types) {
        Types = types;
    }
}