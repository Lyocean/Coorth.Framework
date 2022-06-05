using System;
using System.Diagnostics;

namespace Coorth.Logs; 

public static class LogUtil {
        
    private static ILogger Logger => Logs.Logger.Root;
        
    private static Func<string, ILogger> factory = _ => new LoggerConsole();

    public static void Bind(ILogger value, Func<string, ILogger> func) {
        Logs.Logger.Root = value;
        factory = func;
    }

    public static ILogger Create(string name) => factory(name);
        
    [Conditional("DEBUG")]
    public static void Debug(string message) => Logger.Debug(message);

    [Conditional("DEBUG")]
    public static void Debug(string module, string message) => Logger.Debug(module, message);

    [Conditional("DEBUG")]
    public static void Debug(string module, string message, LogColor color) => Logger.Debug(module, message, color);

    
    public static void Trace(string message) => Logger.Trace(message);
    public static void Trace(string message, LogColor color) => Logger.Log(LogLevel.Trace, message, color);

    public static void Info(string message) => Logger.Info(message);
    public static void Info(string module, string message) => Logger.Info(module, message);

    public static void Info(string module, string message, LogColor color) => Logger.Info(module, message, color);

    public static void Warning(string message) => Logger.Warn(message);

    public static void Error(string message) => Logger.Error(message);

    public static void Exception<T>(T? e = null) where T : Exception => Logger.Error(e);
}