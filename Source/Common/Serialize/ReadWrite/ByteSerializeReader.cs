using System;
using System.Runtime.CompilerServices;
using Coorth.Serializes;

namespace Coorth {
    public abstract class ByteSerializeReader : SerializeReader {

        public override DateTime ReadDateTime() {
            return new DateTime(ReadLong());
        }

        public override TimeSpan ReadTimeSpan() {
            return new TimeSpan(ReadLong());
        }

        public override Guid ReadGuid() {
            int size = Unsafe.SizeOf<Guid>();
            var bytes = new byte[size];
            for (var i = 0; i < size; i++) {
                bytes[i] = ReadByte();
            }
            return new Guid(bytes);
        }

        public override Type ReadType() {
            var flag = (StoreTypeFlags)ReadByte();
            if (flag == StoreTypeFlags.ByName) {
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