using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Coorth.Logs;

namespace Coorth.Framework;

public interface IGameHost {
}

public interface IGameHost<THost, TApp, TSetting> : IGameHost {
}

public interface IGameSetting {
    TickSetting Tick { get; }
    AppSetting App { get; }
}

public abstract class GameHost<THost, TApp, TSetting> : Disposable, IGameHost<THost, TApp, TSetting> where THost : GameHost<THost, TApp, TSetting> where TApp : AppFrame where TSetting : IGameSetting {
    
    public readonly TApp App;

    public readonly TSetting Setting;

    protected TimeTicker Ticker;

    protected abstract ILogger Logger { get; }

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
                Logger.Error(task.Exception);
            }

            task.Wait();
        }
        catch (Exception e) {
            if (e.InnerException != null) {
                Logger.Error(e.InnerException);
                throw e.InnerException;
            }
            else {
                Logger.Error(e);
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
        App.Execute(new EventTickBefore(Ticker.TickTotalTime, deltaTickTime, Ticker.TotalTickFrameCount));

        //Step Update
        frameStepTime += Ticker.StepDeltaTime;
        for (var i = 0;
             i < Ticker.MaxStepPerFrame && frameStepTime >= TimeSpan.Zero;
             i++, frameStepTime -= Ticker.StepDeltaTime) {
            App.Execute(new EventStepUpdate(Ticker.StepTotalTime, Ticker.StepDeltaTime, Ticker.TotalStepFrameCount));
            Ticker.StepUpdate();
        }

        //Tick Update
        App.Execute(new EventTickUpdate(Ticker.TickTotalTime, deltaTickTime, Ticker.TotalTickFrameCount));

        //Late Update
        App.Execute(new EventLateUpdate(Ticker.TickTotalTime, deltaTickTime, Ticker.TotalTickFrameCount));
    }

    protected virtual void OnSetup() {
    }

    protected virtual void OnAfterSetup() {
    }

    protected Task OnLoad() {
        return Task.CompletedTask;
    }

    protected virtual void OnStartup() {
    }

    protected virtual void OnBeforeTick() {
    }

    protected virtual void OnTicking() {
    }

    protected virtual void OnAfterTick() {
    }

    protected virtual void OnShutdown() {
    }

    protected virtual void OnDispose() {
    }
}