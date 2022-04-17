using System;
using System.Runtime.CompilerServices;
using Coorth.Serializes;

namespace Coorth {
    public abstract class ByteSerializeReader : SerializeReader {

        public override DateTime ReadDateTime() => new(ReadLong());

        public override TimeSpan ReadTimeSpan() => new(ReadLong());

        public abstract int ReadBytes(Span<byte> value);

        public override Guid ReadGuid() {
            var size = Unsafe.SizeOf<Guid>();
            var span = (stackalloc byte[size]);
            if (ReadBytes(span) != size) {
                throw new ArgumentException("Read Guid failed.");
            }
            return new Guid(span);
        }

        public override Type? ReadType() {
            var flag = (DataTypeFlags)ReadByte();
            if (flag == DataTypeFlags.Name) {
                return Type.GetType(ReadString());
            }
            return TypeBinding.GetType(ReadGuid());
        }

        public override T ReadEnum<T>() {
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
}