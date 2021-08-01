using System.Numerics;

namespace Coorth {
    public static class MatrixUtil {
        public static void Deconstruct(this in Matrix4x4 matrix, out Vector3 translation, out Quaternion rotation, out Vector3 scale) {
            Matrix4x4.Decompose(matrix, out scale, out rotation, out translation);
        } 
    }
}