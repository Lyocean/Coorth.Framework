using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Coorth.Maths {
    public static class VectorUtil {

        #region Static

        public static float Angle(Vector2 from, Vector2 to) {
            var num = (float) Math.Sqrt((double)from.LengthSquared() * to.LengthSquared());
            var temp = (float) Math.Acos(MathUtil.Clamp(Vector2.Dot(from, to) / num, -1f, 1f));
            return (double) num <= float.Epsilon ? 0.0f : temp * MathUtil.Rad2Deg;
        }
        
        #endregion
        
        #region Vector3

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 XY(this ref Vector3 self) {
            return new Vector2(self.X, self.Y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 XZ(this ref Vector3 self) {
            return new Vector2(self.X, self.Z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 YZ(this ref Vector3 self) {
            return new Vector2(self.Y, self.Z);
        }

        #endregion

        #region Vector4
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 XY(this ref Vector4 self) {
            return new Vector2(self.X, self.Y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 XZ(this ref Vector4 self) {
            return new Vector2(self.X, self.Z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 YZ(this ref Vector4 self) {
            return new Vector2(self.Y, self.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XYZ(this ref Vector4 self) {
            return new Vector3(self.X, self.Y, self.Z);
        }

        #endregion
        
    }
}