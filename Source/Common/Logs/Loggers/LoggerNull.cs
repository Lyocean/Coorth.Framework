using System;

namespace Coorth.Logs;

public sealed class LoggerNull : Logger {

    public static readonly LoggerNull Instance = new();
    
    public override void Log(LogLevel level, string? message) { }
    
    public override void Log(LogLevel level, string? message, ConsoleColor color) { }
    
    public override void Exception(LogLevel level, Exception e) { }
}