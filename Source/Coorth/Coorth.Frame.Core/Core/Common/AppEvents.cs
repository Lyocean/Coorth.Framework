using System;

namespace Coorth {

    public interface IAppEvent: IEvent {
        
    }
    
    public readonly struct EventBeginInit : IAppEvent {
        
    }

    public readonly struct EventEndInit : IAppEvent {

    }

    public readonly struct EventStepUpdate : IAppEvent {
        
        public readonly TimeSpan DeltaTime;
        
        public readonly long FrameCount;
        
        public float DeltaSecond => (float)DeltaTime.TotalSeconds;

        public EventStepUpdate(TimeSpan deltaTime, long frameCount) {
            this.DeltaTime = deltaTime;
            this.FrameCount = frameCount;
        }
    }

    public readonly struct EventTickUpdate : IAppEvent {
        
        public readonly TimeSpan DeltaTime;
        
        public readonly long FrameCount;
        
        public float DeltaSecond => (float)DeltaTime.TotalSeconds;
        
        public EventTickUpdate(TimeSpan deltaTime, long frameCount) {
            this.DeltaTime = deltaTime;
            this.FrameCount = frameCount;
        }
    }

    public readonly struct EventLateUpdate : IAppEvent {
        
        public readonly TimeSpan DeltaTime;
        
        public readonly long FrameCount;
        
        public float DeltaSecond => (float)DeltaTime.TotalSeconds;
        // public readonly TimeSpan TotalTime;

        public EventLateUpdate(TimeSpan deltaTime, long frameCount) {
            this.DeltaTime = deltaTime;
            this.FrameCount = frameCount;
        }
    }

    public struct EventDestroy : IAppEvent {

    }
}