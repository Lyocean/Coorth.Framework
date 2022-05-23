using System;
using Coorth.Maths;

namespace Coorth.Serialize;

[Serializable, Serializer(typeof(Color))]
internal sealed class ColorSerializer : Serializer<Color> {
    
    public override void Write(ISerializeWriter writer, in Color value) {
        writer.BeginScope(typeof(Color), SerializeScope.Tuple);
        
        writer.WriteField(nameof(value.R), 1, value.R);
        writer.WriteField(nameof(value.G), 2, value.G);
        writer.WriteField(nameof(value.B), 3, value.B);
        writer.WriteField(nameof(value.A), 4, value.A);
        
        writer.EndScope();
    }
    
    public override Color Read(ISerializeReader reader, Color value) {
        reader.BeginScope(typeof(Color), SerializeScope.Tuple);
        
        value.R = reader.ReadField<float>(nameof(value.R), 1);
        value.G = reader.ReadField<float>(nameof(value.G), 2);
        value.B = reader.ReadField<float>(nameof(value.B), 3);
        value.A = reader.ReadField<float>(nameof(value.A), 4);
        
        reader.EndScope();
        return value;
    }
    
}