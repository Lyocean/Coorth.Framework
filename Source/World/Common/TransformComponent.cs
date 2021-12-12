using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Coorth.Maths;

namespace Coorth.Common {
    [Component, StoreContract("482164B6-9432-4AB8-B217-A843BEB96CB8")]
    public struct TransformComponent : IComponent {

        #region Hierarchy

        public Entity Entity { get; private set; }

        public ref HierarchyComponent Hierarchy => ref Entity.Offer<HierarchyComponent>();

        public Entity ParentEntity => Hierarchy.ParentEntity;
        
        public bool HasParent => ParentEntity.IsNotNull && ParentEntity.Has<TransformComponent>();
        
        public ref TransformComponent Parent => ref Hierarchy.ParentEntity.Get<TransformComponent>();
        
        public int ChildCount => Hierarchy.Count;

        public FlagsTypes Flags;
        
        private DirtyTypes dirty;

        public SpaceComponent Space => GetSpace();
        
        public void OnAdd(in Entity entity) {
            if (entity.IsNull) {
                throw new ArgumentException();
            }
            this.Entity = entity;
            this.Flags = FlagsTypes.All;
            this.dirty = DirtyTypes.None;
            this.localPosition = Vector3.Zero;
            this.localRotation = Quaternion.Identity;
            this.localScale = Vector3.One;
            this.localMatrix = Matrix4x4.Identity;
            this.worldMatrix = Matrix4x4.Identity;
        }
        
        public void OnRemove() { }
        
        private SpaceComponent GetSpace() {
            if (Entity.TryGet<SpaceComponent>(out var space)) {
                return space;
            }
            ref var hierarchy = ref Hierarchy;
            var entity = hierarchy.ParentEntity;
            while (!entity.IsNull) {
                if (entity.TryGet(out space)) {
                    return space;
                }
            }
            return null;
        }

        public void SetParent() {
            Hierarchy.SetParent(Entity.Null);
            UpdateWorldMatrix();
        }
        
        public void SetParent(in Entity parent, bool worldPositionStay = true) {
            Hierarchy.SetParent(in parent);
            UpdateWorldMatrix();
            worldMatrix.Decompose(out var position, out var rotation);
            Modify(position, rotation, TransformSpace.World);
        }
        
        public void SetParent(in TransformComponent parent, bool worldPositionStay = true) {
            Hierarchy.SetParent(parent.Entity);
        }

        public EnumerableTransforms GetChildren() {
            return new EnumerableTransforms(this.Entity);
        }

