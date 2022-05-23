using System.Collections.Generic;

namespace Coorth.Serialize;

public static class SerializeReaderExtension {
    public static void BeginScope<T>(this ISerializeReader reader, SerializeScope scope) {
        reader.BeginScope(typeof(T), scope);
    }
    
    public static void BeginList<T>(this ISerializeReader reader, out int count) {
        reader.BeginList(typeof(T), out count);
    }

    public static void BeginDict<TKey, TValue>(this ISerializeReader reader, out int count) {
        reader.BeginDict(typeof(TKey), typeof(TValue), out count);
    }

    public static void ReadValue<T>(this ISerializeReader reader, out T? value) {
        value = reader.ReadValue<T>();
    }
    
    public static T? ReadField<T>(this ISerializeReader reader, string name, int index) {
        reader.ReadTag(name, index);
        return reader.ReadValue<T>();
    }
    
    public static void ReadField<T>(this ISerializeReader reader, string name, int index, out T? value) {
        reader.ReadTag(name, index);
        reader.ReadValue(out value);
    }
    
    public static void ReadList<T>(this ISerializeReader reader, ref List<T?>? list) {
        reader.BeginList<T>(out var count);
        if (list == null) {
            list = count >=0 ? new List<T?>(count) : new List<T?>();
        } else {
            list.Clear();
        }
        if (count >= 0) {
            for (var i = 0; i < count; i++) {
                var value = reader.ReadValue<T>();
                list.Add(value);
            }  
            reader.EndList();
        }
        else {
            while (!reader.EndList()) {
                var value = reader.ReadValue<T>();
                list.Add(value);
            }
        }
    }
    
    public static void ReadList<T>(this ISerializeReader reader, ref T?[]? array) {
        reader.BeginList<T>(out var count);
        if (count >= 0) {
            if (array == null || array.Length != count) {
                array = new T?[count];
            }
            for (var i = 0; i < count; i++) {
                var value = reader.ReadValue<T>();
                array[i] = value;
            }
            reader.EndList();
        }
        else {
            var list = new List<T?>();
            while (!reader.EndList()) {
                var value = reader.ReadValue<T>();
                list.Add(value);
            }
            array = list.ToArray();
        }
    }
    
    public static void ReadDict<TK, TV>(this ISerializeReader reader, ref Dictionary<TK, TV?>? dict) where TK : notnull {
        reader.BeginDict<TK, TV>(out var count);
        if (dict == null) {
            dict = count > 0 ? new Dictionary<TK, TV?>(count) : new Dictionary<TK, TV?>();
        } else {
            dict.Clear();
        }
        if (count >= 0) {
            for (var i = 0; i < count; i++) {
                var key = reader.ReadKey<TK>();
                var value = reader.ReadValue<TV>();
                dict.Add(key, value);
            }
            reader.EndList();
        }
        else {
            while (!reader.EndDict()) {
                var key = reader.ReadKey<TK>();
                var value = reader.ReadValue<TV>();
                dict.Add(key, value);
            }
        }
    }
}