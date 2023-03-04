namespace Coorth.Framework;

[System, Guid("5600578A-FE02-49E4-8ED3-2819BA1BA789")]
public class HierarchySystem : SystemBase {
    protected override void OnAdd() {
        OnComponentSetup((in Entity entity, ref HierarchyComponent component) => component.OnSetup(entity));
        OnComponentClear((in Entity _, ref HierarchyComponent component) => component.OnClear());
    }
}