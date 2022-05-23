using System;
using NUnit.Framework;

namespace Coorth.Framework; 

[TestFixture]
public class ServiceTest {

    private ServiceLocator services;

    [SetUp]
    public void Setup() {
        services = new ServiceLocator();
    }

    [Test]
    public void BindFromGenericDirect() {
        Assert.Catch<NullReferenceException>(() => services.Singleton<ServiceImpl1>());
        services.Bind<ServiceImpl1>();
        services.Singleton<ServiceImpl1>();
    }
    
    [Test]
    public void BindFromGenericByInterface() {
        Assert.Catch<NullReferenceException>(() => services.Singleton<IServiceApi1>());
        services.Bind<IServiceApi1, ServiceImpl1>();
        Assert.Catch<NullReferenceException>(() => services.Singleton<ServiceImpl1>());
        services.Singleton<IServiceApi1>();
    }

    [Test]
    public void BindFromInstanceDirect() {
        services.Bind(new ServiceImpl2() { Value = 1 });
        Assert.IsTrue(services.Singleton<ServiceImpl2>().Value == 1);
    }

    [Test]
    public void BindFromInstanceByInterface() {
        services.Bind<IServiceApi2>(new ServiceImpl2() { Value = 1 });
        Assert.Catch<NullReferenceException>(() => services.Singleton<ServiceImpl2>());
        Assert.IsTrue(services.Singleton<IServiceApi2>().Value == 1);
    }
    
    [Test]
    public void BindFromProviderDirect() {
        services.Bind(_ => new ServiceInst2() { Value = 2 });
        Assert.IsTrue(services.Singleton<ServiceInst2>().Value == 2);
    }

    [Test]
    public void BindFromProviderByInterface() {
        services.Bind<IServiceApi2>(_=>new ServiceImpl2() { Value = 7 });
        Assert.IsTrue(services.Singleton<IServiceApi2>().Value == 7);
    }
    
    [Test]
    public void FindFromParentLocator() {
        var childServices = new ServiceLocator();
        services.AddChild(childServices);
        services.Bind<ServiceInst1>().Singleton();
        Assert.IsTrue(childServices.Find<ServiceInst1>() != null);
        Assert.IsTrue(childServices.Find<ServiceInst2>() == null);
    }
}