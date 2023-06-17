

namespace Coorth.Framework; 

public static class EntityExtension {
    public static Components<T0, T1> Add<T0, T1>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() {
        ref var c0 = ref entity.Add<T0>();
        ref var c1 = ref entity.Add<T1>();
        return new Components<T0, T1>(ref c0, ref c1);
    }

    public static Components<T0, T1> Add<T0, T1>(this Entity entity, in T0 v0, in T1 v1) where T0 : IComponent where T1 : IComponent {
        ref var c0 = ref entity.Add(in v0);
        ref var c1 = ref entity.Add(in v1);
        return new Components<T0, T1>(ref c0, ref c1);
    }

    public static bool Has<T0, T1>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() {
        var result0 = entity.Has<T0>();
        var result1 = entity.Has<T1>();
        return result0 && result1;
    }

    public static Components<T0, T1> Get<T0, T1>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() {
        ref var c0 = ref entity.Get<T0>();
        ref var c1 = ref entity.Get<T1>();
        return new Components<T0, T1>(ref c0, ref c1);
    }

    public static bool Remove<T0, T1>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() {
        var result0 = entity.Remove<T0>();
        var result1 = entity.Remove<T1>();
        return result0 && result1;
    }
    public static Components<T0, T1, T2> Add<T0, T1, T2>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() {
        ref var c0 = ref entity.Add<T0>();
        ref var c1 = ref entity.Add<T1>();
        ref var c2 = ref entity.Add<T2>();
        return new Components<T0, T1, T2>(ref c0, ref c1, ref c2);
    }

