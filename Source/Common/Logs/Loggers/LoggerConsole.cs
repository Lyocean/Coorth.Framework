using System;

namespace Coorth.Logs;

public sealed class LoggerConsole : Logger {
    
    public LoggerConsole() { }
    
    public LoggerConsole(string name) { Name = name; }

    private void LogHead(LogLevel level, string name) {
        var time = DateTime.Now;
        switch (level) {
            case LogLevel.Trace:
                Console.Write($"[{time}][{name}][T]");
                break;
            case LogLevel.Debug:
                Console.Write($"[{time}][{name}][D]");
                break;
            case LogLevel.Info:
                Console.Write($"[{time}][{name}][I]");
                break;
            case LogLevel.Warn:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"[{time}][{name}][W]");
                Console.ResetColor();
                break;
            case LogLevel.Error:
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"[{time}][{name}][E]");
                Console.ResetColor();
                break;
            case LogLevel.Fatal:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"[{time}][{Name}][F]");
                Console.ResetColor();
                break;
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