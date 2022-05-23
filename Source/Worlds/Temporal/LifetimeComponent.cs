using System;
using Coorth.Framework;

namespace Coorth.Worlds; 

[Flags]
public enum LifetimeMode { Countdown = 1, DelayFrame = 2, Condition = 4 }
    
public struct LifetimeComponent : IComponent {

    public readonly LifetimeMode Mode;

    public TimeSpan Duration;

    public int FrameCount;
        
    internal Func<Entity, bool>? Condition;
        
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