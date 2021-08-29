using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;


namespace Coorth {
    using Coorth.Maths;
    [Component, DataContract, Guid("482164B6-9432-4AB8-B217-A843BEB96CB8")]
    public class TransformComponent : RefComponent, ICloneable {

        #region Fields
        
        protected TransformComponent parent;
        public TransformComponent Parent => parent;

        private readonly List<TransformComponent> children = new List<TransformComponent>();
        public IReadOnlyList<TransformComponent> Children => children;

        public int ChildCount => children.Count;
        
        public TransformComponent Root => parent != null ? parent.Root : this;
        
        [StoreIgnore]
        protected Vector3 position;
        [StoreMember(5), Display(Editable = true)]
        public Vector3 Position { get => position; set => Modify(ref position, value); }
        public Vector3 WorldPosition { get => position; set=> Position = value; }
        public Vector3 LocalPosition { get => Parent != null ? position - Parent.position : position; set => Position = Parent != null ? Parent.position + value : value; }

        protected Quaternion rotation;
        [StoreMember(10), Display(Editable = true)]
        public Quaternion Rotation { get => rotation; set => Modify(ref rotation, value); }
        public Quaternion WorldRotation { get => rotation; set => Rotation = value; }
        public Quaternion LocalRotation { get => Parent != null ? rotation/Parent.rotation : rotation; set => Rotation = Parent != null ? Parent.rotation * value : value; }
        
        public Vector3 Angles => rotation.ToAxis();
        public Vector3 WorldAngles => WorldRotation.ToAxis();
        public Vector3 LocalAngles => LocalRotation.ToAxis();
        
        protected Vector3 scale = Vector3.One;
        [StoreMember(15), Display(Editable = true)]
        public Vector3 Scale { get => scale; set => Modify(ref scale, value);  }
        public Vector3 WorldScale { get => scale; set => Scale = value; }
        public Vector3 LocalScale { get => Parent != null ? scale/Parent.scale : scale; set => Scale = Parent != null ? Parent.scale * value : value; }

        protected Matrix4x4 worldMatrix;
        public ref Matrix4x4 WorldMatrix => ref worldMatrix;

        public Matrix4x4 LocalMatrix => Parent != null ? worldMatrix * Matrix4x4.Negate(Parent.worldMatrix) : worldMatrix;

        #endregion

        #region Lifetime

        public void OnAdd() {

        }

        public void OnRemove() {
            if(parent != null) {
                parent.children.Remove(this);
                parent = null;
            }
            foreach(var childTransform in children) {
                childTransform.Entity.Destroy();
            }
            children.Clear();
        }

        #endregion
        
        #region Hierarchy
 
        private void OnParentChange(TransformComponent value) {
            if (parent == value) {
                return;
            }
            parent?.children.Remove(this);
            value?.children.Add(this);
            parent = value;
        }

        public void SetParent(TransformComponent transform, bool worldStay) {
            OnParentChange(transform);
        }

        public void AddChild(TransformComponent child) {
            child.OnParentChange(this);
        }
        
        #endregion

        #region Modify

        
        protected virtual void OnModify(bool sendEvent) {
            MathUtil.Transformation(ref position, ref rotation, ref scale, out worldMatrix);
            if (sendEvent) {
                Entity.Modify<TransformComponent>();
            }
        }
        
        private void Modify<T>(ref T field, T value) {
            if (field.Equals(value)) {
                return;
            }
            field = value;
            OnModify(true);
        }

        public void Modify(Vector3 pos) {
            if (this.position.Equals(pos)) {
                return;
            }
            this.position = pos;
            OnModify(true);
        }

        public void Modify(Vector3 pos, Quaternion rot) {
            if (this.position.Equals(pos) && this.rotation.Equals(rot)) {
                return;
            }
            this.position = pos;
            this.rotation = rot;
            OnModify(true);
        }

        public void Modify(Vector3 pos, Quaternion rot, Vector3 scl) {
            if (this.position.Equals(pos) && this.rotation.Equals(rot) && this.scale.Equals(scl)) {
                return;
            }
            this.position = pos;
            this.rotation = rot;
            this.scale = scl;
            OnModify(true);
        }
        
        public void ModifySilent(Vector3 pos, Quaternion rot, Vector3 scl) {
            if (this.position.Equals(pos) && this.rotation.Equals(rot) && this.scale.Equals(scl)) {
                return;
            }
            this.position = pos;
            this.rotation = rot;
            this.scale = scl;
            OnModify(false);
        }

        #endregion

        #region Transformation

        public Vector3 LocalToWorld(Vector3 pos) {
            return Vector3.Transform(position, WorldMatrix);
        }
        
        public void LocalToWorld(ref Vector3 pos, ref Quaternion rot, ref Vector3 scl) {
            WorldMatrix.Deconstruct(out var worldPos, out var worldRot, out var worldScl);
            //Position
            pos = Vector3.Transform(pos, WorldMatrix);
            //Rotation
            rot = Quaternion.Multiply(rot, worldRot);
            //Scale
            scl = new Vector3(scl.X * worldScl.X, scl.Y * worldScl.Y, scl.Z * worldScl.Z);
        }
        
        public (Vector3 position, Quaternion rotation) LocalToWorld(Vector3 pos, Quaternion rot) {
            var scl = Vector3.One;
            LocalToWorld(ref pos, ref rot, ref scl);
            return (pos, rot);
        }
        
        public Vector3 WorldToLocal(Vector3 pos) {
            Matrix4x4.Invert(WorldMatrix, out var invMatrix);
            return Vector3.Transform(pos, invMatrix);
        }
        
        public void WorldToLocal(ref Vector3 pos, ref Quaternion rot, ref Vector3 scl) {
            WorldMatrix.Deconstruct(out var worldPos, out var worldRot, out var worldScl);
            Matrix4x4.Invert(WorldMatrix, out var invMatrix);
            //Position
            pos = Vector3.Transform(pos, invMatrix);
            //Rotation
            rot = Quaternion.Multiply(rot, Quaternion.Inverse(worldRot));
            //Scale
            scl = new Vector3(scl.X / worldScl.X, scl.Y / worldScl.Y, scl.Z / worldScl.Z);
        }

        #endregion

        public object Clone() {
            return new TransformComponent() {
                Position = this.Position,
                Rotation = this.Rotation,
                Scale = this.Scale,
            };
        }
    }
    
    [Component, DataContract, Guid("B8F4D0DE-0035-4734-A2E9-DFF6481A1195")]
    public struct PositionComponent : IComponent {
        public Vector3 Value;
    }

    [Component, DataContract, Guid("1D86EFC8-611E-498D-8DCC-66729215FEC9")]
    public struct RotationComponent : IComponent {
        public Vector3 Value;
    }

    [Component, DataContract, Guid("782DC9F0-6B8F-4AA0-A1E8-D1CFE095EC03")]
    public struct WorldMatrixComponent : IComponent {
        public Matrix4x4 Value;
    }

}
