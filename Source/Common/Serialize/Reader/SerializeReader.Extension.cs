using System.Collections.Generic;

namespace Coorth.Serialize;

public partial class SerializeReader {
    public void BeginScope<T>(SerializeScope scope) => BeginScope(typeof(T), scope);

    public void BeginList<T>(out int count) => BeginList(typeof(T), out count);

    public void BeginDict<TKey, TValue>(out int count) => BeginDict(typeof(TKey), typeof(TValue), out count);

    public void ReadValue<T>(out T? value) => value = ReadValue<T>();

    public T? ReadField<T>(string name, int index) {
        ReadTag(name, index);
        return ReadValue<T>();
    }
    
    public void ReadField<T>(string name, int index, out T? value) {
        ReadTag(name, index);
        ReadValue(out value);
    }
    
    public void ReadList<T>(ref List<T?>? list) {
        BeginList<T>(out var count);
        if (list == null) {
            list = count >=0 ? new List<T?>(count) : new List<T?>();
        } else {
            list.Clear();
        }
        if (count >= 0) {
            for (var i = 0; i < count; i++) {
                var value = ReadValue<T>();
                list.Add(value);
            }  
            EndList();
        }
        else {
            while (!EndList()) {
                var value = ReadValue<T>();
                list.Add(value);
            }
        }
    }
    
    public void ReadList<T>(ref T?[]? array) {
        BeginList<T>(out var count);
        if (count >= 0) {
            if (array == null || array.Length != count) {
                array = new T?[count];
            }
            for (var i = 0; i < count; i++) {
                var value = ReadValue<T>();
                array[i] = value;
            }
            EndList();
        }
        else {
            var list = new List<T?>();
            while (!EndList()) {
                var value = ReadValue<T>();
                list.Add(value);
            }
            array = list.ToArray();
        }
    }
    
    public void ReadDict<TK, TV>(ref Dictionary<TK, TV?>? dict) where TK : notnull {
        BeginDict<TK, TV>(out var count);
        if (dict == null) {
            dict = count > 0 ? new Dictionary<TK, TV?>(count) : new Dictionary<TK, TV?>();
        } else {
            dict.Clear();
        }
        if (count >= 0) {
            for (var i = 0; i < count; i++) {
                var key = ReadKey<TK>();
                var value = ReadValue<TV>();
                dict.Add(key, value);
            }
            EndList();
        }
        else {
            while (!EndDict()) {
                var key = ReadKey<TK>();
                var value = ReadValue<TV>();
                dict.Add(key, value);
            }
        }
    }
}