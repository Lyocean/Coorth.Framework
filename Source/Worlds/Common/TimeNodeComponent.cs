using System;


namespace Coorth.Framework; 

[Serializable, DataDefine]
[Component, Guid("C22B5044-AFD7-4957-8048-02478DFD644A")]
public partial struct TimeNodeComponent : IComponent {
    
    [DataMember(1)]
    public float TimeScale;
    
    [DataMember(2)]
    public TimeSpan DeltaTime;
    
    [DataMember(3)]
    public DateTime LastTime;
}
