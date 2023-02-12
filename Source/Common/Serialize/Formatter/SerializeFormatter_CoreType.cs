using System;
using System.Numerics;


namespace Coorth.Serialize;

[SerializeFormatter(typeof(DateTime))]
public sealed class SerializeFormatter_DateTime : SerializeFormatter<DateTime> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in DateTime value) => writer.WriteDateTime(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref DateTime value) => value = reader.ReadDateTime();

}

[SerializeFormatter(typeof(TimeSpan))]
public sealed class SerializeFormatter_TimeSpan : SerializeFormatter<TimeSpan> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in TimeSpan value) => writer.WriteTimeSpan(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref TimeSpan value) => value = reader.ReadTimeSpan();

}

[SerializeFormatter(typeof(Guid))]
public sealed class SerializeFormatter_Guid : SerializeFormatter<Guid> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in Guid value) => writer.WriteGuid(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref Guid value) => value = reader.ReadGuid();

}

[SerializeFormatter(typeof(Type))]
public sealed class SerializeFormatter_Type : SerializeFormatter<Type> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in Type value) => writer.WriteType(value);
    public override void SerializeReading(in SerializeReader reader, scoped ref Type? value) => value = reader.ReadType();
    
}

[SerializeFormatter(typeof(Complex))]
public sealed class SerializeFormatter_Complex : SerializeFormatter<Complex> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in Complex value) {
        writer.BeginData<Complex>(2);
        {
            writer.WriteTag(nameof(Complex.Real), 1);
            writer.WriteFloat64(value.Real);

            writer.WriteTag(nameof(Complex.Imaginary), 2);
            writer.WriteFloat64(value.Imaginary);
        }
        writer.EndData();
    }

    public override void SerializeReading(in SerializeReader reader, scoped ref Complex value) {
        reader.BeginData<Complex>();
        {
            reader.ReadTag(nameof(Complex.Real), 1);
            var real = reader.ReadFloat32();

            reader.ReadTag(nameof(Complex.Imaginary), 2);
            var imaginary = reader.ReadFloat32();
            value = new Complex(real, imaginary);
        }
        reader.EndData();
    }
}

