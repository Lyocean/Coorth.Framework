using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Coorth.Collections;

namespace Coorth.Serialize; 

public abstract class Serializers {
        
    private static readonly Dictionary<Type, Serializer> serializers = new();
    private struct Impl<T> {
        public static volatile Serializer<T>? Instance;
    }
    
    static Serializers() {
        TypeUtil.ForEachType(Load, true);
    }
        
    private static void Load(Type type) {
        var attribute = type.GetCustomAttribute<SerializerAttribute>();
        if (attribute == null) {
            return;
        }
        if (Activator.CreateInstance(type) is not Serializer serializer) {
            throw new InvalidDataException($"{nameof(SerializerAttribute)} must be attribute of {typeof(Serializer)}");
        }
        serializers.Add(attribute.Type, serializer);
    }
        
    public static Serializer? GetSerializer(Type type) {
        return serializers.TryGetValue(type, out var serializer) ? serializer : null;
    }

    public static Serializer<T>? GetSerializer<T>() {
        var serializer = Impl<T>.Instance;
        if (serializer != null) {
            return serializer;
        }
        serializer = GetSerializer(typeof(T)) as Serializer<T>;
        if (serializer != null) {
            Impl<T>.Instance = serializer;
        }
        return serializer;
    }
}