
using System;
using System.Runtime.CompilerServices;

namespace Coorth.Framework; 

#region Types

public static class ComponentTypes<T0> {
    public static readonly int Hash;
    public static readonly ComponentType[] Types;
    static ComponentTypes() {
        Types = new [] {
            ComponentRegistry.Get<T0>(),    
        };
        Array.Sort(Types, static (a, b) => a.Id.CompareTo(b.Id));
        Hash = ComponentRegistry.ComputeHash(Types.AsSpan());
    }
}
public static class ComponentTypes<T0, T1> {
    public static readonly int Hash;
    public static readonly ComponentType[] Types;
    static ComponentTypes() {
        Types = new [] {
            ComponentRegistry.Get<T0>(),    
            ComponentRegistry.Get<T1>(),    
        };
        Array.Sort(Types, static (a, b) => a.Id.CompareTo(b.Id));
        Hash = ComponentRegistry.ComputeHash(Types.AsSpan());
    }
}
public static class ComponentTypes<T0, T1, T2> {
    public static readonly int Hash;
    public static readonly ComponentType[] Types;
    static ComponentTypes() {
        Types = new [] {
            ComponentRegistry.Get<T0>(),    
            ComponentRegistry.Get<T1>(),    
            ComponentRegistry.Get<T2>(),    
        };
        Array.Sort(Types, static (a, b) => a.Id.CompareTo(b.Id));
        Hash = ComponentRegistry.ComputeHash(Types.AsSpan());
    }
}
public static class ComponentTypes<T0, T1, T2, T3> {
    public static readonly int Hash;
    public static readonly ComponentType[] Types;
    static ComponentTypes() {
        Types = new [] {
            ComponentRegistry.Get<T0>(),    
            ComponentRegistry.Get<T1>(),    
            ComponentRegistry.Get<T2>(),    
            ComponentRegistry.Get<T3>(),    
        };
        Array.Sort(Types, static (a, b) => a.Id.CompareTo(b.Id));
        Hash = ComponentRegistry.ComputeHash(Types.AsSpan());
    }
}
public static class ComponentTypes<T0, T1, T2, T3, T4> {
    public static readonly int Hash;
    public static readonly ComponentType[] Types;
    static ComponentTypes() {
        Types = new [] {
            ComponentRegistry.Get<T0>(),    
            ComponentRegistry.Get<T1>(),    
            ComponentRegistry.Get<T2>(),    
            ComponentRegistry.Get<T3>(),    
            ComponentRegistry.Get<T4>(),    
        };
        Array.Sort(Types, static (a, b) => a.Id.CompareTo(b.Id));
        Hash = ComponentRegistry.ComputeHash(Types.AsSpan());
    }
}
public static class ComponentTypes<T0, T1, T2, T3, T4, T5> {
    public static readonly int Hash;
    public static readonly ComponentType[] Types;
    static ComponentTypes() {
        Types = new [] {
            ComponentRegistry.Get<T0>(),    
            ComponentRegistry.Get<T1>(),    
            ComponentRegistry.Get<T2>(),    
            ComponentRegistry.Get<T3>(),    
            ComponentRegistry.Get<T4>(),    
            ComponentRegistry.Get<T5>(),    
        };
        Array.Sort(Types, static (a, b) => a.Id.CompareTo(b.Id));
        Hash = ComponentRegistry.ComputeHash(Types.AsSpan());
    }
}
public static class ComponentTypes<T0, T1, T2, T3, T4, T5, T6> {
    public static readonly int Hash;
    public static readonly ComponentType[] Types;
    static ComponentTypes() {
        Types = new [] {
            ComponentRegistry.Get<T0>(),    
            ComponentRegistry.Get<T1>(),    
            ComponentRegistry.Get<T2>(),    
            ComponentRegistry.Get<T3>(),    
            ComponentRegistry.Get<T4>(),    
            ComponentRegistry.Get<T5>(),    
            ComponentRegistry.Get<T6>(),    
        };
        Array.Sort(Types, static (a, b) => a.Id.CompareTo(b.Id));
        Hash = ComponentRegistry.ComputeHash(Types.AsSpan());
    }
}
public static class ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7> {
    public static readonly int Hash;
    public static readonly ComponentType[] Types;
    static ComponentTypes() {
        Types = new [] {
            ComponentRegistry.Get<T0>(),    
            ComponentRegistry.Get<T1>(),    
            ComponentRegistry.Get<T2>(),    
            ComponentRegistry.Get<T3>(),    
            ComponentRegistry.Get<T4>(),    
            ComponentRegistry.Get<T5>(),    
            ComponentRegistry.Get<T6>(),    
            ComponentRegistry.Get<T7>(),    
        };
        Array.Sort(Types, static (a, b) => a.Id.CompareTo(b.Id));
        Hash = ComponentRegistry.ComputeHash(Types.AsSpan());
    }
}
public static class ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7, T8> {
    public static readonly int Hash;
    public static readonly ComponentType[] Types;
    static ComponentTypes() {
        Types = new [] {
            ComponentRegistry.Get<T0>(),    
            ComponentRegistry.Get<T1>(),    
            ComponentRegistry.Get<T2>(),    
            ComponentRegistry.Get<T3>(),    
            ComponentRegistry.Get<T4>(),    
            ComponentRegistry.Get<T5>(),    
            ComponentRegistry.Get<T6>(),    
            ComponentRegistry.Get<T7>(),    
            ComponentRegistry.Get<T8>(),    
        };
        Array.Sort(Types, static (a, b) => a.Id.CompareTo(b.Id));
        Hash = ComponentRegistry.ComputeHash(Types.AsSpan());
    }
}
public static class ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> {
    public static readonly int Hash;
    public static readonly ComponentType[] Types;
    static ComponentTypes() {
        Types = new [] {
            ComponentRegistry.Get<T0>(),    
            ComponentRegistry.Get<T1>(),    
            ComponentRegistry.Get<T2>(),    
            ComponentRegistry.Get<T3>(),    
            ComponentRegistry.Get<T4>(),    
            ComponentRegistry.Get<T5>(),    
            ComponentRegistry.Get<T6>(),    
            ComponentRegistry.Get<T7>(),    
            ComponentRegistry.Get<T8>(),    
            ComponentRegistry.Get<T9>(),    
        };
        Array.Sort(Types, static (a, b) => a.Id.CompareTo(b.Id));
        Hash = ComponentRegistry.ComputeHash(Types.AsSpan());
    }
}
#endregion


