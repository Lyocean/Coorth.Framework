using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Coorth.Common {
    [System, DataContract, Guid("5600578A-FE02-49E4-8ED3-2819BA1BA789")]
    public class HierarchySystem : SystemBase {
        protected override void OnAdd() {
            Sandbox.BindComponent<HierarchyComponent>();
            OnComponentAdd((in Entity entity, ref HierarchyComponent component) => component.OnAdd(entity));
            OnComponentRemove((in Entity _, ref HierarchyComponent component) => component.OnRemove());
        }
    }
}