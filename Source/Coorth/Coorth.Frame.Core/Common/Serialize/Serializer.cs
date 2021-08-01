using System;
using System.IO;

namespace Coorth {
    public interface ISerializeReader {
        bool IsFinish();
        
        bool ReadBool();

        byte ReadByte();
        sbyte ReadSByte();

        short ReadInt16();
        ushort ReadUInt16();

        int ReadInt32();
        uint ReadUInt32();

        long ReadInt64();
        ulong ReadUInt64();

        float ReadSingle();
        double ReadDouble();

        string ReadString();
        Type ReadType();

        Type ReadType(Stream stream);

        Guid ReadGuid(Stream stream);

        T ReadValue<T>();
    }

    public interface ISerializeWriter {
        void WriteBool(bool value);

        void WriteByte(byte value);
        void WriteSByte(sbyte value);

        void WriteInt16(short value);
        void WriteUInt16(ushort value);

        void WriteInt32(int value);
        void WriteUInt32(uint value);

        void WriteInt64(long value);
        void WriteUInt64(ulong value);

        void WriteSingle(float value);
        void WriteDouble(double value);

        void WriteType(Type value);
        void WriteString(string value);

        void WriteValue<T>(T value);
    }

    public interface ISerializer : ISerializeReader, ISerializeWriter {
        
    }
}