#region Components

#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct Components<T0> {

    public readonly ref T0 Value0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0) {
        Value0 = ref v0;
    }

    public void Deconstruct(out T0 v0) {
        v0 = Value0;
    }
}

#else

public ref struct Components<T0> {

    private readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0) {
        value0 = new Ref<T0>(ref v0);
    }

    public void Deconstruct(out T0 v0) {
        v0 = value0.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct Components<T0, T1> {

    public readonly ref T0 Value0;
    public readonly ref T1 Value1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1) {
        Value0 = ref v0;
        Value1 = ref v1;
    }

    public void Deconstruct(out T0 v0, out T1 v1) {
        v0 = Value0;
        v1 = Value1;
    }
}

#else

public ref struct Components<T0, T1> {

    private readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    private readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1) {
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
    }

    public void Deconstruct(out T0 v0, out T1 v1) {
        v0 = value0.Value;
        v1 = value1.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct Components<T0, T1, T2> {

    public readonly ref T0 Value0;
    public readonly ref T1 Value1;
    public readonly ref T2 Value2;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1, ref T2 v2) {
        Value0 = ref v0;
        Value1 = ref v1;
        Value2 = ref v2;
    }

    public void Deconstruct(out T0 v0, out T1 v1, out T2 v2) {
        v0 = Value0;
        v1 = Value1;
        v2 = Value2;
    }
}

#else

public ref struct Components<T0, T1, T2> {

    private readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    private readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;
    private readonly Ref<T2> value2;
    public ref T2 Value2 => ref value2.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1, ref T2 v2) {
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
        value2 = new Ref<T2>(ref v2);
    }

    public void Deconstruct(out T0 v0, out T1 v1, out T2 v2) {
        v0 = value0.Value;
        v1 = value1.Value;
        v2 = value2.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct Components<T0, T1, T2, T3> {

    public readonly ref T0 Value0;
    public readonly ref T1 Value1;
    public readonly ref T2 Value2;
    public readonly ref T3 Value3;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3) {
        Value0 = ref v0;
        Value1 = ref v1;
        Value2 = ref v2;
        Value3 = ref v3;
    }

    public void Deconstruct(out T0 v0, out T1 v1, out T2 v2, out T3 v3) {
        v0 = Value0;
        v1 = Value1;
        v2 = Value2;
        v3 = Value3;
    }
}

#else

public ref struct Components<T0, T1, T2, T3> {

    private readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    private readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;
    private readonly Ref<T2> value2;
    public ref T2 Value2 => ref value2.Value;
    private readonly Ref<T3> value3;
    public ref T3 Value3 => ref value3.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3) {
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
        value2 = new Ref<T2>(ref v2);
        value3 = new Ref<T3>(ref v3);
    }

    public void Deconstruct(out T0 v0, out T1 v1, out T2 v2, out T3 v3) {
        v0 = value0.Value;
        v1 = value1.Value;
        v2 = value2.Value;
        v3 = value3.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct Components<T0, T1, T2, T3, T4> {

    public readonly ref T0 Value0;
    public readonly ref T1 Value1;
    public readonly ref T2 Value2;
    public readonly ref T3 Value3;
    public readonly ref T4 Value4;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4) {
        Value0 = ref v0;
        Value1 = ref v1;
        Value2 = ref v2;
        Value3 = ref v3;
        Value4 = ref v4;
    }

    public void Deconstruct(out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4) {
        v0 = Value0;
        v1 = Value1;
        v2 = Value2;
        v3 = Value3;
        v4 = Value4;
    }
}

#else

