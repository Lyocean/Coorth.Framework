using System;

namespace Coorth.Logs;

public interface ILogger {

    LogScope BeginScope<T>();
    LogScope BeginScope(string name);

    void Log(LogLevel level, string? message, Exception? exception = null);
    void Log(LogLevel level, string? message, LogColor color);

}
