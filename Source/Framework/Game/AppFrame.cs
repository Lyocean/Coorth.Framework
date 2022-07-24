using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Logs;
using Coorth.Tasks;

namespace Coorth.Framework;

public abstract partial class AppFrame : Disposable {

    public readonly int Id;
        
    /// <summary> App唯一Id </summary>
    public readonly Guid Guid;

    /// <summary> App名称 </summary>
    public readonly string Name;

    public readonly Dispatcher Dispatcher;

    public readonly ServiceLocator Services;

    public readonly ActorsRuntime Actors;

    public readonly TaskSyncContext Schedule;
    
    private bool isSetup;
        
    private bool isRunning;

    /// <summary> App是否正在运行中 </summary>
    public bool IsRunning => isRunning && !IsDisposed;

    private ILogger Logger { get; }

    public event Action? OnTicking;
    
    protected AppFrame(AppOptions options) {
        
        Guid = Guid.NewGuid();
        Id = options.Id;
        Name = !string.IsNullOrWhiteSpace(options.Name) ? options.Name : "App-Default";
        Logger = options.Logger;

        Services = options.Services ?? Infra.Services.CreateChild();
        Dispatcher = options.Dispatcher ?? Dispatcher.Root;
        Schedule = options.Schedule ?? new TaskSyncContext(Thread.CurrentThread);

        Actors = new ActorsRuntime(Dispatcher);
        root = InitModules();

        Apps.AddApp(this);
    }

    public AppFrame Setup() {
        Logger.Trace(nameof(Setup));
        OnSetup();
        isSetup = true;
        return this;
    }

    protected virtual void OnSetup() {
    }

    public Task Load() {
        Logger.Trace(nameof(Load));
        return OnLoad();
    }

    protected virtual Task OnLoad() {
        return Task.CompletedTask;
    }

    public void Init() {
        Logger.Trace(nameof(Init));
        Dispatcher.Dispatch(new EventInitBegin());
        OnInit();
        Dispatcher.Dispatch(new EventInitAfter());
    }

    protected virtual void OnInit() {
    }

    public void Startup() {
        if (IsRunning) {
            return;
        }
        Logger.Trace(nameof(Startup));

        isRunning = true;
        root.SetActive(true);
        OnStartup();
        Dispatcher.Dispatch(new EventGameStart(Schedule));
    }

    protected virtual void OnStartup() {
    }

    public void TickLoop() {
        if (IsDisposed || !isSetup) {
            return;
        }
        OnTicking?.Invoke();
    }

    public void Execute<T>(in T e) where T: notnull {
        if (IsDisposed || !isSetup) {
            return;
        }
        OnExecute(in e);
        Dispatcher.Dispatch(e);
    }

    protected virtual void OnExecute<T>(in T e) { }

    public void Shutdown() {
        if (!IsRunning) {
            return;
        }
        Logger.Trace(nameof(Shutdown));
        Dispatcher.Dispatch(new EventGameClose());
        OnShutdown();
        root.SetActive(false);
        isRunning = false;
    }

    protected virtual void OnShutdown() { }

    protected override void OnDispose(bool dispose) {
        Shutdown();
        OnDestroy();
        foreach (var key in modules.Keys.ToArray()) {
            Remove(key);
        }
        Apps.RemoveApp(this);
    }

    protected virtual void OnDestroy() { }
}