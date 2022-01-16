using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Coorth {
    public interface IGameBase {
        
    }
    
    public abstract class GameBase<TGame, TApp> : Disposable, IGameBase where TGame : GameBase<TGame, TApp> where TApp : AppFrame {
        
        public TApp App { get; private set; }

        protected TimeTicker Ticker;

        protected abstract TApp CreateApp();

        public void Setup(ITickSetting setting) {
            Ticker.Setup(setting);
            OnSetup();

            App = CreateApp();
            App.Setup();
        }

        protected abstract void OnSetup();


        protected override void OnDispose(bool dispose) {
            if (App == null) {
                return;
            }
            App.Dispose();
            App = null;
        }
        
        public async Task RunAsync() {
            if (App.IsDisposed) {
                return;
            }
            await App.Load();
            if (App.IsDisposed) {
                return;
            }
            App.Init();
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
            //Before Tick
            App.Execute(new EventBeforeTick(Ticker.TickTotalTime, deltaTickTime, Ticker.TotalTickFrameCount));
            App.Execute(new EventBeforeTick(Ticker.TickTotalTime, deltaTickTime, Ticker.TotalTickFrameCount));

            //Step Update
            frameStepTime += Ticker.StepDeltaTime;
            for (var i = 0; i < Ticker.MaxStepPerFrame && frameStepTime >= TimeSpan.Zero; i++, frameStepTime -= Ticker.StepDeltaTime) {
                App.Execute(new EventStepUpdate(Ticker.StepTotalTime, Ticker.StepDeltaTime, Ticker.TotalStepFrameCount));
                Ticker.StepUpdate();
            }
        
            //Tick Update
            Infra.Execute(new EventTickUpdate(Ticker.TickTotalTime, deltaTickTime, Ticker.TotalTickFrameCount));
            App.Execute(new EventTickUpdate(Ticker.TickTotalTime, deltaTickTime, Ticker.TotalTickFrameCount));
            
            //Late Update
            Infra.Execute(new EventTickUpdate(Ticker.TickTotalTime, deltaTickTime, Ticker.TotalTickFrameCount));
            App.Execute(new EventLateUpdate(Ticker.TickTotalTime, deltaTickTime, Ticker.TotalTickFrameCount));
            
            //End Of Frame
            Infra.Execute(new EventEndOfFrame(Ticker.TickTotalTime, deltaTickTime, Ticker.TotalTickFrameCount));
            App.Execute(new EventEndOfFrame(Ticker.TickTotalTime, deltaTickTime, Ticker.TotalTickFrameCount));

            Ticker.TickUpdate(deltaTickTime);
        }
        
    }
}
