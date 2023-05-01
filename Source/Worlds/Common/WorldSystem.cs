namespace Coorth.Framework; 

[System, Guid("7DA743A9-D0CB-4E81-9BB9-474928962016")]
public sealed class WorldSystem : SystemBase {
    
    protected override void OnAdd() {

        BindComponent<WorldComponent>();
        Singleton<WorldComponent>();
        
        BindComponent<DescriptionComponent>();
        AddChild<DescriptionSystem>();
        
        BindComponent<HierarchyComponent>();
        AddChild<HierarchySystem>();

        BindComponent<TransformComponent>().AddDependency<HierarchyComponent>();
        BindComponent<SpaceComponent>().AddDependency<TransformComponent>().AddDependency<HierarchyComponent>();
        AddChild<TransformSystem>();
        
        BindComponent<LifetimeComponent>();
        AddChild<LifetimeSystem>();
        
    }
}