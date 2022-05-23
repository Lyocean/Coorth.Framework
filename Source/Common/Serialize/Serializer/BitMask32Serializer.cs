using System;
using Coorth.Collections;

namespace Coorth.Serialize;

[Serializable, Serializer(typeof(BitMask32))]
internal sealed class BitMask32Serializer : Serializer<BitMask32> {
    
    public override void Write(ISerializeWriter writer, in BitMask32 value) {
        writer.WriteValue(value.Value);
    }
    
    public override BitMask32 Read(ISerializeReader reader, BitMask32 value) {
        return new BitMask32(reader.ReadValue<uint>());
    }
}