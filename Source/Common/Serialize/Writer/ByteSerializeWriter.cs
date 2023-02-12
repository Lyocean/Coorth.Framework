using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Coorth.Serialize; 

public abstract class ByteSerializeWriter : SerializeWriter {
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void WriteDateTime(DateTime value) => WriteInt64(value.Ticks);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        WriteBytes(span2);
    }

    private static readonly Dictionary<Type, byte> typeHeads = new() {
#if NET7_0_OR_GREATER
        [typeof(Half)] = SerializeConst.TYPE_FLOAT16,
#endif
        [typeof(TimeSpan)] = SerializeConst.TYPE_TIMESPAN,
        [typeof(Vector2)] = SerializeConst.TYPE_VECTOR2,
        [typeof(Vector3)] = SerializeConst.TYPE_VECTOR3,
        [typeof(Vector4)] = SerializeConst.TYPE_VECTOR4,
        [typeof(Quaternion)] = SerializeConst.TYPE_QUATERNION,
        [typeof(Matrix3x2)] = SerializeConst.TYPE_MATRIX_32,
        [typeof(Matrix4x4)] = SerializeConst.TYPE_MATRIX_44,
    };

    public override void WriteType(Type type) {
        var code = Type.GetTypeCode(type);
        switch (code) {
            case TypeCode.Object:
                WriteUInt8(SerializeConst.TYPE_OBJECT);
                return;
            case TypeCode.Boolean:
                WriteUInt8(SerializeConst.TYPE_BOOL);
                return;
            case TypeCode.SByte:
                WriteUInt8(SerializeConst.TYPE_INT8);
                return;
            case TypeCode.Byte:
                WriteUInt8(SerializeConst.TYPE_UINT8);
                return;
            case TypeCode.Int16:
                WriteUInt8(SerializeConst.TYPE_INT16);
                return;
            case TypeCode.UInt16:
                WriteUInt8(SerializeConst.TYPE_UINT16);
                return;
            case TypeCode.Int32:
                WriteUInt8(SerializeConst.TYPE_INT32);
                return;
            case TypeCode.UInt32:
                WriteUInt8(SerializeConst.TYPE_UINT32);
                return;
            case TypeCode.Int64:
                WriteUInt8(SerializeConst.TYPE_INT64);
                return;
            case TypeCode.UInt64:
                WriteUInt8(SerializeConst.TYPE_UINT64);
                return;
            case TypeCode.Char:
                WriteUInt8(SerializeConst.TYPE_CHAR);
                return;
            case TypeCode.String:
                WriteUInt8(SerializeConst.TYPE_STRING);
                return;
            case TypeCode.Single:
                WriteUInt8(SerializeConst.TYPE_FLOAT32);
                return;
            case TypeCode.Double:
                WriteUInt8(SerializeConst.TYPE_FLOAT64);
                return;
            case TypeCode.Decimal:
                WriteUInt8(SerializeConst.TYPE_DECIMAL);
                return;
            case TypeCode.DateTime:
                WriteUInt8(SerializeConst.TYPE_DATETIME);
                return;
        }
        if (typeHeads.TryGetValue(type, out var head)) {
            WriteUInt8(head);
            return;
        }
        var guid = TypeBinding.GetGuid(type);
        if (guid != Guid.Empty) {
            WriteUInt8(SerializeConst.TYPE_GUID);
            WriteGuid(guid);
            return;
        }
        WriteUInt8(SerializeConst.TYPE_TEXT);
        WriteString(type.FullName);
    }

    public override void WriteEnum<T>(T value) {
        var size = Unsafe.SizeOf<T>();
        if (size == sizeof(byte)) {
            WriteUInt8(Unsafe.As<T, byte>(ref value));
        } else if (size == sizeof(short)) {
            WriteUInt16(Unsafe.As<T, ushort>(ref value));
        } else if (size == sizeof(int)) {
            WriteUInt32(Unsafe.As<T, uint>(ref value));
        } else if (size == sizeof(long)) {
            WriteUInt64(Unsafe.As<T, ulong>(ref value));
        }
        throw new NotSupportedException(typeof(T).ToString());
    }

    public override void WriteEnum(Type type, object value) {
        var t = Enum.GetUnderlyingType(type);
        if (t == typeof(byte) || t == typeof(sbyte)) {
            WriteUInt8((byte)value);
        } else if (t == typeof(ushort) || t == typeof(short)) {
            WriteUInt16((ushort)value);
        } else if (t == typeof(uint) || t == typeof(int)) {
            WriteUInt32((uint)value);
        } else if (t == typeof(ulong) || t == typeof(long)) {
            WriteUInt64((ulong)value);
        }
        throw new NotSupportedException(type.ToString());
    }
}