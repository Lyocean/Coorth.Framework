using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Coorth.Serialize; 

public abstract class ByteSerializeWriter : SerializeWriter {
    
    public override void WriteDateTime(DateTime value) => WriteInt64(value.Ticks);

    public override void WriteTimeSpan(TimeSpan value) => WriteInt64(value.Ticks);

    public override void WriteGuid(Guid value) {
        var size = Unsafe.SizeOf<Guid>();
        var span = (stackalloc byte[size]);
        if (!value.TryWriteBytes(span)) {
            throw new SerializationException("[Serialize]: Write Guid failed.");
        }
        WriteBytes(span);
    }
    
    public override void WriteDecimal(decimal value) {
        const int size1 = sizeof(decimal) / sizeof(int);
        const int size2 = sizeof(decimal);
        var span1 = (stackalloc int[size1]);
        var span2 = (stackalloc byte[size2]);
#if NET5_0_OR_GREATER
        decimal.GetBits(value, span1);
#else
        var bits = decimal.GetBits(value);
        bits.AsSpan().CopyTo(span1);
#endif
        
        for (var i = 0; i < size1; i++) {
            BinaryPrimitives.WriteInt32LittleEndian(span2[(i * sizeof(int))..], span1[i]);
        }
    }

    protected abstract void WriteBytes(ReadOnlySpan<byte> value);

    public override void WriteType(Type type) {
        var guid = TypeBinding.GetGuid(type);
        if (guid == Guid.Empty) {
            WriteUInt8((byte)SerializeTypeModes.Name);
            WriteString(type.FullName ?? string.Empty);
            return;
        }
        WriteUInt8((byte)SerializeTypeModes.Guid);
        WriteGuid(guid);
    }

    public override void WriteEnum<T>(T value) {
        var size = Unsafe.SizeOf<T>();
        if (size == sizeof(byte)) {
            WriteUInt8(Unsafe.As<T, byte>(ref value));
        } else if (size == sizeof(short)) {
            WriteInt16(Unsafe.As<T, short>(ref value));
        } else if (size == sizeof(int)) {
            WriteInt32(Unsafe.As<T, int>(ref value));
        } else if (size == sizeof(long)) {
            WriteInt64(Unsafe.As<T, long>(ref value));
        }
        throw new NotSupportedException(typeof(T).ToString());
    }

    public override void WriteEnum(Type type, object? value) {
        
    }
}