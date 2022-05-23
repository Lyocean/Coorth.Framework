using System;
using Coorth.Framework;

namespace Coorth.Worlds;

public static class LifetimeExtension {

    public static void Destroy(this in Entity entity) {
        entity.Set(new LifetimeComponent(0));
    }
        
    public static void Destroy(this in Entity entity, TimeSpan delay) {
        entity.Add(new LifetimeComponent(delay));
    }
        
    public static void Destroy(this in Entity entity, int delayFrame) {
        entity.Add(new LifetimeComponent(delayFrame));
    }
        
    public static void Destroy(this in Entity entity, Func<Entity, bool> condition) {
        entity.Add(new LifetimeComponent(condition));
    }
}