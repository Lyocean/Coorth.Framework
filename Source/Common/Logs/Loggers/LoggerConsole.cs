using System;

namespace Coorth.Logs; 

public class LoggerConsole : Logger {

    private static void LogHead(LogLevel level) {
        var time = DateTime.Now;
        switch (level) {
            case LogLevel.Trace:
                Console.Write($"[{time}][T]");
                break;
            case LogLevel.Debug:
                Console.Write($"[{time}][D]");
                break;
            case LogLevel.Info:
                Console.Write($"[{time}][I]");
                break;
            case LogLevel.Warn:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"[{time}][W]");
                Console.ResetColor();
                break;
            case LogLevel.Error:
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"[{time}][E]");
                Console.ResetColor();
                break;
            case LogLevel.Fatal:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"[{time}][F]");
                Console.ResetColor();
                break;
            default:
                break;
        }
    }
        
    public override void Log(LogLevel level, string? message, Exception? exception = null) {
        if (level == LogLevel.None) {
            return;
        }
        LogHead(level);
        if (message != null) {
            Console.WriteLine(message);
        }
        if (exception != null) {
            Console.WriteLine(message);
        }
    }
        
    public override void Log(LogLevel level, string? message, LogColor color) {
        if (level == LogLevel.None) {
            return;
        }
        LogHead(level);
        Console.ForegroundColor = (ConsoleColor)color;
        Console.Write(message);
        Console.ResetColor();
    }
        
}