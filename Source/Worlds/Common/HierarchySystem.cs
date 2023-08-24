namespace Coorth.Framework;

[System, Guid("5600578A-FE02-49E4-8ED3-2819BA1BA789")]
public class HierarchySystem : SystemBase {
    protected override void OnAdd() {
        OnComponentSetup(static (in Entity entity, ref HierarchyComponent component) => component.OnSetup(entity));
        OnComponentClear(static (in Entity _, ref HierarchyComponent component) => component.OnClear());

        Subscribe<EntityActiveEvent>().OnEvent(OnEntityActiveEvent);
    }

    private static void OnEntityActiveEvent(in EntityActiveEvent e) {
        if (!e.Entity.Has<HierarchyComponent>()) {
            return;
        }
        ref var hierarchy = ref e.Entity.Get<HierarchyComponent>();
        SetChildrenActive(ref hierarchy, e.Active);
    }

    private static void SetChildrenActive(ref HierarchyComponent hierarchy, bool value) {
        foreach (ref var child_hierarchy in hierarchy.Children) {
            var entity = hierarchy.Entity;
            entity.World.OnParentActive(entity.Id, value);
            if (child_hierarchy.Count == 0) {
                continue;
            }
            SetChildrenActive(ref child_hierarchy, value);
        }
    }
}
