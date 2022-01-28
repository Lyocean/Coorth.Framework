using System;

namespace Coorth {
    public sealed partial class SystemSubscription<TEvent> {

        public SystemSubscription<TEvent> Match(EntityMatcher entityMatcher) {
            if (this.matcher != null) {
                throw new InvalidOperationException($"[SystemSubscription] Matcher has exists.");
            }
            this.matcher = entityMatcher;
            if (matcher.Includes != null) {
                foreach (var typeId in matcher.Includes) {
                    var componentGroup = Sandbox.GetComponentGroup(typeId);
                    _Include(componentGroup.Type);
                }
            }
            if (matcher.Excludes != null) {
                foreach (var typeId in matcher.Excludes) {
                    var componentGroup = Sandbox.GetComponentGroup(typeId);
                    _Exclude(componentGroup.Type);
                }
            }
            return this;
        }
        
        public void OnMatch(EntityMatcher entityMatcher, Action<TEvent, Entity> action) {
            Match(entityMatcher);
            OnEvent((in TEvent e) => {
                var entities = Sandbox.GetEntities(matcher);
                foreach (Entity entity in entities) {
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
                var entities = Sandbox.GetEntities(matcher);
                foreach (Entity entity in entities) {
                    action(in e, in entity);
                }
            });
        }
    }
}