using System;
using Coorth.Logs;

namespace Coorth {
    [Manager]
    public abstract class LogManager : Management, ILogManager {
        public abstract ILogger Create(string name);
    }
}