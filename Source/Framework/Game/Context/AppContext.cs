using System.Threading;
using Coorth.Logs;

namespace Coorth.Framework; 

public class AppContext {
        
    public readonly AppTime Time = new();

    public readonly AppSynchronization Synchronization;

    public readonly Thread MainThread;

    public AppContext(Thread thread, ILogger logger) {
        MainThread = thread;
        Synchronization = new AppSynchronization(thread, logger);
        SynchronizationContext.SetSynchronizationContext(Synchronization);
    }
}