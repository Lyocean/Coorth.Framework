using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Coorth.Maths;

namespace Coorth.Framework; 

[Serializable, StoreContract]
[Component, Guid("482164B6-9432-4AB8-B217-A843BEB96CB8")]
public sealed partial class TransformComponent : IComponent, IHierarchyNode {

    #region Hierarchy

    public Entity Entity { get; private set; }

    public TransformComponent? Parent { get; private set; }
        
    public bool HasParent => Parent != null;

    public Entity ParentEntity => Parent?.Entity ?? Entity.Null;
        
    private List<TransformComponent>? children;
    public IReadOnlyList<TransformComponent> Children => children ?? (IReadOnlyList<TransformComponent>)Array.Empty<TransformComponent>();
    public IReadOnlyCollection<TransformComponent> GetChildren() => Children;

    public int ChildCount => Hierarchy.Count;
        
    public IEnumerable<Entity> GetChildrenEntities() => Children.Select(child => child.Entity);

    public ref HierarchyComponent Hierarchy => ref Entity.Offer<HierarchyComponent>();

    public SpaceComponent? Space => GetSpace();

        
    public void OnSetup(in Entity entity) {
        if (entity.IsNull) {
            throw new ArgumentException();
        }
        Entity = entity;
        modes = TransformModes.All;
        flags = TransformFlags.Nothing;
        localPosition = Vector3.Zero;
        localRotation = Quaternion.Identity;
        localScaling = Vector3.One;
        localMatrix = Matrix4x4.Identity;
        worldMatrix = Matrix4x4.Identity;
        Hierarchy.SetNode(this);
    }
        
    public void OnClear() { }
        
    private SpaceComponent? GetSpace() {
        if (Entity.TryGet<SpaceComponent>(out var space)) {
            return space;
        }
        var transform = Parent;
        while (transform != null) {
            if (transform.Entity.TryGet<SpaceComponent>(out space)) {
                return space;
            }
            transform = transform.Parent;
        }
        return null;
    }

    private void OnChangeParent(TransformComponent? parent) {
        Parent?.children?.Remove(this);
        Parent = parent;
        if (Parent == null) {
            return;
        }
        Parent.children ??= new List<TransformComponent>();
        Parent.children.Add(this);
    }

    public void SetParent(in TransformComponent? parent, TransformAttach attach = TransformAttach.WorldStay) {
        if (Parent == parent) {
            return;
        }
        switch (attach) {
            case TransformAttach.LocalStay: {
                OnChangeParent(parent);
            } break;
            case TransformAttach.WorldStay: {
                UpdateWorldMatrix();
                Matrix4x4.Decompose(worldMatrix, out var scaling, out var rotation, out var position);
                OnChangeParent(parent);
                localPosition = parent?.WorldToLocalPosition(position) ?? position;
                localRotation = parent?.WorldToLocalRotation(rotation) ?? rotation;
                localScaling = parent?.WorldToLocalScaling(scaling) ?? scaling;
                UpdateLocalMatrix();
            } break;
            case TransformAttach.ResetZero: {
                OnChangeParent(parent);
                localPosition = Vector3.Zero;
                localRotation = Quaternion.Identity;
                localScaling = Vector3.One;
                UpdateLocalMatrix();
            } break;
            default:
                throw new ArgumentException(attach.ToString());
        }
        MarkFlag(TransformFlags.Position | TransformFlags.Rotation | TransformFlags.Scaling, true);
        UpdateWorldMatrix();
    }
        
    public void SetParent(TransformComponent? parent, Vector3 localPos, Quaternion localRot) {
        SetParent(parent, TransformAttach.LocalStay);
        localPosition = localPos;
        localRotation = localRot;
        MarkFlag(TransformFlags.Position | TransformFlags.Rotation, true);
        UpdateLocalMatrix();
        UpdateWorldMatrix();
    }
        
    #endregion

    #region Matrix

    private Matrix4x4 localMatrix;
        
    private Matrix4x4 worldMatrix;
        
