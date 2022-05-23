using Coorth.Framework;

namespace Coorth.Logs; 

[Manager]
public abstract class LogManager : Manager, ILogManager {
    public abstract ILogger Create(string name);
}