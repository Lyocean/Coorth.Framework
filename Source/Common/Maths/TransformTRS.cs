using System;
using System.Numerics;
using System.Runtime.InteropServices;


namespace Coorth.Maths; 

[DataContract, Guid("1FFE09DA-3C3D-4D67-A3FB-E3DA6EA413E5")]
[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
public record struct TransformTRS {
        
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

}