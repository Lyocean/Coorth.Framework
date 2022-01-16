using System;

namespace Coorth.Logs {
    public class ConsoleLogger : Logger {
        public override void Log(in LogData data) {
            var time = data.Time;
            var message = data.Content;
            switch (data.Level) {
                case LogLevel.Debug:
                    Console.WriteLine($"[{time}][Debug]{message}");
                    break;
                case LogLevel.Info:
                    Console.WriteLine($"[{time}][Debug]{message}");
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"[{time}][Warning]: ");
                    Console.ResetColor();
                    Console.WriteLine(message);
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write($"[{time}][Error]: ");
                    Console.ResetColor();
                    Console.WriteLine(message);
                    break;
                case LogLevel.Exception:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"[{time}][Exception]: ");
                    Console.ResetColor();
                    Console.WriteLine(data.Exception);
                    break;
            }
        }
    }
}