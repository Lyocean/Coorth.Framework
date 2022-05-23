using System.Collections.Generic;
using System.Numerics;

namespace Coorth.Framework; 

public class TestSystem : SystemBase {
}

public class TestSubSystem : SystemBase {
}

public class TestForEachSystem : SystemBase {
    public readonly HashSet<Entity> Entities1 = new HashSet<Entity>();
    public readonly HashSet<Entity> Entities2 = new HashSet<Entity>();
    public readonly HashSet<Entity> Entities3 = new HashSet<Entity>();
    public readonly HashSet<Entity> Entities4 = new HashSet<Entity>();

    protected override void OnAdd() {
        Subscribe<EventTickUpdate>().ForEach<TestClassComponent1>(Execute);
        Subscribe<EventTickUpdate>().ForEach<TestClassComponent1, TestClassComponent2>(Execute);
        Subscribe<EventTickUpdate>()
            .ForEach<TestClassComponent1, TestClassComponent2, TestClassComponent3>(Execute);

        var matcher = new EntityMatcher().Include<TestClassComponent1, TestClassComponent3>().Exclude<TestClassComponent2>();
        Subscribe<EventTickUpdate>().OnMatch(matcher, Execute);
    }

    private void Execute(EventTickUpdate e, Entity entity, TestClassComponent1 component1) {
        component1.a++;
        Entities1.Add(entity);
    }

    private void Execute(EventTickUpdate e, Entity entity, TestClassComponent1 component1,
        TestClassComponent2 component2) {
        component1.a++;
        component2.a++;
        Entities2.Add(entity);
    }

    private void Execute(EventTickUpdate e, Entity entity, TestClassComponent1 component1,
        TestClassComponent2 component2, TestClassComponent3 component3) {
        component1.a++;
        component2.a++;
        component3.v1 = Vector4.One;
        Entities3.Add(entity);
    }

    private void Execute(EventTickUpdate e, Entity entity) {
        entity.Get<TestClassComponent1>().b++;
        Entities4.Add(entity);
    }
}
    
public class TestEnableSystem : SystemBase {

    public int Count = 0;
        
    protected override void OnAdd() {
        Subscribe<EventTickUpdate>().ForEach<TestValueComponent0>(_ => {
            Count ++;
        });
    }
}