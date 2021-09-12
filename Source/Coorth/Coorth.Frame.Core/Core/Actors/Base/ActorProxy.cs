using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth {
    public interface IActorProxy {
        T GetMethod<T>(ushort method);
        void Send<T>(T message);
        Task<TResp> Request<TReq, TResp>(TReq request);
        Task<TResp> Request<TReq, TResp>(TReq request, CancellationToken cancellation);
        Task<TResp> Request<TReq, TResp>(TReq request, TimeSpan timeout);
    }
    
    public abstract class ActorProxy : IActor {
        
        public readonly ActorRef Ref;
        
        protected ActorScheduler Scheduler => Ref.Container.Scheduler;

        public ActorProxy(ActorRef value) {
            this.Ref = value;
        }

        public void OnActive() {
        }

        public virtual Task ReceiveAsync(in ActorMail e) {
            Ref.Send(e.message, Ref);
            return Task.CompletedTask;
        }

        public void DeActive() {
        }
    }

    public abstract class ActorProxy<TActor> : ActorProxy {
        
        public TActor Actor;

        private static readonly Dictionary<ushort, MethodInfo> methods = new Dictionary<ushort, MethodInfo>();

        static ActorProxy() {
            foreach (var method in typeof(TActor).GetMethods()) {
                var rpcAttribute = method.GetCustomAttribute<RpcAttribute>();
                if (rpcAttribute == null) {
                    continue;
                }
                methods[rpcAttribute.Id] = method;
            }
        }

        private readonly Dictionary<ushort, Delegate> delegates = new Dictionary<ushort, Delegate>();

        protected ActorProxy(ActorRef value) : base(value) {
            // foreach (var pair in methods) {
            //     delegates[pair.Key] = Delegate.CreateDelegate(GetType(), pair.Value);
            // }
        }
        
        public override Task ReceiveAsync(in ActorMail e) {
            if (e.Message is IMessageRpcInvoke rpcInvoke) {
                if (methods.TryGetValue(rpcInvoke.Method, out var method)) {
                    
                }
            }

            Ref.Send(e.message, Ref);
            return Task.CompletedTask;
        }
    }
    
    
    
    public static class ActorProxyExtension {
        
        public static async Task RpcInvoke(this IActorProxy proxy, ushort method) {
            var message = new MessageRpcInvoke(method);
            await proxy.Request<MessageRpcInvoke, MessageRpcReturn>(message);
        }
        
        public static async Task<TReturn> RpcInvoke<TReturn>(this IActorProxy proxy, ushort method) {
            var message = new MessageRpcInvoke(method);
            var rpcReturn = await proxy.Request<MessageRpcInvoke, MessageRpcReturn<TReturn>>(message);
            return rpcReturn.Value;
        }
        
        public static async Task RpcInvoke<TP>(this IActorProxy proxy, ushort method, TP p) {
            var message = new MessageRpcInvoke<TP>(method, p);
            await proxy.Request<MessageRpcInvoke<TP>, MessageRpcReturn>(message);
        }
        
        public static async Task<TReturn> RpcInvoke<TP, TReturn>(this IActorProxy proxy, ushort method, TP p) {
            var message = new MessageRpcInvoke<TP>(method, p);
            var rpcReturn = await proxy.Request<MessageRpcInvoke<TP>, MessageRpcReturn<TReturn>>(message);
            return rpcReturn.Value;
        }
        
        public static async Task RpcInvoke<TP1, TP2>(this IActorProxy proxy, ushort method, TP1 p1, TP2 p2) {
            var message = new MessageRpcInvoke<TP1, TP2>(method, p1, p2);
            await proxy.Request<MessageRpcInvoke<TP1, TP2>, MessageRpcReturn>(message);
        }
        
        public static async Task<TReturn> RpcInvoke<TP1, TP2, TReturn>(this IActorProxy proxy, ushort method, TP1 p1, TP2 p2) {
            var message = new MessageRpcInvoke<TP1, TP2>(method, p1, p2);
            var rpcReturn = await proxy.Request<MessageRpcInvoke<TP1, TP2>, MessageRpcReturn<TReturn>>(message);
            return rpcReturn.Value;
        }
        
        public static async Task RpcInvoke<TP1, TP2, TP3>(this IActorProxy proxy, ushort method, TP1 p1, TP2 p2, TP3 p3) {
            var message = new MessageRpcInvoke<TP1, TP2, TP3>(method, p1, p2, p3);
            await proxy.Request<MessageRpcInvoke<TP1, TP2, TP3>, MessageRpcReturn>(message);
        }
        
        public static async Task<TReturn> RpcInvoke<TP1, TP2, TP3, TReturn>(this IActorProxy proxy, ushort method, TP1 p1, TP2 p2, TP3 p3) {
            var message = new MessageRpcInvoke<TP1, TP2, TP3>(method, p1, p2, p3);
            var rpcReturn = await proxy.Request<MessageRpcInvoke<TP1, TP2, TP3>, MessageRpcReturn<TReturn>>(message);
            return rpcReturn.Value;
        }
        
        public static async Task<MessageRpcReturn> OnInvoke(this IActorProxy proxy, MessageRpcInvoke invoke) {
            var method = proxy.GetMethod<Func<Task>>(invoke.Method);
            await method();
            return MessageRpcReturn.Default;
        }
        
        public static async Task<MessageRpcReturn<TReturn>> OnInvoke<TReturn>(this IActorProxy proxy, MessageRpcInvoke invoke) {
            var method = proxy.GetMethod<Func<Task<TReturn>>>(invoke.Method);
            var result = await method();
            return new MessageRpcReturn<TReturn>(result);
        }
        
        public static async Task<MessageRpcReturn> OnInvoke<TP>(this IActorProxy proxy, MessageRpcInvoke<TP> invoke) {
            var method = proxy.GetMethod<Func<TP, Task>>(invoke.Method);
            await method(invoke.Param);
            return MessageRpcReturn.Default;
        }

        public static async Task<MessageRpcReturn<TReturn>> OnInvoke<TP, TReturn>(this IActorProxy proxy, MessageRpcInvoke<TP> invoke) {
            var method = proxy.GetMethod<Func<TP, Task<TReturn>>>(invoke.Method);
            var result = await method(invoke.Param);
            return new MessageRpcReturn<TReturn>(result);
        }
        
        
        public static async Task<MessageRpcReturn> OnInvoke<TP1, TP2>(this IActorProxy proxy, MessageRpcInvoke<TP1, TP2> invoke) {
            var method = proxy.GetMethod<Func<TP1, TP2, Task>>(invoke.Method);
            await method(invoke.Param1, invoke.Param2);
            return MessageRpcReturn.Default;
        }

        public static async Task<MessageRpcReturn<TReturn>> OnInvoke<TP1, TP2, TReturn>(this IActorProxy proxy, MessageRpcInvoke<TP1, TP2> invoke) {
            var method = proxy.GetMethod<Func<TP1, TP2, Task<TReturn>>>(invoke.Method);
            var result = await method(invoke.Param1, invoke.Param2);
            return new MessageRpcReturn<TReturn>(result);
        }
        
        public static async Task<MessageRpcReturn> OnInvoke<TP1, TP2, TP3>(this IActorProxy proxy, MessageRpcInvoke<TP1, TP2, TP3> invoke) {
            var method = proxy.GetMethod<Func<TP1, TP2, TP3, Task>>(invoke.Method);
            await method(invoke.Param1, invoke.Param2, invoke.Param3);
            return MessageRpcReturn.Default;
        }

        public static async Task<MessageRpcReturn<TReturn>> OnInvoke<TP1, TP2, TP3, TReturn>(this IActorProxy proxy, MessageRpcInvoke<TP1, TP2, TP3> invoke) {
            var method = proxy.GetMethod<Func<TP1, TP2, TP3, Task<TReturn>>>(invoke.Method);
            var result = await method(invoke.Param1, invoke.Param2, invoke.Param3);
            return new MessageRpcReturn<TReturn>(result);
        }
    }
}