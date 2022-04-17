using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Coorth {
    public static class SerializeUtil {
        public static Guid ReadGuid(BinaryReader reader) {
            var size = Unsafe.SizeOf<Guid>();
            var span = (stackalloc byte[size]);
            if (reader.Read(span) != size) {
                throw new ArgumentException("Read Guid failed.");
            }
            return new Guid(span);
        }

        public static void WriteGuid(BinaryWriter writer, Guid value) {
            var size = Unsafe.SizeOf<Guid>();
            var span = (stackalloc byte[size]);
            if (!value.TryWriteBytes(span)) {
                throw new ArgumentException("Write Guid failed.");
            }
            writer.Write(span);
        }
    }
}
