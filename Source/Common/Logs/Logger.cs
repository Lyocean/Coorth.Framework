using System;
using System.Collections.Generic;

namespace Coorth.Logs; 

public interface ILogger {

    LogScope BeginScope<T>();
    
    LogScope BeginScope(string name);

    bool IsEnable(LogLevel level);

    void Log(LogLevel level, string? message);
    
    void Log(LogLevel level, string? message, ConsoleColor color);
    
    void Exception(LogLevel level, Exception e);
}


public abstract class Logger : ILogger {

    private Stack<string>? scopes;

    public string Name { get; protected set; } = string.Empty;
    
    public LogScope BeginScope<T>() {
        return BeginScope(typeof(T).Name);
    }
         
    public LogScope BeginScope(string name) {
        scopes ??= new Stack<string>();
        scopes.Push(name);
        return new LogScope(this);
    }

    public bool IsEnable(LogLevel level) {
        throw new NotImplementedException();
    }

    internal void EndScope() => scopes?.Pop();

    public abstract void Log(LogLevel level, string? message);
    
    public abstract void Log(LogLevel level, string? message, ConsoleColor color);
    
    public abstract void Exception(LogLevel level, Exception e);
}

public static class LoggerExtension {
        
    public static void Trace(this ILogger logger, string message) => logger.Log(LogLevel.Trace, message);
    public static void Trace(this ILogger logger, string message, ConsoleColor color) => logger.Log(LogLevel.Trace, message, color);
    public static void Trace(this ILogger logger, Exception exception) => logger.Exception(LogLevel.Trace, exception);

    public static void Debug(this ILogger logger, string message) => logger.Log(LogLevel.Debug, message);
    public static void Debug(this ILogger logger, string message, ConsoleColor color) => logger.Log(LogLevel.Debug, message, color);
    public static void Debug(this ILogger logger, Exception exception) => logger.Exception(LogLevel.Debug, exception);

    public static void Info(this ILogger logger, string message) => logger.Log(LogLevel.Info, message);
    public static void Info(this ILogger logger, string message, ConsoleColor color) => logger.Log(LogLevel.Info, message, color);
    public static void Info(this ILogger logger, Exception exception) => logger.Exception(LogLevel.Info, exception);

    public static void Warn(this ILogger logger, string message) => logger.Log(LogLevel.Warn, message);
    public static void Warn(this ILogger logger, string message, ConsoleColor color) => logger.Log(LogLevel.Warn, message, color);
    public static void Warn(this ILogger logger, Exception exception) => logger.Exception(LogLevel.Warn, exception);
    
    public static void Error(this ILogger logger, string message) => logger.Log(LogLevel.Error, message);
    public static void Error(this ILogger logger, string message, ConsoleColor color) => logger.Log(LogLevel.Error, message, color);
    public static void Error(this ILogger logger, Exception exception) => logger.Exception(LogLevel.Error, exception);

    public static void Fatal(this ILogger logger, string message) => logger.Log(LogLevel.Fatal, message);
    public static void Fatal(this ILogger logger, string message, ConsoleColor color) => logger.Log(LogLevel.Fatal, message, color);
    public static void Fatal(this ILogger logger, Exception exception) => logger.Exception(LogLevel.Fatal, exception);

}