using Coorth.Logs;
using NUnit.Framework;

namespace Coorth.Framework; 

public class ActorRuntimeTest {
        
    [Test]
    public void CreateRuntime() {
        var runtime = new ActorsRuntime(Dispatcher.Root, new ServiceLocator(), new LoggerConsole());
        Assert.NotNull(runtime);
    }
        
    [Test]
    public void CreateDomain() {
        var runtime = new ActorsRuntime(Dispatcher.Root, new ServiceLocator(), new LoggerConsole());
        var domain = runtime.CreateDomain("test");
        Assert.NotNull(domain);
    }
        
    [Test]
    public void GetDomain() {
        var runtime = new ActorsRuntime(Dispatcher.Root, new ServiceLocator(), new LoggerConsole());
        var domain10 = runtime.CreateDomain("111");
        var domain20 = runtime.CreateDomain("222");

        var domain11 = runtime.FindDomain("111");
        var domain21 = runtime.FindDomain("222");
        var domain31 = runtime.FindDomain("333");

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
        var runtime = new ActorsRuntime(Dispatcher.Root, new ServiceLocator(), new LoggerConsole());
        var domain10 = runtime.CreateDomain("111");
        var _ = runtime.CreateDomain("222");

        domain10.Dispose();
            
        Assert.IsNull(runtime.FindDomain("111"));
        Assert.NotNull(runtime.FindDomain("222"));
    }
}