public ref struct Components<T0, T1, T2, T3, T4> {

    private readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    private readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;
    private readonly Ref<T2> value2;
    public ref T2 Value2 => ref value2.Value;
    private readonly Ref<T3> value3;
    public ref T3 Value3 => ref value3.Value;
    private readonly Ref<T4> value4;
    public ref T4 Value4 => ref value4.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4) {
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
        value2 = new Ref<T2>(ref v2);
        value3 = new Ref<T3>(ref v3);
        value4 = new Ref<T4>(ref v4);
    }

    public void Deconstruct(out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4) {
        v0 = value0.Value;
        v1 = value1.Value;
        v2 = value2.Value;
        v3 = value3.Value;
        v4 = value4.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct Components<T0, T1, T2, T3, T4, T5> {

    public readonly ref T0 Value0;
    public readonly ref T1 Value1;
    public readonly ref T2 Value2;
    public readonly ref T3 Value3;
    public readonly ref T4 Value4;
    public readonly ref T5 Value5;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5) {
        Value0 = ref v0;
        Value1 = ref v1;
        Value2 = ref v2;
        Value3 = ref v3;
        Value4 = ref v4;
        Value5 = ref v5;
    }

    public void Deconstruct(out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5) {
        v0 = Value0;
        v1 = Value1;
        v2 = Value2;
        v3 = Value3;
        v4 = Value4;
        v5 = Value5;
    }
}

#else

public ref struct Components<T0, T1, T2, T3, T4, T5> {

    private readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    private readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;
    private readonly Ref<T2> value2;
    public ref T2 Value2 => ref value2.Value;
    private readonly Ref<T3> value3;
    public ref T3 Value3 => ref value3.Value;
    private readonly Ref<T4> value4;
    public ref T4 Value4 => ref value4.Value;
    private readonly Ref<T5> value5;
    public ref T5 Value5 => ref value5.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5) {
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
        value2 = new Ref<T2>(ref v2);
        value3 = new Ref<T3>(ref v3);
        value4 = new Ref<T4>(ref v4);
        value5 = new Ref<T5>(ref v5);
    }

    public void Deconstruct(out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5) {
        v0 = value0.Value;
        v1 = value1.Value;
        v2 = value2.Value;
        v3 = value3.Value;
        v4 = value4.Value;
        v5 = value5.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct Components<T0, T1, T2, T3, T4, T5, T6> {

    public readonly ref T0 Value0;
    public readonly ref T1 Value1;
    public readonly ref T2 Value2;
    public readonly ref T3 Value3;
    public readonly ref T4 Value4;
    public readonly ref T5 Value5;
    public readonly ref T6 Value6;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5, ref T6 v6) {
        Value0 = ref v0;
        Value1 = ref v1;
        Value2 = ref v2;
        Value3 = ref v3;
        Value4 = ref v4;
        Value5 = ref v5;
        Value6 = ref v6;
    }

    public void Deconstruct(out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6) {
        v0 = Value0;
        v1 = Value1;
        v2 = Value2;
        v3 = Value3;
        v4 = Value4;
        v5 = Value5;
        v6 = Value6;
    }
}

#else

public ref struct Components<T0, T1, T2, T3, T4, T5, T6> {

    private readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    private readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;
    private readonly Ref<T2> value2;
    public ref T2 Value2 => ref value2.Value;
    private readonly Ref<T3> value3;
    public ref T3 Value3 => ref value3.Value;
    private readonly Ref<T4> value4;
    public ref T4 Value4 => ref value4.Value;
    private readonly Ref<T5> value5;
    public ref T5 Value5 => ref value5.Value;
    private readonly Ref<T6> value6;
    public ref T6 Value6 => ref value6.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5, ref T6 v6) {
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
        value2 = new Ref<T2>(ref v2);
        value3 = new Ref<T3>(ref v3);
        value4 = new Ref<T4>(ref v4);
        value5 = new Ref<T5>(ref v5);
        value6 = new Ref<T6>(ref v6);
    }

    public void Deconstruct(out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6) {
        v0 = value0.Value;
        v1 = value1.Value;
        v2 = value2.Value;
        v3 = value3.Value;
        v4 = value4.Value;
        v5 = value5.Value;
        v6 = value6.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7> {

    public readonly ref T0 Value0;
    public readonly ref T1 Value1;
    public readonly ref T2 Value2;
    public readonly ref T3 Value3;
    public readonly ref T4 Value4;
    public readonly ref T5 Value5;
    public readonly ref T6 Value6;
    public readonly ref T7 Value7;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5, ref T6 v6, ref T7 v7) {
        Value0 = ref v0;
        Value1 = ref v1;
        Value2 = ref v2;
        Value3 = ref v3;
        Value4 = ref v4;
        Value5 = ref v5;
        Value6 = ref v6;
        Value7 = ref v7;
    }

    public void Deconstruct(out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6, out T7 v7) {
        v0 = Value0;
        v1 = Value1;
        v2 = Value2;
        v3 = Value3;
        v4 = Value4;
        v5 = Value5;
        v6 = Value6;
        v7 = Value7;
    }
}

#else

