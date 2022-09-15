using System;

namespace Coorth.Serialize; 

[Serializer(typeof(bool))]
public sealed class BoolSerializer : Serializer<bool> {
    public override void Write(SerializeWriter writer, in bool value) => writer.WriteBool(value);
    public override bool Read(SerializeReader reader, bool value) => reader.ReadBool();
}

[Serializer(typeof(byte))]
public sealed class UInt8Serializer : Serializer<byte> {
    public override void Write(SerializeWriter writer, in byte value) => writer.WriteUInt8(value);
    public override byte Read(SerializeReader reader, byte value) => reader.ReadUInt8();
}

[Serializer(typeof(sbyte))]
public sealed class Int8Serializer : Serializer<sbyte> {
    public override void Write(SerializeWriter writer, in sbyte value) => writer.WriteInt8(value);
    public override sbyte Read(SerializeReader reader, sbyte value) => reader.ReadInt8(); 
}

[Serializer(typeof(short))]
public sealed class Int16Serializer : Serializer<short> {
    public override void Write(SerializeWriter writer, in short value) => writer.WriteInt16(value);
    public override short Read(SerializeReader reader, short value) => reader.ReadInt16(); 
}

[Serializer(typeof(ushort))]
public sealed class UInt16Serializer : Serializer<ushort> {
    public override void Write(SerializeWriter writer, in ushort value) => writer.WriteUInt16(value);
    public override ushort Read(SerializeReader reader, ushort value) => reader.ReadUInt16(); 
}

[Serializer(typeof(int))]
public sealed class Int32Serializer : Serializer<int> {
    public override void Write(SerializeWriter writer, in int value) => writer.WriteInt32(value);
    public override int Read(SerializeReader reader, int value) => reader.ReadInt32(); 
}

[Serializer(typeof(uint))]
public sealed class UInt32Serializer : Serializer<uint> {
    public override void Write(SerializeWriter writer, in uint value) => writer.WriteUInt32(value);
    public override uint Read(SerializeReader reader, uint value) => reader.ReadUInt32(); 
}

[Serializer(typeof(long))]
public sealed class Int64Serializer : Serializer<long> {
    public override void Write(SerializeWriter writer, in long value) => writer.WriteInt64(value);
    public override long Read(SerializeReader reader, long value) => reader.ReadInt64(); 
}

[Serializer(typeof(ulong))]
public sealed class UInt64Serializer : Serializer<ulong> {
    public override void Write(SerializeWriter writer, in ulong value) => writer.WriteUInt64(value);
    public override ulong Read(SerializeReader reader, ulong value) => reader.ReadUInt64(); 
}

#if NET5_0_OR_GREATER
[Serializer(typeof(Half))]
    public sealed class Float16Serializer : Serializer<Half> {
        public override void Write(SerializeWriter writer, in Half value) => writer.WriteFloat16(value);
        public override Half Read(SerializeReader reader, Half value) => reader.ReadFloat16(); 
    }
#endif

[Serializer(typeof(float))]
public sealed class Float32Serializer : Serializer<float> {
    public override void Write(SerializeWriter writer, in float value) => writer.WriteFloat32(value);
    public override float Read(SerializeReader reader, float value) => reader.ReadFloat32(); 
}

[Serializer(typeof(double))]
public sealed class Float64Serializer : Serializer<double> {
    public override void Write(SerializeWriter writer, in double value) => writer.WriteFloat64(value);
    public override double Read(SerializeReader reader, double value) => reader.ReadFloat64(); 
}

[Serializer(typeof(decimal))]
public sealed class DecimalSerializer : Serializer<decimal> {
    public override void Write(SerializeWriter writer, in decimal value) => writer.WriteDecimal(value);
    public override decimal Read(SerializeReader reader, decimal value) => reader.ReadDecimal(); 
}


[Serializer(typeof(char))]
public sealed class CharSerializer : Serializer<char> {
    public override void Write(SerializeWriter writer, in char value) => writer.WriteChar(value);
    public override char Read(SerializeReader reader, char value) => reader.ReadChar(); 
}

[Serializer(typeof(string))]
public sealed class StringSerializer : Serializer<string> {
    public override void Write(SerializeWriter writer, in string? value) => writer.WriteString(value);
    public override string? Read(SerializeReader reader, string? value) => reader.ReadString(); 
}


[Serializer(typeof(DateTime))]
public sealed class DateTimeSerializer : Serializer<DateTime> {
    public override void Write(SerializeWriter writer, in DateTime value) => writer.WriteDateTime(value);
    public override DateTime Read(SerializeReader reader, DateTime value) => reader.ReadDateTime(); 
}


[Serializer(typeof(TimeSpan))]
public sealed class TimeSpanSerializer : Serializer<TimeSpan> {
    public override void Write(SerializeWriter writer, in TimeSpan value) => writer.WriteTimeSpan(value);
    public override TimeSpan Read(SerializeReader reader, TimeSpan value) => reader.ReadTimeSpan(); 
}

[Serializer(typeof(Type))]
public sealed class TypeSerializer : Serializer<Type> {
    public override void Write(SerializeWriter writer, in Type? value) => writer.WriteType(value!);
    public override Type Read(SerializeReader reader, Type? value) => reader.ReadType(); 
}

[Serializer(typeof(Guid))]
public sealed class GuidSerializer : Serializer<Guid> {
    public override void Write(SerializeWriter writer, in Guid value) => writer.WriteGuid(value);
    public override Guid Read(SerializeReader reader, Guid value) => reader.ReadGuid(); 
}