    public static Components<T0, T1, T2> Add<T0, T1, T2>(this Entity entity, in T0 v0, in T1 v1, in T2 v2) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ref var c0 = ref entity.Add(in v0);
        ref var c1 = ref entity.Add(in v1);
        ref var c2 = ref entity.Add(in v2);
        return new Components<T0, T1, T2>(ref c0, ref c1, ref c2);
    }

    public static bool Has<T0, T1, T2>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() {
        var result0 = entity.Has<T0>();
        var result1 = entity.Has<T1>();
        var result2 = entity.Has<T2>();
        return result0 && result1 && result2;
    }

    public static Components<T0, T1, T2> Get<T0, T1, T2>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() {
        ref var c0 = ref entity.Get<T0>();
        ref var c1 = ref entity.Get<T1>();
        ref var c2 = ref entity.Get<T2>();
        return new Components<T0, T1, T2>(ref c0, ref c1, ref c2);
    }

    public static bool Remove<T0, T1, T2>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() {
        var result0 = entity.Remove<T0>();
        var result1 = entity.Remove<T1>();
        var result2 = entity.Remove<T2>();
        return result0 && result1 && result2;
    }
    public static Components<T0, T1, T2, T3> Add<T0, T1, T2, T3>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() {
        ref var c0 = ref entity.Add<T0>();
        ref var c1 = ref entity.Add<T1>();
        ref var c2 = ref entity.Add<T2>();
        ref var c3 = ref entity.Add<T3>();
        return new Components<T0, T1, T2, T3>(ref c0, ref c1, ref c2, ref c3);
    }

    public static Components<T0, T1, T2, T3> Add<T0, T1, T2, T3>(this Entity entity, in T0 v0, in T1 v1, in T2 v2, in T3 v3) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ref var c0 = ref entity.Add(in v0);
        ref var c1 = ref entity.Add(in v1);
        ref var c2 = ref entity.Add(in v2);
        ref var c3 = ref entity.Add(in v3);
        return new Components<T0, T1, T2, T3>(ref c0, ref c1, ref c2, ref c3);
    }

    public static bool Has<T0, T1, T2, T3>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() {
        var result0 = entity.Has<T0>();
        var result1 = entity.Has<T1>();
        var result2 = entity.Has<T2>();
        var result3 = entity.Has<T3>();
        return result0 && result1 && result2 && result3;
    }

    public static Components<T0, T1, T2, T3> Get<T0, T1, T2, T3>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() {
        ref var c0 = ref entity.Get<T0>();
        ref var c1 = ref entity.Get<T1>();
        ref var c2 = ref entity.Get<T2>();
        ref var c3 = ref entity.Get<T3>();
        return new Components<T0, T1, T2, T3>(ref c0, ref c1, ref c2, ref c3);
    }

    public static bool Remove<T0, T1, T2, T3>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() {
        var result0 = entity.Remove<T0>();
        var result1 = entity.Remove<T1>();
        var result2 = entity.Remove<T2>();
        var result3 = entity.Remove<T3>();
        return result0 && result1 && result2 && result3;
    }
    public static Components<T0, T1, T2, T3, T4> Add<T0, T1, T2, T3, T4>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() {
        ref var c0 = ref entity.Add<T0>();
        ref var c1 = ref entity.Add<T1>();
        ref var c2 = ref entity.Add<T2>();
        ref var c3 = ref entity.Add<T3>();
        ref var c4 = ref entity.Add<T4>();
        return new Components<T0, T1, T2, T3, T4>(ref c0, ref c1, ref c2, ref c3, ref c4);
    }

    public static Components<T0, T1, T2, T3, T4> Add<T0, T1, T2, T3, T4>(this Entity entity, in T0 v0, in T1 v1, in T2 v2, in T3 v3, in T4 v4) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ref var c0 = ref entity.Add(in v0);
        ref var c1 = ref entity.Add(in v1);
        ref var c2 = ref entity.Add(in v2);
        ref var c3 = ref entity.Add(in v3);
        ref var c4 = ref entity.Add(in v4);
        return new Components<T0, T1, T2, T3, T4>(ref c0, ref c1, ref c2, ref c3, ref c4);
    }

    public static bool Has<T0, T1, T2, T3, T4>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() {
        var result0 = entity.Has<T0>();
        var result1 = entity.Has<T1>();
        var result2 = entity.Has<T2>();
        var result3 = entity.Has<T3>();
        var result4 = entity.Has<T4>();
        return result0 && result1 && result2 && result3 && result4;
    }

    public static Components<T0, T1, T2, T3, T4> Get<T0, T1, T2, T3, T4>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() {
        ref var c0 = ref entity.Get<T0>();
        ref var c1 = ref entity.Get<T1>();
        ref var c2 = ref entity.Get<T2>();
        ref var c3 = ref entity.Get<T3>();
        ref var c4 = ref entity.Get<T4>();
        return new Components<T0, T1, T2, T3, T4>(ref c0, ref c1, ref c2, ref c3, ref c4);
    }

    public static bool Remove<T0, T1, T2, T3, T4>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() {
        var result0 = entity.Remove<T0>();
        var result1 = entity.Remove<T1>();
        var result2 = entity.Remove<T2>();
        var result3 = entity.Remove<T3>();
        var result4 = entity.Remove<T4>();
        return result0 && result1 && result2 && result3 && result4;
    }
    public static Components<T0, T1, T2, T3, T4, T5> Add<T0, T1, T2, T3, T4, T5>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() {
        ref var c0 = ref entity.Add<T0>();
        ref var c1 = ref entity.Add<T1>();
        ref var c2 = ref entity.Add<T2>();
        ref var c3 = ref entity.Add<T3>();
        ref var c4 = ref entity.Add<T4>();
        ref var c5 = ref entity.Add<T5>();
        return new Components<T0, T1, T2, T3, T4, T5>(ref c0, ref c1, ref c2, ref c3, ref c4, ref c5);
    }

    public static Components<T0, T1, T2, T3, T4, T5> Add<T0, T1, T2, T3, T4, T5>(this Entity entity, in T0 v0, in T1 v1, in T2 v2, in T3 v3, in T4 v4, in T5 v5) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ref var c0 = ref entity.Add(in v0);
        ref var c1 = ref entity.Add(in v1);
        ref var c2 = ref entity.Add(in v2);
        ref var c3 = ref entity.Add(in v3);
        ref var c4 = ref entity.Add(in v4);
        ref var c5 = ref entity.Add(in v5);
        return new Components<T0, T1, T2, T3, T4, T5>(ref c0, ref c1, ref c2, ref c3, ref c4, ref c5);
    }

    public static bool Has<T0, T1, T2, T3, T4, T5>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() {
        var result0 = entity.Has<T0>();
        var result1 = entity.Has<T1>();
        var result2 = entity.Has<T2>();
        var result3 = entity.Has<T3>();
        var result4 = entity.Has<T4>();
        var result5 = entity.Has<T5>();
        return result0 && result1 && result2 && result3 && result4 && result5;
    }

    public static Components<T0, T1, T2, T3, T4, T5> Get<T0, T1, T2, T3, T4, T5>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() {
        ref var c0 = ref entity.Get<T0>();
        ref var c1 = ref entity.Get<T1>();
        ref var c2 = ref entity.Get<T2>();
        ref var c3 = ref entity.Get<T3>();
        ref var c4 = ref entity.Get<T4>();
        ref var c5 = ref entity.Get<T5>();
        return new Components<T0, T1, T2, T3, T4, T5>(ref c0, ref c1, ref c2, ref c3, ref c4, ref c5);
    }

    public static bool Remove<T0, T1, T2, T3, T4, T5>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() {
        var result0 = entity.Remove<T0>();
        var result1 = entity.Remove<T1>();
        var result2 = entity.Remove<T2>();
        var result3 = entity.Remove<T3>();
        var result4 = entity.Remove<T4>();
        var result5 = entity.Remove<T5>();
        return result0 && result1 && result2 && result3 && result4 && result5;
    }
    public static Components<T0, T1, T2, T3, T4, T5, T6> Add<T0, T1, T2, T3, T4, T5, T6>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() where T6 : IComponent, new() {
        ref var c0 = ref entity.Add<T0>();
        ref var c1 = ref entity.Add<T1>();
        ref var c2 = ref entity.Add<T2>();
        ref var c3 = ref entity.Add<T3>();
        ref var c4 = ref entity.Add<T4>();
        ref var c5 = ref entity.Add<T5>();
        ref var c6 = ref entity.Add<T6>();
        return new Components<T0, T1, T2, T3, T4, T5, T6>(ref c0, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6);
    }

    public static Components<T0, T1, T2, T3, T4, T5, T6> Add<T0, T1, T2, T3, T4, T5, T6>(this Entity entity, in T0 v0, in T1 v1, in T2 v2, in T3 v3, in T4 v4, in T5 v5, in T6 v6) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ref var c0 = ref entity.Add(in v0);
        ref var c1 = ref entity.Add(in v1);
        ref var c2 = ref entity.Add(in v2);
        ref var c3 = ref entity.Add(in v3);
        ref var c4 = ref entity.Add(in v4);
        ref var c5 = ref entity.Add(in v5);
        ref var c6 = ref entity.Add(in v6);
        return new Components<T0, T1, T2, T3, T4, T5, T6>(ref c0, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6);
    }

    public static bool Has<T0, T1, T2, T3, T4, T5, T6>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() where T6 : IComponent, new() {
        var result0 = entity.Has<T0>();
        var result1 = entity.Has<T1>();
        var result2 = entity.Has<T2>();
        var result3 = entity.Has<T3>();
        var result4 = entity.Has<T4>();
        var result5 = entity.Has<T5>();
        var result6 = entity.Has<T6>();
        return result0 && result1 && result2 && result3 && result4 && result5 && result6;
    }

    public static Components<T0, T1, T2, T3, T4, T5, T6> Get<T0, T1, T2, T3, T4, T5, T6>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() where T6 : IComponent, new() {
        ref var c0 = ref entity.Get<T0>();
        ref var c1 = ref entity.Get<T1>();
        ref var c2 = ref entity.Get<T2>();
        ref var c3 = ref entity.Get<T3>();
        ref var c4 = ref entity.Get<T4>();
        ref var c5 = ref entity.Get<T5>();
        ref var c6 = ref entity.Get<T6>();
        return new Components<T0, T1, T2, T3, T4, T5, T6>(ref c0, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6);
    }

    public static bool Remove<T0, T1, T2, T3, T4, T5, T6>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() where T6 : IComponent, new() {
        var result0 = entity.Remove<T0>();
        var result1 = entity.Remove<T1>();
        var result2 = entity.Remove<T2>();
        var result3 = entity.Remove<T3>();
        var result4 = entity.Remove<T4>();
        var result5 = entity.Remove<T5>();
        var result6 = entity.Remove<T6>();
        return result0 && result1 && result2 && result3 && result4 && result5 && result6;
    }
    public static Components<T0, T1, T2, T3, T4, T5, T6, T7> Add<T0, T1, T2, T3, T4, T5, T6, T7>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() where T6 : IComponent, new() where T7 : IComponent, new() {
        ref var c0 = ref entity.Add<T0>();
        ref var c1 = ref entity.Add<T1>();
        ref var c2 = ref entity.Add<T2>();
        ref var c3 = ref entity.Add<T3>();
        ref var c4 = ref entity.Add<T4>();
        ref var c5 = ref entity.Add<T5>();
        ref var c6 = ref entity.Add<T6>();
        ref var c7 = ref entity.Add<T7>();
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7>(ref c0, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6, ref c7);
    }

    public static Components<T0, T1, T2, T3, T4, T5, T6, T7> Add<T0, T1, T2, T3, T4, T5, T6, T7>(this Entity entity, in T0 v0, in T1 v1, in T2 v2, in T3 v3, in T4 v4, in T5 v5, in T6 v6, in T7 v7) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ref var c0 = ref entity.Add(in v0);
        ref var c1 = ref entity.Add(in v1);
        ref var c2 = ref entity.Add(in v2);
        ref var c3 = ref entity.Add(in v3);
        ref var c4 = ref entity.Add(in v4);
        ref var c5 = ref entity.Add(in v5);
        ref var c6 = ref entity.Add(in v6);
        ref var c7 = ref entity.Add(in v7);
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7>(ref c0, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6, ref c7);
    }

    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() where T6 : IComponent, new() where T7 : IComponent, new() {
        var result0 = entity.Has<T0>();
        var result1 = entity.Has<T1>();
        var result2 = entity.Has<T2>();
        var result3 = entity.Has<T3>();
        var result4 = entity.Has<T4>();
        var result5 = entity.Has<T5>();
        var result6 = entity.Has<T6>();
        var result7 = entity.Has<T7>();
        return result0 && result1 && result2 && result3 && result4 && result5 && result6 && result7;
    }

    public static Components<T0, T1, T2, T3, T4, T5, T6, T7> Get<T0, T1, T2, T3, T4, T5, T6, T7>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() where T6 : IComponent, new() where T7 : IComponent, new() {
        ref var c0 = ref entity.Get<T0>();
        ref var c1 = ref entity.Get<T1>();
        ref var c2 = ref entity.Get<T2>();
        ref var c3 = ref entity.Get<T3>();
        ref var c4 = ref entity.Get<T4>();
        ref var c5 = ref entity.Get<T5>();
        ref var c6 = ref entity.Get<T6>();
        ref var c7 = ref entity.Get<T7>();
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7>(ref c0, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6, ref c7);
    }

    public static bool Remove<T0, T1, T2, T3, T4, T5, T6, T7>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() where T6 : IComponent, new() where T7 : IComponent, new() {
        var result0 = entity.Remove<T0>();
        var result1 = entity.Remove<T1>();
        var result2 = entity.Remove<T2>();
        var result3 = entity.Remove<T3>();
        var result4 = entity.Remove<T4>();
        var result5 = entity.Remove<T5>();
        var result6 = entity.Remove<T6>();
        var result7 = entity.Remove<T7>();
        return result0 && result1 && result2 && result3 && result4 && result5 && result6 && result7;
    }
    public static Components<T0, T1, T2, T3, T4, T5, T6, T7, T8> Add<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() where T6 : IComponent, new() where T7 : IComponent, new() where T8 : IComponent, new() {
        ref var c0 = ref entity.Add<T0>();
        ref var c1 = ref entity.Add<T1>();
        ref var c2 = ref entity.Add<T2>();
        ref var c3 = ref entity.Add<T3>();
        ref var c4 = ref entity.Add<T4>();
        ref var c5 = ref entity.Add<T5>();
        ref var c6 = ref entity.Add<T6>();
        ref var c7 = ref entity.Add<T7>();
        ref var c8 = ref entity.Add<T8>();
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ref c0, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6, ref c7, ref c8);
    }

    public static Components<T0, T1, T2, T3, T4, T5, T6, T7, T8> Add<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this Entity entity, in T0 v0, in T1 v1, in T2 v2, in T3 v3, in T4 v4, in T5 v5, in T6 v6, in T7 v7, in T8 v8) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ref var c0 = ref entity.Add(in v0);
        ref var c1 = ref entity.Add(in v1);
        ref var c2 = ref entity.Add(in v2);
        ref var c3 = ref entity.Add(in v3);
        ref var c4 = ref entity.Add(in v4);
        ref var c5 = ref entity.Add(in v5);
        ref var c6 = ref entity.Add(in v6);
        ref var c7 = ref entity.Add(in v7);
        ref var c8 = ref entity.Add(in v8);
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ref c0, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6, ref c7, ref c8);
    }

    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() where T6 : IComponent, new() where T7 : IComponent, new() where T8 : IComponent, new() {
        var result0 = entity.Has<T0>();
        var result1 = entity.Has<T1>();
        var result2 = entity.Has<T2>();
        var result3 = entity.Has<T3>();
        var result4 = entity.Has<T4>();
        var result5 = entity.Has<T5>();
        var result6 = entity.Has<T6>();
        var result7 = entity.Has<T7>();
        var result8 = entity.Has<T8>();
        return result0 && result1 && result2 && result3 && result4 && result5 && result6 && result7 && result8;
    }

    public static Components<T0, T1, T2, T3, T4, T5, T6, T7, T8> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() where T6 : IComponent, new() where T7 : IComponent, new() where T8 : IComponent, new() {
        ref var c0 = ref entity.Get<T0>();
        ref var c1 = ref entity.Get<T1>();
        ref var c2 = ref entity.Get<T2>();
        ref var c3 = ref entity.Get<T3>();
        ref var c4 = ref entity.Get<T4>();
        ref var c5 = ref entity.Get<T5>();
        ref var c6 = ref entity.Get<T6>();
        ref var c7 = ref entity.Get<T7>();
        ref var c8 = ref entity.Get<T8>();
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ref c0, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6, ref c7, ref c8);
    }

    public static bool Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() where T6 : IComponent, new() where T7 : IComponent, new() where T8 : IComponent, new() {
        var result0 = entity.Remove<T0>();
        var result1 = entity.Remove<T1>();
        var result2 = entity.Remove<T2>();
        var result3 = entity.Remove<T3>();
        var result4 = entity.Remove<T4>();
        var result5 = entity.Remove<T5>();
        var result6 = entity.Remove<T6>();
        var result7 = entity.Remove<T7>();
        var result8 = entity.Remove<T8>();
        return result0 && result1 && result2 && result3 && result4 && result5 && result6 && result7 && result8;
    }
    public static Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() where T6 : IComponent, new() where T7 : IComponent, new() where T8 : IComponent, new() where T9 : IComponent, new() {
        ref var c0 = ref entity.Add<T0>();
        ref var c1 = ref entity.Add<T1>();
        ref var c2 = ref entity.Add<T2>();
        ref var c3 = ref entity.Add<T3>();
        ref var c4 = ref entity.Add<T4>();
        ref var c5 = ref entity.Add<T5>();
        ref var c6 = ref entity.Add<T6>();
        ref var c7 = ref entity.Add<T7>();
        ref var c8 = ref entity.Add<T8>();
        ref var c9 = ref entity.Add<T9>();
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ref c0, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6, ref c7, ref c8, ref c9);
    }

    public static Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Entity entity, in T0 v0, in T1 v1, in T2 v2, in T3 v3, in T4 v4, in T5 v5, in T6 v6, in T7 v7, in T8 v8, in T9 v9) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ref var c0 = ref entity.Add(in v0);
        ref var c1 = ref entity.Add(in v1);
        ref var c2 = ref entity.Add(in v2);
        ref var c3 = ref entity.Add(in v3);
        ref var c4 = ref entity.Add(in v4);
        ref var c5 = ref entity.Add(in v5);
        ref var c6 = ref entity.Add(in v6);
        ref var c7 = ref entity.Add(in v7);
        ref var c8 = ref entity.Add(in v8);
        ref var c9 = ref entity.Add(in v9);
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ref c0, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6, ref c7, ref c8, ref c9);
    }

    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() where T6 : IComponent, new() where T7 : IComponent, new() where T8 : IComponent, new() where T9 : IComponent, new() {
        var result0 = entity.Has<T0>();
        var result1 = entity.Has<T1>();
        var result2 = entity.Has<T2>();
        var result3 = entity.Has<T3>();
        var result4 = entity.Has<T4>();
        var result5 = entity.Has<T5>();
        var result6 = entity.Has<T6>();
        var result7 = entity.Has<T7>();
        var result8 = entity.Has<T8>();
        var result9 = entity.Has<T9>();
        return result0 && result1 && result2 && result3 && result4 && result5 && result6 && result7 && result8 && result9;
    }

    public static Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() where T6 : IComponent, new() where T7 : IComponent, new() where T8 : IComponent, new() where T9 : IComponent, new() {
        ref var c0 = ref entity.Get<T0>();
        ref var c1 = ref entity.Get<T1>();
        ref var c2 = ref entity.Get<T2>();
        ref var c3 = ref entity.Get<T3>();
        ref var c4 = ref entity.Get<T4>();
        ref var c5 = ref entity.Get<T5>();
        ref var c6 = ref entity.Get<T6>();
        ref var c7 = ref entity.Get<T7>();
        ref var c8 = ref entity.Get<T8>();
        ref var c9 = ref entity.Get<T9>();
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ref c0, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6, ref c7, ref c8, ref c9);
    }

    public static bool Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Entity entity) where T0 : IComponent, new() where T1 : IComponent, new() where T2 : IComponent, new() where T3 : IComponent, new() where T4 : IComponent, new() where T5 : IComponent, new() where T6 : IComponent, new() where T7 : IComponent, new() where T8 : IComponent, new() where T9 : IComponent, new() {
        var result0 = entity.Remove<T0>();
        var result1 = entity.Remove<T1>();
        var result2 = entity.Remove<T2>();
        var result3 = entity.Remove<T3>();
        var result4 = entity.Remove<T4>();
        var result5 = entity.Remove<T5>();
        var result6 = entity.Remove<T6>();
        var result7 = entity.Remove<T7>();
        var result8 = entity.Remove<T8>();
        var result9 = entity.Remove<T9>();
        return result0 && result1 && result2 && result3 && result4 && result5 && result6 && result7 && result8 && result9;
    }
}


