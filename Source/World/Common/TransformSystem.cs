using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Coorth.Common {
    [System, DataContract, Guid("782DC9F0-6B8F-4AA0-A1E8-D1CFE095EC03")]
    public class TransformSystem : SystemBase {
        protected override void OnAdd() {
            Parent!.OfferSystem<HierarchySystem>();
            //Bind
            Sandbox.BindComponent<TransformComponent>().AddDependency<HierarchyComponent>();
            Sandbox.BindComponent<SpaceComponent>().AddDependency<TransformComponent>().AddDependency<HierarchyComponent>();

            //SpaceComponent
            OnComponentSetup((ref SpaceComponent _) => _.OnSetup());
            OnComponentClear((ref SpaceComponent _) => _.OnClear());
            
            //TransformComponent
            OnComponentSetup((in Entity entity, ref TransformComponent component) => component.OnSetup(in entity));
            OnComponentClear((ref TransformComponent component) => component.OnClear());
            
            //WorldMatrixComponent
            OnComponentClear((ref WorldMatrixComponent _) => _.Value = Matrix4x4.Identity);
        }
    }
}