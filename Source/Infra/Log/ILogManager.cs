using Coorth.Logs;

namespace Coorth {
    [Manager]
    public interface ILogManager {
        ILogger Create(string name);
    }
}