using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Coorth.Maths;

public static partial class MathUtil {
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Normalize(this in Vector2 v) {
        return v.LengthSquared() > MathUtil.ZERO_TOLERANCE ? Vector2.Normalize(v) : Vector2.Zero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Min(in Vector2 value1, in Vector2 value2, out Vector2 result) {
        result.X = Math.Min(value1.X, value2.X);
        result.Y = Math.Min(value1.Y, value2.Y);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Min(in Vector2 value1, in Vector2 value2) {
        Min(in value1, in value2, out var result);
        return result;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Max(in Vector2 value1, in Vector2 value2, out Vector2 result) {
        result.X = Math.Max(value1.X, value2.X);
        result.Y = Math.Max(value1.Y, value2.Y);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Max(in Vector2 value1, in Vector2 value2) {
        Max(in value1, in value2, out var result);
        return result;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Abs(in Vector2 value, out Vector2 result) {
        result.X = Math.Abs(value.X);
        result.Y = Math.Abs(value.Y);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Abs(this in Vector2 value) {
        Abs(in value, out var result);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Pow(in Vector2 value, float amount, out Vector2 result) {
        result.X = (float)Math.Pow(value.X, amount);
        result.Y = (float)Math.Pow(value.Y, amount);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Pow(this in Vector2 value, float amount) {
        Pow(in value, amount, out var result);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Clamp(Vector2 value, in Vector2 min, in Vector2 max) {
        value.X = MathUtil.Clamp(value.X, min.X, max.X);
        value.Y = MathUtil.Clamp(value.Y, min.Y, max.Y);
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Clamp(ref Vector2 value, in Vector2 min, in Vector2 max) {
        value.X = MathUtil.Clamp(value.X, min.X, max.X);
        value.Y = MathUtil.Clamp(value.Y, min.Y, max.Y);
    }
        
    public static Vector2 ClampMagnitude(Vector2 vector, float maxLength) {
        if (vector.LengthSquared() <= maxLength * maxLength) {
            return vector;
        }
        vector = Vector2.Normalize(vector);
        return vector * maxLength;
    }
        
    public static float Angle(in Vector2 from, in Vector2 to) {
        var num = (float) Math.Sqrt((double) from.LengthSquared() * to.LengthSquared());
        var temp = (float) Math.Acos(MathUtil.Clamp(Vector2.Dot(from, to) / num, -1f, 1f));
        return (double) num <= float.Epsilon ? 0.0f : temp * MathUtil.RAD_2_DEG;
    }

    public static Vector2 Lerp(in Vector2 start, in Vector2 end, float amount) {
        Lerp(in start, in end, amount, out var result);
        return result;
    }
        
    public static void Lerp(in Vector2 start, in Vector2 end, float amount, out Vector2 result) {
        result.X = start.X + ((end.X - start.X) * amount);
        result.Y = start.Y + ((end.Y - start.Y) * amount);
    }

    public static Vector2 SmoothStep(in Vector2 value1, in Vector2 value2, float amount) {
        SmoothStep(in value1, in value2, amount, out var result);
        return result;
    }
        
    public static void SmoothStep(in Vector2 value1, in Vector2 value2, float amount, out Vector2 result) {
        amount = ((amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount));
        amount = amount * amount * (3f - 2f * amount);
        result.X = value1.X + (value2.X - value1.X) * amount;
        result.Y = value1.Y + (value2.Y - value1.Y) * amount;
    }

    public static void Project(in Vector2 vector, in Vector2 onVector, out Vector2 result) {
        float num = onVector.LengthSquared();
        if (MathUtil.NearZero(num)) {
            result = Vector2.Zero;
        } else {
            result = onVector * Vector2.Dot(vector, onVector) / num;
        }
    }
        
    public static Vector2 Project(in Vector2 vector, in Vector2 onVector) {
        Project(in vector, in onVector, out Vector2 result);
        return result;
    }
}