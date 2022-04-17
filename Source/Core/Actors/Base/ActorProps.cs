using System;
using System.Threading.Tasks;

namespace Coorth {
    public class ActorProps {

        private Type type;

        private object[] arguments;

        private Func<ActorMail, Task> action;

        private Func<Actor> provider;

        public static ActorProps None => new ActorProps();

        public static ActorProps From(Type type, params object[] args) {
            var props = new ActorProps {type = type, arguments = args };
            return props;
        }
        
        public static ActorProps From(Func<ActorMail, Task> func) {
            var props = new ActorProps {action = func};
            return props;
        }

        public static ActorProps From(Func<Actor> func) {
            var props = new ActorProps {provider = func};
            return props;
        }
        
        internal Actor CreateActor() {
            if (provider != null) {
                return provider();
            }
            if (action != null) {
                var actor = new ActionActor(action);
                return actor;
            }
            if (type !=null) {
                if (arguments != null) {
                    return (Actor)Activator.CreateInstance(type, arguments);
                }
                else {
                    return (Actor)Activator.CreateInstance(type);
                }
            }
            throw new NotImplementedException();
        }
    }
}
