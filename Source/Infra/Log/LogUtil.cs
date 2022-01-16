using System;
using System.Diagnostics;
using Coorth.Logs;
using Coorth.Maths;

namespace Coorth {
    public static class LogUtil {
        private static ILogger logger = new ConsoleLogger();

        internal static void Bind(ILogger value) {
            logger = value;
        }

        [Conditional("DEBUG")]
        public static void Debug(string message) {
            logger.LogDebug(message);
        }
        
        [Conditional("DEBUG")]
        public static void Debug(string module, string message) {
            logger.LogDebug(module, message, LogColors.White);
        }
        
        [Conditional("DEBUG")]
        public static void Debug(string module, string message, Color color) {
            logger.LogDebug(module, message, color);
        }

        public static void Info(string module, string message) {
            logger.LogInfo(module, message, LogColors.White);
        }
        
        public static void Info(string module, string message, Color color) {
            logger.LogInfo(module, message, color);
        }
        
        public static void Warning(string message) {
            logger.LogWarning(message);
        }
        
        public static void Error(string message) {
            logger.LogError(message);
        }
        
        public static void Exception<T>(T e = null) where T : Exception {
            logger.LogException(e);
        }
    }
    
    public static class LogColors {
        // public static Color DarkGreen = new Color(0, 1f, 0, 0.5f);

        public static Color White = new Color(1f, 1f, 1f, 0.8f);

        public static Color Black = new Color(0, 0, 0, 0.7f);
        
        // public static Color LightGreen = new Color(0, 1f, 0, 0.5f);

        public static Color Yellow = new Color(1f, 1f, 0, 0.7f);
        
        public static Color Orange = new Color(1, 1f, 0.5f, 0.8f);
        
        public static Color Red = new Color(1, 0, 0, 0.7f);

        public static Color Green = new Color(0, 1f, 0, 0.5f);

        public static Color Blue = new Color(0, 0, 1, 0.8f);
    }
}