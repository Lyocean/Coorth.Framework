using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Coorth.Tasks {
    // public readonly struct MainThreadAwaitable<T> where T: IEvent {
    //     private readonly int mainThreadId;
    //     private readonly EventDispatcher dispatcher;
    //     private readonly CancellationToken cancellationToken;
    //     
    //     public MainThreadAwaitable(int mainThreadId, EventDispatcher dispatcher, CancellationToken cancellationToken) {
    //         this.mainThreadId = mainThreadId;
    //         this.dispatcher = dispatcher;
    //         this.cancellationToken = cancellationToken;
    //     }
    //     
    //     public Awaiter GetAwaiter() => new Awaiter(mainThreadId, dispatcher, cancellationToken);
    //     
    //     public readonly struct Awaiter : ICriticalNotifyCompletion {
    //         private readonly int mainThreadId;
    //         private readonly EventDispatcher dispatcher;
    //         private readonly CancellationToken cancellationToken;
    //
    //         public Awaiter(int mainThreadId, EventDispatcher dispatcher, CancellationToken cancellationToken) {
    //             this.mainThreadId = mainThreadId;
    //             this.dispatcher = dispatcher;
    //             this.cancellationToken = cancellationToken;
    //         }
    //         
    //         public bool IsCompleted => mainThreadId == Thread.CurrentThread.ManagedThreadId;
    //         public void GetResult() => cancellationToken.ThrowIfCancellationRequested();
    //         public void OnCompleted(Action continuation) {
    //             
    //         }
    //         public void UnsafeOnCompleted(Action continuation) {
    //             
    //         }
    //     }
    //     
    // }
    //
    //
    // public struct SwitchToMainThreadAwaitable
    // {
    //     readonly PlayerLoopTiming playerLoopTiming;
    //     readonly CancellationToken cancellationToken;
    //
    //     public SwitchToMainThreadAwaitable(PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken)
    //     {
    //         this.playerLoopTiming = playerLoopTiming;
    //         this.cancellationToken = cancellationToken;
    //     }
    //
    //     public Awaiter GetAwaiter() => new Awaiter(playerLoopTiming, cancellationToken);
    //
    //     public struct Awaiter : ICriticalNotifyCompletion
    //     {
    //         readonly PlayerLoopTiming playerLoopTiming;
    //         readonly CancellationToken cancellationToken;
    //
    //         public Awaiter(PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken)
    //         {
    //             this.playerLoopTiming = playerLoopTiming;
    //             this.cancellationToken = cancellationToken;
    //         }
    //
    //         public bool IsCompleted
    //         {
    //             get
    //             {
    //                 var currentThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
    //                 if (PlayerLoopHelper.MainThreadId == currentThreadId)
    //                 {
    //                     return true; // run immediate.
    //                 }
    //                 else
    //                 {
    //                     return false; // register continuation.
    //                 }
    //             }
    //         }
    //
    //         public void GetResult() { cancellationToken.ThrowIfCancellationRequested(); }
    //
    //         public void OnCompleted(Action continuation)
    //         {
    //             PlayerLoopHelper.AddContinuation(playerLoopTiming, continuation);
    //         }
    //
    //         public void UnsafeOnCompleted(Action continuation)
    //         {
    //             PlayerLoopHelper.AddContinuation(playerLoopTiming, continuation);
    //         }
    //     }
    // }
    //
    // public struct ReturnToMainThread
    // {
    //     readonly PlayerLoopTiming playerLoopTiming;
    //     readonly CancellationToken cancellationToken;
    //
    //     public ReturnToMainThread(PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken)
    //     {
    //         this.playerLoopTiming = playerLoopTiming;
    //         this.cancellationToken = cancellationToken;
    //     }
    //
    //     public Awaiter DisposeAsync()
    //     {
    //         return new Awaiter(playerLoopTiming, cancellationToken); // run immediate.
    //     }
    //
    //     public readonly struct Awaiter : ICriticalNotifyCompletion
    //     {
    //         readonly PlayerLoopTiming timing;
    //         readonly CancellationToken cancellationToken;
    //
    //         public Awaiter(PlayerLoopTiming timing, CancellationToken cancellationToken)
    //         {
    //             this.timing = timing;
    //             this.cancellationToken = cancellationToken;
    //         }
    //
    //         public Awaiter GetAwaiter() => this;
    //
    //         public bool IsCompleted => PlayerLoopHelper.MainThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId;
    //
    //         public void GetResult() { cancellationToken.ThrowIfCancellationRequested(); }
    //
    //         public void OnCompleted(Action continuation)
    //         {
    //             PlayerLoopHelper.AddContinuation(timing, continuation);
    //         }
    //
    //         public void UnsafeOnCompleted(Action continuation)
    //         {
    //             PlayerLoopHelper.AddContinuation(timing, continuation);
    //         }
    //     }
    // }

}