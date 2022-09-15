using System;
using Coorth.Collections;

namespace Coorth.Serialize;

[Serializer(typeof(BitMask32))]
internal sealed class BitMask32Serializer : Serializer<BitMask32> {
    
    public override void Write(SerializeWriter writer, in BitMask32 value) {
        writer.WriteValue(value.Value);
    }
    
    public override BitMask32 Read(SerializeReader reader, BitMask32 value) {
        return new BitMask32(reader.ReadValue<uint>());
    }
}


[Serializer(typeof(BitMask64))]
internal sealed class BitMask64Serializer : Serializer<BitMask64> {
    public override void Write(SerializeWriter writer, in BitMask64 value) {
        writer.WriteValue(value.Value);
    }
    
    public override BitMask64 Read(SerializeReader reader, BitMask64 value) {
        return new BitMask64(reader.ReadValue<ulong>());
    }
}