using System;
using System.IO;

namespace Coorth {
    public interface ISerializer {
        
        T Read<T>();

        void Read<T>(ref T value);
        
        void WriteTag(string name, int index);

        void WriteValue<T>(in T value);

        void WriteDictBegin(bool isObject, Type type);

        void WriteKey<T>(in T key);
        
        void WriteDictEnd();

        void WriteListBegin(Type type);
        
        void WriteListEnd();
    }

    public interface ISerializer<T> {

        void Read(ref T value);

        void Write(in T value);
    }
}

namespace Coorth.Serializes {
    public abstract class SerializerBase : ISerializer {
        public T Read<T>() {
            T value = default;
            Read(ref value);
            return value;
        }

        public abstract void Read<T>(ref T value);

        public abstract void WriteDictBegin(Type type);

        public abstract void WriteDictBegin(bool isObject, Type type);

        public abstract void WriteKey<T>(in T key);

        public abstract void WriteDictEnd();
        
        public abstract void WriteListBegin(Type type);
        
        public abstract void WriteListEnd();

        public abstract void WriteTag(string name, int index);

        public abstract void WriteValue<T>(in T value);
    }

    public abstract class SerializerBase<TSerializer> : SerializerBase {
        protected struct Serializer<T> {
            public static Func<TSerializer, T> Reader;
            public static Action<TSerializer, T> Writer;
        }
    }
}

    // public interface ISerializeReader {
    //     bool IsFinish();
    //     
    //     bool ReadBool();
    //
    //     byte ReadByte();
    //     sbyte ReadSByte();
    //
    //     short ReadInt16();
    //     ushort ReadUInt16();
    //
    //     int ReadInt32();
    //     uint ReadUInt32();
    //
    //     long ReadInt64();
    //     ulong ReadUInt64();
    //
    //     float ReadSingle();
    //     double ReadDouble();
    //
    //     string ReadString();
    //     Type ReadType();
    //
    //     Type ReadType(Stream stream);
    //
    //     Guid ReadGuid(Stream stream);
    //
    //     T ReadValue<T>();
    // }
    //
    // public interface ISerializeWriter {
    //     void WriteBool(bool value);
    //
    //     void WriteByte(byte value);
    //     void WriteSByte(sbyte value);
    //
    //     void WriteInt16(short value);
    //     void WriteUInt16(ushort value);
    //
    //     void WriteInt32(int value);
    //     void WriteUInt32(uint value);
    //
    //     void WriteInt64(long value);
    //     void WriteUInt64(ulong value);
    //
    //     void WriteSingle(float value);
    //     void WriteDouble(double value);
    //
    //     void WriteType(Type value);
    //     void WriteString(string value);
    //
    //     void WriteValue<T>(T value);
    // }