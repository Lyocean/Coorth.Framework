using System;

namespace Coorth.Serialize; 

public interface ISerializeWriter {
    
    T GetContext<T>();
    
    void BeginRoot(Type type);
    void EndRoot();
    
    void BeginScope(Type type, SerializeScope scope);
    void EndScope();
    
    void BeginList(Type item, int count);
    void EndList();
    
    void BeginDict(Type key, Type value, int count);
    void EndDict();
    
    void WriteTag(string name, int index);
    void WriteKey<T>(in T key) where T : notnull;
    void WriteValue<T>(in T? value);
}