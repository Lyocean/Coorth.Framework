using Coorth.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Coorth.Logs; 

[Manager]
public interface ILogManager {
    
    ILogger Create(string name);
    
    ILogger? Find(string name);
    
    ILogger Offer(string name);

    void Remove(string name);
}

[Manager]
public class LogManager : Manager, ILogManager {

    private readonly Func<string, ILogger> provider;

    private readonly Dictionary<string, ILogger> loggers = new();

    public LogManager(Func<string, ILogger> func) {
        provider = func;
        LogUtil.Bind(this);
    }

    public ILogger Create(string name) {
        var logger = provider.Invoke(name);
        loggers.Add(name, logger);
        return logger;
    }

    public ILogger? Find(string name) {
        return loggers.TryGetValue(name, out var logger) ? logger : null;
    }

    public ILogger Offer(string name) {
        return loggers.TryGetValue(name, out var logger) ? logger : Create(name);
    }

    public void Remove(string name) {
        loggers.Remove(name);
    }
}