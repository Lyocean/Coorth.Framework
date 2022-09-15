using System.Numerics;
using Coorth.Maths;

namespace Coorth.Serialize; 

[Serializer(typeof(Vector2))]
internal sealed class Vector2Serializer : Serializer<Vector2> {
    
    public override void Write(SerializeWriter writer, in Vector2 value) {
        writer.BeginScope(typeof(Vector2), SerializeScope.Tuple);
        
        writer.WriteTag(nameof(value.X), 1);
        writer.WriteFloat32(value.X);
        
        writer.WriteTag(nameof(value.Y), 2);
        writer.WriteFloat32(value.Y);
        
        writer.EndScope();
    }
    
    public override Vector2 Read(SerializeReader reader, Vector2 value) {
        reader.BeginScope(typeof(Vector2), SerializeScope.Tuple);
        
        reader.ReadTag(nameof(value.X), 1);
        value.X = reader.ReadFloat32();
        
        reader.ReadTag(nameof(value.Y), 2);
        value.Y = reader.ReadFloat32();
        
        reader.EndScope();
        return value;
    }
}

[Serializer(typeof(Vector3))]
internal sealed class Vector3Serializer : Serializer<Vector3> {
    
    public override void Write(SerializeWriter writer, in Vector3 value) {
        writer.BeginScope(typeof(Vector3), SerializeScope.Tuple);
        
        writer.WriteTag(nameof(value.X), 1);
        writer.WriteFloat32(value.X);
        
        writer.WriteTag(nameof(value.Y), 2);
        writer.WriteFloat32(value.Y);

        writer.WriteTag(nameof(value.Z), 3);
        writer.WriteFloat32(value.Z);
        
        writer.EndScope();
    }
    
    public override Vector3 Read(SerializeReader reader, Vector3 value) {
        reader.BeginScope(typeof(Vector3), SerializeScope.Tuple);
        
        reader.ReadTag(nameof(value.X), 1);
        value.X = reader.ReadFloat32();
        
        reader.ReadTag(nameof(value.Y), 2);
        value.Y = reader.ReadFloat32();
        
        reader.ReadTag(nameof(value.Z), 3);
        value.Z = reader.ReadFloat32();
        
        reader.EndScope();
        return value;
    }
}

[Serializer(typeof(Vector4))]
internal sealed class Vector4Serializer : Serializer<Vector4> {
    
    public override void Write(SerializeWriter writer, in Vector4 value) {
        writer.BeginScope(typeof(Vector4), SerializeScope.Tuple);
        
        writer.WriteTag(nameof(value.X), 1);
        writer.WriteFloat32(value.X);
        
        writer.WriteTag(nameof(value.Y), 2);
        writer.WriteFloat32(value.Y);

        writer.WriteTag(nameof(value.Z), 3);
        writer.WriteFloat32(value.Z);
        
        writer.WriteTag(nameof(value.W), 4);
        writer.WriteFloat32(value.W);

        writer.EndScope();
    }
    
    public override Vector4 Read(SerializeReader reader, Vector4 value) {
        reader.BeginScope(typeof(Vector4), SerializeScope.Tuple);
        
        reader.ReadTag(nameof(value.X), 1);
        value.X = reader.ReadFloat32();
        
        reader.ReadTag(nameof(value.Y), 2);
        value.Y = reader.ReadFloat32();
        
        reader.ReadTag(nameof(value.Z), 3);
        value.Z = reader.ReadFloat32();

        reader.ReadTag(nameof(value.W), 4);
        value.W = reader.ReadFloat32();

        reader.EndScope();
        return value;
    }
}

[Serializer(typeof(Quaternion))]
internal sealed class QuaternionSerializer : Serializer<Quaternion> {
    public override void Write(SerializeWriter writer, in Quaternion value) {
        writer.BeginScope(typeof(Quaternion), SerializeScope.Tuple);
        
        writer.WriteTag(nameof(value.X), 1);
        writer.WriteFloat32(value.X);
        
        writer.WriteTag(nameof(value.Y), 2);
        writer.WriteFloat32(value.Y);

        writer.WriteTag(nameof(value.Z), 3);
        writer.WriteFloat32(value.Z);
        
        writer.WriteTag(nameof(value.W), 4);
        writer.WriteFloat32(value.W);

        writer.EndScope();
    }
    
