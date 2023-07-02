using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;


namespace Coorth.Framework; 

public readonly record struct ComponentType(Type Type, int Id, int Size) {
    
    public readonly Type Type = Type;
    
    public readonly int Id = Id;

    public readonly int Size = Size;

    public override int GetHashCode() => Id;
}

public static class ComponentType<T> {
    
    public static readonly ComponentType Value;

    public static int Id => Value.Id;
    
    static ComponentType() {
        Value = ComponentRegistry.Get<T>();
    }
}

public static class ComponentRegistry {
    
    private static int count;
    public static int Count => count;

    private static ComponentType[] array = Array.Empty<ComponentType>();
    private static readonly Dictionary<Type, ComponentType> types = new(64);
    public static IReadOnlyDictionary<Type, ComponentType> Types => types;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGet(Type type, out ComponentType component_type) {
        return types.TryGetValue(type, out component_type);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentType Get(in ComponentType type) {
        return array[type.Id];
    }

    private static ComponentType AddComponent(Type type, int size) {
        var component_type = new ComponentType(type, Interlocked.Increment(ref count), size);
        while (component_type.Id >= array.Length) {
            var length = Math.Max(array.Length * 2, 16);
            Array.Resize(ref array, length);
        }
        array[component_type.Id] = component_type;
        types.Add(component_type.Type, component_type);
        return component_type;
    }
    
    public static ComponentType Get(Type type) {
        if (types.TryGetValue(type, out var component_type)) {
            return component_type;
        }
        var method = typeof(Unsafe).GetMethod(nameof(Unsafe.SizeOf))!.MakeGenericMethod(type);
        var size_method = (Func<int>)method.CreateDelegate(typeof(Func<int>));
        var component_size = type.IsValueType ? size_method() : IntPtr.Size;
        return AddComponent(type, component_size);
    }
    
    public static ComponentType Get<T>() {
        var type = typeof(T);
        if (types.TryGetValue(type, out var component_type)) {
            return component_type;
        }
        var component_size = type.IsValueType ? Unsafe.SizeOf<T>() : IntPtr.Size;
        return AddComponent(type, component_size);
    }

    public static ComponentMask ComputeMask(Span<ComponentType> span) {
        var length = 0;
        for (var i = 0; i < span.Length; i++) {
            length = span[i].Id > length ? span[i].Id : length;
        }
        var mask = new ComponentMask(length);
        for (var i = 0; i < span.Length; i++) {
            mask.Set(span[i].Id, true);
        }
        return mask;
    }
    
    public static int ComputeHash(Span<ComponentType> span) {
        if (span.Length == 0) {
            return 0;
        }
        switch (span.Length) {
            case 0:
                return 0;
            case 1:
                return span[0].Id;
            case 2:
                return HashCode.Combine(span[0].Id, span[1].Id);
            case 3:
                return HashCode.Combine(span[0].Id, span[1].Id, span[2].Id);
            case 4:
                return HashCode.Combine(span[0].Id, span[1].Id, span[2].Id, span[3].Id);
            case 5:
                return HashCode.Combine(span[0].Id, span[1].Id, span[2].Id, span[3].Id, span[4].Id);
            default:
                var hash = HashCode.Combine(span[0].Id, span[1].Id);
                for (var i = 2; i < span.Length; i++) {
                    hash = HashCode.Combine(hash, span[i].Id);
                }
                return hash;
        }
    }

    public static ComponentType[] Combine(ComponentType[] source, ComponentType type) {
        var result = new ComponentType[source.Length + 1];
        Array.Copy(source, result, source.Length);
        result[source.Length] = type;
        Array.Sort(result, (a, b) => a.Id.CompareTo(b.Id));
        return result;
    }

    public static ComponentType[] Combine(ComponentType[] source, ComponentType[] target) {
        var result = new ComponentType[source.Length + target.Length];
        Array.Copy(source, result, source.Length);
        Array.Copy(target, 0, result, source.Length, target.Length);
        Array.Sort(result, (a, b) => a.Id.CompareTo(b.Id));
        return result;
    }

    public static ComponentType[] Subtract(ComponentType[] source, ComponentType type) {
        var result = new ComponentType[source.Length - 1];
        var index = 0;
        for (var i = 0; i < source.Length; i++) {
            if (source[i].Id == type.Id) {
                continue;
            }
            result[index] = source[i];
            index++;
        }
        return result;
    }


    public static int ComputeSize(Span<ComponentType> span) {
        var size = 0;
        for (var i = 0; i < span.Length; i++) {
            size += span[i].Size;
        }
        return size;
    }
}
