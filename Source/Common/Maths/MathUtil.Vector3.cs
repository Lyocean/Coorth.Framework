using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Coorth.Maths;

public static partial class MathUtil {
     [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 XY(this in Vector3 v) => new (v.X, v.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetXY(this ref Vector3 v, in Vector2 xy) {
        v.X = xy.X;
        v.Y = xy.Y;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 XZ(this in Vector3 v) => new (v.X, v.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 YZ(this in Vector3 v) => new (v.Y, v.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float LengthXZ(this in Vector3 v) => (float) Math.Sqrt(v.X * v.X + v.Z * v.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float LengthXY(this in Vector3 v) => (float) Math.Sqrt(v.X * v.X + v.Y * v.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float LengthYZ(this in Vector3 v) => (float) Math.Sqrt(v.Y * v.Y + v.Z * v.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Normalize(this in Vector3 v) => v.LengthSquared() > MathUtil.ZERO_TOLERANCE ? Vector3.Normalize(v) : Vector3.Zero;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Min(in Vector3 value1, in Vector3 value2, out Vector3 result) {
        result.X = Math.Min(value1.X, value2.X);
        result.Y = Math.Min(value1.Y, value2.Y);
        result.Z = Math.Min(value1.Z, value2.Z);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Min(in Vector3 value1, in Vector3 value2) {
        Min(in value1, in value2, out var result);
        return result;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Max(in Vector3 value1, in Vector3 value2, out Vector3 result) {
        result.X = Math.Max(value1.X, value2.X);
        result.Y = Math.Max(value1.Y, value2.Y);
        result.Z = Math.Max(value1.Z, value2.Z);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Max(in Vector3 value1, in Vector3 value2) {
        Max(in value1, in value2, out var result);
        return result;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Abs(in Vector3 value, out Vector3 result) {
        result.X = Math.Abs(value.X);
        result.Y = Math.Abs(value.Y);
        result.Z = Math.Abs(value.Z);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Abs(this in Vector3 value) {
        Abs(in value, out var result);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Pow(in Vector3 value, float amount, out Vector3 result) {
        result.X = (float)Math.Pow(value.X, amount);
        result.Y = (float)Math.Pow(value.Y, amount);
        result.Z = (float)Math.Pow(value.Z, amount);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Pow(this in Vector3 value, float amount) {
        Pow(in value, amount, out var result);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Clamp(Vector3 value, in Vector3 min, in Vector3 max) {
        Clamp(ref value, in min, in max);
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Clamp(ref Vector3 value, in Vector3 min, in Vector3 max) {
        value.X = MathUtil.Clamp(value.X, min.X, max.X);
        value.Y = MathUtil.Clamp(value.Y, min.Y, max.Y);
        value.Z = MathUtil.Clamp(value.Z, min.Z, max.Z);
    }
        
    public static Vector3 ClampMagnitude(Vector3 vector, float maxLength) {
        if (vector.LengthSquared() <= maxLength * maxLength) {
            return vector;
        }
        vector = Vector3.Normalize(vector);
        return vector * maxLength;
    }

    public static float Angle(Vector3 from, Vector3 to) {
        from = from.Normalize();
        to = to.Normalize();
        float num = Vector3.Dot(from, to);
        num = (float)Math.Acos(MathUtil.Clamp(num, -1f, 1f));
        if (from.X * to.Y - to.X * from.Y > 0f) {
            num = 0f - num;
        }
        return num;
    }

    public static Vector3 Lerp(in Vector3 start, in Vector3 end, float amount) {
        Lerp(in start, in end, amount, out var result);
        return result;
    }
        
    public static void Lerp(in Vector3 start, in Vector3 end, float amount, out Vector3 result) {
        result.X = start.X + ((end.X - start.X) * amount);
        result.Y = start.Y + ((end.Y - start.Y) * amount);
        result.Z = start.Z + ((end.Z - start.Z) * amount);
    }
        
    public static Vector3 SmoothStep(in Vector3 start, in Vector3 end, float amount) {
        SmoothStep(in start, in end, amount, out var result);
        return result;
    }
        
    public static void SmoothStep(in Vector3 start, in Vector3 end, float amount, out Vector3 result) {
        amount = (amount > 1.0f) ? 1.0f : ((amount < 0.0f) ? 0.0f : amount);
        amount = (amount * amount) * (3.0f - (2.0f * amount));
        result.X = start.X + ((end.X - start.X) * amount);
        result.Y = start.Y + ((end.Y - start.Y) * amount);
        result.Z = start.Z + ((end.Z - start.Z) * amount);
    }
        
    public static void Project(in Vector3 vector, in Vector3 onVector, out Vector3 result) {
        float num = onVector.LengthSquared();
        if (MathUtil.NearZero(num)) {
            result = Vector3.Zero;
        } else {
            result = onVector * Vector3.Dot(vector, onVector) / num;
        }
    }

    public static Vector3 Project(in Vector3 vector, in Vector3 onVector) {
        Project(in vector, in onVector, out Vector3 result);
        return result;
    }
        
    public static Vector3 Reflect(in Vector3 vector, in Vector3 normal) {
        Reflect(in vector, in normal, out var result);
        return result;
    }

    public static void Reflect(in Vector3 vector, in Vector3 normal, out Vector3 result) {
        float num = vector.X * normal.X + vector.Y * normal.Y + vector.Z * normal.Z;
        result.X = vector.X - 2f * num * normal.X;
        result.Y = vector.Y - 2f * num * normal.Y;
        result.Z = vector.Z - 2f * num * normal.Z;
    }

    public static Vector3 ProjectOnPlane(in Vector3 vector, in Vector3 normal) {
        ProjectOnPlane(in vector, normal, out var result);
        return result;
    }
        
    public static void ProjectOnPlane(in Vector3 vector, in Vector3 normal, out Vector3 result) {
        var value1 = Vector3.Dot(normal, normal);
        if (value1 < MathUtil.ZERO_TOLERANCE) {
            result = vector;
        }
        var value2 = Vector3.Dot(vector, normal);
        result = new Vector3(vector.X - normal.X * value2 / value1, vector.Y - normal.Y * value2 / value1, vector.Z - normal.Z * value2 / value1);
    }

    public static Vector3 GetTangent(in Vector3 vector, in Vector3 normal, in Vector3 up) {
        var right = Vector3.Cross(vector, up);
        var tangent = Vector3.Cross(normal, right);
        return tangent.Normalize();
    }

    public static Vector3 MoveTowards(in Vector3 source, in Vector3 target, float delta) {
        var vector = target - source;
        var length = vector.Length();
        if (delta >= length) {
            return target;
        }
        return source + vector.Normalize() * delta;
    }
}