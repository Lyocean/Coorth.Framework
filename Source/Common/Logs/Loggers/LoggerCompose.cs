using System;
using System.Collections.Generic;

namespace Coorth.Logs;

public class LoggerCompose : Logger {
    private List<ILogger> Loggers { get; } = new();

    public LoggerCompose(ILogger logger1, ILogger logger2) {
        Loggers.Add(logger1);
        Loggers.Add(logger2);
    }

    public LoggerCompose(ILogger logger1, ILogger logger2, ILogger logger3) {
        Loggers.Add(logger1);
        Loggers.Add(logger2);
        Loggers.Add(logger3);
    }

    public LoggerCompose(params ILogger[] loggers) {
        Loggers.AddRange(loggers);
    }

    public override void Log(LogLevel level, string? message) {
        foreach (var logger in Loggers) {
            logger.Log(level, message);
        }
    }

    public override void Log(LogLevel level, string? message, ConsoleColor color) {
        foreach (var logger in Loggers) {
            logger.Log(level, message, color);
        }
    }

    public override void Exception(LogLevel level, Exception e) {
        foreach (var logger in Loggers) {
            logger.Exception(level, e);
        }
    }
}