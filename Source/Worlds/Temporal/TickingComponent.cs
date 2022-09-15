using System;
using System.Runtime.InteropServices;
using Coorth.Framework;

namespace Coorth.Worlds; 

[Serializable, StoreContract]
[Component, Guid("C22B5044-AFD7-4957-8048-02478DFD644A")]
public struct TickingComponent : IComponent {
    
    [StoreMember(1)]
    public float TimeScale;
    
    [StoreMember(2)]
    public TimeSpan DeltaTime;
    
    [StoreMember(3)]
    public DateTime LastTime;
}