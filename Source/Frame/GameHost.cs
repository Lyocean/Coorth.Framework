using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Coorth {
    public interface IGameHost { }

    public interface IGameHost<THost, TApp, TSetting> : IGameHost { }

    public interface IGameSetting {
        TickSetting Tick { get; }
    }
    
    public abstract class GameHost<THost, TApp, TSetting> : Disposable, IGameHost<THost, TApp, TSetting>
                                    where THost : GameHost<THost, TApp, TSetting>
                                    where TApp  : AppFrame
                                    where TSetting : IGameSetting {

        public readonly TApp App;

        public readonly TSetting Setting;

        protected TimeTicker Ticker;

        protected GameHost(TApp app, TSetting setting) {
            App = app;
            Setting = setting;
            Ticker = new TimeTicker(setting.Tick);
        }
        
        protected sealed override void OnDispose(bool dispose) {
            App.Dispose();
            OnDispose();
        }

        private async Task Load() {
            await OnLoad();
            await App.Load();
        }
        
        public void Run() {
            if (App.IsDisposed) {
                return;
            }

            OnSetup();
            App.Setup();
            OnAfterSetup();
            
            try {
                var task = Load();
                if (task.IsFaulted) {
                    LogUtil.Exception(task.Exception);
                }
                task.Wait();
            }
            catch (Exception e) {
                if (e.InnerException != null) {
                    LogUtil.Exception(e.InnerException);
                    throw e.InnerException;
                }
                else {
                    LogUtil.Exception(e);
                    throw;
                }
            }
            if (App.IsDisposed) {
                return;
            }
            App.Init();
            App.Startup();
            OnStartup();
            
            var lastTime = new DateTime(Stopwatch.GetTimestamp());
            var frameStepTime = TimeSpan.Zero;
            
            while (!App.IsDisposed && App.IsRunning) {

                var currentTime = new DateTime(Stopwatch.GetTimestamp());
                var deltaTickTime = currentTime - lastTime;
                
                OnBeforeTick();
                OnTicking();
                
                TickLoop(lastTime, ref frameStepTime, deltaTickTime);
                
                OnAfterTick();
                
                //End Of Frame
                Infra.Execute(new EventEndOfFrame(Ticker.TickTotalTime, deltaTickTime, Ticker.TotalTickFrameCount));
                App.Execute(new EventEndOfFrame(Ticker.TickTotalTime, deltaTickTime, Ticker.TotalTickFrameCount));
                Ticker.TickUpdate(deltaTickTime);
                
                var remainingTime = Ticker.TickDeltaTime - deltaTickTime;
                Ticker.WaitTime(remainingTime);

                lastTime = currentTime;
            }
            App.Shutdown();
        }

        protected virtual void TickLoop(in DateTime lastTime, ref TimeSpan frameStepTime, in TimeSpan deltaTickTime) {
            //Before Tick
            Infra.Execute(new EventBeforeTick(Ticker.TickTotalTime, deltaTickTime, Ticker.TotalTickFrameCount));
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
        }
        
        protected virtual void OnSetup() { }

        protected virtual void OnAfterSetup() { }

        protected Task OnLoad() { return Task.CompletedTask; }
        
        protected virtual void OnStartup() { }

        protected virtual void OnBeforeTick() { }
        protected virtual void OnTicking() { }
        
        protected virtual void OnAfterTick() { }

        protected virtual void OnShutdown() { }

        protected virtual void OnDispose() { }
    }
}
