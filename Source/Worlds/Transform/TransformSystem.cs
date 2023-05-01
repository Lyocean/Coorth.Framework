using System.Numerics;

namespace Coorth.Framework; 

[System, Guid("782DC9F0-6B8F-4AA0-A1E8-D1CFE095EC03")]
public class TransformSystem : SystemBase {
    protected override void OnAdd() {
        Parent.OfferChild<HierarchySystem>();

        //SpaceComponent
        OnComponentSetup(static (ref SpaceComponent _) => _.OnSetup());
        OnComponentClear(static (ref SpaceComponent _) => _.OnClear());
            
        //TransformComponent
        OnComponentSetup(static (in Entity entity, ref TransformComponent _) => _.OnSetup(in entity));
        OnComponentClear(static (ref TransformComponent _) => _.OnClear());

        //WorldMatrixComponent
        BindComponent(new DefaultFactory<PositionComponent>(new PositionComponent(Vector3.Zero)));
        BindComponent(new DefaultFactory<RotationComponent>(new RotationComponent(Quaternion.Identity)));
        BindComponent(new DefaultFactory<ScalingComponent>(new ScalingComponent(Vector3.One)));
        BindComponent(new DefaultFactory<LocalMatrixComponent>(new LocalMatrixComponent(Matrix4x4.Identity)));
        BindComponent(new DefaultFactory<WorldMatrixComponent>(new WorldMatrixComponent(Matrix4x4.Identity)));
    }
    
}