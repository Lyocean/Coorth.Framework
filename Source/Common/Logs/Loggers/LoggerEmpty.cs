using System;

namespace Coorth.Logs; 

public class LoggerEmpty : Logger {
    public override void Log(LogLevel level, string? message) {
    }

    public override void Log(LogLevel level, string? message, LogColor color) {
    }

    public override void Exception(LogLevel level, Exception e) {
    }
}