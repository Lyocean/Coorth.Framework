using System;
using System.Collections.Generic;

namespace Coorth.Logs {
    public class ComposeLogger : Logger {

        private readonly List<ILogger> loggers = new List<ILogger>();

        public void AddLogger(ILogger logger) {
            this.loggers.Add(logger);
        }
        
        public override void LogDebug(string message) {
            foreach (var logger in loggers) {
                logger.LogDebug(message);
            }
        }

        public override void LogWarning(string message) {
            foreach (var logger in loggers) {
                logger.LogWarning(message);
            }
        }

        public override void LogError(string message) {
            foreach (var logger in loggers) {
                logger.LogError(message);
            }
        }

        public override void LogException<T>(T e = default(T)) {
            foreach (var logger in loggers) {
                logger.LogException(e);
            }
        }
    }

    public class ComposeLogger<TLogger1, TLogger2> : ComposeLogger where TLogger1: class, ILogger, new() where TLogger2: class, ILogger, new() {

        public ComposeLogger() {
            var logger1 = Activator.CreateInstance<TLogger1>();
            var logger2 = Activator.CreateInstance<TLogger2>();
            AddLogger(logger1);
            AddLogger(logger2);
        }
    }
}