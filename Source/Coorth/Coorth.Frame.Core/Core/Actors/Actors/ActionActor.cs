using System;
using System.Threading.Tasks;

namespace Coorth {
    public class ActionActor : Actor {
        private readonly Func<ActorMail, Task> action;
        public ActionActor(Func<ActorMail, Task> func) {
            this.action = func;
        }

        public override Task ExecuteAsync(in ActorMail e) {
            return action.Invoke(e);
        }
    }
}