using System;

namespace Coorth {
    [Event]
    public readonly struct EventAppBeginInit {
        
    }

    [Event]
    public readonly struct EventAppEndInit {
        
    }
    
    [Event]
    public readonly struct EventAppStartup {
        public readonly int ThreadId;

        public EventAppStartup(int threadId) {
            this.ThreadId = threadId;
        }
    }
    
    [Event]
    public readonly struct EventBeforeStep : ITimeEvent, ITickEvent {
        public readonly TimeSpan TotalTime;

        public readonly TimeSpan DeltaTime;
        
        public readonly long FrameCount;
        
        public DateTime CurrentTime => DateTime.MinValue + TotalTime;
        
        public float DeltaSecond => (float)DeltaTime.TotalSeconds;
        
        public float TotalSecond => (float)TotalTime.TotalSeconds;

        public EventBeforeStep(TimeSpan totalTime, TimeSpan deltaTime, long frameCount) {
            this.TotalTime = totalTime;
            this.DeltaTime = deltaTime;
            this.FrameCount = frameCount;
        }
        
        public TimeSpan GetDeltaTime() => DeltaTime;
        public TimeSpan GetTotalTime() => TotalTime;
    }
    
    [Event]
    public readonly struct EventStepUpdate : ITimeEvent, ITickEvent {

        public readonly TimeSpan TotalTime;
        
        public readonly TimeSpan DeltaTime;
        
        public readonly long FrameCount;
        
        public float DeltaSecond => (float)DeltaTime.TotalSeconds;

        public float TotalSecond => (float)TotalTime.TotalSeconds;

        public EventStepUpdate(TimeSpan totalTime, TimeSpan deltaTime, long frameCount) {
            this.TotalTime = totalTime;
            this.DeltaTime = deltaTime;
            this.FrameCount = frameCount;
        }

        public TimeSpan GetDeltaTime() => DeltaTime;
        public TimeSpan GetTotalTime() => TotalTime;

    }
    
    [Event]
    public readonly struct EventBeforeTick : ITimeEvent, ITickEvent {
        
        public readonly TimeSpan TotalTime;

        public readonly TimeSpan DeltaTime;
        
        public readonly long FrameCount;
        
        public DateTime CurrentTime => DateTime.MinValue + TotalTime;
        
        public float DeltaSecond => (float)DeltaTime.TotalSeconds;
        
        public float TotalSecond => (float)TotalTime.TotalSeconds;

        public EventBeforeTick(TimeSpan totalTime, TimeSpan deltaTime, long frameCount) {
            this.TotalTime = totalTime;
            this.DeltaTime = deltaTime;
            this.FrameCount = frameCount;
        }
        
        public TimeSpan GetDeltaTime() => DeltaTime;
        public TimeSpan GetTotalTime() => TotalTime;
    }

    [Event]
    public readonly struct EventTickUpdate : ITimeEvent, ITickEvent {
        
        public readonly TimeSpan TotalTime;

        public readonly TimeSpan DeltaTime;
        
        public readonly long FrameCount;
        
        public DateTime CurrentTime => DateTime.MinValue + TotalTime;
        
        public float DeltaSecond => (float)DeltaTime.TotalSeconds;
        
        public float TotalSecond => (float)TotalTime.TotalSeconds;

        public EventTickUpdate(TimeSpan totalTime, TimeSpan deltaTime, long frameCount) {
            this.TotalTime = totalTime;
            this.DeltaTime = deltaTime;
            this.FrameCount = frameCount;
        }
        
        public TimeSpan GetDeltaTime() => DeltaTime;
        public TimeSpan GetTotalTime() => TotalTime;

    }
    
    [Event]
    public readonly struct EventLateUpdate : ITimeEvent, ITickEvent {

        public readonly TimeSpan TotalTime;

        public readonly TimeSpan DeltaTime;
        
        public readonly long FrameCount;
        
        public float DeltaSecond => (float)DeltaTime.TotalSeconds;

        public float TotalSecond => (float)TotalTime.TotalSeconds;

        public EventLateUpdate(TimeSpan totalTime, TimeSpan deltaTime, long frameCount) {
            this.TotalTime = totalTime;
            this.DeltaTime = deltaTime;
            this.FrameCount = frameCount;
        }

        public TimeSpan GetDeltaTime() => DeltaTime;
        public TimeSpan GetTotalTime() => TotalTime;
    }

    [Event]
    public readonly struct EventEndOfFrame {
        
        public readonly TimeSpan TotalTime;

        public readonly TimeSpan DeltaTime;
        
        public readonly long FrameCount;

        public float DeltaSecond => (float)DeltaTime.TotalSeconds;
        
        public float TotalSecond => (float)TotalTime.TotalSeconds;

        public EventEndOfFrame(TimeSpan totalTime, TimeSpan deltaTime, long frameCount) {
            this.TotalTime = totalTime;
            this.DeltaTime = deltaTime;
            this.FrameCount = frameCount;
        }
    }
    
    [Event]
    public readonly struct EventAppShutdown {
        
    }

    [Event]
    public readonly struct EventAppStatus {
        public readonly bool IsPause;

        public EventAppStatus(bool isPause) {
            this.IsPause = isPause;
        }
    }
    
    [Event]
    public readonly struct EventAppQuit {
        
    }
}