using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

    #region Fields & Properties

    public int Id { get; }

    public AppKey Key { get; }

    public string Name { get; }

    public readonly TaskSyncContext SyncContext;

    public Dispatcher Dispatcher { get; }

    public ServiceLocator Services { get; }

    public readonly ActorsRuntime Actors;
    public readonly ActorLocalDomain Domain;
    
    private readonly ModuleRoot rootModule;

    private readonly Dictionary<Type, ModuleBase> modules = new();

    public readonly ServiceLocator Modules = new();

    private readonly List<IAppFeature> features = new();

    private bool isSetup;
        
    private bool isRunning;
    
    public bool IsRunning => isRunning && !IsDisposed;

    protected bool isLoaded;
    
    protected ILogger Logger { get; }

    public Action<TimeSpan>? OnTicking;
    
    #endregion
    
    #region Liftime

    protected AppBase(AppBuilder builder) {
        //Common
        Id = builder.Id;
        Key = builder.GetKey();
        Name = !string.IsNullOrWhiteSpace(builder.Name) ? builder.Name : "App-Default";
        Logger = builder.Logger;
        //Features
        features.AddRange(builder.Features);
        //Services
        Services = builder.Services;
        //Dispatcher & Scheduler
        Dispatcher = builder.Dispatcher;
        SyncContext = new TaskSyncContext(Thread.CurrentThread);
        SynchronizationContext.SetSynchronizationContext(SyncContext);
        //Actors & Modules
        Actors = new ActorsRuntime(Dispatcher, Services, Logger);
        rootModule = new ModuleRoot(this, Services, Dispatcher, Actors);
        Domain = Actors.CreateDomain("App", rootModule);
        rootModule.SetActive(false);

        foreach (var feature in features) {
            try {
                feature.Install(this);
            }
            catch (Exception e) {
                Logger.Exception(LogLevel.Error, e);
            }
        }
        Applications.Add(this);
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
        Dispatcher.Dispatch(new AppInitBeginEvent());
        OnInit();
        Dispatcher.Dispatch(new AppInitAfterEvent());
    }

    protected virtual void OnInit() {
    }

    public void Start() {
        if (IsRunning) {
            return;
        }
        Logger.Trace(nameof(Start));

        isRunning = true;
        rootModule.SetActive(true);
        OnStartup();
        Dispatcher.Dispatch(new AppStartEvent(this));
    }

    protected virtual void OnStartup() { }

    public virtual void OnTickLoop(TimeSpan delta_time) { }

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
        ticking.OnTicking += OnTicking;
        
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

    public void RunInThread() {
        new Thread(Run).Start();
        while (!IsRunning) {
            Thread.Sleep(0);
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
        Dispatcher.Dispatch(new AppCloseEvent(this));
        OnClose();
        rootModule.SetActive(false);

        isRunning = false;
        OnDestroy();
        foreach (var key in modules.Keys.ToArray()) {
            Logger.Debug($"Remove module: {key}");
            RemoveModule(key);
        }
        SyncContext.Cancel();
        
    }

    protected virtual void OnClose() { }

    protected override void OnDispose() {
        Applications.Remove(this);
        Close();
    }

    protected virtual void OnDestroy() { }
    
    public void Quit(int code) {
        SyncContext.Cancel();
    }
 

    #endregion

    #region Service

    public object GetService(Type service_type) {
        if (modules.TryGetValue(service_type, out var module_base)) {
            return module_base;
        }
        return Services.Get(service_type);
    }

    public T GetService<T>() where T : class {
        return (T)GetService(typeof(T));
    }
    
    public object? FindService(Type service_type) {
        if (modules.TryGetValue(service_type, out var module_base)) {
            return module_base;
        }
        return Services.Find(service_type);
    }
    
    public T? FindService<T>() where T : class {
        return GetService(typeof(T)) as T;
    }

    #endregion

    #region Module
    
    internal void OnAddModule(Type key, ModuleBase module) {
        Logger.Trace($"AddModule:{key.Name} - {module.GetType().Name}");
        modules[key] = module;
        var option = new ActorOptions(key.Name, -1, -1);
        Domain.CreateActor(key, module, option);
        Dispatcher.Dispatch(new ModuleAddEvent(key, module));
    }
    
    internal void OnRemoveModule(Type key, ModuleBase module) {
        if (!modules.Remove(key)) {
            return;
        }
        Domain.RemoveActor(module.Node.Ref);
        Dispatcher.Dispatch(new ModuleRemoveEvent(key, module));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void _AddModule(Type key, ModuleBase module) {
        Domain.CreateActor(module);
        rootModule.AddChild(key, module);
    }
    
    public TModule AddModule<TModule>(TModule module) where TModule : class, IModule {
        _AddModule(typeof(TModule), (ModuleBase)(object)module);
        return module;
    }
    
    public TKey AddModule<TKey, TModule>(Func<AppBase, TModule> func)  where TModule : ModuleBase, TKey where TKey : IModule {
        var module = func(this);
        _AddModule(typeof(TKey), module);
        return module;
    }
        
    public TModule AddModule<TModule>() where TModule : ModuleBase, new() {
        var module = new TModule();
        _AddModule(typeof(TModule), module);
        return module;
    }
    
    public TModule AddModule<TKey, TModule>(TModule module) where TModule : ModuleBase, TKey where TKey : IModule {
        _AddModule(typeof(TKey), module);
        return module;
    }

    public TKey GetModule<TKey>() where TKey : IModule {
        return modules.TryGetValue(typeof(TKey), out var module) ? (TKey)(object)module : throw new KeyNotFoundException();
    }
    
    public TKey? FindModule<TKey>() where TKey : IModule {
        return modules.TryGetValue(typeof(TKey), out var module) ? (TKey)(object)module : default;
    }
    
    public ModuleBase GetModule(Type key) {
        return modules.TryGetValue(key, out var module) ? module : throw new KeyNotFoundException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool _RemoveModule(Type key) {
        if (!modules.TryGetValue(key, out var module)) {
            return false;
        }
        module.ClearChildren();
        modules.Remove(key);
        Domain.RemoveActor(module.Node.Ref);
        module.Dispose();
        return true;
    }
    
    public bool RemoveModule(Type key) {
        return _RemoveModule(key);
    }
    
    public bool RemoveModule<TKey>() where TKey : IModule {
        return _RemoveModule(typeof(TKey));
    }

    #endregion
    
}

public static class Applications {

    private static readonly Dictionary<AppKey, AppBase> apps = new();
    public static IReadOnlyDictionary<AppKey, AppBase> Apps => apps;

    private static object locker = new();
    
    internal static void Add(AppBase app) {
        lock (locker) {
            apps.Add(app.Key, app);
        }
    }

    internal static void Remove(AppBase app) {
        lock (locker) {
            apps.Remove(app.Key);
        }
    }
}

public readonly record struct AppKey(Guid Guid) {
    public readonly Guid Guid = Guid;
    public override int GetHashCode() => Guid.GetHashCode();
}