
namespace Coorth.Framework;

public partial class World {
    public Components<T0, T1> AddComponent<T0, T1>(scoped in EntityId id) where T0 : IComponent where T1 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id);
        ref var component1 = ref AddComponent<T1>(in id);
        return new Components<T0, T1>(ref component0, ref component1);
    }

    public Components<T0, T1> AddComponent<T0, T1>(scoped in EntityId id, in T0 value0, in T1 value1) where T0 : IComponent where T1 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id, in value0);
        ref var component1 = ref AddComponent<T1>(in id, in value1);
        return new Components<T0, T1>(ref component0, ref component1);
    }

    public Components<T0, T1, T2> AddComponent<T0, T1, T2>(scoped in EntityId id) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id);
        ref var component1 = ref AddComponent<T1>(in id);
        ref var component2 = ref AddComponent<T2>(in id);
        return new Components<T0, T1, T2>(ref component0, ref component1, ref component2);
    }

    public Components<T0, T1, T2> AddComponent<T0, T1, T2>(scoped in EntityId id, in T0 value0, in T1 value1, in T2 value2) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id, in value0);
        ref var component1 = ref AddComponent<T1>(in id, in value1);
        ref var component2 = ref AddComponent<T2>(in id, in value2);
        return new Components<T0, T1, T2>(ref component0, ref component1, ref component2);
    }

    public Components<T0, T1, T2, T3> AddComponent<T0, T1, T2, T3>(scoped in EntityId id) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id);
        ref var component1 = ref AddComponent<T1>(in id);
        ref var component2 = ref AddComponent<T2>(in id);
        ref var component3 = ref AddComponent<T3>(in id);
        return new Components<T0, T1, T2, T3>(ref component0, ref component1, ref component2, ref component3);
    }

    public Components<T0, T1, T2, T3> AddComponent<T0, T1, T2, T3>(scoped in EntityId id, in T0 value0, in T1 value1, in T2 value2, in T3 value3) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id, in value0);
        ref var component1 = ref AddComponent<T1>(in id, in value1);
        ref var component2 = ref AddComponent<T2>(in id, in value2);
        ref var component3 = ref AddComponent<T3>(in id, in value3);
        return new Components<T0, T1, T2, T3>(ref component0, ref component1, ref component2, ref component3);
    }

    public Components<T0, T1, T2, T3, T4> AddComponent<T0, T1, T2, T3, T4>(scoped in EntityId id) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id);
        ref var component1 = ref AddComponent<T1>(in id);
        ref var component2 = ref AddComponent<T2>(in id);
        ref var component3 = ref AddComponent<T3>(in id);
        ref var component4 = ref AddComponent<T4>(in id);
        return new Components<T0, T1, T2, T3, T4>(ref component0, ref component1, ref component2, ref component3, ref component4);
    }

    public Components<T0, T1, T2, T3, T4> AddComponent<T0, T1, T2, T3, T4>(scoped in EntityId id, in T0 value0, in T1 value1, in T2 value2, in T3 value3, in T4 value4) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id, in value0);
        ref var component1 = ref AddComponent<T1>(in id, in value1);
        ref var component2 = ref AddComponent<T2>(in id, in value2);
        ref var component3 = ref AddComponent<T3>(in id, in value3);
        ref var component4 = ref AddComponent<T4>(in id, in value4);
        return new Components<T0, T1, T2, T3, T4>(ref component0, ref component1, ref component2, ref component3, ref component4);
    }

    public Components<T0, T1, T2, T3, T4, T5> AddComponent<T0, T1, T2, T3, T4, T5>(scoped in EntityId id) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id);
        ref var component1 = ref AddComponent<T1>(in id);
        ref var component2 = ref AddComponent<T2>(in id);
        ref var component3 = ref AddComponent<T3>(in id);
        ref var component4 = ref AddComponent<T4>(in id);
        ref var component5 = ref AddComponent<T5>(in id);
        return new Components<T0, T1, T2, T3, T4, T5>(ref component0, ref component1, ref component2, ref component3, ref component4, ref component5);
    }

    public Components<T0, T1, T2, T3, T4, T5> AddComponent<T0, T1, T2, T3, T4, T5>(scoped in EntityId id, in T0 value0, in T1 value1, in T2 value2, in T3 value3, in T4 value4, in T5 value5) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id, in value0);
        ref var component1 = ref AddComponent<T1>(in id, in value1);
        ref var component2 = ref AddComponent<T2>(in id, in value2);
        ref var component3 = ref AddComponent<T3>(in id, in value3);
        ref var component4 = ref AddComponent<T4>(in id, in value4);
        ref var component5 = ref AddComponent<T5>(in id, in value5);
        return new Components<T0, T1, T2, T3, T4, T5>(ref component0, ref component1, ref component2, ref component3, ref component4, ref component5);
    }

    public Components<T0, T1, T2, T3, T4, T5, T6> AddComponent<T0, T1, T2, T3, T4, T5, T6>(scoped in EntityId id) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id);
        ref var component1 = ref AddComponent<T1>(in id);
        ref var component2 = ref AddComponent<T2>(in id);
        ref var component3 = ref AddComponent<T3>(in id);
        ref var component4 = ref AddComponent<T4>(in id);
        ref var component5 = ref AddComponent<T5>(in id);
        ref var component6 = ref AddComponent<T6>(in id);
        return new Components<T0, T1, T2, T3, T4, T5, T6>(ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6);
    }

    public Components<T0, T1, T2, T3, T4, T5, T6> AddComponent<T0, T1, T2, T3, T4, T5, T6>(scoped in EntityId id, in T0 value0, in T1 value1, in T2 value2, in T3 value3, in T4 value4, in T5 value5, in T6 value6) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id, in value0);
        ref var component1 = ref AddComponent<T1>(in id, in value1);
        ref var component2 = ref AddComponent<T2>(in id, in value2);
        ref var component3 = ref AddComponent<T3>(in id, in value3);
        ref var component4 = ref AddComponent<T4>(in id, in value4);
        ref var component5 = ref AddComponent<T5>(in id, in value5);
        ref var component6 = ref AddComponent<T6>(in id, in value6);
        return new Components<T0, T1, T2, T3, T4, T5, T6>(ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6);
    }

    public Components<T0, T1, T2, T3, T4, T5, T6, T7> AddComponent<T0, T1, T2, T3, T4, T5, T6, T7>(scoped in EntityId id) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id);
        ref var component1 = ref AddComponent<T1>(in id);
        ref var component2 = ref AddComponent<T2>(in id);
        ref var component3 = ref AddComponent<T3>(in id);
        ref var component4 = ref AddComponent<T4>(in id);
        ref var component5 = ref AddComponent<T5>(in id);
        ref var component6 = ref AddComponent<T6>(in id);
        ref var component7 = ref AddComponent<T7>(in id);
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7>(ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7);
    }

    public Components<T0, T1, T2, T3, T4, T5, T6, T7> AddComponent<T0, T1, T2, T3, T4, T5, T6, T7>(scoped in EntityId id, in T0 value0, in T1 value1, in T2 value2, in T3 value3, in T4 value4, in T5 value5, in T6 value6, in T7 value7) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id, in value0);
        ref var component1 = ref AddComponent<T1>(in id, in value1);
        ref var component2 = ref AddComponent<T2>(in id, in value2);
        ref var component3 = ref AddComponent<T3>(in id, in value3);
        ref var component4 = ref AddComponent<T4>(in id, in value4);
        ref var component5 = ref AddComponent<T5>(in id, in value5);
        ref var component6 = ref AddComponent<T6>(in id, in value6);
        ref var component7 = ref AddComponent<T7>(in id, in value7);
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7>(ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7);
    }

    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8> AddComponent<T0, T1, T2, T3, T4, T5, T6, T7, T8>(scoped in EntityId id) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id);
        ref var component1 = ref AddComponent<T1>(in id);
        ref var component2 = ref AddComponent<T2>(in id);
        ref var component3 = ref AddComponent<T3>(in id);
        ref var component4 = ref AddComponent<T4>(in id);
        ref var component5 = ref AddComponent<T5>(in id);
        ref var component6 = ref AddComponent<T6>(in id);
        ref var component7 = ref AddComponent<T7>(in id);
        ref var component8 = ref AddComponent<T8>(in id);
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8);
    }

    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8> AddComponent<T0, T1, T2, T3, T4, T5, T6, T7, T8>(scoped in EntityId id, in T0 value0, in T1 value1, in T2 value2, in T3 value3, in T4 value4, in T5 value5, in T6 value6, in T7 value7, in T8 value8) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id, in value0);
        ref var component1 = ref AddComponent<T1>(in id, in value1);
        ref var component2 = ref AddComponent<T2>(in id, in value2);
        ref var component3 = ref AddComponent<T3>(in id, in value3);
        ref var component4 = ref AddComponent<T4>(in id, in value4);
        ref var component5 = ref AddComponent<T5>(in id, in value5);
        ref var component6 = ref AddComponent<T6>(in id, in value6);
        ref var component7 = ref AddComponent<T7>(in id, in value7);
        ref var component8 = ref AddComponent<T8>(in id, in value8);
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8);
    }

    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> AddComponent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(scoped in EntityId id) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id);
        ref var component1 = ref AddComponent<T1>(in id);
        ref var component2 = ref AddComponent<T2>(in id);
        ref var component3 = ref AddComponent<T3>(in id);
        ref var component4 = ref AddComponent<T4>(in id);
        ref var component5 = ref AddComponent<T5>(in id);
        ref var component6 = ref AddComponent<T6>(in id);
        ref var component7 = ref AddComponent<T7>(in id);
        ref var component8 = ref AddComponent<T8>(in id);
        ref var component9 = ref AddComponent<T9>(in id);
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9);
    }

    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> AddComponent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(scoped in EntityId id, in T0 value0, in T1 value1, in T2 value2, in T3 value3, in T4 value4, in T5 value5, in T6 value6, in T7 value7, in T8 value8, in T9 value9) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ref var component0 = ref AddComponent<T0>(in id, in value0);
        ref var component1 = ref AddComponent<T1>(in id, in value1);
        ref var component2 = ref AddComponent<T2>(in id, in value2);
        ref var component3 = ref AddComponent<T3>(in id, in value3);
        ref var component4 = ref AddComponent<T4>(in id, in value4);
        ref var component5 = ref AddComponent<T5>(in id, in value5);
        ref var component6 = ref AddComponent<T6>(in id, in value6);
        ref var component7 = ref AddComponent<T7>(in id, in value7);
        ref var component8 = ref AddComponent<T8>(in id, in value8);
        ref var component9 = ref AddComponent<T9>(in id, in value9);
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9);
    }

}