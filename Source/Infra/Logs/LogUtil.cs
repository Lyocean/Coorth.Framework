using System;
using System.Diagnostics;

namespace Coorth {
    using Coorth.Logs;
    public static class LogUtil {
        private static ILogger logger = new ConsoleLogger();

        internal static void Bind(ILogger value) {
            logger = value;
        }

        [Conditional("DEBUG")]
        public static void Debug(string message) {
            logger.LogDebug(message);
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
}