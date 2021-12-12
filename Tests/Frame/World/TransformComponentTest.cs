using System.Linq;
using Coorth.Common;
using Coorth.Maths;
using Coorth.Tests.Entities;
using NUnit.Framework;
using Quaternion = System.Numerics.Quaternion;
using Vector3 = System.Numerics.Vector3;

namespace Coorth.Tests.World {
    public class TransformComponentTest {
        private Sandbox sandbox;
        
        [SetUp]
        public void Setup() {
            sandbox = SandboxTest.Create();
            sandbox.AddSystem<TransformSystem>();
        }

        [Test]
        public void AddComponent() {
            var entity = sandbox.CreateEntity();
            ref var transform = ref entity.Add<TransformComponent>();
            Assert.IsTrue(transform.LocalPosition == Vector3.Zero);
            Assert.IsTrue(transform.LocalRotation == Quaternion.Identity);
            Assert.IsTrue(transform.LocalAngles == Vector3.Zero);
            Assert.IsTrue(transform.LocalScale == Vector3.One);
            
            Assert.IsTrue(transform.WorldPosition == Vector3.Zero);
            Assert.IsTrue(transform.WorldRotation == Quaternion.Identity);
            Assert.IsTrue(transform.WorldAngles == Vector3.Zero);
            Assert.IsTrue(transform.WorldScale == Vector3.One);
        }

        [Test]
        public void SetPosition() {
            var entity = sandbox.CreateEntity();
            ref var transform = ref entity.Add<TransformComponent>();
            Assert.IsTrue(transform.LocalPosition == Vector3.Zero);
            var position = Vector3.One;
            transform.LocalPosition = position;
            Assert.IsTrue(transform.LocalPosition == position);
            Assert.IsTrue(transform.WorldPosition == position);
        }
        
        
        [Test]
        public void SeRotation() {
            var entity = sandbox.CreateEntity();
            ref var transform = ref entity.Add<TransformComponent>();
            Assert.IsTrue(transform.LocalRotation == Quaternion.Identity);
            var quaternion = (new Vector3(30, 170, 65)).ToQuaternion();
            transform.LocalRotation = quaternion;
            Assert.IsTrue((transform.LocalRotation - quaternion).Length() < 1e-5);
            Assert.IsTrue((transform.WorldRotation - quaternion).Length() < 1e-5);
        }
        
        [Test]
        public void SetAngles() {
            var entity = sandbox.CreateEntity();
            ref var transform = ref entity.Add<TransformComponent>();
            Assert.IsTrue(transform.LocalAngles == Vector3.Zero);
            var angles = (new Vector3(30, 170, 65)).ToQuaternion().ToEulerDegree();
            transform.LocalAngles = angles;
            Assert.IsTrue((transform.LocalAngles - angles).Length() < 0.01f);
            Assert.IsTrue((transform.WorldAngles - angles).Length() < 0.01f);
        }
        
        [Test]
        public void SetScaling() {
            var entity = sandbox.CreateEntity();
            ref var transform = ref entity.Add<TransformComponent>();
            Assert.IsTrue(transform.LocalScale == Vector3.One);
            var scaling = Vector3.UnitX;
            transform.LocalScale = scaling;
            Assert.IsTrue(transform.LocalScale == scaling);
            Assert.IsTrue(transform.WorldScale == scaling);
        }

        [Test]
        public void SetParent() {
            var parentEntity = sandbox.CreateEntity();
            ref var parentTransform = ref parentEntity.Add<TransformComponent>();
            
            var childEntity = sandbox.CreateEntity();
            ref var childTransform = ref childEntity.Add<TransformComponent>();

            Assert.IsTrue(parentTransform.ChildCount == 0);
            Assert.IsTrue(!parentTransform.GetChildren().Any());

            childTransform.SetParent(in parentTransform);

            Assert.IsTrue(childTransform.ParentEntity == parentEntity);
            Assert.IsTrue(parentTransform.ChildCount == 1);
            Assert.IsTrue(parentTransform.GetChildren().Count() == 1);
        }
        
        [Test]
        public void Children0() {
            var entity1 = sandbox.CreateEntity();
            ref var transform1 = ref entity1.Add<TransformComponent>();

            var entity2 = sandbox.CreateEntity();
            ref var transform2 = ref entity2.Add<TransformComponent>();
            transform1.SetParent(in transform2);

            Assert.IsTrue(transform1.ParentEntity == entity2);
        }
        
        [Test]
        public void ParentPosition() {
            var childEntity = sandbox.CreateEntity();
            ref var childTransform = ref childEntity.Add<TransformComponent>();
            var parentEntity = sandbox.CreateEntity();
            ref var parentTransform = ref parentEntity.Add<TransformComponent>();
            childTransform.SetParent(in parentTransform);

            childTransform.LocalPosition = Vector3.One;
            
            Assert.IsTrue(MathUtil.Approximate(parentTransform.LocalPosition, Vector3.Zero));
            Assert.IsTrue(MathUtil.Approximate(parentTransform.WorldPosition, Vector3.Zero));
            Assert.IsTrue(MathUtil.Approximate(childTransform.LocalPosition, Vector3.One));
            Assert.IsTrue(MathUtil.Approximate(childTransform.WorldPosition, Vector3.One));

            parentTransform.WorldPosition = Vector3.One;
            
            Assert.IsTrue(MathUtil.Approximate(childTransform.LocalPosition, Vector3.One));
            Assert.IsTrue(MathUtil.Approximate(childTransform.WorldPosition, Vector3.One * 2));
        }
    }
}