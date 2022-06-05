using Coorth.Framework;
using System;

namespace Coorth.Logs;

[Manager]
public interface ILogManager {

    void Register(Func<string, ILogger> provider);

    ILogger Create(string name);
}