using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Framework;
using Coorth.Tasks.Ticking;

namespace Coorth.Worlds; 

[System, DataContract, Guid("32B3C700-5454-4C27-83A1-B6B94C6B386D")]
public class DirectorSystem : SystemBase {

    private CancellationTokenSource? cancellationTokenSource;
    
    protected override void OnAdd() {
        Sandbox.BindComponent<DirectorComponent>();
        Subscribe<EventSandboxStartup>().OnEvent(OnStartup);
        Subscribe<EventSandboxRunTick>().OnEvent(OnRunTick);
        Subscribe<EventSandboxTicking>().OnEvent(OnTicking);
    }

    protected override void OnRemove() {
        cancellationTokenSource?.Cancel();
    }

    protected override void OnActive() {
        var component = Singleton<DirectorComponent>();
        component.IsRunning = true;
    }

    protected override void OnDeActive() {
        var component = Singleton<DirectorComponent>();
        component.IsRunning = false;
    }

    private void OnStartup(EventSandboxStartup e) {
        var component = Singleton<DirectorComponent>();
        // component.Ticker.Setup(e.Setting);
        // Sandbox.Context.Startup(Thread.CurrentThread);
        component.IsRunning = true;
    }
    
    private void OnRunTick(EventSandboxRunTick e) {
        cancellationTokenSource = new();
        var component = Singleton<DirectorComponent>();
        var ticking = new TaskTicking(Sandbox.Dispatcher, e.Setting, cancellationTokenSource.Token);
        var thread = ticking.RunInThread(cancellationTokenSource.Token);
        ticking.OnComplete += () => e.Completion.SetResult(Sandbox);
    }
    
    private void OnTicking(EventSandboxTicking e) { }

}

[Event]
public readonly record struct EventSandboxStartup(TickSetting Setting) : IEvent {
    public readonly TickSetting Setting = Setting;
}

[Event]
public readonly record struct EventSandboxRunTick(TickSetting Setting, TaskCompletionSource<Sandbox> Completion) : IEvent {
    public readonly TickSetting Setting = Setting;
    public readonly TaskCompletionSource<Sandbox> Completion = Completion;
}

[Event]
public readonly record struct EventSandboxTicking(TimeSpan DeltaTickTime) : IEvent {
    public readonly TimeSpan DeltaTickTime = DeltaTickTime;
}