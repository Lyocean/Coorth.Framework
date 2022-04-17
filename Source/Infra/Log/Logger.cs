using System;
using System.Collections.Generic;

namespace Coorth.Logs {
    public abstract class Logger : ILogger {
        
        private Stack<string>? scopes;

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

        public abstract void Log(LogLevel level, string? message, Exception? exception = null);

        public abstract void Log(LogLevel level, string? message, LogColor color);

        public void Trace(object value) => Log(LogLevel.Trace, value.ToString());
        public void Debug(object value) => Log(LogLevel.Debug, value.ToString());
        public void Info(object value) => Log(LogLevel.Info, value.ToString());
        public void Warn(object value) => Log(LogLevel.Warn, value.ToString());
        public void Error(object value) => Log(LogLevel.Error, value.ToString());
        public void Fatal(object value) => Log(LogLevel.Fatal, value.ToString());
        
        public void Trace(string message) => Log(LogLevel.Trace, message);
        public void Debug(string message) => Log(LogLevel.Debug, message);
        public void Info(string message) => Log(LogLevel.Info, message);
        public void Warn(string message) => Log(LogLevel.Warn, message);
        public void Error(string message) => Log(LogLevel.Error, message);
        public void Fatal(string message) => Log(LogLevel.Fatal, message);

        public void Trace<T>(T value) => Log(LogLevel.Trace, value?.ToString() ?? "null");
        public void Debug<T>(T value) => Log(LogLevel.Debug, value?.ToString() ?? "null");
        public void Info<T>(T value) => Log(LogLevel.Info, value?.ToString() ?? "null");
        public void Warn<T>(T value) => Log(LogLevel.Warn, value?.ToString() ?? "null");
        public void Error<T>(T value) => Log(LogLevel.Error, value?.ToString() ?? "null");
        public void Fatal<T>(T value) => Log(LogLevel.Fatal, value?.ToString() ?? "null");
        
        public void Trace(Exception exception) => Log(LogLevel.Trace, null, exception);
        public void Debug(Exception exception) => Log(LogLevel.Debug, null, exception);
        public void Info(Exception exception) => Log(LogLevel.Info, null, exception);
        public void Warn(Exception exception) => Log(LogLevel.Warn, null, exception);
        public void Error(Exception exception) => Log(LogLevel.Error, null, exception);
        public void Fatal(Exception exception) => Log(LogLevel.Fatal, null, exception);
    }
}