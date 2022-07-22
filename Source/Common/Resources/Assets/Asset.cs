using System;
using System.Threading;

namespace Coorth.Resources; 

public abstract class Asset : IDisposable {
    
    public readonly Guid AssetId;

    private volatile int disposed;
    public bool IsDisposed => disposed != 0;
        
    protected Asset() => AssetId = Guid.NewGuid();

    protected Asset(Guid id) => AssetId = id;

    public void Dispose() {
        if (Interlocked.CompareExchange(ref disposed, 1, 0) == 0) {
            GC.SuppressFinalize(this);
            OnDispose(true);
        }
    }

    ~Asset() {
        if (Interlocked.CompareExchange(ref disposed, 1, 0) == 0) {
            OnDispose(false);
        }
    }
    
    protected virtual void OnDispose(bool dispose) { }
}
    
public interface IAssetData {
        
}