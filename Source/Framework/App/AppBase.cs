using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Logs;
using Coorth.Platforms;
using Coorth.Tasks;
using Coorth.Tasks.Ticking;

namespace Coorth.Framework;

public interface IApplication {
    int Id { get; }
    
    string Name { get; }
    
    Dispatcher Dispatcher { get; }
    
    ServiceLocator Services { get; }
    
    void Quit(int code);
}

public abstract class AppBase : Disposable, IApplication, IServiceCollection {

    public int Id { get; }

    public string Name { get; }

    public Dispatcher Dispatcher { get; }

    public ServiceLocator Services { get; }

    protected readonly ActorsRuntime Actors;

    public readonly TaskSyncContext SyncContext;
    
    private readonly ModuleRoot root;

    private readonly Dictionary<Type, ModuleBase> modules = new();

    public readonly ServiceLocator Modules = new();

    private readonly List<IAppFeature> features = new();

    private bool isSetup;
        
    private bool isRunning;
    
    public bool IsRunning => isRunning && !IsDisposed;

    protected bool isLoaded;
    
    protected ILogger Logger { get; }
    
    protected AppBase(AppBuilder builder) {
        Id = builder.Id;
        Name = !string.IsNullOrWhiteSpace(builder.Name) ? builder.Name : "App-Default";
        Logger = builder.Logger;

        Services = builder.Services;
        Dispatcher = builder.Dispatcher;
        SyncContext = new TaskSyncContext(Thread.CurrentThread);
        SynchronizationContext.SetSynchronizationContext(SyncContext);

        Actors = new ActorsRuntime(Dispatcher, Services, Logger);
        root = new ModuleRoot(this, Services, Dispatcher, Actors);
        root.SetActive(false);

        features.AddRange(builder.Features);
        
        foreach (var feature in features) {
            try {
                feature.Install(this);
            }
            catch (Exception e) {
                Logger.Exception(LogLevel.Error, e);
            }
        }
    }
    
    public AppBase Setup() {
        Logger.Trace(nameof(Setup));
        OnSetup();
        foreach (var feature in features) {
            feature.OnSetup(this);
        }
        isSetup = true;
        
        return this;
    }

    protected virtual void OnSetup() {
    }

    public AppBase Load() {
        isLoaded = false;
        Logger.Trace(nameof(Load));
        OnLoad().ContinueWith(_=>isLoaded = true).Forget();
        while (!isLoaded) {
            Thread.Yield();
            SyncContext.Execute();
        }
        return this;
    }

    protected virtual Task OnLoad() {
        var tasks = new Task[features.Count];
        for (var i = 0; i < features.Count; i++) {
            tasks[i] = features[i].LoadAsync(this);
        }
        return Task.WhenAll(tasks);
    }

    public void Init() {
        Logger.Trace(nameof(Init));
        Dispatcher.Dispatch(new ApplicationInitBeginEvent());
        OnInit();
        Dispatcher.Dispatch(new ApplicationInitAfterEvent());
    }

    protected virtual void OnInit() {
    }

    public void Start() {
        if (IsRunning) {
            return;
        }
        Logger.Trace(nameof(Start));

        isRunning = true;
        root.SetActive(true);
        OnStartup();
        Dispatcher.Dispatch(new ApplicationStartEvent(this));
    }

    protected virtual void OnStartup() { }

    public virtual void OnTickLoop() { }

    public void Run() {
        if (IsDisposed) {
            Logger.Error("Game has been disposed.");
            return;
        }
        
        try {
            Setup();
            Load();
            Init();
            Start();
        }
        catch (Exception e) {
            Logger.Exception(LogLevel.Debug, e);
        }
        var setting = new TickSetting();
        
        var platform = Services.Get<IPlatformManager>();
        var ticking = new TickingTask(platform, setting);
        ticking.OnTicking += OnTickLoop;
        
        try {
            ticking.RunLoop(SyncContext, Dispatcher);
        }
        catch (Exception e) {
            Logger.Error(e);
        }

        try {
            Close();
        }
        catch (Exception e) {
            Logger.Exception(LogLevel.Error, e);
        }
    }

    public void Execute<T>(in T e) where T: notnull {
        if (IsDisposed || !isSetup) {
            return;
        }
        OnExecute(in e);
        Dispatcher.Dispatch(e);
    }

    protected virtual void OnExecute<T>(in T e) { }

    public void Close() {
        if (!IsRunning) {
            return;
        }
        Logger.Trace(nameof(Close));
        Dispatcher.Dispatch(new ApplicationCloseEvent());
        OnClose();
        root.SetActive(false);

        isRunning = false;
        OnDestroy();
        foreach (var key in modules.Keys.ToArray()) {
            Remove(key);
        }
        SyncContext.Cancel();
        
    }

    protected virtual void OnClose() { }

    protected override void OnDispose(bool dispose) {
        Close();
    }

    protected virtual void OnDestroy() { }
    
    public void Quit(int code) {
        SyncContext.Cancel();
    }
 
    internal void OnAddModule(Type key, ModuleBase module) {
        Logger.Trace($"AddModule:{key.Name} - {module.GetType().Name}");
        modules[key] = module;
        Dispatcher.Dispatch(new ModuleAddEvent(key, module));
    }
    
    internal void OnRemoveModule(Type key, ModuleBase module) {
        if (!modules.Remove(key)) {
            return;
        }
        Dispatcher.Dispatch(new ModuleRemoveEvent(key, module));
    }
    
    public TModule Bind<TModule>(TModule module) where TModule : IModule {
        root.AddChild(typeof(TModule), (ModuleBase)(object)module);
        return module;
    }
    
    public TKey Bind<TKey, TModule>(Func<AppBase, TModule> func)  where TModule : ModuleBase, TKey where TKey : IModule {
        var module = func(this);
        return Bind<TKey, TModule>(module);
    }
        
    public TModule Bind<TModule>() where TModule : ModuleBase, new() {
        var module = new TModule();
        return Bind<TModule, TModule>(module);
    }
    
    public TModule Bind<TKey, TModule>(TModule module) where TModule : ModuleBase, TKey where TKey : IModule {
        root.AddChild(typeof(TKey), module);
        return module;
    }

    public TKey Get<TKey>() where TKey : IModule {
        return modules.TryGetValue(typeof(TKey), out var module) ? (TKey)(object)module : throw new KeyNotFoundException();
    }
    
    public TKey? Find<TKey>() where TKey : IModule {
        return modules.TryGetValue(typeof(TKey), out var module) ? (TKey)(object)module : default;
    }
    
    public ModuleBase Get(Type key) {
        return modules.TryGetValue(key, out var module) ? module : throw new KeyNotFoundException();
    }
    
    public bool Remove(Type key) {
        if (!modules.TryGetValue(key, out var module)) {
            return false;
        }
        module.Dispose();
        modules.Remove(key);
        return true;
    }
    
    public bool Remove<TKey>() where TKey : IModule {
        return Remove(typeof(TKey));
    }

    public object GetService(Type serviceType) {
        if (modules.TryGetValue(serviceType, out var moduleBase)) {
            return moduleBase;
        }
        return Services.Get(serviceType);
    }

    public T GetService<T>() where T : class {
        return (T)GetService(typeof(T));
    }
    
    public object? FindService(Type serviceType) {
        if (modules.TryGetValue(serviceType, out var moduleBase)) {
            return moduleBase;
        }
        return Services.Find(serviceType);
    }
    
    public T? FindService<T>() where T : class {
        return GetService(typeof(T)) as T;
    }
}