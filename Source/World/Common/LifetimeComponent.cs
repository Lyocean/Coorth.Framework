using System;

namespace Coorth.Common {
    
    [Flags]
    public enum LifetimeMode { Countdown = 1, DelayFrame = 2, Condition = 4 }
    
    public struct LifetimeComponent : IComponent<TimeSpan>, IComponent<int>, IComponent<Func<Entity, bool>> {

        public LifetimeMode Mode;

        public TimeSpan Duration;

        public int FrameCount;
        
        internal Func<Entity, bool> Condition;

        public void OnSetup(TimeSpan duration) {
            Mode = LifetimeMode.Countdown;
            Duration = duration;
        }

        public void OnSetup(int frameCount) {
            Mode = LifetimeMode.DelayFrame;
            FrameCount = frameCount;
        }

        public void OnSetup(Func<Entity, bool> condition) {
            this.Mode = LifetimeMode.Condition;
            this.Condition = condition;
        }
    }

    public static class LifetimeExtension {

        public static void Destroy(this in Entity entity) {
            entity.Add<LifetimeComponent, int>(0);
        }
        
        public static void Destroy(this in Entity entity, TimeSpan delay) {
            entity.Add<LifetimeComponent, TimeSpan>(delay);
        }
        
        public static void Destroy(this in Entity entity, int delayFrame) {
            entity.Add<LifetimeComponent, int>(delayFrame);
        }
        
        public static void Destroy(this in Entity entity, Func<Entity, bool> condition) {
            entity.Add<LifetimeComponent, Func<Entity, bool>>(condition);
        }
    }
}