using System;
using System.Diagnostics;
#if NET5_0_OR_GREATER
using System.Threading.Channels;
using System.Threading.Tasks;
#endif

namespace Coorth.Logs {
    public interface ILogger : IDisposable {
        void Setup(LogManager manager, IServiceFactory services);
        void LogDebug(string message);

        void LogWarning(string message);
        
        void LogError(string message);

        void LogException<T>(T e = null) where T : Exception;
    }

    public enum LogLevel {
        Debug,
        Warning,
        Error,
        Exception,
    }
    
    public readonly struct LogData {
        public readonly DateTime Time;
        public readonly LogLevel Level;
        public readonly StackTrace Stack;
        public readonly object Module;
        public readonly string Content;

        public LogData(DateTime time, LogLevel level, object module, string content, StackTrace stack) {
            Time = time;
            Level = level;
            Module = module;
            Content = content;
            Stack = stack;
        }
    }
    
    public abstract class Logger : Disposable, ILogger {

        protected LogManager Manager { get; private set; }

        protected IServiceFactory Services { get; private set; }

        public virtual void Setup(LogManager manager, IServiceFactory services) {
            this.Manager = manager;
            this.Services = services;
        }
            
        
        public abstract void LogDebug(string message);
        public abstract void LogWarning(string message);
        public abstract void LogError(string message);
        public abstract void LogException<T>(T e = default(T)) where T : Exception;
    }
    
    #if NET5_0_OR_GREATER

    public abstract class AsyncLogger : Logger {

        private readonly Channel<LogData> channel = Channel.CreateUnbounded<LogData>();

        public override void Setup(LogManager manager, IServiceFactory services) {
            base.Setup(manager, services);
            var taskManager = services.GetService<TaskManager>();
            taskManager.RunAsync(WriteLogAsync);
        }
        
        private async Task WriteLogAsync() {
            while (await channel.Reader.WaitToReadAsync().ConfigureAwait(false)) {
                if (channel.Reader.TryRead(out LogData logData)) {
                    WriteLog(logData);
                }
            }
        }

        protected abstract void WriteLog(LogData logData);
        
        public override void LogDebug(string message) {
            var logData = new LogData(DateTime.Now, LogLevel.Debug, null, message, null);
            channel.Writer.TryWrite(logData);
        }

        public override void LogWarning(string message) {
            var logData = new LogData(DateTime.Now, LogLevel.Debug, null, message, null);
            channel.Writer.TryWrite(logData);
        }

        public override void LogError(string message) {
            var logData = new LogData(DateTime.Now, LogLevel.Debug, null, message, null);
            channel.Writer.TryWrite(logData);
        }

        public override void LogException<T>(T e = default(T)) {
            var logData = new LogData(DateTime.Now, LogLevel.Debug, null, e.ToString(), new StackTrace(e));
            channel.Writer.TryWrite(logData);
        }
    }
    #endif
}