public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7> {

    private readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    private readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;
    private readonly Ref<T2> value2;
    public ref T2 Value2 => ref value2.Value;
    private readonly Ref<T3> value3;
    public ref T3 Value3 => ref value3.Value;
    private readonly Ref<T4> value4;
    public ref T4 Value4 => ref value4.Value;
    private readonly Ref<T5> value5;
    public ref T5 Value5 => ref value5.Value;
    private readonly Ref<T6> value6;
    public ref T6 Value6 => ref value6.Value;
    private readonly Ref<T7> value7;
    public ref T7 Value7 => ref value7.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5, ref T6 v6, ref T7 v7) {
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
        value2 = new Ref<T2>(ref v2);
        value3 = new Ref<T3>(ref v3);
        value4 = new Ref<T4>(ref v4);
        value5 = new Ref<T5>(ref v5);
        value6 = new Ref<T6>(ref v6);
        value7 = new Ref<T7>(ref v7);
    }

    public void Deconstruct(out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6, out T7 v7) {
        v0 = value0.Value;
        v1 = value1.Value;
        v2 = value2.Value;
        v3 = value3.Value;
        v4 = value4.Value;
        v5 = value5.Value;
        v6 = value6.Value;
        v7 = value7.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8> {

    public readonly ref T0 Value0;
    public readonly ref T1 Value1;
    public readonly ref T2 Value2;
    public readonly ref T3 Value3;
    public readonly ref T4 Value4;
    public readonly ref T5 Value5;
    public readonly ref T6 Value6;
    public readonly ref T7 Value7;
    public readonly ref T8 Value8;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5, ref T6 v6, ref T7 v7, ref T8 v8) {
        Value0 = ref v0;
        Value1 = ref v1;
        Value2 = ref v2;
        Value3 = ref v3;
        Value4 = ref v4;
        Value5 = ref v5;
        Value6 = ref v6;
        Value7 = ref v7;
        Value8 = ref v8;
    }

    public void Deconstruct(out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6, out T7 v7, out T8 v8) {
        v0 = Value0;
        v1 = Value1;
        v2 = Value2;
        v3 = Value3;
        v4 = Value4;
        v5 = Value5;
        v6 = Value6;
        v7 = Value7;
        v8 = Value8;
    }
}

#else

public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8> {

    private readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    private readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;
    private readonly Ref<T2> value2;
    public ref T2 Value2 => ref value2.Value;
    private readonly Ref<T3> value3;
    public ref T3 Value3 => ref value3.Value;
    private readonly Ref<T4> value4;
    public ref T4 Value4 => ref value4.Value;
    private readonly Ref<T5> value5;
    public ref T5 Value5 => ref value5.Value;
    private readonly Ref<T6> value6;
    public ref T6 Value6 => ref value6.Value;
    private readonly Ref<T7> value7;
    public ref T7 Value7 => ref value7.Value;
    private readonly Ref<T8> value8;
    public ref T8 Value8 => ref value8.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5, ref T6 v6, ref T7 v7, ref T8 v8) {
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
        value2 = new Ref<T2>(ref v2);
        value3 = new Ref<T3>(ref v3);
        value4 = new Ref<T4>(ref v4);
        value5 = new Ref<T5>(ref v5);
        value6 = new Ref<T6>(ref v6);
        value7 = new Ref<T7>(ref v7);
        value8 = new Ref<T8>(ref v8);
    }

    public void Deconstruct(out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6, out T7 v7, out T8 v8) {
        v0 = value0.Value;
        v1 = value1.Value;
        v2 = value2.Value;
        v3 = value3.Value;
        v4 = value4.Value;
        v5 = value5.Value;
        v6 = value6.Value;
        v7 = value7.Value;
        v8 = value8.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> {

    public readonly ref T0 Value0;
    public readonly ref T1 Value1;
    public readonly ref T2 Value2;
    public readonly ref T3 Value3;
    public readonly ref T4 Value4;
    public readonly ref T5 Value5;
    public readonly ref T6 Value6;
    public readonly ref T7 Value7;
    public readonly ref T8 Value8;
    public readonly ref T9 Value9;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5, ref T6 v6, ref T7 v7, ref T8 v8, ref T9 v9) {
        Value0 = ref v0;
        Value1 = ref v1;
        Value2 = ref v2;
        Value3 = ref v3;
        Value4 = ref v4;
        Value5 = ref v5;
        Value6 = ref v6;
        Value7 = ref v7;
        Value8 = ref v8;
        Value9 = ref v9;
    }

    public void Deconstruct(out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6, out T7 v7, out T8 v8, out T9 v9) {
        v0 = Value0;
        v1 = Value1;
        v2 = Value2;
        v3 = Value3;
        v4 = Value4;
        v5 = Value5;
        v6 = Value6;
        v7 = Value7;
        v8 = Value8;
        v9 = Value9;
    }
}

#else

