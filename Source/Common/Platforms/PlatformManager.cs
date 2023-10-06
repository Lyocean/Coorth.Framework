using System;
using System.Collections.Generic;
using System.Threading;
using Coorth.Framework;

namespace Coorth.Platforms;

public interface IPlatformManager : IManager {

    PlatformBase Platform { get; }
    PlatformTypes GetPlatformType();

    IReadOnlyList<WindowBase> Windows { get; }
    int WindowCount { get; }
    WindowBase? ActiveWindow { get; }
    WindowBase CreateWindow(in WindowDescription description);
    void Register(WindowBase window);
    void Unregister(WindowBase window);
    
    IReadOnlyList<IGraphicsSurface> Surfaces { get; }
    IGraphicsSurface? ActiveSurface { get; set; }
    void RegisterSurface(IGraphicsSurface surface);
    void UnRegisterSurface(IGraphicsSurface surface);
}

public abstract class PlatformManager : Manager, IPlatformManager {

    #region Window management

    private readonly List<WindowBase> windows = new();

    private WindowBase[]? windowCache;

    private WindowBase? activeWindow;

    private readonly object windowLock = new();

    public IReadOnlyList<WindowBase> Windows {
        get {
            lock (windowLock) {
                return windowCache ??= windows.ToArray();
            }
        }
    }

    public int WindowCount { get { lock(windowLock) { return windows.Count; } } }

    public WindowBase? ActiveWindow { get => activeWindow; set => activeWindow = value; }
    
    public abstract WindowBase CreateWindow(in WindowDescription description);

    public void Register(WindowBase window) {
        lock (windowLock) {
            windowCache = null;
            windows.Add(window);
        }
    }

    public void Unregister(WindowBase window) {
        lock (windowLock) {
            windowCache = null;
            windows.Remove(window);
        }
    }

    public abstract IConsoleWindow CreateConsole();
    
    #endregion
    
    #region Surface management

    protected readonly List<IGraphicsSurface> surfaces = new();
    public IReadOnlyList<IGraphicsSurface> Surfaces => surfaces;
    
    private IGraphicsSurface? activeSurface;
    public IGraphicsSurface? ActiveSurface {
        get => activeSurface;
        set {
            if (activeSurface == value) {
                return;
            }
            activeSurface = value;
            OnChangeActiveSurface();
        }
    }

    public void RegisterSurface(IGraphicsSurface surface) {
        if (surfaces.Contains(surface)) {
            return;
        }
        surfaces.Add(surface);
        OnRegisterSurface(surface);
    }

    public void UnRegisterSurface(IGraphicsSurface surface) {
        surfaces.Remove(surface);
    }

    protected virtual void OnRegisterSurface(IGraphicsSurface surface) {
        
    }

    protected virtual void OnChangeActiveSurface() {
        
    }

    #endregion

    #region Platform types

    public abstract PlatformBase Platform { get; }
    
    public virtual PlatformTypes GetPlatformType() => PlatformTypes.Other;
    
    #endregion

}


