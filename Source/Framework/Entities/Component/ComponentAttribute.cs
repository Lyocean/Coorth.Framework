using System;

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