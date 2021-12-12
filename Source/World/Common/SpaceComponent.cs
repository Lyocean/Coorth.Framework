using System.Numerics;
using System.Runtime.CompilerServices;

namespace Coorth.Common {
    public class SpaceComponent : Component {

        public ref HierarchyComponent Hierarchy => ref Entity.Get<HierarchyComponent>();
        
        public ref TransformComponent Transform => ref Entity.Get<TransformComponent>();

        public void OnAdd() { }

        public void OnRemove() { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 WorldToSpacePosition(in Vector3 value) => Transform.WorldToLocalPosition(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 SpaceToWorldPosition(in Vector3 value) => Transform.LocalToWorldPosition(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion WorldToSpaceRotation(in Quaternion value) => Transform.WorldToLocalRotation(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion SpaceToWorldRotation(in Quaternion value) => Transform.LocalToWorldRotation(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 WorldToSpaceScale(in Vector3 value) => Transform.WorldToLocalScaling(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 SpaceToWorldScale(in Vector3 value) => Transform.LocalToWorldScaling(value);
    }
}