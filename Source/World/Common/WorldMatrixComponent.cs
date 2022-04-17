using System.Numerics;

namespace Coorth {
    [Component]
    public struct WorldMatrixComponent : IComponent {
        public Matrix4x4 Value;
    }
}