public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> {

    private readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    private readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;
    private readonly Ref<T2> value2;
    public ref T2 Value2 => ref value2.Value;
    private readonly Ref<T3> value3;
    public ref T3 Value3 => ref value3.Value;
    private readonly Ref<T4> value4;
    public ref T4 Value4 => ref value4.Value;
    private readonly Ref<T5> value5;
    public ref T5 Value5 => ref value5.Value;
    private readonly Ref<T6> value6;
    public ref T6 Value6 => ref value6.Value;
    private readonly Ref<T7> value7;
    public ref T7 Value7 => ref value7.Value;
    private readonly Ref<T8> value8;
    public ref T8 Value8 => ref value8.Value;
    private readonly Ref<T9> value9;
    public ref T9 Value9 => ref value9.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5, ref T6 v6, ref T7 v7, ref T8 v8, ref T9 v9) {
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
        value2 = new Ref<T2>(ref v2);
        value3 = new Ref<T3>(ref v3);
        value4 = new Ref<T4>(ref v4);
        value5 = new Ref<T5>(ref v5);
        value6 = new Ref<T6>(ref v6);
        value7 = new Ref<T7>(ref v7);
        value8 = new Ref<T8>(ref v8);
        value9 = new Ref<T9>(ref v9);
    }

    public void Deconstruct(out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6, out T7 v7, out T8 v8, out T9 v9) {
        v0 = value0.Value;
        v1 = value1.Value;
        v2 = value2.Value;
        v3 = value3.Value;
        v4 = value4.Value;
        v5 = value5.Value;
        v6 = value6.Value;
        v7 = value7.Value;
        v8 = value8.Value;
        v9 = value9.Value;
    }
}
#endif

#endregion


#region EntityComponents

#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct EntityComponents<T0> {
    public readonly Entity Entity;
    public readonly ref T0 Value0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0) {
        Entity = entity;
        Value0 = ref v0;
    }

    public void Deconstruct(out Entity entity, out T0 v0) {
        entity = Entity;
        v0 = Value0;
    }
}

#else

public ref struct EntityComponents<T0> {
    public readonly Entity Entity;
    public readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0) {
        Entity = entity;
        value0 = new Ref<T0>(ref v0);
    }

    public void Deconstruct(out Entity entity, out T0 v0) {
        entity = Entity;
        v0 = value0.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct EntityComponents<T0, T1> {
    public readonly Entity Entity;
    public readonly ref T0 Value0;
    public readonly ref T1 Value1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1) {
        Entity = entity;
        Value0 = ref v0;
        Value1 = ref v1;
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1) {
        entity = Entity;
        v0 = Value0;
        v1 = Value1;
    }
}

#else

public ref struct EntityComponents<T0, T1> {
    public readonly Entity Entity;
    public readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    public readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1) {
        Entity = entity;
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1) {
        entity = Entity;
        v0 = value0.Value;
        v1 = value1.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct EntityComponents<T0, T1, T2> {
    public readonly Entity Entity;
    public readonly ref T0 Value0;
    public readonly ref T1 Value1;
    public readonly ref T2 Value2;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1, ref T2 v2) {
        Entity = entity;
        Value0 = ref v0;
        Value1 = ref v1;
        Value2 = ref v2;
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1, out T2 v2) {
        entity = Entity;
        v0 = Value0;
        v1 = Value1;
        v2 = Value2;
    }
}

#else

public ref struct EntityComponents<T0, T1, T2> {
    public readonly Entity Entity;
    public readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    public readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;
    public readonly Ref<T2> value2;
    public ref T2 Value2 => ref value2.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1, ref T2 v2) {
        Entity = entity;
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
        value2 = new Ref<T2>(ref v2);
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1, out T2 v2) {
        entity = Entity;
        v0 = value0.Value;
        v1 = value1.Value;
        v2 = value2.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct EntityComponents<T0, T1, T2, T3> {
    public readonly Entity Entity;
    public readonly ref T0 Value0;
    public readonly ref T1 Value1;
    public readonly ref T2 Value2;
    public readonly ref T3 Value3;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3) {
        Entity = entity;
        Value0 = ref v0;
        Value1 = ref v1;
        Value2 = ref v2;
        Value3 = ref v3;
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1, out T2 v2, out T3 v3) {
        entity = Entity;
        v0 = Value0;
        v1 = Value1;
        v2 = Value2;
        v3 = Value3;
    }
}

#else

public ref struct EntityComponents<T0, T1, T2, T3> {
    public readonly Entity Entity;
    public readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    public readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;
    public readonly Ref<T2> value2;
    public ref T2 Value2 => ref value2.Value;
    public readonly Ref<T3> value3;
    public ref T3 Value3 => ref value3.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3) {
        Entity = entity;
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
        value2 = new Ref<T2>(ref v2);
        value3 = new Ref<T3>(ref v3);
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1, out T2 v2, out T3 v3) {
        entity = Entity;
        v0 = value0.Value;
        v1 = value1.Value;
        v2 = value2.Value;
        v3 = value3.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct EntityComponents<T0, T1, T2, T3, T4> {
    public readonly Entity Entity;
    public readonly ref T0 Value0;
    public readonly ref T1 Value1;
    public readonly ref T2 Value2;
    public readonly ref T3 Value3;
    public readonly ref T4 Value4;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4) {
        Entity = entity;
        Value0 = ref v0;
        Value1 = ref v1;
        Value2 = ref v2;
        Value3 = ref v3;
        Value4 = ref v4;
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4) {
        entity = Entity;
        v0 = Value0;
        v1 = Value1;
        v2 = Value2;
        v3 = Value3;
        v4 = Value4;
    }
}

