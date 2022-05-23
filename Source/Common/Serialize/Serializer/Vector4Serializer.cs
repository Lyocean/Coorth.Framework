using System;
using System.Numerics;

namespace Coorth.Serialize;

[Serializable, Serializer(typeof(Vector4))]
internal sealed class Vector4Serializer : Serializer<Vector4> {
    
    public override void Write(ISerializeWriter writer, in Vector4 value) {
        writer.BeginScope(typeof(Vector4), SerializeScope.Tuple);
        
        writer.WriteField(nameof(value.X), 1, value.X);
        writer.WriteField(nameof(value.Y), 2, value.Y);
        writer.WriteField(nameof(value.Z), 3, value.Z);
        writer.WriteField(nameof(value.W), 4, value.W);
        
        writer.EndScope();
    }
    
    public override Vector4 Read(ISerializeReader reader, Vector4 value) {
        reader.BeginScope(typeof(Vector4), SerializeScope.Tuple);
        
        value.X = reader.ReadField<float>(nameof(value.X), 1);
        value.Y = reader.ReadField<float>(nameof(value.Y), 2);
        value.Z = reader.ReadField<float>(nameof(value.Z), 3);
        value.W = reader.ReadField<float>(nameof(value.W), 4);
        
        reader.EndScope();
        return value;
    }
}