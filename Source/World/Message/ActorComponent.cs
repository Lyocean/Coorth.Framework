using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth {
    public class RouterActor : Actor {
        
    }
    
    public class ActorComponent : Component {
        public RouterComponent Router { get; private set; }

        public ActorRef Remote { get; private set; }
        
        public RouterActor Local { get; private set; }
        
        public void Setup(ActorRef remote, RouterActor local) {
            this.Remote = remote;
            this.Local = local;
        }
        
        public void OnRegister(RouterComponent router) {
            this.Router = router;
            
        }
        
        public void UnRegister(RouterComponent router) {
            this.Router = null;
        }

        public void Send<T>(T message) {
            Remote.Send(message);
        }

        public Task<TResp> Request<TReq, TResp>(TReq request) {
            return Remote.Request<TReq, TResp>(request);
        }
        
        public Task<TResp> Request<TReq, TResp>(TReq request, CancellationToken cancellation) {
            return Remote.Request<TReq, TResp>(request, cancellation);
        }
        
        public Task<TResp> Request<TReq, TResp>(TReq request, TimeSpan timeout) {
            return Remote.Request<TReq, TResp>(request, timeout);
        }
    }



}