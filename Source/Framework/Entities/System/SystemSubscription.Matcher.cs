using System;

namespace Coorth.Framework; 

public sealed partial class SystemSubscription<TEvent> {

    public SystemSubscription<TEvent> Match(EntityMatcher entityMatcher) {
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
        return this;
    }
        
    public void OnMatch(EntityMatcher entityMatcher, Action<TEvent, Entity> action) {
        Match(entityMatcher);
        OnEvent((in TEvent e) => {
            var entities = Sandbox.GetEntities(entityMatcher);
            foreach (var entity in entities) {
                action(e, entity);
            }
        });
    }

    public void OnMatch(EventAction<TEvent, Entity> action) {
        if (this.matcher == null) {
            throw new InvalidOperationException($"[SystemSubscription] Matcher has not exists.");
        }
        OnEvent((in TEvent e) => {
            var entities = Sandbox.GetEntities(matcher);
            foreach (Entity entity in entities) {
                action(in e, in entity);
            }
        });
    }
        
    public void OnMatch(EntityMatcher entityMatcher, EventAction<TEvent, Entity> action) {
        Match(entityMatcher);
        OnEvent((in TEvent e) => {
            var entities = Sandbox.GetEntities(entityMatcher);
            foreach (Entity entity in entities) {
                action(in e, in entity);
            }
        });
    }
}