using System;

namespace Coorth.Logs;

public static class LogExtension {
    
    public static void Trace(this ILogger logger, object value) => logger.Log(LogLevel.Trace, value.ToString());
    public static void Debug(this ILogger logger, object value) => logger.Log(LogLevel.Debug, value.ToString());
    public static void Info(this ILogger logger, object value) => logger.Log(LogLevel.Info, value.ToString());
    public static void Warn(this ILogger logger, object value) => logger.Log(LogLevel.Warn, value.ToString());
    public static void Error(this ILogger logger, object value) => logger.Log(LogLevel.Error, value.ToString());
    public static void Fatal(this ILogger logger, object value) => logger.Log(LogLevel.Fatal, value.ToString());
        
    public static void Trace(this ILogger logger, string message) => logger.Log(LogLevel.Trace, message);
    public static void Debug(this ILogger logger, string message) => logger.Log(LogLevel.Debug, message);
    public static void Info(this ILogger logger, string message) => logger.Log(LogLevel.Info, message);
    public static void Warn(this ILogger logger, string message) => logger.Log(LogLevel.Warn, message);
    public static void Error(this ILogger logger, string message) => logger.Log(LogLevel.Error, message);
    public static void Fatal(this ILogger logger, string message) => logger.Log(LogLevel.Fatal, message);

    public static void Trace<T>(this ILogger logger, T value) => logger.Log(LogLevel.Trace, value?.ToString() ?? "null");
    public static void Debug<T>(this ILogger logger, T value) => logger.Log(LogLevel.Debug, value?.ToString() ?? "null");
    public static void Info<T>(this ILogger logger, T value) => logger.Log(LogLevel.Info, value?.ToString() ?? "null");
    public static void Warn<T>(this ILogger logger, T value) => logger.Log(LogLevel.Warn, value?.ToString() ?? "null");
    public static void Error<T>(this ILogger logger, T value) => logger.Log(LogLevel.Error, value?.ToString() ?? "null");
    public static void Fatal<T>(this ILogger logger, T value) => logger.Log(LogLevel.Fatal, value?.ToString() ?? "null");
        
    public static void Trace(this ILogger logger, Exception exception) => logger.Log(LogLevel.Trace, null, exception);
    public static void Debug(this ILogger logger, Exception exception) => logger.Log(LogLevel.Debug, null, exception);
    public static void Info(this ILogger logger, Exception exception) => logger.Log(LogLevel.Info, null, exception);
    public static void Warn(this ILogger logger, Exception exception) => logger.Log(LogLevel.Warn, null, exception);
    public static void Error(this ILogger logger, Exception exception) => logger.Log(LogLevel.Error, null, exception);
    public static void Fatal(this ILogger logger, Exception exception) => logger.Log(LogLevel.Fatal, null, exception);
}