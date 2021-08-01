using System;
using System.Threading.Tasks;

namespace Coorth {
    public class ActorProps {

        private readonly object[] arguments;

        private Func<ActorMail, Task> action;

        private Func<IActor> provider;

        public static ActorProps None => new ActorProps();

        public static ActorProps From(Func<ActorMail, Task> func) {
            var props = new ActorProps {action = func};
            return props;
        }

        public static ActorProps From(Func<IActor> func) {
            var props = new ActorProps {provider = func};
            return props;
        }
        
        internal IActor CreateActor() {
            if (provider != null) {
                return provider();
            }else if (action != null) {
                var actor = new ActionActor(action);
                return actor;
            }
            else {
                throw new NotImplementedException();
            }
        }
    }
}
