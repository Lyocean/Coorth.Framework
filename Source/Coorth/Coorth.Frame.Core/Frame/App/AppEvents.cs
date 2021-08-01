using System;

namespace Coorth {

    public readonly struct EventAppStartup : IAppEvent {
        public readonly int ThreadId;

        public EventAppStartup(int threadId) {
            this.ThreadId = threadId;
        }
    }

    public readonly struct EventAppShutdown : IAppEvent {
        
    }

    public readonly struct EventAppBeginInit : IAppEvent {
        
    }

    public readonly struct EventAppEndInit : IAppEvent {
        
    }

    public readonly struct EventAppPause : IAppEvent {
        public readonly bool IsPause;

        public EventAppPause(bool isPause) {
            this.IsPause = isPause;
        }
    }

    public readonly struct EventStepUpdate : ITimeEvent, ITickEvent {

        public readonly TimeSpan TotalTime;
        
        public readonly TimeSpan DeltaTime;
        
        public readonly long FrameCount;
        
        public float DeltaSecond => (float)DeltaTime.TotalSeconds;

        public EventStepUpdate(TimeSpan totalTime, TimeSpan deltaTime, long frameCount) {
            this.TotalTime = totalTime;
            this.DeltaTime = deltaTime;
            this.FrameCount = frameCount;
        }

        public TimeSpan GetDeltaTime() => DeltaTime;
        public TimeSpan GetTotalTime() => TotalTime;

    }

    public readonly struct EventTickUpdate : ITimeEvent, ITickEvent {
        
        public readonly TimeSpan TotalTime;

        public readonly TimeSpan DeltaTime;
        
        public readonly long FrameCount;
        
        public float DeltaSecond => (float)DeltaTime.TotalSeconds;
        
        public EventTickUpdate(TimeSpan totalTime, TimeSpan deltaTime, long frameCount) {
            this.TotalTime = totalTime;
            this.DeltaTime = deltaTime;
            this.FrameCount = frameCount;
        }
        
        public TimeSpan GetDeltaTime() => DeltaTime;
        public TimeSpan GetTotalTime() => TotalTime;

    }

    public readonly struct EventLateUpdate : ITimeEvent, ITickEvent {

        public readonly TimeSpan TotalTime;

        public readonly TimeSpan DeltaTime;
        
        public readonly long FrameCount;
        
        public float DeltaSecond => (float)DeltaTime.TotalSeconds;

        public EventLateUpdate(TimeSpan totalTime, TimeSpan deltaTime, long frameCount) {
            this.TotalTime = totalTime;
            this.DeltaTime = deltaTime;
            this.FrameCount = frameCount;
        }

        public TimeSpan GetDeltaTime() => DeltaTime;
        public TimeSpan GetTotalTime() => TotalTime;
    }
}