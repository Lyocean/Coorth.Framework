using System;

namespace Coorth.Framework; 

public sealed partial class SystemSubscription<TEvent> {
    private void _Match(EntityMatcher entityMatcher) {
        if (matcher != null) {
            throw new InvalidOperationException($"[SystemSubscription] Matcher has exists.");
        }
        matcher = entityMatcher;
        foreach (var typeId in matcher.Includes) {
            var componentGroup = Sandbox.GetComponentGroup(typeId);
            _Include(componentGroup.Type);
        }
        foreach (var typeId in matcher.Excludes) { 
            var componentGroup = Sandbox.GetComponentGroup(typeId);
            _Exclude(componentGroup.Type);
        }
    }
        
    public void OnMatch(EntityMatcher entityMatcher, Action<TEvent, Entity> action) {
        _Match(entityMatcher);
        OnEvent(e => {
            var entities = Sandbox.GetEntities(entityMatcher);
            entities.Execute(e, executor, action);
        });
    }
}