using System;

namespace Coorth.Logs; 

public class LoggerEmpty : Logger {
    public override void Log(LogLevel level, string? message, Exception? exception = null) {
    }

    public override void Log(LogLevel level, string? message, LogColor color) {
    }
}