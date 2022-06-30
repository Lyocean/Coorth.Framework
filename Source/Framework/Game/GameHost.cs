using System;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Logs;
using Coorth.Platforms;
using Coorth.Tasks;
using Coorth.Tasks.Ticking;

namespace Coorth.Framework;

public interface IGameHost {
}

public interface IGameSetting {
    TickSetting Tick { get; }
    AppSetting App { get; }
}

public abstract class GameHost<TApp, TSetting> : Disposable, IGameHost where TApp : AppFrame where TSetting : IGameSetting {
    
    public readonly TApp App;

    public readonly TSetting Setting;
    
    protected abstract ILogger Logger { get; }
    
    private readonly ScheduleContext schedule;
    
    public bool IsRunning { get; private set; }

    private bool IsStarting { get; set; }

    protected GameHost(TApp app, TSetting setting) {
        App = app;
        Setting = setting;
        schedule = new ScheduleContext(Thread.CurrentThread);
        SynchronizationContext.SetSynchronizationContext(schedule);
    }
    
    public async Task Run(TickSetting setting) {
        if (App.IsDisposed) {
            Logger.Error("Game has been disposed.");
            return;
        }

        Thread.CurrentThread.Name = "[Main thread]";
        await GameStart();

        var platform = Infra.Get<IPlatformManager>();
        var ticking = new TickingTask(platform, setting);
        ticking.OnTicking += () => App.TickLoop();
        
        try {
            ticking.RunLoop(schedule, Dispatcher.Root);
        }
        catch (Exception e) {
            Logger.Error(e);
        }

        GameClose();
    }

    protected async ValueTask GameStart() {
        Logger.Trace("Game start begin.");
        if (IsDisposed) {
            Logger.Error("Game has been disposed.");
            return;
        }
        if (IsRunning || IsStarting) {
            Logger.Error("Game has been started.");
            return;
        }
        IsStarting = true;

        try {
            OnSetup();
            App.Setup();
            
            await OnLoad();
            await App.Load();
            
            if (IsDisposed || App.IsDisposed) {
                return;
            }
            
            OnInit();
            App.Init();
            
            OnStartup();
            App.Startup();

            IsStarting = false;
            IsRunning = true;
            
            Logger.Trace("Game start end.");
        }
        catch (Exception e) {
            Logger.Exception(LogLevel.Error, e);
        }
    }
    
    
    
    protected void GameClose() {
        if (IsDisposed || !IsRunning) {
            return;
        }
        Logger.Debug("Game close begin", LogColor.DarkYellow);
        try {
            OnShutdown();
            App.Shutdown();
        }
        catch (Exception e) {
            Logger.Exception(LogLevel.Error, e);
        }
        Logger.Debug("Game close end", LogColor.DarkYellow);
    }
    
    protected virtual void OnSetup() { }

    protected Task OnLoad() => Task.CompletedTask;

    protected virtual void OnInit() { }
    
    protected virtual void OnStartup() { }
    
    protected virtual void OnShutdown() { }

    protected sealed override void OnDispose(bool dispose) {
        schedule.Cancel();
        if (IsRunning) {
            GameClose();
        }
        App.Dispose();
    }
}