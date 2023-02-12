using System;


namespace Coorth.Serialize;

[SerializeFormatter(typeof(bool))]
public sealed class SerializeFormatter_Boolean : SerializeFormatter<bool> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in bool value) => writer.WriteBool(value);

    public override void SerializeReading(in SerializeReader reader, scoped ref bool value) => value = reader.ReadBool();

}

[SerializeFormatter(typeof(sbyte))]
public sealed class SerializeFormatter_Int8 : SerializeFormatter<sbyte> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in sbyte value) => writer.WriteInt8(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref sbyte value) => value = reader.ReadInt8();

}

[SerializeFormatter(typeof(byte))]
public sealed class SerializeFormatter_UInt8 : SerializeFormatter<byte> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in byte value) => writer.WriteUInt8(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref byte value) => value = reader.ReadUInt8();

}

[SerializeFormatter(typeof(short))]
public sealed class SerializeFormatter_Int16 : SerializeFormatter<short> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in short value) => writer.WriteInt16(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref short value) => value = reader.ReadInt16();

}

[SerializeFormatter(typeof(ushort))]
public sealed class SerializeFormatter_UInt16 : SerializeFormatter<ushort> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in ushort value) => writer.WriteUInt16(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref ushort value) => value = reader.ReadUInt16();

}

[SerializeFormatter(typeof(int))]
public sealed class SerializeFormatter_Int32 : SerializeFormatter<int> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in int value) => writer.WriteInt32(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref int value) => value = reader.ReadInt32();

}

[SerializeFormatter(typeof(uint))]
public sealed class SerializeFormatter_UInt32 : SerializeFormatter<uint> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in uint value) => writer.WriteUInt32(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref uint value) => value = reader.ReadUInt32();

}

[SerializeFormatter(typeof(long))]
public sealed class SerializeFormatter_Int64 : SerializeFormatter<long> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in long value) => writer.WriteInt64(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref long value) => value = reader.ReadInt64();

}

[SerializeFormatter(typeof(ulong))]
public sealed class SerializeFormatter_UInt64 : SerializeFormatter<ulong> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in ulong value) => writer.WriteUInt64(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref ulong value) => value = reader.ReadUInt64();

}

[SerializeFormatter(typeof(decimal))]
public sealed class SerializeFormatter_Decimal : SerializeFormatter<decimal> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in decimal value) => writer.WriteDecimal(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref decimal value) => value = reader.ReadDecimal();

}

#if NET5_0_OR_GREATER

[SerializeFormatter(typeof(Half))]
public sealed class SerializeFormatter_Float16 : SerializeFormatter<Half> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in Half value) => writer.WriteFloat16(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref Half value) => value = reader.ReadFloat16();

}

#endif

[SerializeFormatter(typeof(float))]
public sealed class SerializeFormatter_Float32 : SerializeFormatter<float> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in float value) => writer.WriteFloat32(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref float value) => value = reader.ReadFloat32();

}

[SerializeFormatter(typeof(double))]
public sealed class SerializeFormatter_Float64 : SerializeFormatter<double> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in double value) => writer.WriteFloat64(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref double value) => value = reader.ReadFloat64();

}

[SerializeFormatter(typeof(char))]
public sealed class SerializeFormatter_Char : SerializeFormatter<char> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in char value) => writer.WriteChar(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref char value) => value = reader.ReadChar();

}

[SerializeFormatter(typeof(string))]
public sealed class SerializeFormatter_String : SerializeFormatter<string> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in string value) => writer.WriteString(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref string? value) => value = reader.ReadString();

}