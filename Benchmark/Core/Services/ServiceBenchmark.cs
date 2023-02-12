﻿using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace Coorth.Framework;

[MemoryDiagnoser]
public class ServiceBenchmark {

    private const int BIND_TEST_COUNT = 10_000;
    
    private const int GET_TEST_COUNT = 100_000;
    
    private readonly ServiceLocator[] locators = new ServiceLocator[BIND_TEST_COUNT];

    private readonly ServiceLocator first = new();
    
    private readonly ServiceLocator repeat = new();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void _BindServices(ServiceLocator services) {
        services.Bind<IServiceApi1>(_ => new ServiceImpl1());
        services.Add<IServiceApi2>(new ServiceImpl2());
        services.Bind<ServiceInst1>();
        services.Add(new ServiceInst2());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void _GetServices(ServiceLocator services) {
        //services.Create<IServiceApi1>();
        services.GetService<IServiceApi2>();
        //services.Create<ServiceInst1>();
        services.GetService<ServiceInst2>();
    }
    
    [GlobalSetup]
    public void Setup() {
        for (var i = 0; i < BIND_TEST_COUNT; i++) {
            locators[i] = new ServiceLocator();
        }
        _BindServices(repeat);
        _GetServices(repeat);
    } 
    
    [IterationSetup]
    public void IterationSetup() {
        for (var i = 0; i < BIND_TEST_COUNT; i++) {
            locators[i] = new ServiceLocator();
        }
        first.Clear();
        _BindServices(first);
    }

    [IterationCleanup]
    public void IterationClear() {
        for (var i = 0; i < BIND_TEST_COUNT; i++) {
            locators[i].Clear();
        }
    }
    
    [Benchmark]
    public void BindService() {
        for (var i = 1; i < BIND_TEST_COUNT; i++) {
            var services = locators[i];
            services.Bind<IServiceApi1>(_ => new ServiceImpl1());
            services.Add<IServiceApi2>(new ServiceImpl2());
            services.Bind<ServiceInst1>();
            services.Add(new ServiceInst2());
        }
    }

    [Benchmark]
    public void NewService() {
        IServiceApi1? service1 = null;
        IServiceApi2? service2 = null;
        ServiceInst1? service11 = null;
        ServiceInst2? service12 = null;

        for (var i = 1; i < GET_TEST_COUNT; i++) {
            service1 = new ServiceImpl1();
            service2 = new ServiceImpl2();
            service11 = new ServiceInst1();
            service12 = new ServiceInst2();
        }

        if (service1 == null || service2 == null || service11 == null || service12 == null) {
            throw new NullReferenceException();
        }
    }
    [Benchmark]
    public void FirstGetService() {
        for (var i = 0; i < GET_TEST_COUNT; i++) {
            _GetServices(first);
        }
    }
    
    [Benchmark]
    public void RepeatGetService() {
        for (var i = 0; i < GET_TEST_COUNT; i++) {
            _GetServices(repeat);
        }
    }
}




public interface IServiceApi1 {

}

public class ServiceImpl1 : IServiceApi1 {

}

public interface IServiceApi2 {
    int Value { get; set; }
}

public sealed class ServiceImpl2 : IServiceApi2 {
    public int Value { get; set; }
}

public sealed class ServiceInst1 {
}

public sealed class ServiceInst2 {
    public int Value { get; set; }
}