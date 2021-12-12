using System;

namespace Coorth.Serializes {
    [Serializer(typeof(bool))]
    public class BoolSerializer : Serializer<bool> {
        public override void Write(SerializeWriter writer, in bool value) => writer.WriteBool(value);
        public override bool Read(SerializeReader reader, bool value) => reader.ReadBool();
    }
    
    [Serializer(typeof(byte))]
    public class ByteSerializer : Serializer<byte> {
        public override void Write(SerializeWriter writer, in byte value) => writer.WriteByte(value);
        public override byte Read(SerializeReader reader, byte value) => reader.ReadByte();
    }
    
    [Serializer(typeof(sbyte))]
    public class SByteSerializer : Serializer<sbyte> {
        public override void Write(SerializeWriter writer, in sbyte value) => writer.WriteSByte(value);
        public override sbyte Read(SerializeReader reader, sbyte value) => reader.ReadSByte();
    }
    
    [Serializer(typeof(short))]
    public class ShortSerializer : Serializer<short> {
        public override void Write(SerializeWriter writer, in short value) => writer.WriteShort(value);
        public override short Read(SerializeReader reader, short value) => reader.ReadShort();
    }

    [Serializer(typeof(ushort))]
    public class UShortSerializer : Serializer<ushort> {
        public override void Write(SerializeWriter writer, in ushort value) => writer.WriteUShort(value);
        public override ushort Read(SerializeReader reader, ushort value) => reader.ReadUShort();
    }
    
    [Serializer(typeof(int))]
    public class IntSerializer : Serializer<int> {
        public override void Write(SerializeWriter writer, in int value) => writer.WriteInt(value);
        public override int Read(SerializeReader reader, int value) => reader.ReadInt();
    }
    
    [Serializer(typeof(uint))]
    public class UIntSerializer : Serializer<uint> {
        public override void Write(SerializeWriter writer, in uint value) => writer.WriteUInt(value);
        public override uint Read(SerializeReader reader, uint value) => reader.ReadUInt();
    }
    
    [Serializer(typeof(long))]
    public class LongSerializer : Serializer<long> {
        public override void Write(SerializeWriter writer, in long value) => writer.WriteLong(value);
        public override long Read(SerializeReader reader, long value) => reader.ReadUInt();
    }
    
    [Serializer(typeof(ulong))]
    public class ULongSerializer : Serializer<ulong> {
        public override void Write(SerializeWriter writer, in ulong value) => writer.WriteULong(value);
        public override ulong Read(SerializeReader reader, ulong value) => reader.ReadULong();
    }
    
    [Serializer(typeof(float))]
    public class FloatSerializer : Serializer<float> {
        public override void Write(SerializeWriter writer, in float value) => writer.WriteFloat(value);
        public override float Read(SerializeReader reader, float value) => reader.ReadFloat();
    }
    
    [Serializer(typeof(double))]
    public class DoubleSerializer : Serializer<double> {
        public override void Write(SerializeWriter writer, in double value) => writer.WriteDouble(value);
        public override double Read(SerializeReader reader, double value) => reader.ReadDouble();
    }
    
    [Serializer(typeof(char))]
    public class CharSerializer : Serializer<char> {
        public override void Write(SerializeWriter writer, in char value) => writer.WriteChar(value);
        public override char Read(SerializeReader reader, char value) => reader.ReadChar();
    }
    
    [Serializer(typeof(string))]
    public class StringSerializer : Serializer<string> {
        public override void Write(SerializeWriter writer, in string value) => writer.WriteString(value);
        public override string Read(SerializeReader reader, string value) => reader.ReadString();
    }

    [Serializer(typeof(DateTime))]
    public class DateTimeSerializer : Serializer<DateTime> {
        public override void Write(SerializeWriter writer, in DateTime value) => writer.WriteDateTime(value);
        public override DateTime Read(SerializeReader reader, DateTime value) => reader.ReadDateTime();
    }

    [Serializer(typeof(TimeSpan))]
    public class TimeSpanSerializer : Serializer<TimeSpan> {
        public override void Write(SerializeWriter writer, in TimeSpan value) => writer.WriteTimeSpan(value);
        public override TimeSpan Read(SerializeReader reader, TimeSpan value) => reader.ReadTimeSpan();
    }

    [Serializer(typeof(Guid))]
    public class GuidSerializer : Serializer<Guid> {
        public override void Write(SerializeWriter writer, in Guid value) => writer.WriteGuid(value);
        public override Guid Read(SerializeReader reader, Guid value) => reader.ReadGuid();
    }

    [Serializer(typeof(Type))]
    public class TypeSerializer : Serializer<Type> {
        public override void Write(SerializeWriter writer, in Type value) => writer.WriteType(value);
        public override Type Read(SerializeReader reader, Type value) => reader.ReadType();
    }
    
    public class EnumSerializer<T> : Serializer<T> where T : Enum {
        public override void Write(SerializeWriter writer, in T value) {
            writer.WriteEnum(value);
        }

        public override T Read(SerializeReader reader, T value) {
            return reader.ReadEnum<T>();
        }
    }
}