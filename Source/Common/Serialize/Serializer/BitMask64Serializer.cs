using System;
using Coorth.Collections;

namespace Coorth.Serialize;

[Serializable, Serializer(typeof(BitMask64))]
internal sealed class BitMask64Serializer : Serializer<BitMask64> {
    public override void Write(ISerializeWriter writer, in BitMask64 value) {
        writer.WriteValue(value.Value);
    }
    
    public override BitMask64 Read(ISerializeReader reader, BitMask64 value) {
        return new BitMask64(reader.ReadValue<ulong>());
    }
}