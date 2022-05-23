using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Coorth.Maths; 

public static partial class VectorUtil {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 XY(this in Vector4 v) => new (v.X, v.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 XZ(this in Vector4 v) => new (v.X, v.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 YZ(this in Vector4 v) => new (v.Y, v.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 XYZ(this in Vector4 v) => new (v.X, v.Y, v.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float LengthXZ(this in Vector4 v) => (float) Math.Sqrt(v.X * v.X + v.Z * v.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float LengthXY(this in Vector4 v) => (float) Math.Sqrt(v.X * v.X + v.Y * v.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float LengthYZ(this in Vector4 v) => (float) Math.Sqrt(v.Y * v.Y + v.Z * v.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 Normalize(this in Vector4 v) => v.LengthSquared() > MathUtil.ZERO_TOLERANCE ? Vector4.Normalize(v) : Vector4.Zero;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Min(in Vector4 value1, in Vector4 value2, out Vector4 result) {
        result.X = Math.Min(value1.X, value2.X);
        result.Y = Math.Min(value1.Y, value2.Y);
        result.Z = Math.Min(value1.Z, value2.Z);
        result.W = Math.Min(value1.W, value2.W);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 Min(in Vector4 value1, in Vector4 value2) {
        Min(in value1, in value2, out var result);
        return result;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Max(in Vector4 value1, in Vector4 value2, out Vector4 result) {
        result.X = Math.Max(value1.X, value2.X);
        result.Y = Math.Max(value1.Y, value2.Y);
        result.Z = Math.Max(value1.Z, value2.Z);
        result.W = Math.Max(value1.W, value2.W);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 Max(in Vector4 value1, in Vector4 value2) {
        Max(in value1, in value2, out var result);
        return result;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Abs(in Vector4 value, out Vector4 result) {
        result.X = Math.Abs(value.X);
        result.Y = Math.Abs(value.Y);
        result.Z = Math.Abs(value.Z);
        result.W = Math.Abs(value.W);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 Abs(this in Vector4 value) {
        Abs(in value, out var result);
        return result;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Pow(in Vector4 value, float amount, out Vector4 result) {
        result.X = (float)Math.Pow(value.X, amount);
        result.Y = (float)Math.Pow(value.Y, amount);
        result.Z = (float)Math.Pow(value.Z, amount);
        result.W = (float)Math.Pow(value.W, amount);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 Pow(this in Vector4 value, float amount) {
        Pow(in value, amount, out var result);
        return result;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 Clamp(Vector4 value, in Vector4 min, in Vector4 max) {
        Clamp(ref value, in min, in max);
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Clamp(ref Vector4 value, in Vector4 min, in Vector4 max) {
        value.X = MathUtil.Clamp(value.X, min.X, max.X);
        value.Y = MathUtil.Clamp(value.Y, min.Y, max.Y);
        value.Z = MathUtil.Clamp(value.Z, min.Z, max.Z);
        value.W = MathUtil.Clamp(value.W, min.W, max.W);
    }
        
    public static Vector4 ClampMagnitude(Vector4 vector, float maxLength) {
        if (vector.LengthSquared() <= maxLength * maxLength) {
            return vector;
        }
        vector = Vector4.Normalize(vector);
        return vector * maxLength;
    }
        
    public static Vector4 Lerp(in Vector4 start, in Vector4 end, float amount) {
        Lerp(in start, in end, amount, out var result);
        return result;
    }
        
    public static void Lerp(in Vector4 start, in Vector4 end, float amount, out Vector4 result) {
        result.X = start.X + ((end.X - start.X) * amount);
        result.Y = start.Y + ((end.Y - start.Y) * amount);
        result.Z = start.Z + ((end.Z - start.Z) * amount);
        result.W = start.W + ((end.W - start.W) * amount);
    }
        
    public static Vector4 SmoothStep(in Vector4 start, in Vector4 end, float amount) {
        SmoothStep(in start, in end, amount, out var result);
        return result;
    }
        
    public static void SmoothStep(in Vector4 start, in Vector4 end, float amount, out Vector4 result) {
        amount = (amount > 1.0f) ? 1.0f : ((amount < 0.0f) ? 0.0f : amount);
        amount = (amount * amount) * (3.0f - (2.0f * amount));
        result.X = start.X + ((end.X - start.X) * amount);
        result.Y = start.Y + ((end.Y - start.Y) * amount);
        result.Z = start.Z + ((end.Z - start.Z) * amount);
        result.W = start.W + (end.W - start.W) * amount;
    }
        
    public static void Project(in Vector4 vector, in Vector4 onVector, out Vector4 result) {
        float num = onVector.LengthSquared();
        if (MathUtil.NearZero(num)) {
            result = Vector4.Zero;
        } else {
            result = onVector * Vector4.Dot(vector, onVector) / num;
        }
    }
        
    public static Vector4 Project(in Vector4 vector, in Vector4 onVector) {
        Project(in vector, in onVector, out Vector4 result);
        return result;
    }
}