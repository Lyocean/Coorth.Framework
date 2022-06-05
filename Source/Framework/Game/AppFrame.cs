using System;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Logs;

namespace Coorth.Framework;

public abstract partial class AppFrame : Disposable {

    public readonly int Id;
        
    /// <summary> App唯一Id </summary>
    public readonly Guid Guid;

    /// <summary> App名称 </summary>
    public readonly string Name;
    
    public readonly AppContext Context;

    public readonly Dispatcher Dispatcher;

    public readonly ServiceLocator Services;

    public readonly ActorsRuntime Actors;

    private bool isSetup;
        
    private bool isRunning;

    /// <summary> App是否正在运行中 </summary>
    public bool IsRunning => isRunning && !IsDisposed;

    /// <summary> App主线程Id </summary>
    public int MainThreadId => Context.MainThread.ManagedThreadId;

    private ILogger Logger { get; }

    public event Action? OnTicking;
    
    protected AppFrame(AppOptions options) {
        Guid = Guid.NewGuid();
        Id = options.Id;
        Name = !string.IsNullOrWhiteSpace(options.Name) ? options.Name : "App-Default";
        Logger = options.Logger;

        Context = new AppContext(Thread.CurrentThread, Logger);
        Services = options.Services ?? Infra.Services.CreateChild();
        Dispatcher = options.Dispatcher ?? Dispatcher.Root;

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
        Dispatcher.Dispatch(new EventStartup(MainThreadId));
    }

    protected virtual void OnStartup() {
    }

    public void TickLoop() {
        if (IsDisposed || !isSetup) {
            return;
        }
        OnTicking?.Invoke();
        Context.Synchronization.Invoke();
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
        Logger.Error(nameof(Shutdown));
        Dispatcher.Dispatch(new EventShutdown());
        OnShutdown();
        root.SetActive(false);
        isRunning = false;
    }

    protected virtual void OnShutdown() { }

    protected override void OnDispose(bool dispose) {
        Shutdown();
        OnDestroy();
        Apps.RemoveApp(this);
    }

    protected virtual void OnDestroy() { }
}