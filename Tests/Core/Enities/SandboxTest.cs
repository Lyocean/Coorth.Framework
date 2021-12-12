using NUnit.Framework;

namespace Coorth.Tests.Entities {
    public class SandboxTest {
        [Test]
        public void CreateSandbox() {
            var sandbox = new Sandbox();
            Assert.NotNull(sandbox);
            Assert.NotNull(sandbox.Config);
            Assert.IsFalse(sandbox.IsDisposed);

            sandbox.Dispose();
            Assert.IsTrue(sandbox.IsDisposed);
        }
        
        [Test]
        public void BindComponent() {
            var sandbox = new Sandbox();
            sandbox.BindComponent<TestValueComponent0>();
        }
        
        [Test]
        public void Service() {
            var sandbox = new Sandbox();
            Assert.IsNull(sandbox.GetService(typeof(SandboxTest)));
            Assert.IsNull(sandbox.GetService<SandboxTest>());
        }
        
        public static Sandbox Create() {
            var sandbox = new Sandbox();
            sandbox.BindComponent<TestValueComponent0>();
            sandbox.BindComponent<TestValueComponent1>();
            sandbox.BindComponent<TestValueComponent2>();
            sandbox.BindComponent<TestValueComponent3>();
            sandbox.BindComponent<TestValueComponent4>();

            sandbox.BindComponent<TestClassComponent0>();
            sandbox.BindComponent<TestClassComponent1>();
            sandbox.BindComponent<TestClassComponent2>();
            sandbox.BindComponent<TestClassComponent3>();
            sandbox.BindComponent<TestClassComponent4>();

            return sandbox;
        }
    }
}