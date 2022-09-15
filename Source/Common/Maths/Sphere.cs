using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Coorth.Maths; 

[StoreContract, Guid("82EF8D53-2DE2-401E-A910-DAC37A03A46F")]
[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4)]
public record struct Sphere(Vector3 Center, float Radius) {
        
    public Vector3 Center = Center;
        
    public float Radius = Radius;
}