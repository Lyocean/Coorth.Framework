using System;
using System.Collections.Generic;
using Coorth.Logs;

namespace Coorth.Framework;

public class AppBuilder {

    public int Id { get; private set; }

    public string Name { get; private set; }

    private Dispatcher? dispatcher;
    public Dispatcher Dispatcher => dispatcher ?? new Dispatcher(null!);

    private ServiceLocator? services;
    public ServiceLocator Services => services ?? new ServiceLocator();

    private ILogger? logger;
    public ILogger Logger => logger ??= LoggerProvider.Invoke(Name);
    public Func<string, ILogger> LoggerProvider { get; set; } = name => new LoggerConsole(name);

    public string Path { get; set; } = string.Empty;

    private readonly List<IAppFeature> features = new();
    public IReadOnlyList<IAppFeature> Features => features;

    public AppBuilder() {
        Id = 0;
        Name = "App";
    }
    
    public AppBuilder(int id, string name) {
        Id = id;
        Name = name;
    }
    
    public AppBuilder WithId(int id) {
        Id = id;
        return this;
    }
    
    public AppBuilder WithName(string name) {
        Name = name;
        return this;
    }

    public AppBuilder WithPath(string path) {
        Path = path;
        return this;
    }
    
    public AppBuilder WithLog(Func<string, ILogger> provider) {
        LoggerProvider = provider;
        return this;
    }

    public AppBuilder WithDispatcher(Dispatcher value) {
        dispatcher = value;
        return this;
    }

    public AppBuilder WithServices(ServiceLocator value) {
        services = value;
        return this;
    }

    public AppBuilder WithFeature(IAppFeature feature) {
        features.Add(feature);
        return this;
    }

    public T Build<T>() where T: AppBase {
        var app = Activator.CreateInstance(typeof(T), this) as T;
        if (app == null) {
            throw new ApplicationException("Create app failed.");
        }
        return app;
    }
    
    public T Build<T>(Func<AppBuilder, T> provider) where T: AppBase {
        var app = provider(this);
        if (app == null) {
            throw new ApplicationException("Create app failed.");
        }
        return app;
    }
}