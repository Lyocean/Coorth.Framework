using System;
using System.Runtime.CompilerServices;
using Coorth.Serializes;

namespace Coorth {
    public abstract class ByteSerializeWriter : SerializeWriter {
        public override void WriteDateTime(DateTime value) {
            WriteLong(value.Ticks);
        }

        public override void WriteTimeSpan(TimeSpan value) {
            WriteLong(value.Ticks);
        }

        public override void WriteGuid(Guid value) {
            var bytes = value.ToByteArray();
            foreach (var b in bytes) {
                WriteByte(b);
            }
        }

        public override void WriteType(Type type) {
            var guid = TypeBinding.GetGuid(type);
            if (guid == Guid.Empty) {
                WriteByte((byte)StoreTypeFlags.ByName);
                WriteString(type.FullName);
                return;
            }
            WriteByte((byte)StoreTypeFlags.ByGuid);
            WriteGuid(guid);
        }

        public override void WriteEnum<T>(T value) {
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
}