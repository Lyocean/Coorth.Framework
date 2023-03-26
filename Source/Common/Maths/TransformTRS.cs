using System;
using System.Numerics;
using System.Runtime.InteropServices;


namespace Coorth.Maths; 

[DataDefine(StoreFlags.PublicField), Guid("1FFE09DA-3C3D-4D67-A3FB-E3DA6EA413E5")]
[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
public partial record struct TransformTRS {

    public Vector3 Position;
        
    public Quaternion Rotation;
        
    public Vector3 Scaling;
        
    public static TransformTRS Identity => new(Vector3.Zero);

    
    public TransformTRS(Vector3 position) : this(position, Quaternion.Identity, Vector3.One) {  }

    public TransformTRS(Vector3 position, Quaternion rotation) : this(position, rotation, Vector3.One) {  }
        
    public TransformTRS(Vector3 position, Quaternion rotation, Vector3 scaling) {
        Position = position;
        Rotation = rotation;
        Scaling = scaling;
    }

    public TransformTRS(in Matrix4x4 matrix) {
        matrix.Decompose(out Position, out Rotation, out Scaling);
    }

    public Matrix4x4 ToMatrix() => MathUtil.Transformation(in Position, in Rotation, in Scaling);

    public static TransformTRS FromPos(float x, float y, float z) {
        return new TransformTRS(new Vector3(x, y, z));
    }

    public TransformTRS LookAt(float x, float y, float z) {
        var target = new Vector3(x, y, z);
        Rotation = MathUtil.LookRotation(target - Position);
        return this;
    }
    
    public TransformTRS LookAt(Vector3 target) {
        Rotation = MathUtil.LookRotation(target - Position);
        return this;
    }
}