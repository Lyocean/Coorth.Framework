using Coorth.Logs;
using NUnit.Framework;

namespace Coorth.Framework; 

public class ActorRuntimeTest {
        
    [Test]
    public void CreateContainer() {
        var container = new ActorsRuntime(Dispatcher.Root, new ServiceLocator(), new LoggerConsole());
        Assert.NotNull(container);
    }
        
    [Test]
    public void CreateDomain() {
        var container = new ActorsRuntime(Dispatcher.Root, new ServiceLocator(), new LoggerConsole());
        var domain = container.CreateDomain("test");
        Assert.NotNull(domain);
    }
        
    [Test]
    public void GetDomain() {
        var container = new ActorsRuntime(Dispatcher.Root, new ServiceLocator(), new LoggerConsole());
        var domain10 = container.CreateDomain("111");
        var domain20 = container.CreateDomain("222");

        var domain11 = container.FindDomain("111");
        var domain21 = container.FindDomain("222");
        var domain31 = container.FindDomain("333");

        Assert.NotNull(domain10);
        Assert.NotNull(domain20);
        
        Assert.NotNull(domain11);
        Assert.NotNull(domain21);
        Assert.IsNull(domain31);
            
        Assert.IsTrue(ReferenceEquals(domain10, domain11));
        Assert.IsTrue(ReferenceEquals(domain20, domain21));
    }
        
    [Test]
    public void DisposeDomain() {
        var container = new ActorsRuntime(Dispatcher.Root, new ServiceLocator(), new LoggerConsole());
        var domain10 = container.CreateDomain("111");
        var _ = container.CreateDomain("222");

        domain10.Dispose();
            
        Assert.IsNull(container.FindDomain("111"));
        Assert.NotNull(container.FindDomain("222"));
    }
}