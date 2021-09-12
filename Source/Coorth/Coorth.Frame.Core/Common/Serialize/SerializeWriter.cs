using System;

namespace Coorth {
    public interface ISerializeWriter {
        SerializeScope WriteScope(Type type);
        SerializeList WriteList(Type type, int count);
        SerializeDict WriteDict(Type key, Type value, int count);

        void WriteTag(string name, int index);
        void WriteKey<T>(in T key);
        void WriteValue<T>(in T value);
    }
    
    
    public abstract class SerializeWriter : ISerializeWriter, ISerializerRegion {
        public abstract SerializeScope WriteScope(Type type);
        public abstract void EndScope();
        public abstract SerializeList WriteList(Type type, int count);
        public abstract void EndList();
        public abstract SerializeDict WriteDict(Type key, Type value, int count);
        public abstract void EndDict();
        
        public abstract void WriteTag(string name, int index);
        public abstract void WriteKey<T>(in T key);
        public abstract void WriteValue<T>(in T value);
    }

    public interface IBinarySerializeWriter {
        void Serialize<T>(BinarySerializeWriter writer, in T value);
        void DeSerialize<T>(BinarySerializeWriter reader, ref T value);
    }
    
    public abstract class BinarySerializeWriter : SerializeWriter {
        
    }
    
    public interface IStringSerializer {
        void Serialize<T>(StringSerializeWriter writer, in T value);
        void DeSerialize<T>(StringSerializeReader reader, ref T value);
    }
    
    public abstract class StringSerializeWriter : SerializeWriter {
        
    }
}