#else

public ref struct EntityComponents<T0, T1, T2, T3, T4> {
    public readonly Entity Entity;
    public readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    public readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;
    public readonly Ref<T2> value2;
    public ref T2 Value2 => ref value2.Value;
    public readonly Ref<T3> value3;
    public ref T3 Value3 => ref value3.Value;
    public readonly Ref<T4> value4;
    public ref T4 Value4 => ref value4.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4) {
        Entity = entity;
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
        value2 = new Ref<T2>(ref v2);
        value3 = new Ref<T3>(ref v3);
        value4 = new Ref<T4>(ref v4);
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4) {
        entity = Entity;
        v0 = value0.Value;
        v1 = value1.Value;
        v2 = value2.Value;
        v3 = value3.Value;
        v4 = value4.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct EntityComponents<T0, T1, T2, T3, T4, T5> {
    public readonly Entity Entity;
    public readonly ref T0 Value0;
    public readonly ref T1 Value1;
    public readonly ref T2 Value2;
    public readonly ref T3 Value3;
    public readonly ref T4 Value4;
    public readonly ref T5 Value5;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5) {
        Entity = entity;
        Value0 = ref v0;
        Value1 = ref v1;
        Value2 = ref v2;
        Value3 = ref v3;
        Value4 = ref v4;
        Value5 = ref v5;
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5) {
        entity = Entity;
        v0 = Value0;
        v1 = Value1;
        v2 = Value2;
        v3 = Value3;
        v4 = Value4;
        v5 = Value5;
    }
}

#else

public ref struct EntityComponents<T0, T1, T2, T3, T4, T5> {
    public readonly Entity Entity;
    public readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    public readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;
    public readonly Ref<T2> value2;
    public ref T2 Value2 => ref value2.Value;
    public readonly Ref<T3> value3;
    public ref T3 Value3 => ref value3.Value;
    public readonly Ref<T4> value4;
    public ref T4 Value4 => ref value4.Value;
    public readonly Ref<T5> value5;
    public ref T5 Value5 => ref value5.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5) {
        Entity = entity;
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
        value2 = new Ref<T2>(ref v2);
        value3 = new Ref<T3>(ref v3);
        value4 = new Ref<T4>(ref v4);
        value5 = new Ref<T5>(ref v5);
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5) {
        entity = Entity;
        v0 = value0.Value;
        v1 = value1.Value;
        v2 = value2.Value;
        v3 = value3.Value;
        v4 = value4.Value;
        v5 = value5.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct EntityComponents<T0, T1, T2, T3, T4, T5, T6> {
    public readonly Entity Entity;
    public readonly ref T0 Value0;
    public readonly ref T1 Value1;
    public readonly ref T2 Value2;
    public readonly ref T3 Value3;
    public readonly ref T4 Value4;
    public readonly ref T5 Value5;
    public readonly ref T6 Value6;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5, ref T6 v6) {
        Entity = entity;
        Value0 = ref v0;
        Value1 = ref v1;
        Value2 = ref v2;
        Value3 = ref v3;
        Value4 = ref v4;
        Value5 = ref v5;
        Value6 = ref v6;
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6) {
        entity = Entity;
        v0 = Value0;
        v1 = Value1;
        v2 = Value2;
        v3 = Value3;
        v4 = Value4;
        v5 = Value5;
        v6 = Value6;
    }
}

#else

public ref struct EntityComponents<T0, T1, T2, T3, T4, T5, T6> {
    public readonly Entity Entity;
    public readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    public readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;
    public readonly Ref<T2> value2;
    public ref T2 Value2 => ref value2.Value;
    public readonly Ref<T3> value3;
    public ref T3 Value3 => ref value3.Value;
    public readonly Ref<T4> value4;
    public ref T4 Value4 => ref value4.Value;
    public readonly Ref<T5> value5;
    public ref T5 Value5 => ref value5.Value;
    public readonly Ref<T6> value6;
    public ref T6 Value6 => ref value6.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5, ref T6 v6) {
        Entity = entity;
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
        value2 = new Ref<T2>(ref v2);
        value3 = new Ref<T3>(ref v3);
        value4 = new Ref<T4>(ref v4);
        value5 = new Ref<T5>(ref v5);
        value6 = new Ref<T6>(ref v6);
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6) {
        entity = Entity;
        v0 = value0.Value;
        v1 = value1.Value;
        v2 = value2.Value;
        v3 = value3.Value;
        v4 = value4.Value;
        v5 = value5.Value;
        v6 = value6.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct EntityComponents<T0, T1, T2, T3, T4, T5, T6, T7> {
    public readonly Entity Entity;
    public readonly ref T0 Value0;
    public readonly ref T1 Value1;
    public readonly ref T2 Value2;
    public readonly ref T3 Value3;
    public readonly ref T4 Value4;
    public readonly ref T5 Value5;
    public readonly ref T6 Value6;
    public readonly ref T7 Value7;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5, ref T6 v6, ref T7 v7) {
        Entity = entity;
        Value0 = ref v0;
        Value1 = ref v1;
        Value2 = ref v2;
        Value3 = ref v3;
        Value4 = ref v4;
        Value5 = ref v5;
        Value6 = ref v6;
        Value7 = ref v7;
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6, out T7 v7) {
        entity = Entity;
        v0 = Value0;
        v1 = Value1;
        v2 = Value2;
        v3 = Value3;
        v4 = Value4;
        v5 = Value5;
        v6 = Value6;
        v7 = Value7;
    }
}

