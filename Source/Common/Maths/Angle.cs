using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Coorth.Maths; 

[DataDefine(DataFlags.PubField), Guid("63BF89D9-3E1D-4B94-A292-CB80B7F7C510")]
[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
public partial struct Angle {
    
    public float Radians;

    public float Degrees { get => Radians * MathUtil.RAD_2_DEG; set => Radians = value * MathUtil.DEG_2_RAD; }

    public Angle(float value, Types type) {
        switch (type) {
            case Types.Radian:
                Radians = value;
                break;
            case Types.Degree:
                Degrees = value;
                break;
            default:
                Radians = 0f;
                break;
        }
    }
    
    public static Angle FromVector(in Vector2 vector2) {
        return new Angle();
    }
    
    public enum Types {
        Radian,
        Degree,
    }
}