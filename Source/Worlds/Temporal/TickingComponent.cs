using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Coorth.Framework;

namespace Coorth.Worlds; 

[Component, DataContract, Guid("C22B5044-AFD7-4957-8048-02478DFD644A")]
public struct TickingComponent : IComponent {
    public float TimeScale;
    
    public TimeSpan DeltaTime;
    
    public DateTime LastTime;
    
}