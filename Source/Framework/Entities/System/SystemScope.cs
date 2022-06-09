namespace Coorth.Framework; 

public readonly ref struct SystemScope {
    
    private readonly SystemBase current;
    
    public SystemScope(SystemBase value) {
        current = value;
    }

    public T Add<T>() where T : SystemBase, new() => current.AddSystem<T>();
    
    public void Dispose() { }
}