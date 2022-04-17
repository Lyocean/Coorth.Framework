using System;
using System.Diagnostics;
using Coorth.Logs;

namespace Coorth {
    public static class LogUtil {
        
        private static ILogger logger = new ConsoleLogger();
        
        private static Func<string, ILogger> factory = _ => new ConsoleLogger();

        public static void Bind(ILogger value, Func<string, ILogger> func) {
            logger = value;
            factory = func;
        }

        [Conditional("DEBUG")]
        public static void Debug(string message) => logger.Debug(message);

        [Conditional("DEBUG")]
        public static void Debug(string module, string message) => logger.Debug(module, message);

        [Conditional("DEBUG")]
        public static void Debug(string module, string message, LogColor color) => logger.Debug(module, message, color);

        public static void Info(string message) => logger.Info(message);
        public static void Info(string module, string message) => logger.Info(module, message);

        public static void Info(string module, string message, LogColor color) => logger.Info(module, message, color);

        public static void Warning(string message) => logger.Warn(message);

        public static void Error(string message) => logger.Error(message);

        public static void Exception<T>(T? e = null) where T : Exception => logger.Error(e);
    }

    public enum LogColor {
        Black,
        DarkBlue,
        DarkGreen,
        DarkCyan,
        DarkRed,
        DarkMagenta,
        DarkYellow,
        Gray,
        DarkGray,
        Blue,
        Green,
        Cyan,
        Red,
        Magenta,
        Yellow,
        White,
    }
}