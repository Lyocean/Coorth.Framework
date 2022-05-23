using System;
using Coorth.Maths;

namespace Coorth.Serialize;

[Serializable, Serializer(typeof(Int3))]
internal sealed class Int3Serializer : Serializer<Int3> {
    public override void Write(ISerializeWriter writer, in Int3 value) {
        writer.BeginScope(typeof(Int3), SerializeScope.Tuple);
        
        writer.WriteField(nameof(value.X), 1, value.X);
        writer.WriteField(nameof(value.Y), 2, value.Y);
        writer.WriteField(nameof(value.Z), 3, value.Z);
        
        writer.EndScope();
    }
    
    public override Int3 Read(ISerializeReader reader, Int3 value) {
        reader.BeginScope(typeof(Int3), SerializeScope.Tuple);
        
        value.X = reader.ReadField<int>(nameof(value.X), 1);
        value.Y = reader.ReadField<int>(nameof(value.Y), 2);
        value.Z = reader.ReadField<int>(nameof(value.Z), 3);
        
        reader.EndScope();
        return value;
    }
}