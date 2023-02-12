using System;
using System.Buffers.Binary;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;


namespace Coorth.Serialize;

public abstract class ByteSerializeReader : SerializeReader {

    public override DateTime ReadDateTime() {
        return new DateTime(ReadInt64());
    }

    public override TimeSpan ReadTimeSpan() {
        return new TimeSpan(ReadInt64());
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
        var array = System.Buffers.ArrayPool<int>.Shared.Rent(span1.Length);
        span1.CopyTo(array);
        decimal result;
        try {
            result = new decimal(array);
        }
        finally {
            System.Buffers.ArrayPool<int>.Shared.Return(array);
        }
        return result;
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
        var flag = ReadUInt8();
        switch (flag) {
            case SerializeConst.TYPE_NULL: throw new SerializationException();
            case SerializeConst.TYPE_OBJECT: return typeof(object);
            case SerializeConst.TYPE_BOOL: return typeof(bool);
            case SerializeConst.TYPE_INT8: return typeof(sbyte);
            case SerializeConst.TYPE_UINT8: return typeof(byte);
            case SerializeConst.TYPE_INT16: return typeof(short);
            case SerializeConst.TYPE_UINT16: return typeof(ushort);
            case SerializeConst.TYPE_INT32: return typeof(int);
            case SerializeConst.TYPE_UINT32: return typeof(uint);
            case SerializeConst.TYPE_INT64: return typeof(long);
            case SerializeConst.TYPE_UINT64: return typeof(ulong);
           
            case SerializeConst.TYPE_CHAR: return typeof(char);
            case SerializeConst.TYPE_STRING: return typeof(string);
#if NET7_0_OR_GREATER
            case SerializeConst.TYPE_FLOAT16: return typeof(Half);
#endif
            case SerializeConst.TYPE_FLOAT32: return typeof(float);
            case SerializeConst.TYPE_FLOAT64: return typeof(double);
            case SerializeConst.TYPE_DECIMAL: return typeof(decimal);
            case SerializeConst.TYPE_DATETIME: return typeof(DateTime);
            case SerializeConst.TYPE_TIMESPAN: return typeof(TimeSpan);
            case SerializeConst.TYPE_VECTOR2: return typeof(Vector2);
            case SerializeConst.TYPE_VECTOR3: return typeof(Vector3);
            case SerializeConst.TYPE_VECTOR4: return typeof(Vector4);
            case SerializeConst.TYPE_QUATERNION: return typeof(Quaternion);
            case SerializeConst.TYPE_MATRIX_32: return typeof(Matrix3x2);
            case SerializeConst.TYPE_MATRIX_44: return typeof(Matrix4x4);
            case SerializeConst.TYPE_TEXT: {
                var type = Type.GetType(ReadString());
                if (type == null) {
                    throw new SerializationException();
                }
                return type;
            }
            case SerializeConst.TYPE_GUID: {
                var type = TypeBinding.GetType(ReadGuid());
                if (type == null) {
                    throw new SerializationException();
                }
                return type;
            }
            default:
                throw new SerializationException($"[Serialize]: Read type failed, unknown head: {flag}");
        }
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