namespace Coorth.Logs; 

public static class LoggerExtension {
        
    public static void Debug(this ILogger logger, string message, LogColor color) => logger.Log(LogLevel.Debug, $"{message}");

    public static void Debug(this ILogger logger, string module, string message) => logger.Log(LogLevel.Debug, $"[{module}]: {message}");

    public static void Debug(this ILogger logger, string module, string message, LogColor color) => logger.Log(LogLevel.Debug, $"[{module}]: {message}", color);

    public static void Info(this ILogger logger, string message, LogColor color) => logger.Log(LogLevel.Info, $"{message}", color);

    public static void Info(this ILogger logger, string module, string message) => logger.Log(LogLevel.Info, $"[{module}]: {message}");

    public static void Info(this ILogger logger, string module, string message, LogColor color) => logger.Log(LogLevel.Info, $"[{module}]: {message}", color);
}