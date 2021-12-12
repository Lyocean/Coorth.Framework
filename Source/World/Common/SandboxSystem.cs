using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Coorth.Common {
    [System, StoreContract("32B3C700-5454-4C27-83A1-B6B94C6B386D")]
    public class SandboxSystem : SystemBase {
        protected override void OnAdd() {
            Sandbox.BindComponent<SandboxComponent>();
            Subscribe<EventSandboxStartup>().OnEvent(Execute);
            Subscribe<EventSandboxRunTick>().OnEvent(Execute);
            Subscribe<EventSandboxTicking>().OnEvent(Execute);
        }

        protected override void OnActive() {
            var component = Singleton<SandboxComponent>();
            component.IsRunning = true;
        }

        protected override void OnDeActive() {
            var component = Singleton<SandboxComponent>();
            component.IsRunning = false;
        }

        private void Execute(EventSandboxStartup e) {
            var component = Singleton<SandboxComponent>();
            component.Ticker.Setup(e.Setting);
            Sandbox.Context.Startup(Thread.CurrentThread);
            component.IsRunning = true;
        }
        
        private void Execute(EventSandboxRunTick e) {
            var component = Singleton<SandboxComponent>();
            component.Ticker.Setup(e.Setting);
            var thread = new Thread(Run);
            Sandbox.Context.Startup(thread);
            component.CompletionSource.Task.ContinueWith(_ => {
                e.Completion.SetResult(Sandbox);
            });
            thread.Start();
        }
        
        private void Execute(EventSandboxTicking e) {
            var component = Singleton<SandboxComponent>();
            if (Sandbox.IsDisposed || !component.IsRunning) {
                return;
            }
            TickLoop(ref component.Ticker, ref component.Ticker.RemainingStepTime, e.DeltaTickTime);
        }

        private void Run() {
            var component = Singleton<SandboxComponent>();
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
        public readonly ITickSetting Setting;
        
        public EventSandboxStartup(ITickSetting setting) {
            this.Setting = setting;
        }
    }

    [Event]
    public readonly struct EventSandboxRunTick : IEvent {
        public readonly ITickSetting Setting;
        public readonly TaskCompletionSource<Sandbox> Completion;

        public EventSandboxRunTick(ITickSetting setting, TaskCompletionSource<Sandbox> completion) {
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
}