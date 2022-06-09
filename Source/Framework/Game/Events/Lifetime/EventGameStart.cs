namespace Coorth.Framework;

[Event]
public readonly struct EventGameStart {
        
    public readonly int ThreadId;
        
    public EventGameStart(int threadId) {
        ThreadId = threadId;
    }
}