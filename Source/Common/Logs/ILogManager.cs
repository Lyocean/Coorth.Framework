using Coorth.Framework;

namespace Coorth.Logs; 

[Manager]
public interface ILogManager {
    ILogger Create(string name);
}