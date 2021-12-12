using NUnit.Framework;


namespace Coorth.Tests.Services {
    [TestFixture]
    public class ServiceTest {

        private ServiceLocator services;

        [SetUp]
        public void Setup() {
            services = new ServiceLocator();
        }

        [Test]
        public void BindFromGeneric() {
            Assert.IsTrue(services.Singleton<TestService1>() == null);
            services.Bind<TestService1>();
            Assert.IsTrue(services.Singleton<TestService1>() != null);
        }

        [Test]
        public void BindFromInstance() {
            services.Bind<TestService2>(new TestService2() { Value = 1 });
            Assert.IsTrue(services.Singleton<TestService2>().Value == 1);
        }

        [Test]
        public void BindFromProvider() {
            services.Bind<TestService3>(_=>new TestService3() { Value = 2 });
            Assert.IsTrue(services.Singleton<TestService3>().Value == 2);
        }

        [Test]
        public void ChildServices() {
            var childServices = new ServiceLocator();
            services.AddChild(childServices);
            services.Bind<TestService1>().Singleton();
            Assert.IsTrue(childServices.GetService<TestService1>() != null);
            Assert.IsTrue(childServices.GetService<TestService4>() == null);
        }
    }

    public class TestService1 {

        public int Value;
    }

    public class TestService2 {

        public int Value;
    }

    public class TestService3 {

        public int Value;
    }

    public class TestService4 {

        public int Value;
    }

    public class TestService5 {

        public int Value;
    }
}
