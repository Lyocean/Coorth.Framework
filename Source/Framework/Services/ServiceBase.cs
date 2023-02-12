namespace Coorth.Framework;

public abstract class ServiceBase {
    public virtual string Name => GetType().Name;

    protected abstract Dispatcher Dispatcher { get; }

    protected abstract IServiceLocator Services { get; }

    protected Disposables Managed;

    protected Disposables Actives;

    public abstract bool IsActive { get; }

    protected ref Disposables Collector => ref (IsActive ? ref Actives : ref Managed);
}