using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Coorth {
    public readonly partial struct SystemSubscription<TEvent> where TEvent : IEvent {
        
        private readonly SystemBase system;
        
        internal SystemSubscription(SystemBase system) {
            this.system = system;
        }

        public void OnEvent(Action<TEvent> action) {
            var reaction = system.CreateReaction<TEvent>();
            reaction.OnEvent((in TEvent e) => action(e));
        }
        
        public void OnEvent(EventAction<TEvent> action) {
            var reaction = system.CreateReaction<TEvent>();
            reaction.OnEvent(action);
        }
        
        public void OnEvent(Func<TEvent, ValueTask> action, bool continueOnCapturedContext = true) {
            var reaction = system.CreateReaction<TEvent>();
            reaction.OnEvent((in TEvent e) => action(e).ConfigureAwait(continueOnCapturedContext));
        }
        
        public void OnEvent(EventFunc<TEvent, ValueTask> action, bool continueOnCapturedContext = true) {
            var reaction = system.CreateReaction<TEvent>();
            reaction.OnEvent((in TEvent e) => action(in e).ConfigureAwait(continueOnCapturedContext));
        }

        public void OnMatch(EntityMatcher matcher, Action<TEvent, Entity> action) {
            var sandbox = system.Sandbox;
            var reaction = system.CreateReaction<TEvent>();
            if (matcher.Includes != null) {
                foreach (var typeId in matcher.Includes) {
                    var componentGroup = sandbox.GetComponentGroup(typeId);
                    reaction.Include(componentGroup.Type);
                }
            }
            if (matcher.Excludes != null) {
                foreach (var typeId in matcher.Excludes) {
                    var componentGroup = sandbox.GetComponentGroup(typeId);
                    reaction.Exclude(componentGroup.Type);
                }
            }

            reaction.OnEvent((in TEvent e) => {
                var entities = sandbox.GetEntities(matcher);
                foreach (Entity entity in entities) {
                    action(e, entity);
                }
            });
        }
    }
}