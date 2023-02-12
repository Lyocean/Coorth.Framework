using System.Collections.Generic;
using System.Runtime.InteropServices;
using Coorth.Framework;

namespace Coorth.Framework; 

[Component, Guid("2C5DB88D-DA53-4C61-9BA8-1B62C6A98432")]
public class SceneGraphComponent : Component {
    public SceneGraphComponent? Parent;
    public List<SceneGraphComponent> Children = new();
    
}
//SceneGraph
//Transform
//Hierarchy
