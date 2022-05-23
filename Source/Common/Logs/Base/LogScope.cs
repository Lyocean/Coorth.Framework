using System;

namespace Coorth.Logs;

public readonly struct LogScope : IDisposable {
        
    private readonly Logger logger;

    public LogScope(Logger logger) {
        this.logger = logger;
    }
        
    public void Dispose() {
        logger?.EndScope();
    }
}