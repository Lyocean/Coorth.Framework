using System;
using Coorth.Maths;

namespace Coorth.Serialize;

[Serializable, Serializer(typeof(Int2))]
internal sealed class Int2Serializer : Serializer<Int2> {
    
    public override void Write(ISerializeWriter writer, in Int2 value) {
        writer.BeginScope(typeof(Int2), SerializeScope.Tuple);
        
        writer.WriteField(nameof(value.X), 1, value.X);
        writer.WriteField(nameof(value.Y), 2, value.Y);
        
        writer.EndScope();
    }
    
    public override Int2 Read(ISerializeReader reader, Int2 value) {
        reader.BeginScope(typeof(Int2), SerializeScope.Tuple);
        
        value.X = reader.ReadField<int>(nameof(value.X), 1);
        value.Y = reader.ReadField<int>(nameof(value.Y), 2);
        
        reader.EndScope();
        return value;
    }
}