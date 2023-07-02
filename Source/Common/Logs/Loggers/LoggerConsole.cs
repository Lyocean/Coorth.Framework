using System;

namespace Coorth.Logs;

public sealed class LoggerConsole : Logger {
    
    public LoggerConsole() { }
    
    public LoggerConsole(string name) { Name = name; }

    private static void LogHead(LogLevel level, string name) {
        var time = DateTime.Now;
        switch (level) {
            case LogLevel.Trace:
                Console.Write($"[{time}][T][{name}]");
                break;
            case LogLevel.Debug:
                Console.Write($"[{time}][D][{name}]");
                break;
            case LogLevel.Info:
                Console.Write($"[{time}][I][{name}]");
                break;
            case LogLevel.Warn:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"[{time}][W][{name}]");
                Console.ResetColor();
                break;
            case LogLevel.Error:
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"[{time}][E][{name}]");
                Console.ResetColor();
                break;
            case LogLevel.Fatal:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"[{time}][F][{name}]");
                Console.ResetColor();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(level), level, null);
        }
    }
        
    public override void Log(LogLevel level, string? message) {
        LogHead(level, Name);
        Console.WriteLine(message ?? string.Empty);
    }
    
    public override void Log(LogLevel level, string? message, ConsoleColor color) {
        LogHead(level, Name);
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    
    public override void Exception(LogLevel level, Exception e) {
        LogHead(level, Name);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(e.ToString());
        Console.ResetColor();
    }
}