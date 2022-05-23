using System;
using System.Numerics;

namespace Coorth.Serialize;

[Serializable, Serializer(typeof(Vector3))]
internal sealed class Vector3Serializer : Serializer<Vector3> {
    
    public override void Write(ISerializeWriter writer, in Vector3 value) {
        writer.BeginScope(typeof(Vector3), SerializeScope.Tuple);
        
        writer.WriteField(nameof(value.X), 1, value.X);
        writer.WriteField(nameof(value.Y), 2, value.Y);
        writer.WriteField(nameof(value.Z), 3, value.Z);
        
        writer.EndScope();
    }
    
    public override Vector3 Read(ISerializeReader reader, Vector3 value) {
        reader.BeginScope(typeof(Vector3), SerializeScope.Tuple);
        
        value.X = reader.ReadField<float>(nameof(value.X), 1);
        value.Y = reader.ReadField<float>(nameof(value.Y), 2);
        value.Z = reader.ReadField<float>(nameof(value.Z), 3);
        
        reader.EndScope();
        return value;
    }
}