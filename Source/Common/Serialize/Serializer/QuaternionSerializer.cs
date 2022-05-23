using System;
using System.Numerics;

namespace Coorth.Serialize;

[Serializable, Serializer(typeof(Quaternion))]
internal sealed class QuaternionSerializer : Serializer<Quaternion> {
    public override void Write(ISerializeWriter writer, in Quaternion value) {
        writer.BeginScope(typeof(Quaternion), SerializeScope.Tuple);
        
        writer.WriteField(nameof(value.X), 1, value.X);
        writer.WriteField(nameof(value.Y), 2, value.Y);
        writer.WriteField(nameof(value.Z), 3, value.Z);
        writer.WriteField(nameof(value.W), 4, value.W);
        
        writer.EndScope();
    }
    
    public override Quaternion Read(ISerializeReader reader, Quaternion value) {
        reader.BeginScope(typeof(Quaternion), SerializeScope.Tuple);
        
        value.X = reader.ReadField<float>(nameof(value.X), 1);
        value.Y = reader.ReadField<float>(nameof(value.Y), 2);
        value.Z = reader.ReadField<float>(nameof(value.Z), 3);
        value.W = reader.ReadField<float>(nameof(value.W), 4);
        
        reader.EndScope();
        return value;
    }
}