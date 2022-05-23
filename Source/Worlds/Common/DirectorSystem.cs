using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Framework;

namespace Coorth.Worlds; 

[System, DataContract, Guid("32B3C700-5454-4C27-83A1-B6B94C6B386D")]
public class DirectorSystem : SystemBase {
    protected override void OnAdd() {
        Sandbox.BindComponent<DirectorComponent>();
        Subscribe<EventSandboxStartup>().OnEvent(OnStartup);
        Subscribe<EventSandboxRunTick>().OnEvent(OnRunTick);
        Subscribe<EventSandboxTicking>().OnEvent(OnTicking);
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
        component.Ticker.Setup(e.Setting);
        // Sandbox.Context.Startup(Thread.CurrentThread);
        component.IsRunning = true;
    }
        
    private void OnRunTick(EventSandboxRunTick e) {
        var component = Singleton<DirectorComponent>();
        component.Ticker.Setup(e.Setting);
        var thread = new Thread(Run);
        // Sandbox.Context.Startup(thread);
        component.CompletionSource.Task.ContinueWith(_ => {
            e.Completion.SetResult(Sandbox);
        });
        thread.Start();
    }
        
    private void OnTicking(EventSandboxTicking e) {
        var component = Singleton<DirectorComponent>();
        if (Sandbox.IsDisposed || !component.IsRunning) {
            return;
        }
        TickLoop(ref component.Ticker, ref component.Ticker.RemainingStepTime, e.DeltaTickTime);
    }

    private void Run() {
        var component = Singleton<DirectorComponent>();
        if (Sandbox.IsDisposed || !component.IsRunning) {
            return;
        }
        component.IsRunning = true;
            
        var lastTime = new DateTime(Stopwatch.GetTimestamp());
        ref var frameStepTime = ref component.Ticker.RemainingStepTime;
        while (!Sandbox.IsDisposed && component.IsRunning) {
            var currentTime = new DateTime(Stopwatch.GetTimestamp());
            var deltaTickTime = currentTime - lastTime;
            TickLoop(ref component.Ticker, ref frameStepTime, deltaTickTime);
            var remainingTime = component.Ticker.TickDeltaTime - deltaTickTime;
            component.Ticker.WaitTime(remainingTime);
            lastTime = currentTime;
        }
    }

    private void TickLoop(ref TimeTicker ticker, ref TimeSpan frameStepTime, in TimeSpan deltaTickTime) {
        //Step Update
        frameStepTime += ticker.StepDeltaTime;
        for (var i = 0; i < ticker.MaxStepPerFrame && frameStepTime >= TimeSpan.Zero; i++, frameStepTime -= ticker.StepDeltaTime) {
            Sandbox.Execute(new EventStepUpdate(ticker.StepTotalTime, ticker.StepDeltaTime, ticker.TotalStepFrameCount));
            ticker.StepUpdate();
        }

        //Tick Update
        Sandbox.Execute(new EventTickUpdate(ticker.TickTotalTime, deltaTickTime, ticker.TotalTickFrameCount));
        Sandbox.Execute(new EventLateUpdate(ticker.TickTotalTime, deltaTickTime, ticker.TotalTickFrameCount));
        ticker.TickUpdate(deltaTickTime);
    }
}

[Event]
public readonly struct EventSandboxStartup : IEvent {
        
    public readonly TickSetting Setting;
        
    public EventSandboxStartup(TickSetting setting) {
        this.Setting = setting;
    }
}

[Event]
public readonly struct EventSandboxRunTick : IEvent {
    public readonly TickSetting Setting;
    public readonly TaskCompletionSource<Sandbox> Completion;

    public EventSandboxRunTick(TickSetting setting, TaskCompletionSource<Sandbox> completion) {
        this.Setting = setting;
        this.Completion = completion;
    }
}

[Event]
public readonly struct EventSandboxTicking : IEvent {
    public readonly TimeSpan DeltaTickTime;

    public EventSandboxTicking(TimeSpan deltaTickTime) {
        DeltaTickTime = deltaTickTime;
    }
}