        private void OnModify(DirtyTypes type, bool send) {
            dirty |= type;
            if ((Flags & FlagsTypes.Immediate) != 0) {
                foreach (var childEntity in Hierarchy.GetChildrenEntities()) {
                    if (!childEntity.Has<TransformComponent>()) {
                        continue;
                    }
                    ref var childTransform = ref childEntity.Get<TransformComponent>();
                    childTransform.OnModify(type, false);
                }
            }
            if (send && ((Flags & FlagsTypes.ChangeEvent) != 0)) {
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

        #region Matrix
        
        private Matrix4x4 localMatrix;
        public Matrix4x4 LocalMatrix { get => GetLocalMatrix(); set => SetLocalMatrix(in value); }
        
        private Matrix4x4 worldMatrix;
        public Matrix4x4 WorldMatrix { get => GetWorldMatrix(); set => SetWorldMatrix(in value); }

        public Matrix4x4 SpaceMatrix { get => GetSpaceMatrix(); set => SetSpaceMatrix(in value); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Matrix4x4 GetLocalMatrix() {
            return localMatrix;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetLocalMatrix(in Matrix4x4 value) {
            if (localMatrix == value) {
                return;
            }
            localMatrix = value;
            OnModify(DirtyTypes.All, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateLocalMatrix() {
            MatrixUtil.Transformation(in localPosition, in localRotation, in localScale, out localMatrix);
        }
        
        private void UpdateWorldMatrix() {
            if (dirty == DirtyTypes.None) {
                return;
            }
            dirty = DirtyTypes.None;
            if (!HasParent) {
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
            if (!HasParent) {
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
            else {
                Matrix4x4.Invert(Space.Transform.WorldMatrix, out var invMatrix);
                return worldMatrix * invMatrix;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetSpaceMatrix(in Matrix4x4 value) {
            if (Space == null) {
                SetWorldMatrix(value);
            }
            else {
                Matrix4x4.Invert(Space.Transform.WorldMatrix, out var invMatrix);
                SetLocalMatrix(value * invMatrix);
            }
        }
        
        #endregion

        #region Position

        private Vector3 localPosition;
        
        public Vector3 LocalPosition { get => GetLocalPosition(); set => SetLocalPosition(value); }

        [Display]
        public Vector3 WorldPosition { get => GetWorldPosition(); set => SetWorldPosition(value); }

        public Vector3 SpacePosition { get => GetSpacePosition(); set => SetSpacePosition(value); }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector3 GetLocalPosition() { 
            return localPosition;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetLocalPosition(in Vector3 value) {
            localPosition = value;
            UpdateLocalMatrix();
            OnModify(DirtyTypes.Position, true);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector3 GetWorldPosition() {
            UpdateWorldMatrix();
            return worldMatrix.GetPosition();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetWorldPosition(Vector3 value) {
            if (HasParent) {
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
        
        private Quaternion localRotation;

        public Quaternion LocalRotation { get => GetLocalRotation(); set => SetLocalRotation(value); }

        public Quaternion WorldRotation { get => GetWorldRotation(); set => SetWorldRotation(value); }

        public Quaternion SpaceRotation { get => GetSpaceRotation(); set => SetSpaceRotation(value); }
        
        public Vector3 LocalAngles { get => LocalRotation.ToEulerDegree(); set => LocalRotation = value.ToQuaternion(); }

        public Vector3 WorldAngles { get => WorldRotation.ToEulerDegree(); set => WorldRotation = value.ToQuaternion(); }

        public Vector3 SpaceAngles { get => SpaceRotation.ToEulerDegree(); set => SpaceRotation = value.ToQuaternion(); }

        public Vector3 LocalYawPitchRoll { get => LocalRotation.ToEulerRad(); set => LocalRotation = QuaternionUtil.CreateFromEulerRad(in value); }

        public Vector3 WorldYawPitchRoll { get => WorldRotation.ToEulerRad(); set => WorldRotation = QuaternionUtil.CreateFromEulerRad(in value); }

        public Vector3 SpaceYawPitchRoll { get => SpaceRotation.ToEulerRad(); set => SpaceRotation = QuaternionUtil.CreateFromEulerRad(in value); }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Quaternion GetLocalRotation() {
            return localRotation;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetLocalRotation(in Quaternion value) {
            localRotation = value;
            UpdateLocalMatrix();
            OnModify(DirtyTypes.Rotation, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Quaternion GetWorldRotation() {
            UpdateWorldMatrix();
            return worldMatrix.GetRotation();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetWorldRotation(Quaternion value) {
            if (HasParent) {
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

        public Quaternion WorldToLocalRotation(in Quaternion value) {
            return Quaternion.Inverse(WorldRotation) * value;
        }

        public Quaternion LocalToWorldRotation(in Quaternion value) {
            return WorldRotation * value;
        }

        #endregion

        #region Scaling

        private Vector3 localScale;
        
        public Vector3 LocalScale { get => GetLocalScaling(); set => SetLocalScaling(value); }

        [Display]
        public Vector3 WorldScale { get => GetWorldScaling(); set => SetWorldScaling(value); }

        public Vector3 SpaceScale { get => GetSpaceScaling(); set => SetSpaceScaling(value); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector3 GetLocalScaling() {
            return localScale;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetLocalScaling(in Vector3 value) {
            localScale = value;
            UpdateLocalMatrix();
            OnModify(DirtyTypes.Scale, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector3 GetWorldScaling() {
            UpdateWorldMatrix();
            return worldMatrix.GetScale();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetWorldScaling(Vector3 value) {
            if (HasParent) {
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
            OnModify(DirtyTypes.Position, !silent);
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
            OnModify(DirtyTypes.Position | DirtyTypes.Rotation, !silent);
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
            localScale = scaling;
            UpdateLocalMatrix();
            OnModify(DirtyTypes.All, !silent);
        }
        
        #endregion
        
        #region Enumerator
        
        public struct TransformEnumerator : IEnumerator<ComponentPtr<TransformComponent>> {
            private HierarchyComponent.EntityEnumerator hierarchies;
            private Entity current;
            
            public TransformEnumerator(Entity entity) {
                this.hierarchies = entity.Offer<HierarchyComponent>().GetChildrenEntities().GetEnumerator();
                this.current = Entity.Null;
            }

            public bool MoveNext() {
                while (hierarchies.MoveNext()) {
                    current = hierarchies.Current;
                    if (!current.IsNull && current.Has<TransformComponent>()) {
                        return true;
                    }
                }
                return false;
            }

            public void Reset() {
                hierarchies.Reset();
            }

            public ComponentPtr<TransformComponent> Current => current.Ptr<TransformComponent>();

            object IEnumerator.Current => Current;

            public void Dispose() { }
        }

        public readonly struct EnumerableTransforms : IEnumerable<ComponentPtr<TransformComponent>> {
            private readonly Entity entity;

            public EnumerableTransforms(Entity entity) {
                this.entity = entity;
            }
            
            public TransformEnumerator GetEnumerator() => new TransformEnumerator(entity);

            IEnumerator<ComponentPtr<TransformComponent>> IEnumerable<ComponentPtr<TransformComponent>>.GetEnumerator() => GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Flags]
        private enum DirtyTypes : byte { None = 0, Position = 1, Rotation = 1 << 1, Scale = 1 << 2, All = Position | Rotation | Scale }

        [Flags]
        public enum FlagsTypes : byte { None = 0, UseTRS = 1, Immediate = 1 << 1, ChangeEvent = 1 << 2, All = UseTRS | Immediate | ChangeEvent }

        #endregion
    }
    
    
    
    
    
    
    
    
    
    
    //
    //
    //     [Component, StoreContract("482164B6-9432-4AB8-B217-A843BEB96CB8")]
    // public class TransformComponent : Component {
    //
    //     #region Common
    //
    //     [Flags]
    //     private enum DirtyTypes : byte { Position = 1, Rotation = 2, Scale = 3, All = Position | Rotation | Scale}
    //
    //     private DirtyTypes dirty;
    //
    //     [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //     private void MarkDirty(DirtyTypes type) { dirty |= type; }
    //
    //     public void OnAdd() {  }
    //
    //     public void OnRemove() { Clear(); } 
    //
    //     #endregion
    //
    //     #region Hierarchy
    //
    //     public SpaceComponent Space { get; private set; }
    //
    //     public TransformComponent Root => Parent != null ? Parent.Root : this;
    //     
    //     public TransformComponent Parent { get; private set; }
    //
    //     public Entity ParentEntity => Parent?.Entity ?? Entity.Null;
    //
    //     private readonly List<TransformComponent> children = new List<TransformComponent>();
    //     public IReadOnlyList<TransformComponent> Children => children;
    //
    //     public int ChildCount => children.Count;
    //     
    //     public void SetParent(TransformComponent parent, bool worldStay = false) {
    //         if(Parent == parent) { return; }
    //         Parent?.children?.Remove(this);
    //         parent?.children?.Add(this);
    //         Parent = parent;
    //         MarkDirty(DirtyTypes.All);
    //         // Sandbox.Execute(new EventHierarchyChange(Entity, parent?.Entity ?? Entity.Null));
    //         OnModify(true);
    //     }
    //
    //     public void SetSpace(SpaceComponent space) {
    //         if (this.Space == space) {
    //             return;
    //         }
    //         this.Space = space;
    //     }
    //
    //     public void AddChild(TransformComponent child) {
    //         child.SetParent(this);
    //     }
    //
    //     public IEnumerable<Entity> GetChildrenEntities() {
    //         foreach (var child in children) {
    //             yield return child.Entity;
    //         }
    //     }
    //     
    //     public void Clear() {
    //         foreach (var child in children) {
    //             child.Entity.Dispose();
    //         }
    //         children.Clear();
    //     }
    //
    //     private void MarkHierarchy(DirtyTypes type) {
    //         MarkDirty(type);
    //         foreach (var child in children) {
    //             child.MarkHierarchy(type);
    //         }
    //     }
    //
    //     #endregion
    //
    //     #region Position
    //
    //     [StoreMember(1)]
    //     private Vector3 localPosition;
    //
    //     public Vector3 LocalPosition { get => localPosition; set => SetLocalPosition(value); }
    //
    //     public Vector3 WorldPosition { get => Parent == null ? localPosition : GetWorldMatrix().Translation; set => SetWorldPosition(value); }
    //
    //     public Vector3 SpacePosition { get => Space == null ? localPosition : Space.WorldToSpacePosition(WorldPosition); set => WorldPosition = Space == null ? value : Space.SpaceToWorldPosition(value); }
    //
    //     [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //     private void SetLocalPosition(in Vector3 value, bool send = true) {
    //         if(localPosition == value) {
    //             return;
    //         }
    //         localPosition = value;
    //         MarkHierarchy(DirtyTypes.Position);
    //         localMatrix.Translation = localPosition;
    //         OnModify(send);
    //     }
    //
    //     [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //     private void SetWorldPosition(in Vector3 value, bool send = true) {
    //         SetLocalPosition(Parent == null ? value : Parent.WorldToLocalPosition(value), send);
    //     }
    //
    //     public Vector3 WorldToLocalPosition(in Vector3 value) {
    //         ref var matrix = ref GetWorldMatrix();
    //         Matrix4x4.Invert(matrix, out var invMatrix);
    //         return Vector3.Transform(value, invMatrix);
    //     }
    //
    //     public Vector3 LocalToWorldPosition(in Vector3 value) {
    //         ref var matrix = ref GetWorldMatrix();
    //         return Vector3.Transform(value, matrix);
    //     }
    //
    //     #endregion
    //
    //     #region Rotation
    //
    //     [StoreMember(2)]
    //     private Quaternion localRotation;
    //
    //     public Quaternion LocalRotation { get => localRotation; set => SetLocalRotation(value); }
    //
    //     public Vector3 LocalAngles { get => localRotation.ToEulerAngle(); set => LocalRotation = value.ToQuaternion(); }
    //
    //     public Quaternion WorldRotation { get => Parent == null ? localRotation : GetWorldMatrix().GetRotation(); set => SetWorldRotation(value); }
    //
    //     public Vector3 WorldAngles { get => Parent == null ? LocalAngles : GetWorldMatrix().GetEulerAngles(); set => WorldRotation = value.ToQuaternion(); }
    //
    //     public Quaternion SpaceRotation { get => Space == null ? localRotation : Space.WorldToSpaceRotation(WorldRotation); set => WorldRotation = Space == null ? value : Space.SpaceToWorldRotation(value); }
    //
    //
    //     [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //     private void SetLocalRotation(in Quaternion value, bool send = true) {
    //         if (localRotation == value) {
    //             return;
    //         }
    //         localRotation = value;
    //         UpdateLocalMatrix();
    //         MarkHierarchy(DirtyTypes.Rotation);
    //         OnModify(send);
    //     }
    //
    //     [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //     private void SetWorldRotation(in Quaternion value, bool send = true) {
    //         SetLocalRotation(Parent == null ? value : Parent.WorldToLocalRotation(value), send);
    //     }
    //
    //     public Quaternion WorldToLocalRotation(in Quaternion value) {
    //         return Quaternion.Inverse(WorldRotation) * value;
    //     }
    //
    //     public Quaternion LocalToWorldRotation(in Quaternion value) {
    //         return WorldRotation * value;
    //     }
    //
    //     #endregion
    //
    //     #region Scale
    //
    //     #endregion
    //
    //     #region Matrix
    //
    //     protected Matrix4x4 localMatrix;
    //
    //     protected Matrix4x4 worldMatrix;
    //
    //     public Matrix4x4 LocalMatrix { get => localMatrix; set => SetLocalMatrix(value); }
    //
    //     public Matrix4x4 WorldMatrix { get => GetWorldMatrix(); set => SetWorldMatrix(value); }
    //
    //
    //     [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //     private void SetLocalMatrix(in Matrix4x4 value) {
    //         if (localMatrix == value) {
    //             return;
    //         }
    //         localMatrix = value;
    //         localMatrix.Deconstruct(out localPosition, out localRotation, out localScale);
    //         MarkHierarchy(DirtyTypes.All);
    //         OnModify(true);
    //     }
    //
    //     [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //     private void UpdateLocalMatrix() {
    //         MathUtil.Transformation(in localPosition, in localRotation, in localScale, out localMatrix);
    //     }
    //
    //     private void SetWorldMatrix(in Matrix4x4 value) {
    //         if (Parent == null) {
    //             LocalMatrix = value;
    //         } else {
    //             Matrix4x4.Invert(Parent.WorldMatrix, out var invMatrix);
    //             LocalMatrix = value * invMatrix;
    //         }
    //     }
    //
    //     private ref Matrix4x4 GetWorldMatrix() {
    //         if (dirty != 0) {
    //             if (Parent == null) {
    //                 worldMatrix = localMatrix;
    //             } else {
    //                 ref var parentWorldMatrix = ref Parent.GetWorldMatrix();
    //                 worldMatrix = Matrix4x4.Multiply(localMatrix, parentWorldMatrix);
    //             }
    //         }
    //         return ref worldMatrix;
    //     }
    //
    //     #endregion
    //
    //     #region Modify
    //
    //     [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //     private void OnModify(bool sendEvent) {
    //         if (!sendEvent) {
    //             return;
    //         }
    //         Entity.Modify<TransformComponent>();
    //     }
    //
    //     public void Modify(in Vector3 pos, in Quaternion rot) {
    //         var localPos = Parent == null ? pos : Parent.WorldToLocalPosition(pos);
    //         var localRot = Parent == null ? rot : Parent.WorldToLocalRotation(rot);
    //         DirtyTypes posDity = localPos == localPosition ? 0 : DirtyTypes.Position;
    //         DirtyTypes rotDity = localRot == localRotation ? 0 : DirtyTypes.Rotation;
    //         if((posDity|rotDity) == 0) {
    //             return;
    //         }
    //         localPosition = localPos;
    //         localRotation = localRot;
    //         UpdateLocalMatrix();
    //         MarkHierarchy(rotDity);
    //         OnModify(true);
    //     }
    //
    //     public void Modify(Vector3 pos, Quaternion rot, Vector3 scl) {
    //         var localPos = Parent == null ? pos : Parent.WorldToLocalPosition(pos);
    //         var localRot = Parent == null ? rot : Parent.WorldToLocalRotation(rot);
    //         var localScl = Parent == null ? scl : Parent.WorldToLocalScale(scl);
    //
    //         DirtyTypes posDity = localPos == localPosition ? 0 : DirtyTypes.Position;
    //         DirtyTypes rotDity = localRot == localRotation ? 0 : DirtyTypes.Rotation;
    //         DirtyTypes sclDity = localScl == LocalScale ? 0 : DirtyTypes.Scale;
    //
    //         if ((posDity | rotDity | sclDity) == 0) {
    //             return;
    //         }
    //         localPosition = localPos;
    //         localRotation = localRot;
    //         localScale = localScl;
    //         UpdateLocalMatrix();
    //         MarkHierarchy(rotDity);
    //         OnModify(true);
    //     }
    //     
    //     public void ModifySilent(Vector3 pos, Quaternion rot, Vector3 scl) {
    //         SetWorldPosition(pos, false);
    //         SetWorldRotation(rot, false);
    //         SetWorldScale(scl, false);
    //     }
    //
    //     public (Vector3 position, Quaternion rotation) LocalToWorld(Vector3 pos, Quaternion rot) {
    //         return (LocalToWorldPosition(pos), LocalToWorldRotation(rot));
    //     }
    //
    //     #endregion
    //       
    // }

}
