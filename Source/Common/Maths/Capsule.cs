using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Coorth.Maths; 

[StoreContract(StoreFlags.PublicField), Guid("2B22E356-FF59-422D-8177-5E4841405314")]
[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
public partial record struct Capsule(Vector3 Center, float Height, float Radius) {
    
    public Vector3 Center = Center;
    
    public float Radius = Radius;

    public float Height = Height;
    
}