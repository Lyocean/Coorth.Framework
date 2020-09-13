using System;
using System.Runtime.CompilerServices;

namespace Coorth {
    public static class MathUtil {

        public const float ZeroTolerance = 1e-6f;

        public const float Deg2Rad = (float)(Math.PI / 180f);

        public const float Rad2Deg = (float)(180f / Math.PI);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float value, float min, float max) {
            return value < min ? min : (value > max ? max : value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(int value, int min, int max) {
            return value < min ? min : (value > max ? max : value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearZero(float v) {
            return -ZeroTolerance < v && v < ZeroTolerance;
        }


    }
}
