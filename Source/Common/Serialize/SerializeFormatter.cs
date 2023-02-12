using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Coorth.Serialize;

public interface ISerializeFormatter {
    
    void SerializeWriting(in SerializeWriter writer, scoped in object value);

    void SerializeReading(in SerializeReader reader, scoped ref object? value);

}

public interface ISerializeFormatter<T> {
    void SerializeWriting(in SerializeWriter writer, scoped in T value);

    void SerializeReading(in SerializeReader reader, scoped ref T? value);
}

public abstract class SerializeFormatter<T> : ISerializeFormatter, ISerializeFormatter<T> {
    
    public void SerializeWriting(in SerializeWriter writer, scoped in object value) {
        var v = (T)value;
        SerializeWriting(in writer, in v);
    }

    public abstract void SerializeWriting(in SerializeWriter writer, scoped in T value);
    
    public void SerializeReading(in SerializeReader reader, scoped ref object? value) {
        var v = value != null ? (T)value : default;  
        SerializeReading(in reader, ref v);
        value = v!;
    }

    public abstract void SerializeReading(in SerializeReader reader, scoped ref T? value);
    
}

[AttributeUsage(AttributeTargets.Class)]
public class SerializeFormatterAttribute : Attribute {
    public readonly Type Type;

    public SerializeFormatterAttribute(Type type) {
        Type = type;
    }
}

public static class SerializeFormatter {
        
    private static readonly Dictionary<Type, ISerializeFormatter> formatters = new();
    
    private struct Formatter<T> { public static volatile ISerializeFormatter<T>? Instance; }
    
    static SerializeFormatter() {
        TypeUtil.ForEachType(Load, true);
    }
        
    private static void Load(Type type) {
        var attribute = type.GetCustomAttribute<SerializeFormatterAttribute>();
        if (attribute == null) {
            return;
        }
        if (Activator.CreateInstance(type) is not ISerializeFormatter serializer) {
            throw new InvalidDataException($"{nameof(SerializeFormatterAttribute)} must be attribute of {typeof(ISerializeFormatter)}");
        }
        formatters.Add(attribute.Type, serializer);
    }

    public static void Register<T>(SerializeFormatter<T> formatter) {
        formatters[typeof(T)] = formatter;
        Formatter<T>.Instance = formatter;
    }
    
    public static ISerializeFormatter? GetFormatter(Type type) {
        return formatters.TryGetValue(type, out var formatter) ? formatter : null;
    }

    public static ISerializeFormatter<T>? GetFormatter<T>() {
        var serializer = Formatter<T>.Instance;
        if (serializer != null) {
            return serializer;
        }
        serializer = GetFormatter(typeof(T)) as ISerializeFormatter<T>;
        if (serializer != null) {
            Formatter<T>.Instance = serializer;
        }
        return serializer;
    }
}
