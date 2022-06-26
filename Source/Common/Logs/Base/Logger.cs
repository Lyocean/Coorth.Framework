using System;
using System.Collections.Generic;

namespace Coorth.Logs; 

public abstract class Logger : ILogger {

    public static ILogger Root = new LoggerConsole();
    
    private Stack<string>? scopes;

    public string? Name { get; protected set; }
    
    public LogScope BeginScope<T>() {
        return BeginScope(typeof(T).Name);
    }
         
    public LogScope BeginScope(string name) {
        scopes ??= new Stack<string>();
        scopes.Push(name);
        return new LogScope(this);
    }
    
    internal void EndScope() {
        scopes?.Pop();
    }

    public abstract void Log(LogLevel level, string? message);
    public abstract void Log(LogLevel level, string? message, LogColor color);
    public abstract void Exception(LogLevel level, Exception e);
}