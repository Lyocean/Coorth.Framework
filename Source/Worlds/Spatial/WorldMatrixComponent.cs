using System.Numerics;
using Coorth.Framework;

namespace Coorth.Worlds; 

[Component]
public struct WorldMatrixComponent : IComponent {
    public Matrix4x4 Value;
}