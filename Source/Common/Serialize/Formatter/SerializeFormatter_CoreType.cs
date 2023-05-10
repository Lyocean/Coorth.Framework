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

[SerializeFormatter(typeof(Vector2))]
public sealed class SerializeFormatter_Vector2 : SerializeFormatter<Vector2> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in Vector2 value) {
        writer.BeginData<Vector2>(2);
        {
            writer.WriteTag(nameof(Vector2.X), 1);
            writer.WriteFloat64(value.X);

            writer.WriteTag(nameof(Vector2.Y), 2);
            writer.WriteFloat64(value.Y);
        }
        writer.EndData();
    }

    public override void SerializeReading(in SerializeReader reader, scoped ref Vector2 value) {
        reader.BeginData<Vector2>();
        {
            reader.ReadTag(nameof(Vector2.X), 1);
            var x = reader.ReadFloat32();

            reader.ReadTag(nameof(Vector2.Y), 2);
            var y = reader.ReadFloat32();
            value = new Vector2(x, y);
        }
        reader.EndData();
    }
}

[SerializeFormatter(typeof(Vector3))]
public sealed class SerializeFormatter_Vector3 : SerializeFormatter<Vector3> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in Vector3 value) {
        writer.BeginData<Vector3>(3);
        {
            writer.WriteTag(nameof(Vector3.X), 1);
            writer.WriteFloat64(value.X);

            writer.WriteTag(nameof(Vector3.Y), 2);
            writer.WriteFloat64(value.Y);
            
            writer.WriteTag(nameof(Vector3.Z), 3);
            writer.WriteFloat64(value.Z);
        }
        writer.EndData();
    }

    public override void SerializeReading(in SerializeReader reader, scoped ref Vector3 value) {
        reader.BeginData<Vector3>();
        {
            reader.ReadTag(nameof(Vector3.X), 1);
            var x = reader.ReadFloat32();

            reader.ReadTag(nameof(Vector3.Y), 2);
            var y = reader.ReadFloat32();
            
            reader.ReadTag(nameof(Vector3.Y), 3);
            var z = reader.ReadFloat32();
            
            value = new Vector3(x, y, z);
        }
        reader.EndData();
    }
}

[SerializeFormatter(typeof(Vector4))]
public sealed class SerializeFormatter_Vector4 : SerializeFormatter<Vector4> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in Vector4 value) {
        writer.BeginData<Vector4>(4);
        {
            writer.WriteTag(nameof(Vector4.X), 1);
            writer.WriteFloat64(value.X);

            writer.WriteTag(nameof(Vector4.Y), 2);
            writer.WriteFloat64(value.Y);
            
            writer.WriteTag(nameof(Vector4.Z), 3);
            writer.WriteFloat64(value.Z);
            
            writer.WriteTag(nameof(Vector4.W), 4);
            writer.WriteFloat64(value.W);
        }
        writer.EndData();
    }

    public override void SerializeReading(in SerializeReader reader, scoped ref Vector4 value) {
        reader.BeginData<Vector4>();
        {
            reader.ReadTag(nameof(Vector4.X), 1);
            var x = reader.ReadFloat32();

            reader.ReadTag(nameof(Vector4.Y), 2);
            var y = reader.ReadFloat32();
            
            reader.ReadTag(nameof(Vector4.Z), 3);
            var z = reader.ReadFloat32();
            
            reader.ReadTag(nameof(Vector4.W), 4);
            var w = reader.ReadFloat32();
            
            value = new Vector4(x, y, z, w);
        }
        reader.EndData();
    }
}

[SerializeFormatter(typeof(Quaternion))]
public sealed class SerializeFormatter_Quaternion : SerializeFormatter<Quaternion> {

    public override void SerializeWriting(in SerializeWriter writer, scoped in Quaternion value) {
        writer.BeginData<Quaternion>(4);
        {
            writer.WriteTag(nameof(Quaternion.X), 1);
            writer.WriteFloat64(value.X);

            writer.WriteTag(nameof(Quaternion.Y), 2);
            writer.WriteFloat64(value.Y);
            
            writer.WriteTag(nameof(Quaternion.Z), 3);
            writer.WriteFloat64(value.Z);
            
            writer.WriteTag(nameof(Quaternion.W), 4);
            writer.WriteFloat64(value.W);
        }
        writer.EndData();
    }

    public override void SerializeReading(in SerializeReader reader, scoped ref Quaternion value) {
        reader.BeginData<Quaternion>();
        {
            reader.ReadTag(nameof(Quaternion.X), 1);
            var x = reader.ReadFloat32();

            reader.ReadTag(nameof(Quaternion.Y), 2);
            var y = reader.ReadFloat32();
            
            reader.ReadTag(nameof(Quaternion.Z), 3);
            var z = reader.ReadFloat32();
            
            reader.ReadTag(nameof(Quaternion.W), 4);
            var w = reader.ReadFloat32();
            
            value = new Quaternion(x, y, z, w);
        }
        reader.EndData();
    }
}
