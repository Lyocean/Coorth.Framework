using System;

namespace Coorth.Serialize; 

public interface ISerializeReader {
    
    T GetContext<T>();
    
    void BeginRoot(Type type);
    void EndRoot();
    
    void BeginScope(Type type, SerializeScope scope);
    void EndScope();
    
    void BeginList(Type item, out int count);
    bool EndList();
    
    void BeginDict(Type key, Type value, out int count);
    bool EndDict();
    
    int ReadTag(string name, int index);
    // T? ReadField<T>(string name, int index);
    T ReadKey<T>() where T: notnull;
    T? ReadValue<T>();
}