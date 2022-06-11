using System;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Logs;
using Coorth.Platforms;
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

    private readonly CancellationTokenSource cancellationTokenSource = new();

    protected GameHost(TApp app, TSetting setting) {
        App = app;
        Setting = setting;
    }

    protected sealed override void OnDispose(bool dispose) {
        App.Dispose();
        OnDispose();
    }

    public async Task Run(TickSetting setting) {
        Thread.CurrentThread.Name = "[Main thread]";

        if (App.IsDisposed) {
            return;
        }

        OnSetup();
        App.Setup();

        await OnLoad();
        await App.Load();
        
        if (App.IsDisposed) {
            return;
        }

        OnInit();
        App.Init();
        
        OnStartup();
        App.Startup();

        var platform = Infra.Get<IPlatformManager>();
        var ticking = new TickingTask(platform, Dispatcher.Root, setting, cancellationTokenSource.Token);
        ticking.OnTicking += () => App.TickLoop();
        
        try {
            ticking.RunLoop();
        }
        catch (Exception e) {
            Logger.Error(e);
        }
        
        OnShutdown();
        App.Shutdown();
    }
    
    protected virtual void OnSetup() { }

    protected Task OnLoad() => Task.CompletedTask;

    protected virtual void OnInit() { }
    
    protected virtual void OnStartup() { }
    
    protected virtual void OnShutdown() { }

    protected virtual void OnDispose() {
        cancellationTokenSource.Cancel();
        App.Dispose();
    }
}