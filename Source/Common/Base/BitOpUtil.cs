using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Coorth; 

// public enum BitPrecision {
//     Bit16,
//     Bit32,
//     Bit64,
// }


public static class BitOpUtil {
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Guid ReadGuid(BinaryReader reader) {
        var size = Unsafe.SizeOf<Guid>();
        var span = (stackalloc byte[size]);
        if (reader.Read(span) != size) {
            throw new ArgumentException("Read Guid failed.");
        }
        return new Guid(span);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteGuid(BinaryWriter writer, Guid value) {
        var size = Unsafe.SizeOf<Guid>();
        var span = (stackalloc byte[size]);
        if (!value.TryWriteBytes(span)) {
            throw new ArgumentException("Write Guid failed.");
        }
        writer.Write(span);
    }

    public static void Test() {
        // Marshal
        // MemoryMarshal
        // SequenceMarshal
        // CollectionsMarshal
           //System.Buffers.SequenceReader<>
        // ReadOnlySequence<>
    }
//

//     [MethodImpl(MethodImplOptions.AggressiveInlining)]
//     public static int RoundUpToPowerOf2(int value) {
// #if NET5_0_OR_GREATER
//         return (int)System.Numerics.BitOperations.RoundUpToPowerOf2((uint)value * 2);
// #else
//         var newSize = array.Length * 2;
//         while (newSize < size) {
//             newSize *= 2;
//         }
//         return newSize;
// #endif
//     }
}