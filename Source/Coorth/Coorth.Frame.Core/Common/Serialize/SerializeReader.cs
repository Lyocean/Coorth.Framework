using System;

namespace Coorth {
    public interface ISerializeReader {
        SerializeScope ReadScope(Type type);
        SerializeList ReadList(Type item, out int count);
        SerializeDict ReadDict(Type key, Type value, out int count);
        
        int ReadTag(string name);
        TKey ReadKey<TKey>();
        void ReadValue<T>(ref T value);
        T ReadValue<T>();
    }
    
    public abstract class SerializeReader : ISerializeReader, ISerializerRegion {
        public abstract SerializeScope ReadScope(Type type);
        public abstract SerializeList ReadList(Type item, out int count);
        public abstract SerializeDict ReadDict(Type key, Type value, out int count);
        public abstract void EndScope();
        public abstract void EndList();
        public abstract void EndDict();
        
        public abstract int ReadTag(string name);
        public abstract TKey ReadKey<TKey>();
        public abstract void ReadValue<T>(ref T value);
        public virtual T ReadValue<T>() {
            T value = default;
            ReadValue(ref value);
            return value;
        }
    }
    
    public abstract class BinarySerializeReader : SerializeReader {
        
    }
    
    public abstract class StringSerializeReader : SerializeReader {
        
    }
}