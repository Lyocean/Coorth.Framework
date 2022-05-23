using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Coorth.Serialize; 

public abstract class ByteSerializeWriter : BaseSerializeWriter {
    protected override void WriteDateTime(DateTime value) => WriteLong(value.Ticks);

    protected override void WriteTimeSpan(TimeSpan value) => WriteLong(value.Ticks);

    protected override void WriteGuid(Guid value) {
        var size = Unsafe.SizeOf<Guid>();
        var span = (stackalloc byte[size]);
        if (!value.TryWriteBytes(span)) {
            throw new SerializationException("[Serialize]: Write Guid failed.");
        }
        WriteBytes(span);
    }

    protected abstract void WriteBytes(ReadOnlySpan<byte> value);

    protected override void WriteType(Type type) {
        var guid = TypeBinding.GetGuid(type);
        if (guid == Guid.Empty) {
            WriteByte((byte)SerializeTypeModes.Name);
            WriteString(type.FullName ?? string.Empty);
            return;
        }
        WriteByte((byte)SerializeTypeModes.Guid);
        WriteGuid(guid);
    }

    protected override void WriteEnum<T>(T value) {
        var size = Unsafe.SizeOf<T>();
        if (size == sizeof(byte)) {
            WriteByte(Unsafe.As<T, byte>(ref value));
        } else if (size == sizeof(short)) {
            WriteShort(Unsafe.As<T, short>(ref value));
        } else if (size == sizeof(int)) {
            WriteInt(Unsafe.As<T, int>(ref value));
        } else if (size == sizeof(long)) {
            WriteLong(Unsafe.As<T, long>(ref value));
        }
        throw new NotSupportedException(typeof(T).ToString());
    }
}