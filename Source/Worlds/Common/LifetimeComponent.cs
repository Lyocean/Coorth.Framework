using System;


namespace Coorth.Framework; 

[Flags]
public enum LifetimeMode { Countdown = 1, DelayFrame = 2, Condition = 4 }
    
public struct LifetimeComponent : IComponent {

    public readonly LifetimeMode Mode;

    public TimeSpan Duration;

    public int FrameCount;
        
    internal readonly Func<Entity, bool>? Condition;
        
    public LifetimeComponent(TimeSpan duration) {
        Mode = LifetimeMode.Countdown;
        Duration = duration;
        FrameCount = 0;
        Condition = null;
    }

    public LifetimeComponent(int frameCount) {
        Mode = LifetimeMode.DelayFrame;
        Duration = TimeSpan.Zero;
        FrameCount = frameCount;
        Condition = null;
    }

    public LifetimeComponent(Func<Entity, bool> condition) {
        Mode = LifetimeMode.Condition;
        Duration = TimeSpan.Zero;
        FrameCount = 0;
        Condition = condition;
    }
}

public static class LifetimeExtension {

    public static void DestroyDelay(this in Entity entity) {
        entity.Set(new LifetimeComponent(0));
    }

    public static void DestroyDelay(this in Entity entity, TimeSpan delay) {
        entity.Add(new LifetimeComponent(delay));
    }
        
    public static void DestroyDelay(this in Entity entity, int delayFrame) {
        entity.Add(new LifetimeComponent(delayFrame));
    }
        
    public static void DestroyDelay(this in Entity entity, Func<Entity, bool> condition) {
        entity.Add(new LifetimeComponent(condition));
    }
}