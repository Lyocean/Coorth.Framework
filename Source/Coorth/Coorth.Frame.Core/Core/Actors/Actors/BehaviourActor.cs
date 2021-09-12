using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Coorth {
    public class BehaviourActor<TBehaviour> : Actor {

        public TBehaviour Behaviour;

        private MessageDispatcher<ActorMail> dispatcher = new MessageDispatcher<ActorMail>();

        private Dictionary<ushort, MethodInfo> methods = new Dictionary<ushort, MethodInfo>();

        public void Receive<TMsg>(Action<TMsg> action) where TMsg: IMessage {
            dispatcher.Receive<TMsg>((mail, message) => {
                action(message);
            });
        }
        
        public void Receive<TMsg>(Func<TMsg, Task> action) where TMsg: IMessage {
            dispatcher.Receive<TMsg>((mail, message) => {
                action(message);
            });
        }
        
        public void Receive<TReq, TResp>(Func<TReq, Task<TResp>> action) where TReq: IRequest {
            async void Action(ActorMail mail, TReq request) {
                var result = await action(request);
                mail.Response(result);
            }
            dispatcher.Receive<TReq>(Action);
        }
        
        protected void Receive<TReq, TResp>(Func<TReq, TResp> action) where TReq: IRequest {
            dispatcher.Receive<TReq>((mail, request) => {
                var result = action(request);
                mail.Response(result);
            });
        }
        
        public override Task ReceiveAsync(in ActorMail mail) {
            if (mail.Message is IMessageRpcInvoke rpcInvoke) {
                if (methods.TryGetValue(rpcInvoke.Method, out var method)) {
                    
                }
            }
            else {
                dispatcher.Execute(mail, mail.Message);
            }
            return base.ReceiveAsync(mail);
        }
    }
}