namespace Coorth.Framework;

[Event]
public readonly struct EventStartup {
        
    public readonly int ThreadId;
        
    public EventStartup(int threadId) {
        this.ThreadId = threadId;
    }
}