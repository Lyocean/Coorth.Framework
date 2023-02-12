using System;


namespace Coorth.Framework; 

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