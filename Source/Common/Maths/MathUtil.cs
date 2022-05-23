using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Coorth.Maths; 

public static partial class MathUtil {
        
    public const float ZERO_TOLERANCE = 1e-6f;

    public const float DEG_2_RAD = (float) (Math.PI / 180f);

    public const float RAD_2_DEG = (float) (180f / Math.PI);
        
    public const double PI = Math.PI;

    public const double E = Math.E;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Repeat(float value, float length) {
        return Clamp(value - (float)Math.Floor(value / length) * length, 0.0f, length);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Repeat(double value, double length) {
        return Clamp(value - Math.Floor(value / length) * length, 0.0f, length);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Clamp(int value, int min, int max) {
        return value < min ? min : (value > max ? max : value);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Clamp(long value, long min, long max) {
        return value < min ? min : (value > max ? max : value);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Clamp(float value, float min, float max) {
        return value < min ? min : (value > max ? max : value);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Clamp(double value, double min, double max) {
        return value < min ? min : (value > max ? max : value);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NearZero(float v) {
        return -ZERO_TOLERANCE < v && v < ZERO_TOLERANCE;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NearZero(double a) {
        return -ZERO_TOLERANCE < a && a < ZERO_TOLERANCE;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NearOne(float a) {
        return NearZero(a - 1.0f);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Approximate(float value, float target) {
        return NearZero(value - target);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Approximate(double value, double target) {
        return NearZero(value - target);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Approximate(in Vector2 value, in Vector2 target) {
        return NearZero((value - target).LengthSquared());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Approximate(in Vector3 value, in Vector3 target) {
        return NearZero((value - target).LengthSquared());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Approximate(in Vector4 value, in Vector4 target) {
        return NearZero((value - target).LengthSquared());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Approximate(in Quaternion value, in Quaternion target) {
        return NearZero((value - target).LengthSquared());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Lerp(float from, float to, float amount) {
        return (1.0f - amount) * from + amount * to;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float MoveTowards(float current, float target, float delta) {
        return Math.Abs(target - current) <= (double) delta
            ? target
            : current + Math.Sign(target - current) * delta;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Swap<T>(ref T a, ref T b) {
        (a, b) = (b, a);
    }
}