using System;
using System.Numerics;
using Coorth.Maths;

namespace Coorth.Serialize; 

[Serializable, Serializer(typeof(Vector2))]
internal sealed class Vector2Serializer : Serializer<Vector2> {
    public override void Write(SerializeWriter writer, in Vector2 value) {
        writer.BeginScope(typeof(Vector2), SerializeScope.Tuple);
        
        writer.WriteField(nameof(value.X), 1, value.X);
        writer.WriteField(nameof(value.Y), 2, value.Y);
        
        writer.EndScope();
    }
    
    public override Vector2 Read(SerializeReader reader, Vector2 value) {
        reader.BeginScope(typeof(Vector2), SerializeScope.Tuple);
        
        value.X = reader.ReadField<float>(nameof(value.X), 1);
        value.Y = reader.ReadField<float>(nameof(value.Y), 2);
        
        reader.EndScope();
        return value;
    }
}

[Serializable, Serializer(typeof(Vector3))]
internal sealed class Vector3Serializer : Serializer<Vector3> {
    
    public override void Write(SerializeWriter writer, in Vector3 value) {
        writer.BeginScope(typeof(Vector3), SerializeScope.Tuple);
        
        writer.WriteField(nameof(value.X), 1, value.X);
        writer.WriteField(nameof(value.Y), 2, value.Y);
        writer.WriteField(nameof(value.Z), 3, value.Z);
        
        writer.EndScope();
    }
    
    public override Vector3 Read(SerializeReader reader, Vector3 value) {
        reader.BeginScope(typeof(Vector3), SerializeScope.Tuple);
        
        value.X = reader.ReadField<float>(nameof(value.X), 1);
        value.Y = reader.ReadField<float>(nameof(value.Y), 2);
        value.Z = reader.ReadField<float>(nameof(value.Z), 3);
        
        reader.EndScope();
        return value;
    }
}

[Serializable, Serializer(typeof(Vector4))]
internal sealed class Vector4Serializer : Serializer<Vector4> {
    
    public override void Write(SerializeWriter writer, in Vector4 value) {
        writer.BeginScope(typeof(Vector4), SerializeScope.Tuple);
        
        writer.WriteField(nameof(value.X), 1, value.X);
        writer.WriteField(nameof(value.Y), 2, value.Y);
        writer.WriteField(nameof(value.Z), 3, value.Z);
        writer.WriteField(nameof(value.W), 4, value.W);
        
        writer.EndScope();
    }
    
    public override Vector4 Read(SerializeReader reader, Vector4 value) {
        reader.BeginScope(typeof(Vector4), SerializeScope.Tuple);
        
        value.X = reader.ReadField<float>(nameof(value.X), 1);
        value.Y = reader.ReadField<float>(nameof(value.Y), 2);
        value.Z = reader.ReadField<float>(nameof(value.Z), 3);
        value.W = reader.ReadField<float>(nameof(value.W), 4);
        
        reader.EndScope();
        return value;
    }
}

[Serializable, Serializer(typeof(Quaternion))]
internal sealed class QuaternionSerializer : Serializer<Quaternion> {
    public override void Write(SerializeWriter writer, in Quaternion value) {
        writer.BeginScope(typeof(Quaternion), SerializeScope.Tuple);
        
        writer.WriteField(nameof(value.X), 1, value.X);
        writer.WriteField(nameof(value.Y), 2, value.Y);
        writer.WriteField(nameof(value.Z), 3, value.Z);
        writer.WriteField(nameof(value.W), 4, value.W);
        
        writer.EndScope();
    }
    
    public override Quaternion Read(SerializeReader reader, Quaternion value) {
        reader.BeginScope(typeof(Quaternion), SerializeScope.Tuple);
        
        value.X = reader.ReadField<float>(nameof(value.X), 1);
        value.Y = reader.ReadField<float>(nameof(value.Y), 2);
        value.Z = reader.ReadField<float>(nameof(value.Z), 3);
        value.W = reader.ReadField<float>(nameof(value.W), 4);
        
        reader.EndScope();
        return value;
    }
}

[Serializable, Serializer(typeof(Int2))]
internal sealed class Int2Serializer : Serializer<Int2> {
    
    public override void Write(SerializeWriter writer, in Int2 value) {
        writer.BeginScope(typeof(Int2), SerializeScope.Tuple);
        
        writer.WriteField(nameof(value.X), 1, value.X);
        writer.WriteField(nameof(value.Y), 2, value.Y);
        
        writer.EndScope();
    }
    
    public override Int2 Read(SerializeReader reader, Int2 value) {
        reader.BeginScope(typeof(Int2), SerializeScope.Tuple);
        
        value.X = reader.ReadField<int>(nameof(value.X), 1);
        value.Y = reader.ReadField<int>(nameof(value.Y), 2);
        
        reader.EndScope();
        return value;
    }
}

[Serializable, Serializer(typeof(Int3))]
internal sealed class Int3Serializer : Serializer<Int3> {
    public override void Write(SerializeWriter writer, in Int3 value) {
        writer.BeginScope(typeof(Int3), SerializeScope.Tuple);
        
        writer.WriteField(nameof(value.X), 1, value.X);
        writer.WriteField(nameof(value.Y), 2, value.Y);
        writer.WriteField(nameof(value.Z), 3, value.Z);
        
        writer.EndScope();
    }
    
    public override Int3 Read(SerializeReader reader, Int3 value) {
        reader.BeginScope(typeof(Int3), SerializeScope.Tuple);
        
        value.X = reader.ReadField<int>(nameof(value.X), 1);
        value.Y = reader.ReadField<int>(nameof(value.Y), 2);
        value.Z = reader.ReadField<int>(nameof(value.Z), 3);
        
        reader.EndScope();
        return value;
    }
}

[Serializable, Serializer(typeof(Color))]
internal sealed class ColorSerializer : Serializer<Color> {
    
    public override void Write(SerializeWriter writer, in Color value) {
        writer.BeginScope(typeof(Color), SerializeScope.Tuple);
        
        writer.WriteField(nameof(value.R), 1, value.R);
        writer.WriteField(nameof(value.G), 2, value.G);
        writer.WriteField(nameof(value.B), 3, value.B);
        writer.WriteField(nameof(value.A), 4, value.A);
        
        writer.EndScope();
    }
    
    public override Color Read(SerializeReader reader, Color value) {
        reader.BeginScope(typeof(Color), SerializeScope.Tuple);
        
        value.R = reader.ReadField<float>(nameof(value.R), 1);
        value.G = reader.ReadField<float>(nameof(value.G), 2);
        value.B = reader.ReadField<float>(nameof(value.B), 3);
        value.A = reader.ReadField<float>(nameof(value.A), 4);
        
        reader.EndScope();
        return value;
    }
    
}