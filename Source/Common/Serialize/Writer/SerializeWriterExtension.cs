using System.Collections.Generic;

namespace Coorth.Serialize;

public static class SerializeWriterExtension {
    
    public static void BeginScope<T>(this ISerializeWriter writer, SerializeScope scope) {
        writer.BeginScope(typeof(T), scope);
    }
    
    public static void BeginList<T>(this ISerializeWriter writer, int count) {
        writer.BeginList(typeof(T), count);
    }

    public static void BeginDict<TKey, TValue>(this ISerializeWriter writer, int count) {
        writer.BeginDict(typeof(TKey), typeof(TValue), count);
    }

    public static void WriteField<T>(this ISerializeWriter writer, string name, int index, in T value) {
        writer.WriteTag(name, index);
        writer.WriteValue(value);
    }
    
    public static void WriteList<T>(this ISerializeWriter writer, in IList<T> list) {
        writer.BeginList<T>(list.Count);
        foreach (var value in list) {
            writer.WriteValue(value);
        }
        writer.EndList();
    }
        
    public static void WriteList<T>(this ISerializeWriter writer, IReadOnlyList<T> list) {
        writer.BeginList<T>(list.Count);
        foreach (var value in list) {
            writer.WriteValue(value);
        }
        writer.EndList();
    }
        
    public static void WriteDict<TK, TV>(this ISerializeWriter writer, IDictionary<TK, TV>? dict) where TK : notnull {
        if(dict == null){
            writer.BeginDict<TK, TV>(0);
            writer.EndDict();
            return;
        }
        writer.BeginDict<TK, TV>(dict.Count);
        foreach (var pair in dict) {
            writer.WriteKey(pair.Key);
            writer.WriteValue(pair.Value);
        }
        writer.EndDict();
    }
        
    public static void WriteDict<TK, TV>(this ISerializeWriter writer, IReadOnlyDictionary<TK, TV>? dict) where TK : notnull {
        if(dict == null){
            writer.BeginDict<TK, TV>(0);
            writer.EndDict();
            return;
        }
        writer.BeginDict<TK, TV>(dict.Count);
        foreach (var pair in dict) {
            writer.WriteKey(pair.Key);
            writer.WriteValue(pair.Value);
        }
        writer.EndDict();
    }
}