    public Matrix4x4 LocalMatrix { get => GetLocalMatrix(); set => SetLocalMatrix(in value); }
    
    public TransformTRS LocalTRS { get => new(GetLocalMatrix()); set => SetLocalMatrix(value.ToMatrix()); }
        
    public Matrix4x4 WorldMatrix { get => GetWorldMatrix(); set => SetWorldMatrix(in value); }
        
    public TransformTRS WorldTRS { get => new(GetWorldMatrix()); set => SetWorldMatrix(value.ToMatrix()); }

    public Matrix4x4 SpaceMatrix { get => GetSpaceMatrix(); set => SetSpaceMatrix(in value); }

    public TransformTRS SpaceTRS { get => new(GetSpaceMatrix()); set => SetSpaceMatrix(value.ToMatrix()); }

    public ref readonly Matrix4x4 WorldMatrixRef => ref worldMatrix;

    public ref readonly Matrix4x4 LocalMatrixRef => ref localMatrix;

    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    private Matrix4x4 GetLocalMatrix() => localMatrix;

    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    private void SetLocalMatrix(in Matrix4x4 value) {
        if (localMatrix == value) {
            return;
        }
        localMatrix = value;
        MarkFlag(TransformFlags.All, true);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void UpdateLocalMatrix() {
        MathUtil.Transformation(in localPosition, in localRotation, in localScaling, out localMatrix);
    }
        
    private void UpdateWorldMatrix() {
        if (flags == TransformFlags.Nothing) {
            return;
        }
        flags = TransformFlags.Nothing;
        if (Parent == null) {
            worldMatrix = localMatrix;
        } else {
            Parent.UpdateWorldMatrix();
            worldMatrix = Matrix4x4.Multiply(localMatrix, Parent.worldMatrix); 
        }
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Matrix4x4 GetWorldMatrix() {
        UpdateWorldMatrix();
        return worldMatrix;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetWorldMatrix(in Matrix4x4 value) {
        if (Parent == null) {
            SetLocalMatrix(in value);
        }
        else {
            Matrix4x4.Invert(Parent.WorldMatrix, out var invMatrix);
            SetLocalMatrix(value * invMatrix);
        }
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Matrix4x4 GetSpaceMatrix() {
        if (Space == null) {
            return GetWorldMatrix();
        }
        Matrix4x4.Invert(Space.Transform.WorldMatrix, out var invMatrix);
        return worldMatrix * invMatrix;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetSpaceMatrix(in Matrix4x4 value) {
        if (Space == null) {
            SetWorldMatrix(value);
            return;
        }
        Matrix4x4.Invert(Space.Transform.WorldMatrix, out var invMatrix);
        SetLocalMatrix(value * invMatrix);
    }
        
    #endregion

    #region Transform
                
    private TransformModes modes;
        
    private TransformFlags flags;

    [StoreMember(1)]
    private Vector3 localPosition;
        
    [StoreMember(2)]
    private Quaternion localRotation;
        
    [StoreMember(3)]
    private Vector3 localScaling;
        
    public Vector3 LocalPosition { get => GetLocalPosition(); set => SetLocalPosition(value); }
        
    public Vector3 WorldPosition { get => GetWorldPosition(); set => SetWorldPosition(value); }
        
    public Vector3 SpacePosition { get => GetSpacePosition(); set => SetSpacePosition(value); }
        
    public Quaternion LocalRotation { get => GetLocalRotation(); set => SetLocalRotation(value); }

    public Quaternion WorldRotation { get => GetWorldRotation(); set => SetWorldRotation(value); }

    public Quaternion SpaceRotation { get => GetSpaceRotation(); set => SetSpaceRotation(value); }
        
    public Vector3 LocalAngles { get => LocalRotation.ToEulerDegree(); set => LocalRotation = value.ToQuaternion(); }
        
    public Vector3 WorldAngles { get => WorldRotation.ToEulerDegree(); set => WorldRotation = value.ToQuaternion(); }
        
    public Vector3 SpaceAngles { get => SpaceRotation.ToEulerDegree(); set => SpaceRotation = value.ToQuaternion(); }
        
    public Vector3 LocalYawPitchRoll { get => LocalRotation.ToEulerRad(); set => LocalRotation = MathUtil.CreateFromEulerRad(in value); }
        
    public Vector3 WorldYawPitchRoll { get => WorldRotation.ToEulerRad(); set => WorldRotation = MathUtil.CreateFromEulerRad(in value); }
        
    public Vector3 SpaceYawPitchRoll { get => SpaceRotation.ToEulerRad(); set => SpaceRotation = MathUtil.CreateFromEulerRad(in value); }
        
    public Vector3 LocalScaling { get => GetLocalScaling(); set => SetLocalScaling(value); }
        
    public Vector3 WorldScaling { get => GetWorldScaling(); set => SetWorldScaling(value); }
        
    public Vector3 SpaceScaling { get => GetSpaceScaling(); set => SetSpaceScaling(value); }
        
    private void MarkFlag(TransformFlags type, bool send) {
        flags |= type;
        if ((modes & TransformModes.Immediate) != 0) {
            foreach (var childEntity in Hierarchy.GetChildrenEntities()) {
                if (!childEntity.Has<TransformComponent>()) {
                    continue;
                }
                ref var childTransform = ref childEntity.Get<TransformComponent>();
                childTransform.MarkFlag(type, false);
            }
        }
        if (send && ((modes & TransformModes.ChangeEvent) != 0)) {
            Entity.Modify<TransformComponent>();
        }
    }

    public void Modify() {
        Entity.Modify<TransformComponent>();
    }

    public TransformTRS GetTransform(TransformSpace space) {
        return space == TransformSpace.Local ? new TransformTRS(in localMatrix) : new TransformTRS(in worldMatrix);
    }
        
    #endregion
        
    #region Position
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Vector3 GetLocalPosition() => localPosition;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetLocalPosition(in Vector3 value) {
        if (localPosition == value) {
            return;
        }
        localPosition = value;
        UpdateLocalMatrix();
        MarkFlag(TransformFlags.Position, true);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Vector3 GetWorldPosition() {
        UpdateWorldMatrix();
        return worldMatrix.GetPosition();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetWorldPosition(Vector3 value) {
        if (Parent != null) {
            value = Parent.WorldToLocalPosition(value);
        }
        SetLocalPosition(value);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Vector3 GetSpacePosition() {
        var position = GetWorldPosition();
        return Space?.WorldToSpacePosition(position) ?? position;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetSpacePosition(in Vector3 value) {
        var position = Space?.SpaceToWorldPosition(value) ?? value;
        SetWorldPosition(position);
    }

    public Vector3 WorldToLocalPosition(in Vector3 value) {
        UpdateWorldMatrix();
        Matrix4x4.Invert(worldMatrix, out var invMatrix);
        return Vector3.Transform(value, invMatrix);
    }
        
    public Vector3 LocalToWorldPosition(in Vector3 value) {
        UpdateWorldMatrix();
        return Vector3.Transform(value, worldMatrix);
    }
        
    #endregion
        
    #region Rotation

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Quaternion GetLocalRotation() => localRotation;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetLocalRotation(in Quaternion value) {
        localRotation = value;
        UpdateLocalMatrix();
        MarkFlag(TransformFlags.Rotation, true);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Quaternion GetWorldRotation() {
        UpdateWorldMatrix();
        return worldMatrix.GetRotation();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetWorldRotation(Quaternion value) {
        if (Parent != null) {
            value = Parent.WorldToLocalRotation(value);
        }
        SetLocalRotation(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Quaternion GetSpaceRotation() {
        var rotation = GetWorldRotation();
        return Space?.WorldToSpaceRotation(rotation) ?? rotation;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetSpaceRotation(Quaternion value) {
        value = Space?.SpaceToWorldRotation(value) ?? value;
        SetWorldRotation(value);
    }

    public Quaternion WorldToLocalRotation(in Quaternion value) => Quaternion.Inverse(WorldRotation) * value;

    public Quaternion LocalToWorldRotation(in Quaternion value) => WorldRotation * value;

    #endregion

    #region Scaling

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Vector3 GetLocalScaling() => localScaling;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetLocalScaling(in Vector3 value) {
        localScaling = value;
        UpdateLocalMatrix();
        MarkFlag(TransformFlags.Scaling, true);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Vector3 GetWorldScaling() {
        UpdateWorldMatrix();
        return worldMatrix.GetScale();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetWorldScaling(Vector3 value) {
        if (Parent!=null) {
            value = Parent.WorldToLocalScaling(value);
        }
        SetLocalScaling(value);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Vector3 GetSpaceScaling() {
        var scale = GetWorldScaling();
        return Space?.WorldToSpaceScale(scale) ?? scale;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetSpaceScaling(Vector3 value) {
        value = Space?.SpaceToWorldScale(value) ?? value;
        SetWorldScaling(value);
    }
        
    public Vector3 WorldToLocalScaling(in Vector3 value) {
        return GetWorldScaling() / value;
    }
        
    public Vector3 LocalToWorldScaling(in Vector3 value) {
        return GetWorldScaling() * value;
    }
        
    #endregion

    #region Modify

    public void LocalToWorld(ref Vector3 position) {
        UpdateWorldMatrix();
        Matrix4x4.Invert(worldMatrix, out var invMatrix);
        position = Vector3.Transform(position, invMatrix);
    }
        
    public void LocalToWorld(ref Vector3 position, ref Quaternion rotation) {
        UpdateWorldMatrix();
        Matrix4x4.Invert(worldMatrix, out var invMatrix);
        position = Vector3.Transform(position, invMatrix);
        rotation = Quaternion.Inverse(worldMatrix.GetRotation()) * rotation;
    }
        
    public (Vector3, Quaternion) LocalToWorld(Vector3 position, Quaternion rotation) {
        LocalToWorld(ref position, ref rotation);
        return (position, rotation);
    }
        
    public void Modify(Vector3 position, TransformSpace space = TransformSpace.Local, bool silent = false) {
        if (space == TransformSpace.World && HasParent) {
            LocalToWorld(ref position);
            UpdateWorldMatrix();
            Matrix4x4.Invert(worldMatrix, out var invMatrix);
            position = Vector3.Transform(position, invMatrix);
        }
        localPosition = position;
        UpdateLocalMatrix();
        MarkFlag(TransformFlags.Position, !silent);
    }

    public void Modify(Vector3 position, Quaternion rotation, TransformSpace space = TransformSpace.Local, bool silent = false) {
        if (space == TransformSpace.World && HasParent) {
            UpdateWorldMatrix();
            Matrix4x4.Invert(worldMatrix, out var invMatrix);
            position = Vector3.Transform(position, invMatrix);
            rotation = Quaternion.Inverse(worldMatrix.GetRotation()) * rotation;
        }
        localPosition = position;
        localRotation = rotation;
        UpdateLocalMatrix();
        MarkFlag(TransformFlags.Position | TransformFlags.Rotation, !silent);
    }

    public void Modify(Vector3 position, Quaternion rotation, Vector3 scaling, TransformSpace space = TransformSpace.Local, bool silent = false) {
        if (space == TransformSpace.World && HasParent) {
            UpdateWorldMatrix();
            Matrix4x4.Invert(worldMatrix, out var invMatrix);
            position = Vector3.Transform(position, invMatrix);
            rotation = Quaternion.Inverse(worldMatrix.GetRotation()) * rotation;
            scaling = worldMatrix.GetScale()/scaling;
        }
        localPosition = position;
        localRotation = rotation;
        localScaling = scaling;
        UpdateLocalMatrix();
        MarkFlag(TransformFlags.All, !silent);
    }

    #endregion
    
}