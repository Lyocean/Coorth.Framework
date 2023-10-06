using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Coorth;

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint RoundUpToPowerOf2(uint value) {
#if NET5_0_OR_GREATER
        return System.Numerics.BitOperations.RoundUpToPowerOf2(value);
#else
        --value;
        value |= value >> 1;
        value |= value >> 2;
        value |= value >> 4;
        value |= value >> 8;
        value |= value >> 16;
        return value + 1U;
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RoundUpToPowerOf2(int value) {
        return (int)RoundUpToPowerOf2((uint)value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPow2(int value) {
#if NET5_0_OR_GREATER
        return System.Numerics.BitOperations.IsPow2(value);
#else
        return (value & value - 1) == 0 && value > 0;
#endif
    }

}


