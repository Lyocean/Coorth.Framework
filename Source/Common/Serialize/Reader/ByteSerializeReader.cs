using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Coorth.Serialize; 

public abstract class ByteSerializeReader : BaseSerializeReader {
    
    protected override DateTime ReadDateTime() {
        var ticks = ReadLong();
        return new DateTime(ticks);
    }

    protected override TimeSpan ReadTimeSpan() {
        var ticks = ReadLong();
        return new TimeSpan(ticks);
    }
    
    protected abstract int ReadBytes(Span<byte> value);

    protected override Guid ReadGuid() {
        var size = Unsafe.SizeOf<Guid>();
        var span = (stackalloc byte[size]);
        if (ReadBytes(span) != size) {
            throw new SerializationException("[Serialize]: Read Guid failed.");
        }
        return new Guid(span);
    }

    protected override Type ReadType() {
        var flag = (SerializeTypeModes)ReadByte();
        var type = (flag == SerializeTypeModes.Name) ? Type.GetType(ReadString()) : TypeBinding.GetType(ReadGuid());
        if (type != null) {
            return type;
        }
        throw new SerializationException("[Serialize]: Read type failed.");
    }

    protected override T ReadEnum<T>() {
        int size = Unsafe.SizeOf<T>();
        switch (size) {
            case sizeof(byte): {
                byte value = ReadByte();
                return Unsafe.As<byte, T>(ref value);
            }
            case sizeof(short): {
                short value = ReadShort();
                return Unsafe.As<short, T>(ref value);
            }
            case sizeof(int): {
                int value = ReadInt();
                return Unsafe.As<int, T>(ref value);
            }
            case sizeof(long): {
                long value = ReadLong();
                return Unsafe.As<long, T>(ref value);
            }
            default:
                throw new NotSupportedException(typeof(T).ToString());
        }
    }
}