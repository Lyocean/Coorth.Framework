using System;
using System.Diagnostics;
using Coorth.Maths;

namespace Coorth.Logs {
    public readonly struct LogData {
        
        public readonly DateTime Time;
        
        public readonly LogLevel Level;
        
        public readonly StackTrace Stack;
        
        public readonly object Module;
        
        public readonly string Content;

        public readonly Color Color;

        public readonly Exception Exception;

        public LogData(DateTime time, LogLevel level, object module, string content, StackTrace stack, Color labelColor) {
            Time = time;
            Level = level;
            Module = module;
            Content = content;
            Stack = stack;
            Color = labelColor;
            Exception = null;
        }
        
        public LogData(DateTime time, object module, Exception exception, Color labelColor) {
            Time = time;
            Level = LogLevel.Exception;
            Module = module;
            Content = null;
            Stack = null;
            Color = labelColor;
            Exception = exception;
        }
    }
}