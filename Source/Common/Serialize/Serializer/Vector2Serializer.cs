using System;
using System.Numerics;
using System.Runtime.Serialization;

namespace Coorth.Serialize; 

[Serializable, Serializer(typeof(Vector2))]
internal sealed class Vector2Serializer : Serializer<Vector2> {
    public override void Write(ISerializeWriter writer, in Vector2 value) {
        writer.BeginScope(typeof(Vector2), SerializeScope.Tuple);
        
        writer.WriteField(nameof(value.X), 1, value.X);
        writer.WriteField(nameof(value.Y), 2, value.Y);
        
        writer.EndScope();
    }
    
    public override Vector2 Read(ISerializeReader reader, Vector2 value) {
        reader.BeginScope(typeof(Vector2), SerializeScope.Tuple);
        
        value.X = reader.ReadField<float>(nameof(value.X), 1);
        value.Y = reader.ReadField<float>(nameof(value.Y), 2);
        
        reader.EndScope();
        return value;
    }
}