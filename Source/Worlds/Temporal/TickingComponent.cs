using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Coorth.Framework;

namespace Coorth.Worlds; 

[Serializable, DataContract]
[Component, Guid("C22B5044-AFD7-4957-8048-02478DFD644A")]
public struct TickingComponent : IComponent {
    
    [DataMember(Order = 1)]
    public float TimeScale;
    
    [DataMember(Order = 2)]
    public TimeSpan DeltaTime;
    
    [DataMember(Order = 3)]
    public DateTime LastTime;
}