#else

public ref struct EntityComponents<T0, T1, T2, T3, T4, T5, T6, T7> {
    public readonly Entity Entity;
    public readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    public readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;
    public readonly Ref<T2> value2;
    public ref T2 Value2 => ref value2.Value;
    public readonly Ref<T3> value3;
    public ref T3 Value3 => ref value3.Value;
    public readonly Ref<T4> value4;
    public ref T4 Value4 => ref value4.Value;
    public readonly Ref<T5> value5;
    public ref T5 Value5 => ref value5.Value;
    public readonly Ref<T6> value6;
    public ref T6 Value6 => ref value6.Value;
    public readonly Ref<T7> value7;
    public ref T7 Value7 => ref value7.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5, ref T6 v6, ref T7 v7) {
        Entity = entity;
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
        value2 = new Ref<T2>(ref v2);
        value3 = new Ref<T3>(ref v3);
        value4 = new Ref<T4>(ref v4);
        value5 = new Ref<T5>(ref v5);
        value6 = new Ref<T6>(ref v6);
        value7 = new Ref<T7>(ref v7);
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6, out T7 v7) {
        entity = Entity;
        v0 = value0.Value;
        v1 = value1.Value;
        v2 = value2.Value;
        v3 = value3.Value;
        v4 = value4.Value;
        v5 = value5.Value;
        v6 = value6.Value;
        v7 = value7.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct EntityComponents<T0, T1, T2, T3, T4, T5, T6, T7, T8> {
    public readonly Entity Entity;
    public readonly ref T0 Value0;
    public readonly ref T1 Value1;
    public readonly ref T2 Value2;
    public readonly ref T3 Value3;
    public readonly ref T4 Value4;
    public readonly ref T5 Value5;
    public readonly ref T6 Value6;
    public readonly ref T7 Value7;
    public readonly ref T8 Value8;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5, ref T6 v6, ref T7 v7, ref T8 v8) {
        Entity = entity;
        Value0 = ref v0;
        Value1 = ref v1;
        Value2 = ref v2;
        Value3 = ref v3;
        Value4 = ref v4;
        Value5 = ref v5;
        Value6 = ref v6;
        Value7 = ref v7;
        Value8 = ref v8;
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6, out T7 v7, out T8 v8) {
        entity = Entity;
        v0 = Value0;
        v1 = Value1;
        v2 = Value2;
        v3 = Value3;
        v4 = Value4;
        v5 = Value5;
        v6 = Value6;
        v7 = Value7;
        v8 = Value8;
    }
}

#else

public ref struct EntityComponents<T0, T1, T2, T3, T4, T5, T6, T7, T8> {
    public readonly Entity Entity;
    public readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    public readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;
    public readonly Ref<T2> value2;
    public ref T2 Value2 => ref value2.Value;
    public readonly Ref<T3> value3;
    public ref T3 Value3 => ref value3.Value;
    public readonly Ref<T4> value4;
    public ref T4 Value4 => ref value4.Value;
    public readonly Ref<T5> value5;
    public ref T5 Value5 => ref value5.Value;
    public readonly Ref<T6> value6;
    public ref T6 Value6 => ref value6.Value;
    public readonly Ref<T7> value7;
    public ref T7 Value7 => ref value7.Value;
    public readonly Ref<T8> value8;
    public ref T8 Value8 => ref value8.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5, ref T6 v6, ref T7 v7, ref T8 v8) {
        Entity = entity;
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
        value2 = new Ref<T2>(ref v2);
        value3 = new Ref<T3>(ref v3);
        value4 = new Ref<T4>(ref v4);
        value5 = new Ref<T5>(ref v5);
        value6 = new Ref<T6>(ref v6);
        value7 = new Ref<T7>(ref v7);
        value8 = new Ref<T8>(ref v8);
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6, out T7 v7, out T8 v8) {
        entity = Entity;
        v0 = value0.Value;
        v1 = value1.Value;
        v2 = value2.Value;
        v3 = value3.Value;
        v4 = value4.Value;
        v5 = value5.Value;
        v6 = value6.Value;
        v7 = value7.Value;
        v8 = value8.Value;
    }
}
#endif


#if NET7_0_OR_GREATER

