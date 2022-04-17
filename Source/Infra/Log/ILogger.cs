using System;

namespace Coorth.Logs {
    public interface ILogger {
        LogScope BeginScope<T>();
        LogScope BeginScope(string name);

        void Log(LogLevel level, string? message, Exception? exception = null);
        void Log(LogLevel level, string? message, LogColor color);

        void Trace(object value);
        void Debug(object value);
        void Info(object value);
        void Warn(object value);
        void Error(object value);
        void Fatal(object value);

        void Trace(string message);
        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Fatal(string message);

        void Trace<T>(T value);
        void Debug<T>(T value);
        void Info<T>(T value);
        void Warn<T>(T value);
        void Error<T>(T value);
        void Fatal<T>(T value);

        void Trace(Exception exception);
        void Debug(Exception exception);
        void Info(Exception exception);
        void Warn(Exception exception);
        void Error(Exception exception);
        void Fatal(Exception exception);
    }
}