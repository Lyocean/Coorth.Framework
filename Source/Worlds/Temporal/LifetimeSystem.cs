using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Coorth.Framework;

namespace Coorth.Worlds; 

[System, DataContract, Guid("FB93ADDC-FF8B-4686-A079-50188F7EB575")]
public class LifetimeSystem : SystemBase {
        
    private readonly List<Entity> entities = new();

    protected override void OnAdd() {
        Sandbox.BindComponent<LifetimeComponent>();
        Subscribe<EventEndOfFrame>().OnEvent(Execute);
    }

    private void Execute(in EventEndOfFrame e) {
        var collection = Sandbox.GetComponents<LifetimeComponent>();
        foreach (var (entity, component) in collection) {
            Execute(in e, in entity, component);
        }
        foreach (var target in entities) {
            target.Dispose();
        }
        entities.Clear();
    }
        
    private void Execute(in EventEndOfFrame e, in Entity entity, LifetimeComponent lifetime) {
        switch (lifetime.Mode) {
            case LifetimeMode.Countdown:
                if (lifetime.Duration > e.DeltaTime) {
                    lifetime.Duration -= e.DeltaTime;
                }
                else {
                    entities.Add(entity);
                }
                break;
            case LifetimeMode.DelayFrame:
                if (lifetime.FrameCount > 0) {
                    lifetime.FrameCount--;
                }
                else {
                    entities.Add(entity);
                }
                break;
            case LifetimeMode.Condition:
                if (lifetime.Condition == null || lifetime.Condition(entity)) {
                    entities.Add(entity);
                }
                break;
            default:
                throw new InvalidEnumArgumentException();
        }
        entity.Set(lifetime);
    }
}