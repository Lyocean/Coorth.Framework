using System.Collections.Generic;

namespace Coorth.Serialize;

public partial class SerializeWriter {
    
    public void BeginScope<T>(SerializeScope scope) => BeginScope(typeof(T), scope);

    public void BeginList<T>(int count) => BeginList(typeof(T), count);

    public void BeginDict<TKey, TValue>(int count) => BeginDict(typeof(TKey), typeof(TValue), count);

    public void WriteField<T>(string name, int index, in T value) {
        WriteTag(name, index);
        WriteValue(value);
    }
    
    public void WriteList<T>(in IList<T> list) {
        BeginList<T>(list.Count);
        foreach (var value in list) {
            WriteValue(value);
        }
        EndList();
    }
        
    public void WriteList<T>(IReadOnlyList<T> list) {
        BeginList<T>(list.Count);
        foreach (var value in list) {
            WriteValue(value);
        }
        EndList();
    }
        
    public void WriteDict<TK, TV>(IDictionary<TK, TV>? dict) where TK : notnull {
        if(dict == null){
            BeginDict<TK, TV>(0);
            EndDict();
            return;
        }
        BeginDict<TK, TV>(dict.Count);
        foreach (var pair in dict) {
            WriteKey(pair.Key);
            WriteValue(pair.Value);
        }
        EndDict();
    }
        
    public void WriteDict<TK, TV>(IReadOnlyDictionary<TK, TV>? dict) where TK : notnull {
        if(dict == null){
            BeginDict<TK, TV>(0);
            EndDict();
            return;
        }
        BeginDict<TK, TV>(dict.Count);
        foreach (var pair in dict) {
            WriteKey(pair.Key);
            WriteValue(pair.Value);
        }
        EndDict();
    }
}