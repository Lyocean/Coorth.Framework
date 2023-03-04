using System;


namespace Coorth.Framework; 

[Serializable, StoreContract]
[Component, Guid("C22B5044-AFD7-4957-8048-02478DFD644A")]
public partial struct TimeNodeComponent : IComponent {
    
    [StoreMember(1)]
    public float TimeScale;
    
    [StoreMember(2)]
    public TimeSpan DeltaTime;
    
    [StoreMember(3)]
    public DateTime LastTime;
}
