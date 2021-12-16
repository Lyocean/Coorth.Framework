using System;

namespace Coorth.Logs {
    public class ConsoleLogger : Logger {
        
        public override void LogDebug(string message) {
            var time = DateTime.Now;
            Console.WriteLine($"[{time}][Debug]{message}");
        }

        
        public override void LogWarning(string message) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            var time = DateTime.Now;
            Console.Write($"[{time}][Warning]: ");
            Console.ResetColor();
            Console.WriteLine(message);
        }

        public override void LogError(string message) {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            var time = DateTime.Now;
            Console.Write($"[{time}][Error]: ");
            Console.ResetColor();
            Console.WriteLine(message);
        }

        public override void LogException<T>(T e = null) {
            if (e == null) {
                e = Activator.CreateInstance<T>();
            }

            Console.ForegroundColor = ConsoleColor.Red;
            var time = DateTime.Now;
            Console.Write($"[{time}][Exception]: ");
            Console.ResetColor();
            Console.WriteLine(e);
        }
    }
}