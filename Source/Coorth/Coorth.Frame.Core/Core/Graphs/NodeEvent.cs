using System;

namespace Coorth {
    
    public readonly struct EventNodeTick {

        public readonly TimeSpan DeltaTime;
        public float DeltaSecond => (float)DeltaTime.TotalSeconds;
        
        public readonly TimeSpan TotalTime;
        public float TotalSecond => (float)TotalTime.TotalSeconds;

        public readonly TimeSpan LocalTime;
        public float LocalSecond => (float)LocalTime.TotalSeconds;

        public TimeSpan LastTime => TotalTime + DeltaTime;
        
        public EventNodeTick(TimeSpan deltaTime, TimeSpan totalTime, TimeSpan localTime) {
            this.DeltaTime = deltaTime;
            this.TotalTime = totalTime;
            this.LocalTime = localTime;
        }

        public EventNodeTick ToLocal(TimeSpan localTime) {
            return new EventNodeTick(DeltaTime, TotalTime, localTime);
        }
    }
    
    public readonly struct EventNodeExit {
        
    }
    

}