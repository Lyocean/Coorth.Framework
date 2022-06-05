using Coorth.Framework;
using System;

namespace Coorth.Logs; 

[Manager]
public class LogManager : Manager, ILogManager {

    private Func<string, ILogger>? provider;

    public void Register(Func<string, ILogger> provider) {
        this.provider = provider;
        LogUtil.Bind(provider.Invoke("[Default]"), provider);
    }

    public virtual ILogger Create(string name) => provider?.Invoke(name) ?? throw new NullReferenceException();
}