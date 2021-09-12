using System;
using System.Threading.Tasks;

namespace Coorth {
    public class ActorScheduler {
        public static readonly ActorScheduler Default = new ActorScheduler();

        public bool IsLocalSchedule => false;
        
        public TResp Schedule<TReq, TResp>(Func<TReq, TResp> func, TReq req) {
            return func(req);
        }
        
        public void Schedule(IActor actor, in ActorMail m) {
            actor.ReceiveAsync(m);
        }

        public void Schedule(LocalContext context) {
            context.Execute();
        }
    }
}