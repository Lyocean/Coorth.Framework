using System.Numerics;
using System.Runtime.InteropServices;
using Coorth.Framework;

namespace Coorth.Framework; 

[System, StoreContract, Guid("782DC9F0-6B8F-4AA0-A1E8-D1CFE095EC03")]
public class TransformSystem : SystemBase {
    protected override void OnAdd() {
        Parent.OfferChild<HierarchySystem>();

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