    public override Quaternion Read(SerializeReader reader, Quaternion value) {
        reader.BeginScope(typeof(Quaternion), SerializeScope.Tuple);
        
        reader.ReadTag(nameof(value.X), 1);
        value.X = reader.ReadFloat32();
        
        reader.ReadTag(nameof(value.Y), 2);
        value.Y = reader.ReadFloat32();
        
        reader.ReadTag(nameof(value.Z), 3);
        value.Z = reader.ReadFloat32();

        reader.ReadTag(nameof(value.W), 4);
        value.W = reader.ReadFloat32();

        reader.EndScope();
        return value;
    }
}

[Serializer(typeof(Int2))]
internal sealed class Int2Serializer : Serializer<Int2> {
    
    public override void Write(SerializeWriter writer, in Int2 value) {
        writer.BeginScope(typeof(Int2), SerializeScope.Tuple);
        
        writer.WriteTag(nameof(value.X), 1);
        writer.WriteInt32(value.X);
        
        writer.WriteTag(nameof(value.Y), 2);
        writer.WriteInt32(value.Y);
        
        writer.EndScope();
    }
    
    public override Int2 Read(SerializeReader reader, Int2 value) {
        reader.BeginScope(typeof(Int2), SerializeScope.Tuple);
        
        reader.ReadTag(nameof(value.X), 1);
        value.X = reader.ReadInt32();
        
        reader.ReadTag(nameof(value.Y), 2);
        value.Y = reader.ReadInt32();
        
        reader.EndScope();
        return value;
    }
}

[Serializer(typeof(Int3))]
internal sealed class Int3Serializer : Serializer<Int3> {
    public override void Write(SerializeWriter writer, in Int3 value) {
        writer.BeginScope(typeof(Int3), SerializeScope.Tuple);
        
        writer.WriteTag(nameof(value.X), 1);
        writer.WriteInt32(value.X);
        
        writer.WriteTag(nameof(value.Y), 2);
        writer.WriteInt32(value.Y);
        
        writer.WriteTag(nameof(value.Z), 3);
        writer.WriteInt32(value.Z);

        writer.EndScope();
    }
    
    public override Int3 Read(SerializeReader reader, Int3 value) {
        reader.BeginScope(typeof(Int3), SerializeScope.Tuple);
        
        reader.ReadTag(nameof(value.X), 1);
        value.X = reader.ReadInt32();
        
        reader.ReadTag(nameof(value.Y), 2);
        value.Y = reader.ReadInt32();
        
        reader.ReadTag(nameof(value.Z), 3);
        value.Z = reader.ReadInt32();

        reader.EndScope();
        return value;
    }
}

[Serializer(typeof(Color))]
internal sealed class ColorSerializer : Serializer<Color> {
    
    public override void Write(SerializeWriter writer, in Color value) {
        writer.BeginScope(typeof(Color), SerializeScope.Tuple);
        
        writer.WriteTag(nameof(value.R), 1);
        writer.WriteFloat32(value.R);
        
        writer.WriteTag(nameof(value.G), 2);
        writer.WriteFloat32(value.G);
        
        writer.WriteTag(nameof(value.B), 3);
        writer.WriteFloat32(value.B);
        
        writer.WriteTag(nameof(value.A), 4);
        writer.WriteFloat32(value.A);
        
        writer.EndScope();
    }
    
    public override Color Read(SerializeReader reader, Color value) {
        reader.BeginScope(typeof(Color), SerializeScope.Tuple);
        
        reader.ReadTag(nameof(value.R), 1);
        value.R = reader.ReadFloat32();
        
        reader.ReadTag(nameof(value.G), 2);
        value.G = reader.ReadFloat32();
        
        reader.ReadTag(nameof(value.B), 3);
        value.B = reader.ReadFloat32();

        reader.ReadTag(nameof(value.A), 4);
        value.A = reader.ReadFloat32();
        
        reader.EndScope();
        return value;
    }
    
}