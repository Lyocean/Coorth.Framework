using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Coorth.Maths {
    public static class MathUtil {
        #region Const

        public const float ZeroTolerance = 1e-6f;

        public const float Deg2Rad = (float) (Math.PI / 180f);

        public const float Rad2Deg = (float) (180f / Math.PI);

        public static double PI => Math.PI;

        public static double E => Math.E;

        #endregion

        #region Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(int value, int min, int max) {
            return value < min ? min : (value > max ? max : value);
        }

        #endregion

        #region Float

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearZero(float v) {
            return -ZeroTolerance < v && v < ZeroTolerance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearZero(double a) {
            return -ZeroTolerance < a && a < ZeroTolerance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearOne(float a) {
            return NearZero(a - 1.0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float value, float min = 0.0f, float max = 1.0f) {
            return value < min ? min : (value > max ? max : value);
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

        #endregion

        #region Vector

        public static float LengthXZ(this in Vector3 v) {
            return (float) Math.Sqrt(v.X * v.X + v.Z * v.Z);
        }

        public static Vector3 Normalize(this Vector3 v) {
            return v.Length() > ZeroTolerance ? Vector3.Normalize(v) : v;
        }

        public static Vector3 Pow(this Vector3 v, float exponent) {
            v.X = (float) Math.Pow(v.X, exponent);
            v.Y = (float) Math.Pow(v.Y, exponent);
            v.Z = (float) Math.Pow(v.Z, exponent);
            return v;
        }

        #endregion

        #region Quaternion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion ToQuaternion(Vector3 v) {
            return Quaternion.CreateFromYawPitchRoll(v.X, v.Y, v.Z);
        }

        public static Vector3 ToAngle(this Quaternion rotation) {
            float xx = rotation.X * rotation.X;
            float yy = rotation.Y * rotation.Y;
            float zz = rotation.Z * rotation.Z;
            float xy = rotation.X * rotation.Y;
            float zw = rotation.Z * rotation.W;
            float zx = rotation.Z * rotation.X;
            float yw = rotation.Y * rotation.W;
            float yz = rotation.Y * rotation.Z;
            float xw = rotation.X * rotation.W;

            Vector3 rotationEuler;
            rotationEuler.Y = (float) Math.Asin(2.0f * (yw - zx));
            double test = Math.Cos(rotationEuler.Y);
            if (test > ZeroTolerance) {
                rotationEuler.Z = (float) Math.Atan2(2.0f * (xy + zw), 1.0f - (2.0f * (yy + zz)));
                rotationEuler.X = (float) Math.Atan2(2.0f * (yz + xw), 1.0f - (2.0f * (yy + xx)));
            }
            else {
                rotationEuler.Z = (float) Math.Atan2(2.0f * (zw - xy), 2.0f * (zx + yw));
                rotationEuler.X = 0.0f;
            }

            return rotationEuler;
        }

        public static Vector3 ToAxis(this Quaternion rotation) {
            float length = (rotation.X * rotation.X) + (rotation.Y * rotation.Y) + (rotation.Z * rotation.Z);
            if (length < MathUtil.ZeroTolerance)
                return Vector3.UnitX;

            float inv = 1.0f / length;
            return new Vector3(rotation.X * inv, rotation.Y * inv, rotation.Z * inv);
        }


        public static Quaternion LookRotation(Vector3 direction) {
            return LookRotation(direction, Vector3.UnitY);
        }

        public static Quaternion LookRotation(Vector3 direction, Vector3 up) {
            LookRotation(ref direction, ref up, out var result);
            return result;
        }

        public static Vector3 Rotate(this Quaternion self, Vector3 vector) {
            var pureQuaternion = new Quaternion(vector, 0);
            pureQuaternion = Quaternion.Conjugate(self) * pureQuaternion * self;
            vector.X = pureQuaternion.X;
            vector.Y = pureQuaternion.Y;
            vector.Z = pureQuaternion.Z;
            return vector;
        }

        public static void LookRotation(ref Vector3 direction, ref Vector3 up, out Quaternion result) {
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

        #endregion

        #region Matrix

        public static void Transformation(ref Vector3 position, ref Quaternion rotation, ref Vector3 scale, out Matrix4x4 result) {
            var positionMatrix = Matrix4x4.CreateTranslation(position);
            var rotationMatrix = Matrix4x4.CreateFromQuaternion(rotation);
            var scaleMatrix = Matrix4x4.CreateScale(scale);
            result = positionMatrix * rotationMatrix * scaleMatrix;
        }

        public static void Transformation(Vector3 position, Quaternion rotation, Vector3 scale, out Matrix4x4 result) {
            var positionMatrix = Matrix4x4.CreateTranslation(position);
            var rotationMatrix = Matrix4x4.CreateFromQuaternion(rotation);
            var scaleMatrix = Matrix4x4.CreateScale(scale);
            result = positionMatrix * rotationMatrix * scaleMatrix;
        }

        #endregion

        #region Random

        private static Random random = new Random(Guid.NewGuid().GetHashCode());

        public static void SetSeed(int seed) {
            random = new Random(seed);
        }

        public static float Random(float min, float max) {
            return (float) (min + (max - min) * random.NextDouble());
        }

        public static double Random(double min, double max) {
            return min + (max - min) * random.NextDouble();
        }

        public static int Random(int min, int max) {
            return random.Next(min, max);
        }


        public static int RandomRange(int min, int max) {
            return (int) (min + (max - min) * random.NextDouble());
        }

        #endregion
    }
}