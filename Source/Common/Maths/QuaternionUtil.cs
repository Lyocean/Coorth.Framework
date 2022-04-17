using System;
using System.Runtime.CompilerServices;
using Quaternion = System.Numerics.Quaternion;
using Vector3 = System.Numerics.Vector3;

namespace Coorth.Maths {
    public static class QuaternionUtil {

        #region EulerToQuaternion

        public static void CreateFromEulerRad(float x, float y, float z, out Quaternion result) {
            var sx = (float)Math.Sin(x * 0.5f);
            var cx = (float)Math.Cos(x * 0.5f);
            var sy = (float)Math.Sin(y * 0.5f);
            var cy = (float)Math.Cos(y * 0.5f);
            var sz = (float)Math.Sin(z * 0.5f);
            var cz = (float)Math.Cos(z * 0.5f);

            result.X = sx * cy * cz + cx * sy * sz;
            result.Y = cx * sy * cz - sx * cy * sz;
            result.Z = cx * cy * sz - sx * sy * cz;
            
            result.W = cx * cy * cz + sx * sy * sz;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion CreateFromEulerRad(float x, float y, float z) {
            CreateFromEulerRad(x, y, z, out var quaternion);
            return quaternion;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion CreateFromEulerRad(in Vector3 value) {
            CreateFromEulerRad(value.X, value.Y, value.Z, out var quaternion);
            return quaternion;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion CreateFromEulerDegree(in Vector3 value) {
            CreateFromEulerDegree(in value, out var quaternion);
            return quaternion;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion CreateFromEulerDegree(float x, float y, float z) {
            CreateFromEulerDegree(x, y, z, out var quaternion);
            return quaternion;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromEulerDegree(float x, float y, float z, out Quaternion result) {
            CreateFromEulerRad(x * MathUtil.DEG_2_RAD, y * MathUtil.DEG_2_RAD, z * MathUtil.DEG_2_RAD, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromEulerDegree(in Vector3 euler, out Quaternion result) {
            CreateFromEulerDegree(euler.X, euler.Y, euler.Z, out result);
        }

        #endregion

        #region QuaternionToEuler
        
        public static void ToEulerRad(this in Quaternion rotation, out Vector3 result) {
            float xx = rotation.X * rotation.X;
            float yy = rotation.Y * rotation.Y;
            float zz = rotation.Z * rotation.Z;

            float xy = rotation.X * rotation.Y;
            float xz = rotation.X * rotation.Z;
            float yz = rotation.Y * rotation.Z;
            float xw = rotation.X * rotation.W;
            float yw = rotation.Y * rotation.W;
            float zw = rotation.Z * rotation.W;
            
            result.X = (float)Math.Asin(2.0f * (xw - yz));
            if (Math.Cos(result.X) > MathUtil.ZERO_TOLERANCE) {
                result.Z = (float)Math.Atan2(2.0f * (xy + zw), 1.0f - (2.0f * (xx + zz)));
                result.Y = (float)Math.Atan2(2.0f * (xz + yw), 1.0f - (2.0f * (xx + yy)));
            } else {
                result.Z = (float)Math.Atan2(2.0f * (zw - xy), 2.0f * (yz + xw));
                result.Y = 0.0f;
            }
            
            if (result.X < 0) {
                result.X += (float) MathUtil.PI * 2f;
            }
            if (result.Y < 0) {
                result.Y += (float) MathUtil.PI * 2f;
            }
            if (result.Z < 0) {
                result.Z += (float) MathUtil.PI * 2f;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToEulerRad(this in Quaternion rotation) {
            ToEulerRad(in rotation, out var result);
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToEulerDegree(this in Quaternion rotation, out Vector3 result) {
            ToEulerRad(in rotation, out var vector);
            result = vector * MathUtil.RAD_2_DEG;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToEulerDegree(this in Quaternion rotation) {
            ToEulerRad(in rotation, out var result);
            return result * MathUtil.RAD_2_DEG;
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion ToQuaternion(this in Vector3 v) {
            return CreateFromEulerDegree(v.X, v.Y, v.Z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToYawPitchRoll1(this in Quaternion rotation) {
            return ToEulerRad(in rotation);
        }
        
        #endregion

        #region AxisToQuaternion
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion CreateFromAxisAngle(in Vector3 axis, float angle) {
            CreateFromAxisAngle(in axis, angle, out Quaternion result);
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion CreateFromAxisAngle(float x, float y, float z, float angle) {
            CreateFromAxisAngle(new Vector3(x, y, z), angle, out Quaternion result);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromAxisAngle(in Vector3 axis, float angle, out Quaternion result) {
            result = Quaternion.CreateFromAxisAngle(axis, angle);
        }
        
        #endregion
        
        #region QuaternionToAxis

        public static Vector3 ToAxisAngle(this in Quaternion rotation) {
            float length = (rotation.X * rotation.X) + (rotation.Y * rotation.Y) + (rotation.Z * rotation.Z);
            if (length < MathUtil.ZERO_TOLERANCE)
                return Vector3.UnitX;

            float inv = 1.0f / length;
            return new Vector3(rotation.X * inv, rotation.Y * inv, rotation.Z * inv);
        }
        
        #endregion

        #region DirectionAndRotation

        public static void LookRotation(in Vector3 direction, in Vector3 up, out Quaternion result) {
            Vector3 forwardNorm = Vector3.Normalize(direction);
            Vector3 rightNorm = Vector3.Normalize(Vector3.Cross(up, forwardNorm));
            Vector3 upNorm = Vector3.Cross(forwardNorm, rightNorm);

            float m00 = rightNorm.X;
            float m01 = rightNorm.Y;
            float m02 = rightNorm.Z;
            float m10 = upNorm.X;
            float m11 = upNorm.Y;
            float m12 = upNorm.Z;
            float m20 = forwardNorm.X;
            float m21 = forwardNorm.Y;
            float m22 = forwardNorm.Z;

            float sum = m00 + m11 + m22;
            if (sum > 0) {
                var num = (float) Math.Sqrt(sum + 1);
                float invNumHalf = 0.5f / num;
                result.X = (m12 - m21) * invNumHalf;
                result.Y = (m20 - m02) * invNumHalf;
                result.Z = (m01 - m10) * invNumHalf;
                result.W = num * 0.5f;
            }
            else if (m00 >= m11 && m00 >= m22) {
                var num = (float) Math.Sqrt(1 + m00 - m11 - m22);
                float invNumHalf = 0.5f / num;
                result.X = 0.5f * num;
                result.Y = (m01 + m10) * invNumHalf;
                result.Z = (m02 + m20) * invNumHalf;
                result.W = (m12 - m21) * invNumHalf;
            }
            else if (m11 > m22) {
                var num = (float) Math.Sqrt(1 + m11 - m00 - m22);
                float invNumHalf = 0.5f / num;
                result.X = (m10 + m01) * invNumHalf;
                result.Y = 0.5f * num;
                result.Z = (m21 + m12) * invNumHalf;
                result.W = (m20 - m02) * invNumHalf;
            }
            else {
                var num = (float) Math.Sqrt(1 + m22 - m00 - m11);
                float invNumHalf = 0.5f / num;
                result.X = (m20 + m02) * invNumHalf;
                result.Y = (m21 + m12) * invNumHalf;
                result.Z = 0.5f * num;
                result.W = (m01 - m10) * invNumHalf;
            }
        }
        
        public static Vector3 Rotate(this Quaternion self, Vector3 vector) {
            var pureQuaternion = new Quaternion(vector, 0);
            pureQuaternion = Quaternion.Conjugate(self) * pureQuaternion * self;
            vector.X = -pureQuaternion.X;
            vector.Y = pureQuaternion.Y;
            vector.Z = pureQuaternion.Z;
            return vector;
        }
        
        public static Vector3 ToDirection(this in Quaternion rotation) {
            return rotation.Rotate(Vector3.UnitZ);
        }
        
        public static Quaternion LookRotation(in Vector3 direction) {
            return LookRotation(in direction, Vector3.UnitY);
        }
        
        public static Quaternion LookRotation(in Vector3 direction, in Vector3 up) {
            LookRotation(in direction, in up, out var result);
            return result;
        }
        
        #endregion

    }
}