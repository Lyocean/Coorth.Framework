using System;
using Coorth.Maths;
// using System.Diagnostics;
// #if NET5_0_OR_GREATER
// using System.Threading.Channels;
// using System.Threading.Tasks;
// #endif

namespace Coorth.Logs {
    public interface ILogger : IDisposable {
        void Setup(LogManager manager, IServiceLocator services);

        void Log(in LogData data);
        
        void LogDebug(string message);

        void LogDebug(string module, string message, Color color);
        
        void LogInfo(string module, string message, Color color);
        
        void LogWarning(string message);
        
        void LogError(string message);

        void LogException<T>(T e = null) where T : Exception;
    }


    
    public abstract class Logger : Disposable, ILogger {

        protected LogManager Manager { get; private set; }

        protected IServiceLocator Services { get; private set; }

        public virtual void Setup(LogManager manager, IServiceLocator services) {
            this.Manager = manager;
            this.Services = services;
        }

        public abstract void Log(in LogData data);

        public virtual void LogDebug(string message) {
            var data = new LogData(DateTime.Now, LogLevel.Debug, null, message, null, LogColors.White);
            Log(in data);
        }

        public virtual void LogDebug(string module, string message, Color color) {
            var data = new LogData(DateTime.Now, LogLevel.Debug, module, message, null, color);
            Log(in data);
        }

        public virtual void LogInfo(string module, string message, Color color) {
            var data = new LogData(DateTime.Now, LogLevel.Info, module, message, null, color);
            Log(in data);
        }

        public virtual void LogWarning(string message) {
            var data = new LogData(DateTime.Now, LogLevel.Warning, null, message, null, LogColors.White);
            Log(in data);
        }

        public virtual void LogError(string message) {
            var data = new LogData(DateTime.Now, LogLevel.Error, null, message, null, LogColors.White);
            Log(in data);
        }

        public virtual void LogException<T>(T e = default(T)) where T : Exception {
            var data = new LogData(DateTime.Now, null, e, LogColors.White);
            Log(in data);
        }
        
        public virtual void LogException<T>(object module, T e = default(T)) where T : Exception {
            var data = new LogData(DateTime.Now, module, e, LogColors.White);
            Log(in data);
        }
    }
    
    // #if NET5_0_OR_GREATER
    //
    // public abstract class AsyncLogger : Logger {
    //
    //     private readonly Channel<LogData> channel = Channel.CreateUnbounded<LogData>();
    //
    //     public override void Setup(LogManager manager, IServiceLocator services) {
    //         base.Setup(manager, services);
    //         var taskManager = services.GetService<TaskManager>();
    //         taskManager.RunAsync(WriteLogAsync);
    //     }
    //     
    //     private async Task WriteLogAsync() {
    //         while (await channel.Reader.WaitToReadAsync().ConfigureAwait(false)) {
    //             if (channel.Reader.TryRead(out LogData logData)) {
    //                 WriteLog(logData);
    //             }
    //         }
    //     }
    //
    //     protected abstract void WriteLog(LogData logData);
    //     
    //     public override void LogDebug(string message) {
    //         var logData = new LogData(DateTime.Now, LogLevel.Debug, null, message, null);
    //         channel.Writer.TryWrite(logData);
    //     }
    //
    //     public override void LogDebug(string module, string message, Color color) {
    //         var logData = new LogData(DateTime.Now, LogLevel.Debug, null, message, null);
    //         channel.Writer.TryWrite(logData);
    //     }
    //
    //     public override void LogInfo(string module, string message, Color color) {
    //         var logData = new LogData(DateTime.Now, LogLevel.Debug, null, message, null);
    //         channel.Writer.TryWrite(logData);
    //     }
    //
    //     public override void LogWarning(string message) {
    //         var logData = new LogData(DateTime.Now, LogLevel.Debug, null, message, null);
    //         channel.Writer.TryWrite(logData);
    //     }
    //
    //     public override void LogError(string message) {
    //         var logData = new LogData(DateTime.Now, LogLevel.Debug, null, message, null);
    //         channel.Writer.TryWrite(logData);
    //     }
    //
    //     public override void LogException<T>(T e = default(T)) {
    //         var logData = new LogData(DateTime.Now, LogLevel.Debug, null, e.ToString(), new StackTrace(e));
    //         channel.Writer.TryWrite(logData);
    //     }
    // }
    // #endif
}