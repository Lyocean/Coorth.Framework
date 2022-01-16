using System;
using System.Collections.Generic;
using Coorth.Maths;

namespace Coorth.Logs {
    public class ComposeLogger : Logger {

        private readonly List<ILogger> loggers = new List<ILogger>();

        protected void AddLogger(ILogger logger) {
            this.loggers.Add(logger);
        }

        public override void Log(in LogData data) {
            foreach (var logger in loggers) {
                logger.Log(in data);
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