[SkipLocalsInit]
public readonly ref struct EntityComponents<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> {
    public readonly Entity Entity;
    public readonly ref T0 Value0;
    public readonly ref T1 Value1;
    public readonly ref T2 Value2;
    public readonly ref T3 Value3;
    public readonly ref T4 Value4;
    public readonly ref T5 Value5;
    public readonly ref T6 Value6;
    public readonly ref T7 Value7;
    public readonly ref T8 Value8;
    public readonly ref T9 Value9;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5, ref T6 v6, ref T7 v7, ref T8 v8, ref T9 v9) {
        Entity = entity;
        Value0 = ref v0;
        Value1 = ref v1;
        Value2 = ref v2;
        Value3 = ref v3;
        Value4 = ref v4;
        Value5 = ref v5;
        Value6 = ref v6;
        Value7 = ref v7;
        Value8 = ref v8;
        Value9 = ref v9;
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6, out T7 v7, out T8 v8, out T9 v9) {
        entity = Entity;
        v0 = Value0;
        v1 = Value1;
        v2 = Value2;
        v3 = Value3;
        v4 = Value4;
        v5 = Value5;
        v6 = Value6;
        v7 = Value7;
        v8 = Value8;
        v9 = Value9;
    }
}

#else

public ref struct EntityComponents<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> {
    public readonly Entity Entity;
    public readonly Ref<T0> value0;
    public ref T0 Value0 => ref value0.Value;
    public readonly Ref<T1> value1;
    public ref T1 Value1 => ref value1.Value;
    public readonly Ref<T2> value2;
    public ref T2 Value2 => ref value2.Value;
    public readonly Ref<T3> value3;
    public ref T3 Value3 => ref value3.Value;
    public readonly Ref<T4> value4;
    public ref T4 Value4 => ref value4.Value;
    public readonly Ref<T5> value5;
    public ref T5 Value5 => ref value5.Value;
    public readonly Ref<T6> value6;
    public ref T6 Value6 => ref value6.Value;
    public readonly Ref<T7> value7;
    public ref T7 Value7 => ref value7.Value;
    public readonly Ref<T8> value8;
    public ref T8 Value8 => ref value8.Value;
    public readonly Ref<T9> value9;
    public ref T9 Value9 => ref value9.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(Entity entity, ref T0 v0, ref T1 v1, ref T2 v2, ref T3 v3, ref T4 v4, ref T5 v5, ref T6 v6, ref T7 v7, ref T8 v8, ref T9 v9) {
        Entity = entity;
        value0 = new Ref<T0>(ref v0);
        value1 = new Ref<T1>(ref v1);
        value2 = new Ref<T2>(ref v2);
        value3 = new Ref<T3>(ref v3);
        value4 = new Ref<T4>(ref v4);
        value5 = new Ref<T5>(ref v5);
        value6 = new Ref<T6>(ref v6);
        value7 = new Ref<T7>(ref v7);
        value8 = new Ref<T8>(ref v8);
        value9 = new Ref<T9>(ref v9);
    }

    public void Deconstruct(out Entity entity, out T0 v0, out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6, out T7 v7, out T8 v8, out T9 v9) {
        entity = Entity;
        v0 = value0.Value;
        v1 = value1.Value;
        v2 = value2.Value;
        v3 = value3.Value;
        v4 = value4.Value;
        v5 = value5.Value;
        v6 = value6.Value;
        v7 = value7.Value;
        v8 = value8.Value;
        v9 = value9.Value;
    }
}
#endif

#endregion


#region Archetype

public partial class World {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype CreateArchetype<T0>() {
        var types = ComponentTypes<T0>.Types;
        return CreateArchetype(types.AsSpan());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype CreateArchetype<T0, T1>() {
        var types = ComponentTypes<T0, T1>.Types;
        return CreateArchetype(types.AsSpan());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype CreateArchetype<T0, T1, T2>() {
        var types = ComponentTypes<T0, T1, T2>.Types;
        return CreateArchetype(types.AsSpan());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype CreateArchetype<T0, T1, T2, T3>() {
        var types = ComponentTypes<T0, T1, T2, T3>.Types;
        return CreateArchetype(types.AsSpan());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype CreateArchetype<T0, T1, T2, T3, T4>() {
        var types = ComponentTypes<T0, T1, T2, T3, T4>.Types;
        return CreateArchetype(types.AsSpan());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype CreateArchetype<T0, T1, T2, T3, T4, T5>() {
        var types = ComponentTypes<T0, T1, T2, T3, T4, T5>.Types;
        return CreateArchetype(types.AsSpan());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype CreateArchetype<T0, T1, T2, T3, T4, T5, T6>() {
        var types = ComponentTypes<T0, T1, T2, T3, T4, T5, T6>.Types;
        return CreateArchetype(types.AsSpan());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype CreateArchetype<T0, T1, T2, T3, T4, T5, T6, T7>() {
        var types = ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7>.Types;
        return CreateArchetype(types.AsSpan());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype CreateArchetype<T0, T1, T2, T3, T4, T5, T6, T7, T8>() {
        var types = ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7, T8>.Types;
        return CreateArchetype(types.AsSpan());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype CreateArchetype<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>() {
        var types = ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.Types;
        return CreateArchetype(types.AsSpan());
    }

}

#endregion
