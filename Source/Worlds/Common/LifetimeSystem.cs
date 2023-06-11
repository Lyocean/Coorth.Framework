using System.Collections.Generic;
using System.ComponentModel;


namespace Coorth.Framework; 

[System, Guid("FB93ADDC-FF8B-4686-A079-50188F7EB575")]
public class LifetimeSystem : SystemBase {
        
    private readonly List<Entity> entities = new();

    protected override void OnAdd() {
        Subscribe<EndOfFrameEvent>().OnEvent(Execute);
    }

    private void Execute(in EndOfFrameEvent e) {
        var collection = World.GetComponents<LifetimeComponent>();
        foreach (var components in collection.GetIter()) {
            Execute(in e, in components.Entity, ref components.Value0);
        }
        foreach (var target in entities) {
            target.Dispose();
        }
        entities.Clear();
    }
        
    private void Execute(in EndOfFrameEvent e, in Entity entity, ref LifetimeComponent lifetime) {
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