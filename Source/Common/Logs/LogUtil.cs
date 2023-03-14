using System;
using System.Diagnostics;

namespace Coorth.Logs; 

public static class LogUtil {

    private static ILogger? logger;
    private static ILogger Logger => logger ?? throw new NullReferenceException();

    private static ILogManager? manager;
    private static ILogManager Manager => manager ?? throw new NullReferenceException();

    public static void Bind(ILogManager value) {
        manager = value;
        logger = value.Create("Default");
    }

    public static ILogger Create(string name) => Manager.Create(name);

    #region Debug

    [Conditional("DEBUG")]
    public static void Debug(string message) => Logger.Log(LogLevel.Debug, message);
    
    [Conditional("DEBUG")]
    public static void Debug(string message, ConsoleColor color) => Logger.Log(LogLevel.Debug, message, color);
    
    [Conditional("DEBUG")]
    public static void Debug(string name, string message) => Manager.Offer(name).Log(LogLevel.Debug, message);
    
    [Conditional("DEBUG")]
    public static void Debug(string name, string message, ConsoleColor color) => Manager.Offer(name).Log(LogLevel.Debug, message, color);

    #endregion

    #region Trace

    public static void Trace(string message) => Logger.Log(LogLevel.Trace, message);
    
    public static void Trace(string message, ConsoleColor color) => Logger.Log(LogLevel.Trace, message, color);
    
    public static void Trace(string name, string message) => Manager.Offer(name).Log(LogLevel.Trace, message);
    
    public static void Trace(string name, string message, ConsoleColor color) => Manager.Offer(name).Log(LogLevel.Trace, message, color);

    #endregion

    #region Info

    public static void Info(string message) => Logger.Log(LogLevel.Info, message);
    
    public static void Info(string message, ConsoleColor color) => Logger.Log(LogLevel.Info, message, color);
    
    public static void Info(string name, string message) => Manager.Offer(name).Log(LogLevel.Info, message);
    
    public static void Info(string name, string message, ConsoleColor color) => Manager.Offer(name).Log(LogLevel.Info, message, color);

    #endregion

    #region Warn

    public static void Warn(string message) => Logger.Log(LogLevel.Warn, message);
    
    public static void Warn(string message, ConsoleColor color) => Logger.Log(LogLevel.Warn, message, color);
    
    public static void Warn(string name, string message) => Manager.Offer(name).Log(LogLevel.Warn, message);
    
    public static void Warn(string name, string message, ConsoleColor color) => Manager.Offer(name).Log(LogLevel.Warn, message, color);


    #endregion

    #region Error

    public static void Error(string message) => Logger.Log(LogLevel.Error, message);
    
    public static void Error(string message, ConsoleColor color) => Logger.Log(LogLevel.Error, message, color);
    
    public static void Error(string name, string message) => Manager.Offer(name).Log(LogLevel.Error, message);
    
    public static void Error(string name, string message, ConsoleColor color) => Manager.Offer(name).Log(LogLevel.Error, message, color);


    #endregion

    #region Exception

    public static void Exception(Exception e) {
        if (logger != null) {
            logger.Exception(LogLevel.Error, e);
            return;
        }
        throw e;
    }

    public static void Exception(string name, Exception e) {
        if (manager != null) {
            Manager.Offer(name).Exception(LogLevel.Error, e);
        }
        throw e;
    }

    public static void Exception<T>(T? e = null) where T : Exception {
        if (logger != null) {
            logger.Exception(LogLevel.Error, e ?? Activator.CreateInstance<T>());
            return;
        }
        if (e != null) {
            throw e;
        }
    }

    public static void Exception<T>(string name, T? e = null) where T : Exception {
        if (manager != null) {
            manager.Offer(name).Exception(LogLevel.Error, e ?? Activator.CreateInstance<T>());
            return;
        }
        if (e != null) {
            throw e;
        }
    }

    #endregion

}