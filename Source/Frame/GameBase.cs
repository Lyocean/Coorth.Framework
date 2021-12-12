using System;
using System.Diagnostics;

namespace Coorth {
    public abstract class GameBase<TGame, TApp> : Disposable where TGame : GameBase<TGame, TApp> where TApp : AppFrame {
        
        public TApp App { get; private set; }

        protected TimeTicker Ticker;

        protected GameBase() {}

        protected abstract TApp CreateApp();
    
        public void Setup(ITickSetting setting) {
            App.Setup();
            Ticker.Setup(setting);
        }

        public void Init() {
            App.BeginInit();
            App.EndInit();
        }

        public void Run() {
            if (App.IsDisposed) {
                return;
            }

            App.Startup();

            DateTime lastTime = new DateTime(Stopwatch.GetTimestamp());
            TimeSpan frameStepTime = TimeSpan.Zero;

            while (!App.IsDisposed && App.IsRunning) {

                var currentTime = new DateTime(Stopwatch.GetTimestamp());
                var deltaTickTime = currentTime - lastTime;

                TickLoop(lastTime, ref frameStepTime, deltaTickTime);


                var remainingTime = Ticker.TickDeltaTime - deltaTickTime;
                Ticker.WaitTime(remainingTime);

                lastTime = currentTime;
            }

            App.Shutdown();
        }

        protected virtual void TickLoop(in DateTime lastTime, ref TimeSpan frameStepTime, in TimeSpan deltaTickTime) {
            //Step Update
            frameStepTime += Ticker.StepDeltaTime;
            for (var i = 0; i < Ticker.MaxStepPerFrame && frameStepTime >= TimeSpan.Zero; i++, frameStepTime -= Ticker.StepDeltaTime) {
                App.Execute(new EventStepUpdate(Ticker.StepTotalTime, Ticker.StepDeltaTime, Ticker.TotalStepFrameCount));
                Ticker.StepUpdate();
            }

            //Tick Update
            App.Execute(new EventTickUpdate(Ticker.TickTotalTime, deltaTickTime, Ticker.TotalTickFrameCount));
            App.Execute(new EventLateUpdate(Ticker.TickTotalTime, deltaTickTime, Ticker.TotalTickFrameCount));
            Ticker.TickUpdate(deltaTickTime);

        }

        protected override void OnDispose(bool dispose) {
            App.Dispose();
            App = default;
        }
    }



}
