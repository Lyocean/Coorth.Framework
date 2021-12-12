using System.Numerics;
using Coorth.Maths;
using NUnit.Framework;

namespace Coorth.Tests.Common {
    public class MathUtilTest {

        
        [Test]
        public void Vector3ToQuaternion() {
            var value = Vector3.Zero.ToQuaternion();
            Assert.IsTrue(value == Quaternion.Identity);
        }
        
        [Test]
        public void QuaternionToVector3() {
            var value = Quaternion.Identity.ToEulerDegree();
            Assert.IsTrue(value == Vector3.Zero);
        }
    }
}