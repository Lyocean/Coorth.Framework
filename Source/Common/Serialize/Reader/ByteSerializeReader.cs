using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Coorth.Serialize; 

public abstract class ByteSerializeReader : SerializeReader {
    
    public override DateTime ReadDateTime() {
        var ticks = ReadInt64();
        return new DateTime(ticks);
    }

    public override TimeSpan ReadTimeSpan() {
        var ticks = ReadInt64();
        return new TimeSpan(ticks);
    }

    public override decimal ReadDecimal() {
        const int size1 = sizeof(decimal) / sizeof(int);
        const int size2 = sizeof(decimal);
        var span1 = (stackalloc int[size1]);
        var span2 = (stackalloc byte[size2]);
        ReadBytes(span2);
        for (var i = 0; i < size1; i++) {
            span1[i] = BinaryPrimitives.ReadInt32LittleEndian(span2[(i * sizeof(int))..]);
        }
#if NET5_0_OR_GREATER
        return new decimal(span1);
#else
        return new decimal(span1.ToArray());
#endif
    }

    protected abstract int ReadBytes(Span<byte> value);

    public override Guid ReadGuid() {
        var size = Unsafe.SizeOf<Guid>();
        var span = (stackalloc byte[size]);
        if (ReadBytes(span) != size) {
            throw new SerializationException("[Serialize]: Read Guid failed.");
        }
        return new Guid(span);
    }

    public override Type ReadType() {
        var flag = (SerializeTypeModes)ReadUInt8();
        var text = ReadString();
        if (text == null) {
            throw new SerializationException();
        }
        var type = (flag == SerializeTypeModes.Name) ? Type.GetType(text) : TypeBinding.GetType(ReadGuid());
        if (type != null) {
            return type;
        }
        throw new SerializationException("[Serialize]: Read type failed.");
    }

    public override T ReadEnum<T>() {
        
        var size = Unsafe.SizeOf<T>();
        switch (size) {
            case sizeof(byte): {
                var value = ReadUInt8();
                return Unsafe.As<byte, T>(ref value);
            }
            case sizeof(short): {
                var value = ReadInt16();
                return Unsafe.As<short, T>(ref value);
            }
            case sizeof(int): {
                var value = ReadInt32();
                return Unsafe.As<int, T>(ref value);
            }
            case sizeof(long): {
                var value = ReadInt64();
                return Unsafe.As<long, T>(ref value);
            }
            default:
                throw new NotSupportedException(typeof(T).ToString());
        }
    }
}