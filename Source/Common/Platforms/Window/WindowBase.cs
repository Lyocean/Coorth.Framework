using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Coorth.Platforms; 

public enum WindowAnchor {
    ParentCenter,
    ScreenCenter,
    CustomSetting,
}

[Flags]
public enum WindowStyle {
    Fullscreen  = 1 << 0,
    HasBorder   = 1 << 1,
    
    CanMinimize = 1 << 2,
    CanMaximize = 1 << 3,
    CanDrag     = 1 << 4,
    
    HasMinBox   = 1 << 5,
    HasMaxBox   = 1 << 6,
    
    CanResize   = 1 << 7,

    DefaultWindow = CanMinimize | CanMaximize | CanDrag | HasMinBox | HasMaxBox | CanResize,
    DefaultSplash = CanDrag
}

public struct WindowDescription {
    public WindowBase? Parent;
    public string Name;
    public string Title;
    public Vector2? Position;
    public Vector2 Size;
    public Vector2 MinSize;
    public Vector2 MaxSize;
    public WindowAnchor Anchor;
    public WindowStyle Style;
    public object? Icon;
    public object? Extra;
}

public abstract class WindowBase : IDisposable {

    protected WindowDescription description;
    public WindowDescription Description => description;

    public bool IsFullscreen { get => (description.Style & WindowStyle.Fullscreen) != 0; set => SetFullscreen(value); }

    public bool IsWindowed { get => !IsFullscreen; set => IsFullscreen = !value; }

    public virtual string Name { get => description.Name; set => description.Name = value; }

    public event Action? OnShow;

    public event Action? OnHide;
    
    public event Action? OnDraw;

    public event Action<float>? OnTick;

    public event Action<bool>? OnActive;

    public event Action<Vector2>? OnResized;

    public event Action<bool>? OnFocusChange;

    public abstract string Title { get; set; }

    public abstract IntPtr Handle { get; }
    
    public abstract int Dpi { get; }

    public abstract Vector2 Position { get; set; }

    public abstract Vector2 Size { get; set; }
    
    public abstract Vector2 ClientPosition { get; set; }
    
    public abstract Vector2 ClientSize { get; set; }

    // public abstract Action OnKeyboard

    public IPanelWindow? PanelWindow { get; private set; }

    protected WindowBase(in WindowDescription desc) {
        description = desc;
    }

    public void Register(IPanelWindow panel) {
        PanelWindow = panel;
        OnRegister(panel);
    }

    protected abstract void OnRegister(IPanelWindow panel);
    
    private void SetFullscreen(bool fullscreen) {
        var current = (description.Style & WindowStyle.Fullscreen) != 0;
        if (current == fullscreen) {
            return;
        }
        description.Style |= WindowStyle.Fullscreen;
        OnSetFullscreen(fullscreen);
    }
    
    protected virtual void OnSetFullscreen(bool fullscreen) { }

    public void SetMinimize() {
        if ((description.Style & WindowStyle.CanMinimize) == 0) {
            return;
        }
        OnSetMinimize();
    }
    
    protected virtual void OnSetMinimize() { }
    
    public void SetMaximize() {
        if ((description.Style & WindowStyle.CanMaximize) == 0) {
            return;
        }
        OnSetMaximize();
    }
    
    protected virtual void OnSetMaximize() { }

    public void SetBorder(bool exist) {
        var current = ((description.Style & WindowStyle.HasBorder) != 0);
        if (current == exist) {
            return;
        }
        OnSetBorder(exist);
    }

    protected virtual void OnSetBorder(bool exist) {
        
    }

    public abstract void Repaint();

    public abstract void Show();

    public abstract void Hide();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void _OnShow() => OnShow?.Invoke();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void _OnHide() => OnHide?.Invoke();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void _OnDraw() => OnDraw?.Invoke();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void _OnTick(float deltaTime) => OnTick?.Invoke(deltaTime);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void _OnResized(Vector2 size) => OnResized?.Invoke(size);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void _OnActive(bool active) => OnActive?.Invoke(active);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void _OnFocus(bool focus) => OnFocusChange?.Invoke(focus);
    
    public abstract void Dispose();
}

public interface IConsoleWindow : IDisposable {
    string Title { get; set; }
}

public interface IPanelWindow : IDisposable {
    
}