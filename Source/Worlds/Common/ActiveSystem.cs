using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Coorth.Framework;

namespace Coorth.Worlds; 

[System, Guid("775695A2-954C-456D-B962-EA7A07DDAC1D")]
public class ActiveSystem : SystemBase {
    protected override void OnAdd() {
        Sandbox.BindComponent<ActiveComponent>();
    }
}