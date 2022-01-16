namespace Coorth.Common {
    [System, StoreContract("782DC9F0-6B8F-4AA0-A1E8-D1CFE095EC03")]
    public class TransformSystem : SystemBase {
        protected override void OnAdd() {
            Parent.OfferSystem<HierarchySystem>();
            //Bind
            Sandbox.BindComponent<TransformComponent>().AddDependency<HierarchyComponent>();
            Sandbox.BindComponent<SpaceComponent>().AddDependency<TransformComponent>().AddDependency<HierarchyComponent>();

            //SpaceComponent
            OnComponentAdd((ref SpaceComponent component) => component.OnAdd());
            OnComponentRemove((ref SpaceComponent component) => component.OnRemove());
            
            //TransformComponent
            OnComponentAdd((in Entity entity, ref TransformComponent component) => component.OnAdd(in entity));
            OnComponentRemove((in Entity _, ref TransformComponent component) => component.OnRemove());
        }
    }
}