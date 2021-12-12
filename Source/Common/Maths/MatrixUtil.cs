using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Coorth.Maths {
    public static class MatrixUtil {

	    #region Position

	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
	    public static Vector3 GetPosition(this in Matrix4x4 matrix) {
		    return matrix.Translation;
	    }
        
	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
	    public static void GetPosition(this in Matrix4x4 matrix, out float x, out float y, out float z) {
		    x = matrix.M41;
		    y = matrix.M42;
		    z = matrix.M43;
	    }
	    
	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
	    public static void GetPosition(this in Matrix4x4 matrix, out Vector3 value) {
		    value = matrix.Translation;
	    }

	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
	    public static void SetPosition(this ref Matrix4x4 matrix, in Vector3 value) {
		    matrix.Translation = value;
	    }
	    
	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
	    public static void SetPosition(this ref Matrix4x4 matrix, float x, float y, float z) {
		    matrix.Translation = new Vector3(x, y, z);
	    }
	    
	    #endregion

	    #region Rotation

	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
	    public static Quaternion GetRotation(this in Matrix4x4 matrix) {
	        Matrix4x4.Decompose(matrix, out _, out Quaternion rotation, out _);
	        return rotation;
        }

	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
	    public static void GetRotation(this in Matrix4x4 matrix, out Quaternion rotation) {
		    Matrix4x4.Decompose(matrix, out _, out rotation, out _);
	    }
	    
	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
	    public static void GetRotation(this in Matrix4x4 matrix, out float x, out float y, out float z, out float w) {
		    Matrix4x4.Decompose(matrix, out _, out Quaternion rotation, out _);
		    x = rotation.X;
		    y = rotation.Y;
		    z = rotation.Z;
		    w = rotation.W;
	    }
	    
	    public static void SetRotation(this ref Matrix4x4 matrix, float x, float y, float z, float w) {
		    SetRotation(ref matrix, new Quaternion(x, y, z, w));
	    }
	    
        public static void SetRotation(this ref Matrix4x4 matrix, in Quaternion rotation) {
	        float xx = rotation.X * rotation.X;
	        float yy = rotation.Y * rotation.Y;
	        float zz = rotation.Z * rotation.Z;
	        float xy = rotation.X * rotation.Y;
	        float zw = rotation.Z * rotation.W;
	        float zx = rotation.Z * rotation.X;
	        float yw = rotation.Y * rotation.W;
	        float yz = rotation.Y * rotation.Z;
	        float xw = rotation.X * rotation.W;
	        
	        matrix.M11 = 1.0f - (2.0f * (yy + zz));
	        matrix.M12 = 2.0f * (xy + zw);
	        matrix.M13 = 2.0f * (zx - yw);
	        matrix.M21 = 2.0f * (xy - zw);
	        matrix.M22 = 1.0f - (2.0f * (zz + xx));
	        matrix.M23 = 2.0f * (yz + xw);
	        matrix.M31 = 2.0f * (zx + yw);
	        matrix.M32 = 2.0f * (yz - xw);
	        matrix.M33 = 1.0f - (2.0f * (yy + xx));
        }
        
        #endregion
        
        #region EulerAngle
        
        public static void GetEulerRad(this in Matrix4x4 matrix, out float x, out float y, out float z) {
	        x = (float) MathUtil.Repeat(Math.Asin(0f - matrix.M32), 2f * MathUtil.PI);
	        y = (float) MathUtil.Repeat(Math.Atan2(matrix.M31, matrix.M33), 2f * MathUtil.PI);
	        z = (float) MathUtil.Repeat(Math.Atan2(matrix.M12, matrix.M22), 2f * MathUtil.PI);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetEulerRad(this in Matrix4x4 matrix, out Vector3 value) {
	        value = default;
	        GetEulerRad(in matrix, out value.X, out value.Y, out value.Z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetEulerRad(this in Matrix4x4 matrix) {
	        GetEulerRad(in matrix, out float x, out float y, out float z);
	        return new Vector3(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEulerRad(this ref Matrix4x4 matrix, float x, float y, float z) {
	        var rotation = QuaternionUtil.CreateFromEulerRad(x, y, z);
	        matrix.SetRotation(in rotation);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEulerRad(this ref Matrix4x4 matrix, in Vector3 value) {
	        var rotation = QuaternionUtil.CreateFromEulerRad(value);
	        matrix.SetRotation(in rotation);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetEulerDegree(this in Matrix4x4 matrix, out float x, out float y, out float z) {
	        GetEulerRad(in matrix, out float rx, out float ry, out float rz);
	        x = rx * MathUtil.RAD_2_DEG;
	        y = ry * MathUtil.RAD_2_DEG;
	        z = rz * MathUtil.RAD_2_DEG;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        
        public static void GetEulerDegree(this in Matrix4x4 matrix, out Vector3 value) {
	        value = default;
	        GetEulerDegree(in matrix, out value.X, out value.Y, out value.Z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetEulerDegree(this in Matrix4x4 matrix) {
	        return matrix.GetEulerRad() * MathUtil.RAD_2_DEG;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEulerDegree(this ref Matrix4x4 matrix, float x, float y, float z) {
	        SetRotation(ref matrix,  QuaternionUtil.CreateFromEulerDegree(x, y, z));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEulerDegree(this ref Matrix4x4 matrix, in Vector3 value) {
	        SetRotation(ref matrix,  QuaternionUtil.CreateFromEulerDegree(in value));
        }

	    #endregion

	    #region Scaling

	    public static Vector3 GetScale(this in Matrix4x4 matrix) {
		    Matrix4x4.Decompose(matrix, out Vector3 scale, out Quaternion _, out Vector3 _);
		    return scale;
	    }
	    
	    public static void GetScale(this in Matrix4x4 matrix, out Vector3 value) {
		    Matrix4x4.Decompose(matrix, out value, out Quaternion _, out Vector3 _);
	    }
	    
	    public static void GetScale(this in Matrix4x4 matrix, out float x, out float y, out float z) {
		    Matrix4x4.Decompose(matrix, out Vector3 scale, out Quaternion _, out Vector3 _);
		    x = scale.X;
		    y = scale.Y;
		    z = scale.Z;
	    }
	    
	    public static void SetScale(this ref Matrix4x4 matrix, float scale) {
		    matrix.SetScale(new Vector3(scale, scale, scale));
	    }
	    
	    public static void SetScale(this ref Matrix4x4 matrix, float x, float y, float z) {
		    matrix.SetScale(new Vector3(x, y, z));
	    }
	    
	    public static void SetScale(this ref Matrix4x4 matrix, in Vector3 scale) {
		    Matrix4x4.Decompose(matrix, out Vector3 _, out Quaternion rotation, out Vector3 translation);
		    Transformation(translation, rotation, scale, out matrix);
	    }

	    #endregion


	    #region Transform
	    
	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Decompose(this in Matrix4x4 matrix, out Vector3 position, out Quaternion rotation, out Vector3 scaling) {
            Matrix4x4.Decompose(matrix, out scaling, out rotation, out position);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Decompose(this in Matrix4x4 matrix, out Vector3 position, out Quaternion rotation) {
	        Matrix4x4.Decompose(matrix, out _, out rotation, out position);
        }
        
        public static Matrix4x4 Transformation(in Vector3 position, in Quaternion rotation) {
	        Transformation(in position, in rotation, Vector3.One, out var matrix);
	        return matrix;
        }
        
        public static void Transformation(in Vector3 position, in Quaternion rotation, out Matrix4x4 result) {
	        Transformation(in position, in rotation, Vector3.One, out result);
        }
        
        public static Matrix4x4 Transformation(in Vector3 position, in Quaternion rotation, in Vector3 scale) {
	        Transformation(in position, in rotation, in scale, out var matrix);
	        return matrix;
        }

        public static void Transformation(in Vector3 position, in Quaternion rotation, in Vector3 scale, out Matrix4x4 result) {
	        // Position
	        result.M41 = position.X;
	        result.M42 = position.Y;
	        result.M43 = position.Z;
	        
	        // Rotation
	        float xx = rotation.X * rotation.X;
	        float yy = rotation.Y * rotation.Y;
	        float zz = rotation.Z * rotation.Z;
	        float xy = rotation.X * rotation.Y;
	        float zw = rotation.Z * rotation.W;
	        float zx = rotation.Z * rotation.X;
	        float yw = rotation.Y * rotation.W;
	        float yz = rotation.Y * rotation.Z;
	        float xw = rotation.X * rotation.W;
	        
	        result.M11 = 1.0f - (2.0f * (yy + zz));
	        result.M12 = 2.0f * (xy + zw);
	        result.M13 = 2.0f * (zx - yw);
	        result.M21 = 2.0f * (xy - zw);
	        result.M22 = 1.0f - (2.0f * (zz + xx));
	        result.M23 = 2.0f * (yz + xw);
	        result.M31 = 2.0f * (zx + yw);
	        result.M32 = 2.0f * (yz - xw);
	        result.M33 = 1.0f - (2.0f * (yy + xx));
	        
	        // Scale
	        result.M11 *= scale.X;
	        result.M12 *= scale.X;
	        result.M13 *= scale.X;
	        
	        result.M21 *= scale.Y;
	        result.M22 *= scale.Y;
	        result.M23 *= scale.Y;
	        
	        result.M31 *= scale.Z;
	        result.M32 *= scale.Z;
	        result.M33 *= scale.Z;
	        
	        // Other
	        result.M14 = 0.0f;
	        result.M24 = 0.0f;
	        result.M34 = 0.0f;
	        result.M44 = 1.0f;
        }
        


	    #endregion

	    #region Common

	    public static void Invert(this ref Matrix4x4 matrix) {
		    Matrix4x4.Invert(matrix, out matrix);
	    }

	    public static Vector3 GetForward(this in Matrix4x4 matrix) {
		    Matrix4x4.Decompose(matrix, out var scale, out var rotation, out var translation);
		    return rotation.Rotate(Vector3.UnitZ);
	    }

	    #endregion	    
	    



        

    }
}


/*

Translation

1	0	0	x
0	1	0	y
0	0	1	z
0	0	0	1

Scaling

x	0	0	0
0	y	0	0
0	0	z	0
0	0	0	1

Rotation
c = cos
s = sin

Rx
1	0	0	0
0	c   -s	0
0	s	c	0
0	0	0	1

Ry

c	0	s	0
0	1	0	0
-s	0	c	0
0	0	0	1

Rz

c	-s	0	0
s	c	0	0
0	0	1	0
0	0	0	1

*/